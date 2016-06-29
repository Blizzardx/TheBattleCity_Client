using Common.Tool;
using Framework.Common;
using Framework.Message;
using Framework.Network.GamePack;
using Thrift.Protocol;
using UnityEngine;

namespace Framework.Network
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private SocketClient m_Socket;


        public NetworkManager()
        {
            m_Socket = new SocketClient();
        }
        public bool IsConnect()
        {
            return m_Socket.IsConnected();
        }
        public void Connect(string ip, int port)
        {
            m_Socket.Connect(ip,port,new GamePack_Thrift());
        }
        public void Disconnect()
        {
            m_Socket.Disconnect();
        }
        public SocketClient.SocketStatus GetNetworkStatus()
        {
            return m_Socket.GetNetworkStatus();
        }
        public void SendMsgToServer(TBase msgValue)
        {
            int id;
            if (!ThriftMessageHelper.Get_REQ_MSG_ID().TryGetValue(msgValue.GetType(), out id))
            {
                // log error
                Debug.LogError("Can't encode msg " + msgValue.ToString());
                return;
            }
            // get value by type
            m_Socket.SendMsgToServer(new MessageElement(id, msgValue));
        }
        public void RestSocketStatus()
        {
            m_Socket.RestSocketStatus();
        }

        public void Update()
        {
            if (null == m_Socket)
            {
                return;
            }
            m_Socket.Update();
        }
    }
}
