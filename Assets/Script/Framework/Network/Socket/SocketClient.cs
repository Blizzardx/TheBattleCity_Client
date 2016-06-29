using System;
using System.Net;
using System.Net.Sockets;
using Framework.Common;
using Framework.Message;
using Framework.Network.GamePack;
using Framework.Queue;
using Thrift.Protocol;
using UnityEngine;

namespace Framework.Network
{
    public class SocketClient 
    {
        public enum SocketStatus
        {
            Idle,
            Connecting,
            Sending,
            Reciving,
            Closing,
        }
        private Socket          m_Socket;
        private SocketStatus    m_Status;
        private IGamePack       m_GamePack;
        private float           m_fBeginConnectTime;
        private const int       DEFAULT_CONNECT_TIME_OUT= 3000;
        private const int       DEFAULT_RECEIVE_SIZE    = 64 * 1024;
        private const int       DEFAULT_SEND_SIZE       = 32 * 1024;
        private byte[]          m_RecieveBuffer         = new byte[DEFAULT_RECEIVE_SIZE];


        #region public interface

        public void Connect(string ip, int port,IGamePack gamePack)
        {
            if (null != m_Socket)
            {
                Close();
            }
            m_GamePack = gamePack;
            Clear();
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_Socket.Blocking = true;
            m_Socket.ReceiveBufferSize = DEFAULT_RECEIVE_SIZE;
            m_Socket.SendBufferSize = DEFAULT_SEND_SIZE;
            m_Socket.ReceiveTimeout = 30000;
            m_Socket.SendTimeout = 30000;
            
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(ip), port);
            m_Status = SocketStatus.Connecting;

            m_Socket.BeginConnect(remoteEP, ConnectEventHandle, m_Socket);
            BeginCheckConnectTimeout();
        }
        public void Disconnect()
        {
            RestSocketStatus();
        }
        public SocketStatus GetNetworkStatus()
        {
            return m_Status;
        }
        public void SendMsgToServer(IMessage msgValue)
        {
            if (CheckSocketStatus())
            {
                byte[] sendBuffer = m_GamePack.Encode(msgValue);
                Send(sendBuffer,msgValue);
            }
        }
        public bool IsConnected()
        {
            return m_Socket != null && m_Socket.Connected;
        }
        public void RestSocketStatus()
        {
            Clear();
            if (null != m_Socket)
            {
                m_Socket.Close();
            }
            m_Socket = null;
            m_Status = SocketStatus.Idle;
        }
        public void Update()
        {
            if (m_Status != SocketStatus.Connecting)
            {
                return;
            }
            float deltaTime = Time.realtimeSinceStartup - m_fBeginConnectTime;
            if (deltaTime*1000.0F >= DEFAULT_CONNECT_TIME_OUT)
            {
                ConnectTimeout();
            }
        }
        #endregion

        #region system function
        private void Clear()
        {
            m_GamePack.ClearBuffer();
        }
        private void Send(byte[] sendBuf,IMessage msgBody)
        {
            if (m_Status == SocketStatus.Idle || m_Status == SocketStatus.Closing)
            {
                return;
            }
            m_Status = SocketStatus.Sending;
            m_Socket.BeginSend(sendBuf, 0, sendBuf.Length, 0, SendEventHandle, msgBody);
        }
        private void Receive()
        {
            try
            {
                m_Status = SocketStatus.Reciving;
                m_Socket.BeginReceive(m_RecieveBuffer, 0, m_RecieveBuffer.Length, SocketFlags.None, ReceiveEventHandle, m_Socket);
            }
            catch (Exception e)
            {
                Debug.LogError("Error on recieve socket msg");
                Debug.LogException(e);
                RestSocketStatus();
                MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_CLOSE, null));
            }

        }
        private void Close()
        {
            if (null == m_Socket)
            {
                return;
            }
            m_Socket.Close();
            m_Status = SocketStatus.Idle;
        }
        private void ConnectEventHandle(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndConnect(ar);
            if (client.Connected)
            {
                Receive();
                Debug.Log("Connected");
                MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_CONNECTED, null));
            }
            else
            {
                Debug.Log("Connected error");
                RestSocketStatus();
                MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_CONNECT_ERROR, null));
            }
        }
        private void ReceiveEventHandle(IAsyncResult ar)
        {
            try
            {
                if (m_Status == SocketStatus.Idle || m_Status == SocketStatus.Closing)
                {
                    Debug.Log("recieved msg on socked closing");
                    return;
                }
                int size = m_Socket.EndReceive(ar);
                m_GamePack.AddToBuffer(m_RecieveBuffer, size);
                IMessage msg = null;
                do
                {
                    msg = m_GamePack.Decode();
                    if (msg != null)
                    {
                        MessageQueue.Instance.Enqueue(msg);
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                if (CheckSocketStatus())
                {
                    m_Status = SocketStatus.Reciving;
                    m_Socket.BeginReceive(m_RecieveBuffer, 0, m_RecieveBuffer.Length, SocketFlags.None, ReceiveEventHandle, m_Socket);
                }
            }
            catch (Exception e)
            {
                // 
                Debug.LogError("Error on recieve socket msg");
                Debug.LogException(e);
                RestSocketStatus();
                MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_CLOSE, null));
            }

        }
        private void SendEventHandle(IAsyncResult ar)
        {
            TBase message = ar.AsyncState as TBase;

            try
            {
                int sendSize = m_Socket.EndSend(ar);
                if (sendSize <= 0)
                {
                    Debug.LogError("socket send fialed " + message.ToString());
                    MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_SEND_FAILED, null));
                }
            }
            catch (Exception e)
            {
                RestSocketStatus();
                Debug.LogError("socket send fialed " + message.ToString());
                Debug.LogError("Error on SendEventHandle");
                Debug.LogException(e);
                MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_SEND_FAILED, e));
            }
            m_Status = SocketStatus.Reciving;
        }
        private bool CheckSocketStatus()
        {
            bool res = m_Socket != null && m_Socket.Connected;
            if (!res)
            {
                // 
                Debug.LogWarning("socket disconnect");
                RestSocketStatus();
                MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_CLOSE, null));
            }
            return res;
        }
        private void ConnectTimeout()
        {
            Debug.Log("Connected time out");
            RestSocketStatus();
            MessageQueue.Instance.Enqueue(new MessageElement(ClientCustomMessageDefine.C_SOCKET_CONNECT_ERROR, null));
        }
        private void BeginCheckConnectTimeout()
        {
            m_fBeginConnectTime = Time.realtimeSinceStartup;
        }
        #endregion
    }
}
