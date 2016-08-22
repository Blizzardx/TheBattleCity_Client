using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIWindowManager
{
    private LinkedList<UIWindowBase> m_ActivedWindowList;

    #region public interface
    public void Open<T>(object param) where T : UIWindowBase
    {
        Open(typeof (T), param);
    }
    public void Hide<T>() where T : UIWindowBase
    {
        Hide(typeof (T));
    }
    public void Close<T>() where T : UIWindowBase
    {
        Close(typeof(T));
    }
    #endregion

    #region system fucntion
    protected void Open(Type type, object param)
    {
        
    }
    protected void Hide(Type type)
    {
        
    }
    protected void Close(Type type)
    {
        
    }
    #endregion
}
