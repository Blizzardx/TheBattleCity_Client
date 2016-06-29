using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Tool;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, UIBase> m_CurrentWindowStore;
    private Camera m_UICamera;
    private UIRoot m_UIRoot;

    public UIManager()
    {
        m_CurrentWindowStore = new Dictionary<Type, UIBase>();
    }
    public UIBase OpenWindow(Type t, object param)
    {
        UIBase ui = null;
        m_CurrentWindowStore.TryGetValue(t, out ui);
        if (null == ui)
        {
            ui = Activator.CreateInstance(t) as UIBase;
            m_CurrentWindowStore.Add(t, ui);

            ui.DoCreate(OnWindowLoaded);
        }
        if (ui.IsOpen())
        {
            // do nothing
            return ui;
        }
        ui.DoOpen(param);
        return ui;
    }
    public void OpenWindow<T>(object param) where T : UIBase
    {
        OpenWindow(typeof (T), param);
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
            Debug.Log(" can't cloas window " + t.ToString());
            return;
        }
        ui.DoClose();
        m_CurrentWindowStore.Remove(t);
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

    }
    //private int GetCurrentWindowDeepth(WindowLayer layer)
    //{
    //    int currentLayerDeepth = ++m_LayerIndexStore[layer].m_iCurrent;
    //    if (currentLayerDeepth > m_LayerIndexStore[layer].m_iMax)
    //    {
    //        currentLayerDeepth = ResetDeepth(layer);
    //    }
    //    if (currentLayerDeepth > m_LayerIndexStore[layer].m_iMax)
    //    {
    //        Debuger.LogError("panel deepth out of range");
    //    }
    //    return currentLayerDeepth;
    //}
    //private int ResetDeepth(WindowLayer layer)
    //{
    //    int lastWindowDeepth = m_LayerIndexStore[layer].m_iMin;
    //    for (int i = 0; i < m_ActivedWindowQueue[layer].Count; ++i)
    //    {
    //        m_ActivedWindowQueue[layer][i].ResetDeepth(lastWindowDeepth + 1);
    //        lastWindowDeepth = m_ActivedWindowQueue[layer][i].GetMaxDeepthValue();
    //    }
    //    m_LayerIndexStore[layer].m_iCurrent = lastWindowDeepth + 1;
    //    return m_LayerIndexStore[layer].m_iCurrent;
    //}
    public void HideAllWindow()
    {
    }
    public void CloseAllWindow()
    {
    }
}
