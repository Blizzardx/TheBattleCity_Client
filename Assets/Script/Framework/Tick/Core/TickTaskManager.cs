using System;
using System.Collections.Generic;
using Common.Tool;
using UnityEngine;

namespace Framework.Tick
{
    public class TickTaskManager:Singleton<TickTaskManager>
    {
        private List<AbstractTickTask> m_TickTaskStore;

        #region public interface

        public TickTaskManager()
        {
            Initialize();
        }
        public void Initialize()
        {
            m_TickTaskStore = new List<AbstractTickTask>();

            m_TickTaskStore.Add(new TimeTickTask());
            m_TickTaskStore.Add(new TaskHandlerTickTask());
            m_TickTaskStore.Add(new MessageTickTask());
            m_TickTaskStore.Add(new EventTickTask());
            m_TickTaskStore.Add(new AsyncTickTask());
            m_TickTaskStore.Add(new NetworkTickTask());
            m_TickTaskStore.Add(new CustomTickTask());
            m_TickTaskStore.Add(new LogTickTask());
        }

        public void Update()
        {
            if (m_TickTaskStore == null)
            {
                Debug.LogFormat("tick    {0}", this.GetHashCode());
                return;
            }
            try
            {
                foreach (AbstractTickTask tickTask in m_TickTaskStore)
                {
                    tickTask.Tick();
                }

            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
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

}