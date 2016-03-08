using System.Collections.Generic;
using UnityEngine;

public class TickTaskManager : Singleton<TickTaskManager>
{
    private List<AbstractTickTask> m_TickTaskStore;

    #region public interface
    public void InitializeTickTaskSystem()
    {
        m_TickTaskStore = new List<AbstractTickTask>();

        m_TickTaskStore.Add(new TimeTickTask());
        m_TickTaskStore.Add(new MessageTickTask());
        m_TickTaskStore.Add(new UITickTask());
        m_TickTaskStore.Add(new PingTickTask());
    }
    public void Update()
    {
        foreach (AbstractTickTask tickTask in m_TickTaskStore)
        {
            tickTask.Tick();
        }
    }
	public void SyncLastTickTime(long lastTickTime)
    {
        foreach (AbstractTickTask tickTask in m_TickTaskStore)
        {
            tickTask.SetLastTickTime(lastTickTime);
        }
	}

    #endregion
}