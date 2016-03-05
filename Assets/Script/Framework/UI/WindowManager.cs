using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
using System.Collections.Generic;
using System;

public class LayerInfo
{
    public LayerInfo(int min, int max,GameObject root)
    {
        m_iMin = min;
        m_iMax = max;
        m_iCurrent = m_iMin;
        m_Root = root;
    }
    public int m_iMin { get; private set; }
    public int m_iMax { get; private set; }
    public int m_iCurrent;
    public GameObject m_Root { get; private set; }
}
public class WindowIndexStruct
{
    public WindowIndexStruct(int id, string path, WindowLayer layer, Type type,bool isBuildWindow = true)
    {
        m_ID = id;
        m_strPath = path;
        m_Layer = layer;
        m_Type = type;
        m_bIsBuildInWindow = isBuildWindow;
    }
    public int          m_ID;
    public string       m_strPath;
    public WindowLayer  m_Layer;
    public Type         m_Type;
    public bool         m_bIsBuildInWindow;
    public string       m_strClassName;

}
public class WindowManager : Singleton<WindowManager>
{
	public GameObject                                  UIWindowCameraRoot{get;private set;}
    private Camera                                      m_UICamera;
    private UIRoot                                      m_UIRoot;
    private WindowBase                                  m_CurrentWindow;
    private List<Action>                                m_UpdateStore;
    private List<Action>                                m_RemoveingUpdateList;
    private bool                                        m_bIsUpdateListBusy;
    private Dictionary<int, WindowBase>                 m_WindowStore;
    private Dictionary<int, WindowIndexStruct>          m_WindowIndexStore;
    private Dictionary<WindowLayer, LayerInfo>          m_LayerIndexStore;
    private Dictionary<WindowLayer, List<WindowBase>>   m_ActivedWindowQueue;
    private bool                                        m_bIsOnUpdateStore;

 
    #region public interface

