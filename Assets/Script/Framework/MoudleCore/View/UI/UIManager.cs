using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Component;
using Common.Tool;

public class UIManager : Singleton<UIManager>
{
    public enum WindowLayer
    {
        Tip,
        Window,
    }
    public class LayerInfo
    {
        public LayerInfo(int min, int max, GameObject root)
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
    private Dictionary<Type, UIBase>                m_CurrentWindowStore;
    private Camera                                  m_UICamera;
    private UIRoot                                  m_UIRoot;
    private Dictionary<WindowLayer, LayerInfo>      m_LayerIndexStore;
    private Dictionary<WindowLayer, List<UIBase>>   m_ActivedWindowQueue;

    public UIManager()
    {
        m_CurrentWindowStore = new Dictionary<Type, UIBase>();
        m_UIRoot = ComponentTool.FindChildComponent<UIRoot>("UI_Root", null);
        m_UICamera = ComponentTool.FindChildComponent<Camera>("Camera", m_UIRoot.gameObject);
        m_LayerIndexStore = new Dictionary<WindowLayer, LayerInfo>();
        m_ActivedWindowQueue = new Dictionary<WindowLayer, List<UIBase>>();

        m_LayerIndexStore.Add(WindowLayer.Window, new LayerInfo(0, 20, ComponentTool.FindChild("LayerWindow", m_UIRoot.gameObject)));
        m_LayerIndexStore.Add(WindowLayer.Tip, new LayerInfo(21, 40, ComponentTool.FindChild("LayerTip", m_UIRoot.gameObject)));
    }
    public UIBase OpenWindow(Type t, WindowLayer layer, object param = null)
    {
        UIBase ui = null;
        m_CurrentWindowStore.TryGetValue(t, out ui);
        if (null == ui)
        {
            ui = Activator.CreateInstance(t) as UIBase;
            m_CurrentWindowStore.Add(t, ui);
            ui.SetLayer(layer);
            ui.DoCreate(OnWindowLoaded);
        }
        else
        {
            ui.SetLayer(layer);
        }
        if (ui.IsOpen())
        {
            // do nothing
            return ui;
        }
        ui.DoOpen(param);
        return ui;
    }
    public void OpenWindow<T>(WindowLayer layer,object param = null) where T : UIBase
    {
        OpenWindow(typeof (T), layer,param);
    }
    public void CloseWindow<T>() where T : UIBase
    {
        CloseWindow(typeof (T));
    }
    public void CloseWindow(Type t)
    {
        UIBase ui = null;
        m_CurrentWindowStore.TryGetValue(t, out ui);
        if (null == ui)
        {
            Debug.Log(" can't close window " + t.ToString());
            return;
        }

        m_CurrentWindowStore.Remove(t);

        //remove from actived window queue
        RemoveFromActivedWindowQueue(ui);

        ui.DoClose();

    }
    public void HideWindow<T>() where T : UIBase
    {
        HideWindow(typeof (T));
    }
    public void HideWindow(Type t)
    {
        UIBase ui = null;
        m_CurrentWindowStore.TryGetValue(t, out ui);
        if (null == ui)
        {
            Debug.Log(" can't cloas window " + t.ToString());
            return;
        }
        //remove from actived window queue
        RemoveFromActivedWindowQueue(ui);

        ui.DoHide();
    }
    public Camera GetUICamera()
    {
        return m_UICamera;
    }
    public UIRoot GetUIRoot()
    {
        return m_UIRoot;
    }
    private void OnWindowLoaded(GameObject windowRoot, UIBase windowBase)
    {
        WindowLayer layer = windowBase.GetLayer();

        var layerinfo = m_LayerIndexStore[layer];
        windowRoot.transform.SetParent(layerinfo.m_Root.transform,false);

        // set deepth ,do it befor add to actived window queue
        windowBase.ResetDeepth(GetCurrentWindowDeepth(layer));
        //reset current layer deepth
        layerinfo.m_iCurrent = windowBase.GetMaxDeepthValue();
        //add to actived window queue
        AddToActivedWindowQueue(windowBase);
    }
    private void AddToActivedWindowQueue( UIBase windowHandler)
    {
        WindowLayer layer = windowHandler.GetLayer();
        if (!m_ActivedWindowQueue.ContainsKey(layer))
        {
            m_ActivedWindowQueue.Add(layer, new List<UIBase>());
        }
        m_ActivedWindowQueue[layer].Add(windowHandler);
    }
    private void RemoveFromActivedWindowQueue( UIBase windowHandler)
    {
        WindowLayer layer = windowHandler.GetLayer();
        if (!m_ActivedWindowQueue.ContainsKey(layer))
        {
            return;
        }
        m_ActivedWindowQueue[layer].Remove(windowHandler);
    }
    private int GetCurrentWindowDeepth(WindowLayer layer)
    {
        int currentLayerDeepth = ++m_LayerIndexStore[layer].m_iCurrent;
        if (currentLayerDeepth > m_LayerIndexStore[layer].m_iMax)
        {
            currentLayerDeepth = ResetDeepth(layer);
        }
        if (currentLayerDeepth > m_LayerIndexStore[layer].m_iMax)
        {
            Debug.LogError("panel deepth out of range");
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
    public void HideAllWindow()
    {
    }
    public void CloseAllWindow()
    {
    }
}
