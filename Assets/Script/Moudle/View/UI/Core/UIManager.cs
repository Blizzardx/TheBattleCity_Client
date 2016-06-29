using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common.Tool;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, UIBase> m_CurrentWindowStore;

    public UIManager()
    {
        m_CurrentWindowStore = new Dictionary<Type, UIBase>();
    }
    public void OpenWindow(Type t, object param)
    {
        UIBase ui = null;
        m_CurrentWindowStore.TryGetValue(t, out ui);
        if (null == ui)
        {
            ui = Activator.CreateInstance(t) as UIBase;
            ui.DoCreate();
        }
        if (ui.IsOpen())
        {
            // do nothing
            return;
        }
        ui.DoOpen(param);
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
}