    public void Initialize()
    {
        UIWindowCameraRoot          = ComponentTool.FindChild("UI_Root", null);
        if (null == UIWindowCameraRoot)
        {
            Debuger.LogError("can't load ui root ");
            return;
        }

        m_UIRoot = UIWindowCameraRoot.GetComponent<UIRoot>();
        m_UICamera = ComponentTool.FindChildComponent<Camera>("Camera", UIWindowCameraRoot);
        m_UpdateStore           = new List<Action>();
        m_RemoveingUpdateList   = new List<Action>();
        m_bIsUpdateListBusy     = false;
        m_WindowStore           = new Dictionary<int, WindowBase>();
        m_WindowIndexStore      = new Dictionary<int, WindowIndexStruct>();
        m_LayerIndexStore       = new Dictionary<WindowLayer, LayerInfo>();
        m_ActivedWindowQueue    = new Dictionary<WindowLayer, List<WindowBase>>();

        m_LayerIndexStore.Add(WindowLayer.Window, new LayerInfo(0, 20, ComponentTool.FindChild("LayerWindow", UIWindowCameraRoot)));
        m_LayerIndexStore.Add(WindowLayer.Tip, new LayerInfo(21, 40, ComponentTool.FindChild("LayerTip", UIWindowCameraRoot)));

        //register window
        Definer.RegisterWindow();
        m_bIsOnUpdateStore = false;
    }
    public void Update()
    {
        ExcutionUpdate();
    }
    public void OpenWindow(int windowId, object param = null)
    {
        if (m_WindowStore.ContainsKey(windowId))
        {
            WindowBase currentWindow = m_WindowStore[windowId];
            WindowIndexStruct currentWindowIndexStruct = m_WindowIndexStore[windowId];

            if (currentWindow.IsOpen())
            {
                return;
            }
            currentWindow.m_ObjectRoot.SetActive(true);
            // reset deepth ,do it befor add to actived window queue
            currentWindow.ResetDeepth(GetCurrentWindowDeepth(currentWindowIndexStruct.m_Layer));
            //reset current layer deepth
            m_LayerIndexStore[currentWindowIndexStruct.m_Layer].m_iCurrent = currentWindow.GetMaxDeepthValue();
            // on open
            currentWindow.OnOpen(param);
            //add to actived window queue
            AddToActivedWindowQueue(currentWindowIndexStruct.m_Layer, currentWindow);
        }
        else
        {
            WindowIndexStruct currentWindowIndexStruct = m_WindowIndexStore[windowId];

            // load prefab
            WindowIndexStruct element = null;
            m_WindowIndexStore.TryGetValue(windowId, out element);
            if (null == element)
            {
                Debuger.LogError("can't load window : " + windowId.ToString());
            }
            else
            {
                if (element.m_bIsBuildInWindow)
                {
                    GameObject root = GameObject.Instantiate(ResourceManager.Instance.LoadBuildInResource<GameObject>(element.m_strPath, AssetType.UI));
                    WindowBase res = Activator.CreateInstance(currentWindowIndexStruct.m_Type) as WindowBase;
                    ComponentTool.Attach(m_LayerIndexStore[currentWindowIndexStruct.m_Layer].m_Root.transform, root.transform);

                    // initialize (include set position,set root ,set deepth ...)
                    res.Initialize(windowId, root);
                    // on init
                    res.OnInit();
                    // set deepth ,do it befor add to actived window queue
                    res.ResetDeepth(GetCurrentWindowDeepth(currentWindowIndexStruct.m_Layer));
                    //reset current layer deepth
                    m_LayerIndexStore[currentWindowIndexStruct.m_Layer].m_iCurrent = res.GetMaxDeepthValue();
                    // on open
                    res.OnOpen(param);
                    //add to actived window queue
                    AddToActivedWindowQueue(currentWindowIndexStruct.m_Layer, res);
                    // save to window store
                    m_WindowStore.Add(windowId, res);
                }
                else
                {
                    ResourceManager.Instance.LoadBuildInAssetsAsync(element.m_strPath, AssetType.UI, (origin) =>
                    {
                        GameObject root = GameObject.Instantiate(origin) as GameObject;
                        WindowBase res = Activator.CreateInstance(currentWindowIndexStruct.m_Type) as WindowBase;
                        ComponentTool.Attach(m_LayerIndexStore[currentWindowIndexStruct.m_Layer].m_Root.transform, root.transform);

                        // initialize (include set position,set root ,set deepth ...)
                        res.Initialize(windowId, root);
                        // on init
                        res.OnInit();
                        // set deepth ,do it befor add to actived window queue
                        res.ResetDeepth(GetCurrentWindowDeepth(currentWindowIndexStruct.m_Layer));
                        //reset current layer deepth
                        m_LayerIndexStore[currentWindowIndexStruct.m_Layer].m_iCurrent = res.GetMaxDeepthValue();
                        // on open
                        res.OnOpen(param);
                        //add to actived window queue
                        AddToActivedWindowQueue(currentWindowIndexStruct.m_Layer, res);
                        // save to window store
                        m_WindowStore.Add(windowId, res);
                    });
                }
            }
        }
    }
    public void CloseWindow(int windowId, bool isRemoveFromWindowStore = true)
    {
        if (!m_WindowStore.ContainsKey(windowId))
        {
            return;
        }
        // on close
        m_WindowStore[windowId].OnClose();
        //remove from actived window queue
        RemoveFromActivedWindowQueue(m_WindowIndexStore[windowId].m_Layer, m_WindowStore[windowId]);
        //destroy object
        GameObject.Destroy(m_WindowStore[windowId].m_ObjectRoot);

        //remove from window map
        if (isRemoveFromWindowStore)
        {
            m_WindowStore.Remove(windowId);
        }
    }
    public void HideWindow(int windowId)
    {
        if (!m_WindowStore.ContainsKey(windowId))
        {
            return;
        }
        // on hide
        m_WindowStore[windowId].OnHide();
        //remove from actived window queue
        RemoveFromActivedWindowQueue(m_WindowIndexStore[windowId].m_Layer, m_WindowStore[windowId]);
        //hide
        m_WindowStore[windowId].m_ObjectRoot.SetActive(false);
    }
    public WindowBase GetWindow(int windowId)
    {
        WindowBase targetWindow = null;
        m_WindowStore.TryGetValue(windowId, out targetWindow);
        return targetWindow;
    }
    public void HideAllWindow()
    {
        foreach (KeyValuePair<int, WindowBase> elem in m_WindowStore)
        {
            HideWindow(elem.Key);
        }
    }
    public void CloseAllWindow()
    {
        foreach (KeyValuePair<int, WindowBase> elem in m_WindowStore)
        {
            CloseWindow(elem.Key,false);
        }
        m_WindowStore.Clear();
    }
    public WindowBase GetFocousWindow()
    {
        if (!m_ActivedWindowQueue.ContainsKey(WindowLayer.Window) || m_ActivedWindowQueue[WindowLayer.Window].Count == 0)
        {
            return null;
        }
        return m_ActivedWindowQueue[WindowLayer.Window][m_ActivedWindowQueue[WindowLayer.Window].Count - 1];
    }
    public void RegisterToUpdateStore(Action element)
    {
        for (int i = 0; i < m_UpdateStore.Count; ++i)
        {
            if (m_UpdateStore[i] == element)
            {
                return;
            }
        }
        m_UpdateStore.Add(element);
        CheckIsNeedPushToUpdateStore();
    }
    public void UnRegisterFromUpdateStore(Action element)
    {
        if (m_bIsUpdateListBusy)
        {
            for (int i = 0; i < m_RemoveingUpdateList.Count; ++i)
            {
                if (m_RemoveingUpdateList[i] == element)
                {
                    return;
                }
            }
            m_RemoveingUpdateList.Add(element);
        }
        else
        {
            m_UpdateStore.Remove(element);
            CheckIsNeedPushToUpdateStore();
        }
    }
    public void RegisterWindow(int id, string path, WindowLayer layer, Type type)
    {
        if (m_WindowIndexStore.ContainsKey(id))
        {
            return;
        }
        WindowIndexStruct element = new WindowIndexStruct(id,path,layer,type);
        m_WindowIndexStore.Add(id,element);
    }
    public Camera GetUICamera()
    {
        return m_UICamera;
    }

