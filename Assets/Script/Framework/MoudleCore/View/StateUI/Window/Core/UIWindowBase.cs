using UnityEngine;
using System.Collections;
using Common.Component;

public abstract class UIWindowBase
{
    public enum Status
    {
        Close,
        Init,
        Open,
        Hide,
    }
    protected object        m_ObjParam;
    protected GameObject    m_ObjectRoot;
    private Status          m_WindowStatus;
    private int             m_iDeepth;

    #region public interface
    public void Init(object param)
    {
        m_WindowStatus = Status.Close;
        m_ObjParam = param;
        OnInit();
        m_WindowStatus = Status.Init;
    }
    public void Open(object param)
    {
        m_ObjParam = param;
        OnOpen(m_ObjParam);
        m_WindowStatus = Status.Open;
    }
    public void Hide()
    {
        OnHide();
        m_WindowStatus = Status.Hide;
    }
    public void Close()
    {
        OnClose();
        m_WindowStatus = Status.Close;
    }
    #endregion

    #region system function
    protected Status GetWindowStatus()
    {
        return m_WindowStatus;
    }
    protected GameObject FindChild(string name)
    {
        return ComponentTool.FindChild(name, m_ObjectRoot);
    }
    protected T GetChildComponent<T>(string name) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(name, m_ObjectRoot);
    }
    #endregion

    #region base event
    protected virtual void OnInit()
    {

    }
    protected virtual void OnOpen(object param)
    {

    }
    protected virtual void OnClose()
    {

    }
    protected virtual void OnHide()
    {

    }
    #endregion
}
