using System;
using UnityEngine;
using System.Collections.Generic;
public class UITickTask : AbstractTickTask
{
    private RegisterList m_RegisterList;

    private static UITickTask m_Instance;

    public UITickTask()
    {
        m_RegisterList = new RegisterList();
        m_Instance = this;
    }
    public static UITickTask Instance
    {
        get
        {
            return m_Instance;
        }
    }
    public void RegisterToUpdateList(Action element)
    {
        m_RegisterList.RegisterToUpdateList(element);
    }
    public void UnRegisterFromUpdateList(Action element)
    {
        m_RegisterList.UnRegisterFromUpdateList(element);
    }
    protected override bool FirstRunExecute()
    {
        m_RegisterList = new RegisterList();
        m_Instance = this;
        return true;
    }
    protected override int GetTickTime()
    {
        return TickTaskConstant.TICK_UIWINDOW;
    }
    protected override void Beat()
    {
        ExcutionUpdateList();
    }
    private void ExcutionUpdateList()
    {
       m_RegisterList.ExcutionUpdateList();
    }
}

