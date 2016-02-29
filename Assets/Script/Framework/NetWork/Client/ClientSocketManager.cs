using System;
using System.Net;
using System.Net.Sockets;
using Thrift.Protocol;
using UnityEngine;

public class ClientSocketManager
{
    public enum SocketStatus
    {
        Idle ,
        Connecting,
        Sending,
        Reciving,
        Closing,
    }
    private MsgBufferTools  m_ClientSocketMsgBufferTools;
    private Socket          m_Socket;
    private SocketStatus    m_Status;
    private Action<bool, string> m_TryConnectCallBack;

    #region singleton
    private static ClientSocketManager m_Instance;
    private ClientSocketManager()
    {
        Initialize();
    }
    public static ClientSocketManager GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new ClientSocketManager();
            }
            return m_Instance;
        }
    }
    #endregion

    #region public interface
    public void Connect(string ip, int port,Action<bool,string> resultCallBack)
    {
        m_TryConnectCallBack = resultCallBack;
        if (null != m_Socket)
        {
            Close();
        }
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(ip), port);
        m_Status = SocketStatus.Connecting;
        m_Socket.BeginConnect(remoteEP, ConnectEventHandle, m_Socket);
    }
    public void Disconnect()
    {
        Close();
    }
    public SocketStatus GetNetworkStatus
    {
        get
        {
            return m_Status;
        }
    }
    public void SendMsgToRemoteServer(params object[] msgValue)
    {
        //reset send buffer 
        m_ClientSocketMsgBufferTools.ResetSendbuffer();
        for (int i = 0; i < msgValue.Length; ++i)
        {
            MessageObject msgObj = msgValue[i] as MessageObject;
            m_ClientSocketMsgBufferTools.PushIntoSendBuffer(msgObj.msgId, (TBase)(msgObj.msgValue));
            if (GameManager.GetInstance.m_bIsShowDebugMsg)
            {
                //SHOW
                Debug.Log("client send:" + msgObj.msgId.ToString());
            }

        }
        SendPkg();
    }
    #endregion

    #region system function
    private void Initialize()
    {
        m_ClientSocketMsgBufferTools = new MsgBufferTools();
        m_ClientSocketMsgBufferTools.Initialize(ClientMessageCenter.GetInstance.BroadCastMsg);
        m_Status = SocketStatus.Idle;
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }
    private void SendPkg()
    {
        if (m_Status == SocketStatus.Idle || m_Status == SocketStatus.Closing)
        {
            return;
        }
        Send(m_ClientSocketMsgBufferTools.GetSendBuffer(), m_ClientSocketMsgBufferTools.GetSendbufferSize());
    }
    private void Send(byte[] buffer,int length)
    {
        m_Status = SocketStatus.Sending;
        m_Socket.BeginSend(buffer, 0, length, 0, SendEventHandle, m_Socket);
    }
    private void Receive()
    {
        m_Status = SocketStatus.Reciving;
        m_Socket.BeginReceive(m_ClientSocketMsgBufferTools.GetReceiveBuffer(), 0, MsgBufferTools.MAXLength, 0, ReceiveEventHandle, m_Socket);
    }
    private void Close()
    {
        m_Socket.Close();
        m_Status = SocketStatus.Idle;
    }
    private void ConnectEventHandle(IAsyncResult ar)
    {
        Socket client = (Socket)ar.AsyncState;
        if( client.Connected)
        {
            DebugLog.Log("Connected");
            m_TryConnectCallBack(true, string.Empty);
        }
        else
        {
            DebugLog.Log("Connected error");
            m_TryConnectCallBack(false, "Connected error");
        }
        client.EndConnect(ar);
        Receive();
    }
    private void ReceiveEventHandle(IAsyncResult ar)
    {
        m_ClientSocketMsgBufferTools.TryReadMsg();

        m_Socket.EndReceive(ar);

        if (m_Status != SocketStatus.Idle)
        {
            m_Socket.BeginReceive(m_ClientSocketMsgBufferTools.GetReceiveBuffer(), 0, MsgBufferTools.MAXLength, 0, ReceiveEventHandle, m_Socket);
        }
    }
    private void SendEventHandle(IAsyncResult ar)
    {
        Socket client = (Socket)ar.AsyncState;

        client.EndSend(ar);
        m_Status = SocketStatus.Reciving;
    }
    #endregion
}