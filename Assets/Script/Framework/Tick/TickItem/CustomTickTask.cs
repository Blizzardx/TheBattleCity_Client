using System;
using UnityEngine;
using System.Collections.Generic;
using Common.Component;

namespace Framework.Tick
{

    public class CustomTickTask : AbstractTickTask
    {
        private RegisterList m_RegisterList;

        public CustomTickTask()
        {
            m_RegisterList = new RegisterList();
            m_Instance = this;
        }
        private static CustomTickTask m_Instance;
        public static CustomTickTask Instance
        {
            get
            {
                if (null == m_Instance)
                {
                    m_Instance = new CustomTickTask();
                }
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

}