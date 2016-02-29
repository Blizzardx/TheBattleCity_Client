using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Thrift.Protocol;
using UnityEngine;

public class ServerSocketManager
{
    public class ClientAcceptInfo
    {
        public ClientAcceptInfo(Socket clientSocket, Action<List<MessageObject>> boradCastCallBack,Action<ClientAcceptInfo> OnLoseHandler)
        {
            m_ClientSocket = clientSocket;
            m_MsgBufferTools = new MsgBufferTools();
            m_MsgBufferTools.Initialize(boradCastCallBack);
            m_OnLoseHandler = OnLoseHandler;
        }

        public Socket                           m_ClientSocket;
        public MessageObject                    m_Message;
        public MsgBufferTools                   m_MsgBufferTools;
        public ClientSocketManager.SocketStatus m_Status;
        public Action<ClientAcceptInfo>         m_OnLoseHandler;

        public void BeginRecieve()
        {
            m_ClientSocket.BeginReceive(m_MsgBufferTools.GetReceiveBuffer(), 0, MsgBufferTools.MAXLength, 0,
                ReceiveEventHandle, m_ClientSocket);
            m_Status = ClientSocketManager.SocketStatus.Reciving;
        }
        private void ReceiveEventHandle(IAsyncResult ar)
        {
            m_MsgBufferTools.TryReadMsg(m_ClientSocket);

            //check is lose ?????
            if (!m_ClientSocket.Connected)
            {
                m_OnLoseHandler(this);
            }
            else
            {
                m_ClientSocket.EndReceive(ar);

                if (m_Status != ClientSocketManager.SocketStatus.Idle)
                {
                    m_ClientSocket.BeginReceive(m_MsgBufferTools.GetReceiveBuffer(), 0, MsgBufferTools.MAXLength, 0,
                        ReceiveEventHandle, m_ClientSocket);
                }
            }
        }
    }

    public enum LocalServerStatus
    {
        Idle,
        Sending,
        Accept
    }
    static public readonly int          m_nLocalServerPort = 2112;
    private Socket                      m_Socket;
    private LocalServerStatus           m_Status;
    private MsgBufferTools              m_LocalServerSocketMsgBufferTools;
    private List<ClientAcceptInfo>      m_ClientList;
    #region singleton
    static private ServerSocketManager m_Instance;
    public static ServerSocketManager GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new ServerSocketManager();
            }
            return m_Instance;
        }
    }
    private ServerSocketManager()
    {
    }
    #endregion

    #region public interface
    public void Initialize()
    {
        m_ClientList = new List<ClientAcceptInfo>();
        m_LocalServerSocketMsgBufferTools = new MsgBufferTools();
        m_LocalServerSocketMsgBufferTools.Initialize(null);
        m_Status = LocalServerStatus.Idle;
    }
    public void StartServer()
    {
        if (m_Status == LocalServerStatus.Idle)
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_Socket.Bind(new IPEndPoint(IPAddress.Any, m_nLocalServerPort));
            m_Socket.Listen(10);
            m_Socket.BeginAccept(OnAcceptHandler,m_Socket);
        }
    }
    public void CloseServer()
    {
        if (null != m_Socket)
        {
            m_Socket.Close();
            m_Status = LocalServerStatus.Idle;
            m_Socket = null;
        }
    }
    public void SendMsgToClient(Socket clientSocket,params object[] msgValue)
    {
        //reset send buffer 
        m_LocalServerSocketMsgBufferTools.ResetSendbuffer();
        for (int i = 0; i < msgValue.Length; ++i)
        {
            MessageObject msgObj = msgValue[i] as MessageObject;
            m_LocalServerSocketMsgBufferTools.PushIntoSendBuffer(msgObj.msgId, (TBase)(msgObj.msgValue));
            if (GameManager.GetInstance.m_bIsShowDebugMsg)
            {
                //SHOW
                Debug.Log("local server send:" + msgObj.msgId.ToString());
            }
        }
        SendPkg(clientSocket);
    }
    #endregion

    #region system function
    private void SendPkg(Socket TargetSocket)
    {
        if (m_Status == LocalServerStatus.Idle)
        {
            return;
        }
        Send(m_LocalServerSocketMsgBufferTools.GetSendBuffer(), m_LocalServerSocketMsgBufferTools.GetSendbufferSize(),TargetSocket);
    }
    private void Send(byte[] buffer, int length,Socket TargetSocket)
    {
        m_Status = LocalServerStatus.Sending;
        TargetSocket.BeginSend(buffer, 0, length, 0, SendEventHandle, TargetSocket);
    }
    private void OnAcceptHandler(IAsyncResult ar)
    {
        Socket skServer = (Socket)ar.AsyncState;

        Socket skClient = skServer.EndAccept(ar);
        ClientAcceptInfo client = new ClientAcceptInfo(skClient, LocalServerMessageCenter.GetInstance.BroadCastMsg, OnClientLoseHandler);
        m_ClientList.Add(client);

        client.BeginRecieve();

        skServer.BeginAccept(OnAcceptHandler, skServer);
        m_Status = LocalServerStatus.Accept;
    }
    private void OnClientLoseHandler(ClientAcceptInfo client)
    {
        m_ClientList.Remove(client);
    }
    private void SendEventHandle(IAsyncResult ar)
    {
        Socket server = (Socket) ar.AsyncState;

        server.EndSend(ar);
        m_Socket.BeginAccept(OnAcceptHandler, m_Socket);
        m_Status = LocalServerStatus.Accept;
    }
    public void BasicUpdate()
    {
        if (m_Status == LocalServerStatus.Idle)
        {
            return;
        }
        LocalServerMessageCenter.GetInstance.OnTick();
    }
    #endregion

}