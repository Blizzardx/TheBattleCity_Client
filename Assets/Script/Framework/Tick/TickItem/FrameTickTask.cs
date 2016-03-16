using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FrameTickTask : AbstractTickTask
{
    private RegisterList m_RegisterList;

    private static FrameTickTask m_Instance;

    public FrameTickTask()
    {
        m_RegisterList = new RegisterList();
        m_Instance = this;
    }
    public static FrameTickTask Instance
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
        return TickTaskConstant.TICK_FRAME;
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