using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;

public abstract class UIStateBase
{
    public class WindowInfo
    {
        public WindowInfo()
        {
            
        }
        public UIWindowBase     handler;
        public object           param;
        public int              deepth;

        public WindowInfo(UIWindowBase window, object param)
        {
            handler = window;
            param = param;
            deepth = 0;
        }
    }
    public class WindowInitInfo
    {
        public object param;
        private Type window;

        public WindowInitInfo(Type window, object param)
        {
            this.window = window;
            this.param = param;
        }
    }

    private List<WindowInitInfo>            m_RegisterWindowList;
    private List<WindowInfo>                m_WindowList;
    private List<WindowInfo>                m_TmpWindowList;
    protected string                        m_strInstanceKey;
    
    #region public interface
    public string GetKey()
    {
        return m_strInstanceKey;
    }
    public void Init(string instanceKey)
    {
        m_RegisterWindowList = new List<WindowInitInfo>();
        m_WindowList = new List<WindowInfo>();
        m_TmpWindowList = new List<WindowInfo>();

        // mark instance key
        m_strInstanceKey = instanceKey;

        // do init
        OnInit();

        // create  window
        for (int i = 0; i < m_RegisterWindowList.Count; ++i)
        {
        }
    }
    public void Open(object param)
    {
        OnOpen();
        for (int i = 0; i < m_WindowList.Count; ++i)
        {
            //m_WindowList[i].handler;
        }
    }
    public void Close()
    {
        OnClose();
        for (int i = 0; i < m_WindowList.Count; ++i)
        {
            //m_WindowList[i].handler;
        }
    }
    public void Hide()
    {
        OnHide();
        for (int i = 0; i < m_WindowList.Count; ++i)
        {
            //m_WindowList[i].handler;
        }
    }
    public void Cover()
    {
        OnCovered();
        Hide();
    }
    public void Resume()
    {
        OnResume();
        for (int i = 0; i < m_WindowList.Count; ++i)
        {
            //m_WindowList[i].handler;
        }
    }
    #endregion

    #region system function
    protected void RegisterWindow(Type window,object param)
    {
        m_RegisterWindowList.Add(new WindowInitInfo(window,param));
    }
    protected void RegisterWindow<T>(T window, object param) where T : UIWindowBase
    {
        RegisterWindow(typeof(T),param);
    }
    #endregion

    #region base event
    protected virtual void OnInit()
    {
        
    }
    protected virtual void OnOpen()
    {
        
    }
    protected virtual void OnResume()
    {
        
    }
    protected virtual void OnClose()
    {
        
    }
    protected virtual void OnHide()
    {
        
    }
    protected virtual void OnCovered()
    {
        
    }
    #endregion

    public void OpenWindow(UIWindowBase window, object param)
    {
        m_TmpWindowList.Add(new WindowInfo(window,param));
    }
}
