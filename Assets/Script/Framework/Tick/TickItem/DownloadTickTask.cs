using System;
using UnityEngine;
using System.Collections.Generic;

public class DownloadTickTask : AbstractTickTask
{
    private RegisterList m_RegisterList;

    public DownloadTickTask()
    {
        m_RegisterList = new RegisterList();
        m_Instance = this;
    }
    private static DownloadTickTask m_Instance;
    public static DownloadTickTask Instance
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
       
        return true;
    }
    protected override int GetTickTime()
    {
        return TickTaskConstant.TICK_PLAYER;
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
