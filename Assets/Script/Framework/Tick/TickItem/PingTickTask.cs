using System;
using UnityEngine;
using System.Collections;
using Framework.Message;
using Framework.Network;

namespace Framework.Tick
{
    public class PingTickTask : AbstractTickTask
    {
        private const int m_nPingDuringTime = 7000;
        private const int m_nTimeOut = 30000;
        private int m_nCurrentTimeout = 0;
        private bool m_bIsConnect;
        private bool m_bIsActive;
        static public long m_iLastSendMsgTime;
        private DateTime m_iSendPintTime;
        private int m_iPingDuringTime;
        private int m_iPingRealDuringTime;

        private static PingTickTask m_Instance;

        public static PingTickTask Instance
        {
            get { return m_Instance; }
        }

        protected override bool FirstRunExecute()
        {
            m_bIsConnect = false;
            //register msg
            //MessageDispatcher.Instance.RegistMessage(MessageTypeConstants.SC_PING, OnPong);
            m_iLastSendMsgTime = (int) (Time.time*1000.0f);
            m_Instance = this;
            return false;
        }

        protected override int GetTickTime()
        {
            return TickTaskConstant.TICK_PING;
        }

        protected override void Beat()
        {
            if (!m_bIsActive)
            {
                return;
            }
            if (NetworkManager.Instance.IsConnect())
            {
                m_bIsConnect = true;
                TryPing();
                if (m_nCurrentTimeout > m_nTimeOut)
                {
                    // time out
                    Debug.LogError("Time Out");
                    //MessageDispatcher.Instance.BroadcastMessage(ClientCustomMessageDefine.C_SOCKET_TIMEOUT, null);
                    SetPingStatus(false);
                }
            }
            else
            {
                if (m_bIsConnect)
                {
                    // disconnect 
                    Debug.LogError("Disconnect");
                    //MessageDispatcher.Instance.BroadcastMessage(ClientCustomMessageDefine.C_SOCKET_CLOSE, null);
                    m_bIsConnect = false;

                    SetPingStatus(false);
                }
            }
        }

        private void TryPing()
        {
            if ((int) (Time.time*1000.0f) - m_iLastSendMsgTime > m_nPingDuringTime)
            {
                //NetworkManager.Instance.SendMsgToServer(new CSPingMsg());
                m_iSendPintTime = DateTime.Now;
                m_nCurrentTimeout += TickTaskConstant.TICK_PING;
            }
            else
            {
                m_nCurrentTimeout = 0;
            }
        }

        private void OnPong(object eb)
        {
            m_iPingDuringTime = (DateTime.Now - m_iSendPintTime).Milliseconds;
            m_nCurrentTimeout = 0;
            m_iLastSendMsgTime = (int) (Time.time*1000.0f);
        }

        public static void ResetSendMsgTime()
        {
            m_iLastSendMsgTime = (int) (Time.time*1000.0f);
        }

        public void SetPingStatus(bool isActive)
        {
            m_bIsActive = isActive;
        }

        public int GetPingDuringTime()
        {
            return m_iPingDuringTime;
        }

        public int GetPingRealDuringTime()
        {
            return m_iPingRealDuringTime;
        }

        public void OnRecievePoingRealTime()
        {
            m_iPingRealDuringTime = (DateTime.Now - m_iSendPintTime).Milliseconds;
        }
    }

}