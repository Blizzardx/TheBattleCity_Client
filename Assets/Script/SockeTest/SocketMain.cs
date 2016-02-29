using UnityEngine;
using System.Collections;
using System;

public class SocketMain : MonoBehaviour 
{
    ClientSocketManager _mClientSocketMgr = ClientSocketManager.GetInstance;
    static public SocketMain m_Instance = null;

    public UILabel m_LabelIP;
    public UILabel m_LabelPort;
    public UILabel m_LabelSendData;
    public UILabel m_ReceivedData;
    public UILabel m_NetworkStatus;

    void Awake()
    {
        m_Instance = this;
    }
	// Use this for initialization
	void Start () 
    {
	}
    void OnDisable()
    {
        _mClientSocketMgr.Disconnect();
    }
	// Update is called once per frame
	void Update ()
    {
        m_NetworkStatus.text = _mClientSocketMgr.GetNetworkStatus.ToString();

        ClientMessageCenter.GetInstance.OnTick();
	}
    public void Connect()
    {
//        _mClientSocketMgr.Connect(m_LabelIP.text, Convert.ToInt32(m_LabelPort.text));
    }
    public void Disconnect()
    {
        _mClientSocketMgr.Disconnect();
    }
    public void SendCustomPkg()
    {
        PkgCToS_Login       loginPkg    = new PkgCToS_Login();
        loginPkg.LoginId = "TestLoginPlayer";
        loginPkg.Password = "123456";

        PkgCToS_PlayerInfo  playerInfo  = new PkgCToS_PlayerInfo();
        playerInfo.PlayerName = "XXX";
        playerInfo.RoomId   = 1024;



        MessageObject pkg1 = new MessageObject(MessageID.CTS_Login, loginPkg);
        MessageObject pkg2 = new MessageObject(MessageID.CTS_PlayerInfo, playerInfo);

        ClientSocketManager.GetInstance.SendMsgToRemoteServer(pkg1, pkg2);
    }
}
