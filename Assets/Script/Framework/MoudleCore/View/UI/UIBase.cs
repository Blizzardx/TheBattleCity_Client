using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common.Component;
using Framework.Asset;
using Framework.Common;
using Framework.Event;
using Framework.Message;
using NetWork.Auto;
using Object = UnityEngine.Object;

public class UIBase
{
    public class EventInfo
    {
        public EventInfo(int id, Action<EventElement> callack)
        {
            this.id = id;
            this.callback = callack;
        }
        public int id;
        public Action<EventElement> callback;
    }

    public class MessageInfo
    {
        public MessageInfo(int id, Action<IMessage> callack)
        {
            this.id = id;
            this.callback = callack;
        }
        public int id;
        public Action<IMessage> callback;
    }
    public class PanelStruct
    {
        public UIPanel m_Panel;
        public int m_defaultDeepth;
    }
    private GameObject          m_ObjectRoot;
    private bool                m_bIsLoading;
    private object              m_OpenParam;
    private PreloadAssetInfo    m_ResourceInfo;
    private bool                m_bIsDestroyed;
    private bool                m_bTriggerOpenAfterLoaded;
    private Action<GameObject, UIBase>  m_CreateCallback;
    private UIManager.WindowLayer       m_Layer;
    
    private int m_iDeepth;
    private List<PanelStruct> m_AllPanelsUnderWindow;
    private int m_iMaxDeepth;
    private List<MessageInfo> m_MessageInfoList;
    private List<EventInfo> m_EventInfoList;
      
    #region public interface
    public void DoCreate(Action<GameObject, UIBase> callback)
    {
        m_MessageInfoList = new List<MessageInfo>();
        m_EventInfoList = new List<EventInfo>();

        m_CreateCallback = callback;
        OnCreate();
        BeginLoadResource();
    }
    public void DoOpen(object param)
    {
        if (m_bIsLoading)
        {
            // open later
            m_bTriggerOpenAfterLoaded = true;
            m_OpenParam = param;
        }
        else
        {
            m_ObjectRoot.SetActive(true);
            OnOpen(param);
        }
    }
    public void DoClose()
    {
        Clear();
        SetDestroyStatus(true);
        OnClose();
        if (null != m_ObjectRoot)
        {
            GameObject.Destroy(m_ObjectRoot);
        }
    }
    public void DoHide()
    {
        if (m_bIsLoading || m_bIsDestroyed)
        {
            return;
        }
        OnHide();
        m_ObjectRoot.SetActive(false);
    }
    public bool IsOpen()
    {
        if (m_bIsLoading || m_bIsDestroyed)
        {
            return false;
        }
        return m_ObjectRoot.activeSelf;
    }
    public int ResetDeepth(int deepth)
    {
        int maxDeepth = -1;
        if (deepth == m_iDeepth)
        {
            //do nothing
            return maxDeepth;
        }

        m_iDeepth = deepth;
        for (int i = 0; i < m_AllPanelsUnderWindow.Count; ++i)
        {
            m_AllPanelsUnderWindow[i].m_Panel.depth = m_AllPanelsUnderWindow[i].m_defaultDeepth + m_iDeepth;
            if (m_AllPanelsUnderWindow[i].m_defaultDeepth + m_iDeepth > maxDeepth)
            {
                maxDeepth = m_AllPanelsUnderWindow[i].m_defaultDeepth + m_iDeepth;
            }
        }
        m_iMaxDeepth = maxDeepth;
        return maxDeepth;
    }

    public void SetLayer(UIManager.WindowLayer layer)
    {
        m_Layer = layer;
    }
    public UIManager.WindowLayer GetLayer()
    {
        return m_Layer;
    }
    #endregion

