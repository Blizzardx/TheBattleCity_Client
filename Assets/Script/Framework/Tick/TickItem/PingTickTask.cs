using NetWork.Auto;
using UnityEngine;
using System.Collections;

public class PingTickTask : AbstractTickTask
{
    private const int   m_nPingDuringTime       = 7000;
    private const int   m_nTimeOut              = 30000;
    private int         m_nCurrentTimeout       = 0;
    private bool        m_bIsConnect;
    private bool        m_bIsActive;
    static public long  m_iLastSendMsgTime;

    private static PingTickTask m_Instance;
    public static PingTickTask Instance
    {
        get
        {
            return m_Instance;
        }
    }
	protected override bool FirstRunExecute ()
	{
	    m_bIsConnect = false;
        //register msg
        //MessageManager.Instance.RegistMessage(MessageIdConstants.SC_PING, OnPong);
        m_iLastSendMsgTime = TimeManager.Instance.Now;
	    m_Instance = this;
		return false;
	}
	protected override int GetTickTime ()
	{
		return TickTaskConstant.TICK_PING;
	}
	protected override void Beat ()
	{
	    if (!m_bIsActive)
	    {
	        return;
	    }
        if (NetWorkManager.Instance.IsConnected())
	    {
	        m_bIsConnect = true;
	        TryPing();
	        if (m_nCurrentTimeout > m_nTimeOut)
	        {
	            // time out
                Debuger.LogError("Time Out");
                MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_SOCKET_TIMEOUT, null));
	        }
	    }
	    else
        {
            if (m_bIsConnect)
            {
                // disconnect 
                Debuger.LogError("Disconnect");
                MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_SOCKET_CLOSE, null));
                m_bIsConnect = false;

                SetPingStatus(false);
            }
	    }
	}
    private void TryPing()
    {
        if (TimeManager.Instance.Now - m_iLastSendMsgTime > m_nPingDuringTime)
        {
            NetWorkManager.Instance.SendMsgToServer(new CSPingMsg());
            m_nCurrentTimeout += TickTaskConstant.TICK_PING;
        }
        else
        {
            m_nCurrentTimeout = 0;
        }
    }
    private void OnPong(object eb)
    {
        m_nCurrentTimeout = 0;
        m_iLastSendMsgTime = TimeManager.Instance.Now;
    }
    public static void ResetSendMsgTime()
    {
        m_iLastSendMsgTime = TimeManager.Instance.Now;
    }
    public void SetPingStatus(bool isActive)
    {
        m_bIsActive = isActive;
    }
}