    public UIRoot GetUIRoot()
    {
        return m_UIRoot;
    }
    #endregion

    #region system function
    private void ExcutionUpdate()
    {
        m_bIsUpdateListBusy = true;
        for (int i = 0; i < m_UpdateStore.Count; ++i)
        {
            m_UpdateStore[i]();
        }
        DoRemove();
        m_bIsUpdateListBusy = false;
    }
    private void DoRemove()
    {
        if (m_RemoveingUpdateList.Count == 0)
        {
            return;
        }
        for (int i = 0; i < m_RemoveingUpdateList.Count; ++i)
        {
            m_UpdateStore.Remove(m_RemoveingUpdateList[i]);
        }
        m_RemoveingUpdateList.Clear();
        CheckIsNeedPushToUpdateStore();
    }
    private int GetCurrentWindowDeepth(WindowLayer layer)
    {
        int currentLayerDeepth = ++m_LayerIndexStore[layer].m_iCurrent;
        if (currentLayerDeepth > m_LayerIndexStore[layer].m_iMax )
        {
            currentLayerDeepth = ResetDeepth(layer);
        }
        if (currentLayerDeepth > m_LayerIndexStore[layer].m_iMax )
        {
            Debuger.LogError("panel deepth out of range");
        }
        return currentLayerDeepth;
    }
    private int ResetDeepth(WindowLayer layer)
    {
        int lastWindowDeepth = m_LayerIndexStore[layer].m_iMin;
        for (int i = 0; i < m_ActivedWindowQueue[layer].Count; ++i)
        {
            m_ActivedWindowQueue[layer][i].ResetDeepth(lastWindowDeepth + 1);
            lastWindowDeepth = m_ActivedWindowQueue[layer][i].GetMaxDeepthValue();
        }
        m_LayerIndexStore[layer].m_iCurrent = lastWindowDeepth + 1;
        return m_LayerIndexStore[layer].m_iCurrent;
    }
    private void AddToActivedWindowQueue(WindowLayer layer, WindowBase windowHandler)
    {
        if (!m_ActivedWindowQueue.ContainsKey(layer))
        {
            m_ActivedWindowQueue.Add(layer, new List<WindowBase>());
        }
        m_ActivedWindowQueue[layer].Add(windowHandler);
    }
    private void RemoveFromActivedWindowQueue(WindowLayer layer, WindowBase windowHandler)
    {
        if (!m_ActivedWindowQueue.ContainsKey(layer))
        {
            return;
        }
        m_ActivedWindowQueue[layer].Remove(windowHandler);
    }
    private void CheckIsNeedPushToUpdateStore()
    {
        if (!m_bIsOnUpdateStore && m_UpdateStore.Count > 0)
        {
            UITickTask.Instance.RegisterToUpdateList(Update);
            m_bIsOnUpdateStore = true;
        }
        else if (m_bIsOnUpdateStore && m_UpdateStore.Count <= 0)
        {
            UITickTask.Instance.UnRegisterFromUpdateList(Update);
            m_bIsOnUpdateStore = false;
        }
    }
    #endregion
}