    #region system function
    private void DoInit()
    {
        OnInit();
    }
    private void BeginLoadResource()
    {
        if (null == m_ObjectRoot)
        {
            SetLoadingStatus(true);
            // begin load ui window resource from bundle or build in resource
            if (m_ResourceInfo.assetType == PerloadAssetType.BuildInAsset)
            {
                // load from build in resource
                ResourceManager.Instance.LoadBuildInResourceAsync(m_ResourceInfo.assetName, WindowLoaded);
            }
            else if (m_ResourceInfo.assetType == PerloadAssetType.AssetBundleAsset)
            {
                // load from assetbundle
                ResourceManager.Instance.LoadAssetFromBundle(m_ResourceInfo.assetName,
                    WindowLoaded);
            }
        }
        else
        {
            WindowLoaded("", m_ObjectRoot);
        }
    }
    private void WindowLoaded(string name, Object obj)
    {
        SetLoadingStatus(false);
        if (m_bIsDestroyed)
        {
            // do nothing
            return;
        }
        m_ObjectRoot = GameObject.Instantiate(obj) as GameObject;
        m_iDeepth = 0;
        List<UIPanel> tmpPanelList = new List<UIPanel>();
        m_AllPanelsUnderWindow = new List<PanelStruct>();
        ComponentTool.FindAllChildComponents(m_ObjectRoot.transform, ref tmpPanelList);
        for (int i = 0; i < tmpPanelList.Count; ++i)
        {
            PanelStruct elem = new PanelStruct();
            elem.m_Panel = tmpPanelList[i];
            elem.m_defaultDeepth = tmpPanelList[i].depth;
            m_AllPanelsUnderWindow.Add(elem);
        }

        m_CreateCallback(m_ObjectRoot,this);
        DoInit();
        if (m_bTriggerOpenAfterLoaded)
        {
            m_bTriggerOpenAfterLoaded = false;
            DoOpen(m_OpenParam);
        }
    }
    protected void SetResourceName(string name,PerloadAssetType type)
    {
        m_ResourceInfo = new PreloadAssetInfo();
        m_ResourceInfo.assetName = name;
        m_ResourceInfo.assetType = type;
    }
    protected void SetResource(GameObject root)
    {
        m_ObjectRoot = root;
    }
    protected void Hide()
    {
        UIManager.Instance.HideWindow(GetType());
    }
    protected void Close()
    {
        UIManager.Instance.CloseWindow(GetType());
    }
    private void SetLoadingStatus(bool status)
    {
        m_bIsLoading = status;
    }
    private void SetDestroyStatus(bool status)
    {
        m_bIsDestroyed = status;
    }
    protected bool IsDestroyed()
    {
        return m_bIsDestroyed;
    }
    protected void LoadResourceAsync(string assetName, Action<string, Object> callback)
    {
        ResourceManager.Instance.LoadBuildInResourceAsync(assetName, (name, obj) =>
        {
            if (m_bIsDestroyed)
            {
                return;
            }
            callback(name, obj);
        });
    }
    private void Clear()
    {
        foreach (var elem in m_EventInfoList)
        {
            EventDispatcher.Instance.UnregistEvent(elem.id, elem.callback);
        }
        m_EventInfoList = new List<EventInfo>();

        foreach (var elem in m_MessageInfoList)
        {
            MessageDispatcher.Instance.UnregistMessage(elem.id, elem.callback);
        }
        m_MessageInfoList = new List<MessageInfo>();

    }
    #endregion

    #region internal function
    protected void RegisterEvent(int id, Action<EventElement> callback)
    {
        m_EventInfoList.Add(new EventInfo(id, callback));
        EventDispatcher.Instance.RegistEvent(id, (obj) =>
        {
            if (m_bIsDestroyed)
            {
                return;
            }
            callback(obj);
        });
    }
    protected void RegisterMessage(int id, Action<IMessage> callback)
    {
        m_MessageInfoList.Add(new MessageInfo(id, callback));
        MessageDispatcher.Instance.RegistMessage(id, (obj) =>
        {
            if (m_bIsDestroyed)
            {
                return;
            }
            callback(obj);
        });
    }
    #endregion

    protected GameObject FindChild(string name)
    {
        return ComponentTool.FindChild(name, m_ObjectRoot);
    }
    protected T GetChildComponent<T>(string name) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(name, m_ObjectRoot);
    }
    protected virtual void OnCreate()
    {
        
    }
    protected virtual void OnInit()
    {
        
    }
    protected virtual void OnOpen(object parma)
    {
        
    }
    protected virtual void OnClose()
    {
        
    }
    protected virtual void OnHide()
    {
        
    }
    public int GetMaxDeepthValue()
    {
        return m_iMaxDeepth;
    }
}
