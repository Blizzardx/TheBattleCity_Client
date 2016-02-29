using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Threading;
using Thrift.Protocol;
using Thrift.Transport;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public class MsgBufferTools
{
    readonly static public int              MAXLength               = 2048 * 100;
    readonly static public int              MaxMsgBufferPoolSize    = 20;
    private byte[]                          m_ReciveBuffer          = new byte[MAXLength];
    private byte[]                          m_SendBuffer            = new byte[MAXLength];
    private List<MessageObject>             m_RecMsgStore           = new List<MessageObject>();
    private Dictionary<MessageID, TBase>    m_MsgFactory            = new Dictionary<MessageID, TBase>();
    private int                             m_nSendBufferSize;
    private TMemoryBuffer                   m_SendTransport;
    private TProtocol                       m_SendProtocol;
    private TMemoryBuffer                   m_RecTransport;
    private TProtocol                       m_RecProtocol;
    private PkgTitle                        m_SendPkgTitle;
    private PkgTitle                        m_RecPkgTitle;
    private Action<List<MessageObject>>     m_BroadCastCallBack;

    #region public interface
    public void Initialize(Action<List<MessageObject>>     broadCastCallBack)
    {
        m_BroadCastCallBack = broadCastCallBack;
        m_SendPkgTitle = new PkgTitle();
        m_SendPkgTitle.IdStore = new List<short>();
        m_RecPkgTitle = new PkgTitle();
    }
    public byte[] GetReceiveBuffer()
    {
        return m_ReciveBuffer;
    }
    public byte[] GetSendBuffer()
    {
        byte[] title = TMemoryBuffer.Serialize(m_SendPkgTitle);
        byte[] buffer = m_SendTransport.GetBuffer();

        Array.Copy(title, 0, m_SendBuffer, 0, title.Length);
        Array.Copy(buffer, 0, m_SendBuffer, title.Length, buffer.Length);

        m_nSendBufferSize = title.Length + buffer.Length;
        return m_SendBuffer;
    }
    public void PushIntoSendBuffer(MessageID id, TBase buffer)
    {
        m_SendPkgTitle.IdStore.Add((short)(id));
        buffer.Write(m_SendProtocol);
    }
    public void ResetSendbuffer()
    {
        m_SendTransport = new TMemoryBuffer();
        m_SendProtocol = new TBinaryProtocol(m_SendTransport);
        m_SendPkgTitle.IdStore.Clear();
        m_nSendBufferSize = 0;
    }
    public int GetSendbufferSize()
    {
        return m_nSendBufferSize;
    }
    public void TryReadMsg(Socket clientSocket = null)
    {
        try
        {
            m_RecTransport = new TMemoryBuffer(m_ReciveBuffer);
            m_RecProtocol = new TBinaryProtocol(m_RecTransport);

            //read message
            m_RecPkgTitle.Read(m_RecProtocol);
            m_RecMsgStore.Clear();

            for (int i = 0; i < m_RecPkgTitle.IdStore.Count; ++i)
            {
                MessageID id = (MessageID)(m_RecPkgTitle.IdStore[i]);
                if (GameManager.GetInstance.m_bIsShowDebugMsg)
                {
                    //SHOW
                    Debug.LogWarning("rec Server:" + id.ToString());
                }
                TBase msgBody = GetMsgStructByID(id);
                if (null != msgBody)
                {
                    msgBody.Read(m_RecProtocol);
                    m_RecMsgStore.Add(new MessageObject(id, msgBody,clientSocket));
                }
                else
                {
                    Debug.LogError("msg id can't find real struct to decode : msg id " + id.ToString());
                }
            }
            if (null != m_BroadCastCallBack)
            {
                m_BroadCastCallBack(m_RecMsgStore);
            }
        }
        catch
        {
            Debug.LogError("Network Msg error");
        }
    }
    #endregion

    #region system function
    private TBase GetMsgStructByID(MessageID id)
    {
        if (!m_MsgFactory.ContainsKey(id))
        {
            if (m_MsgFactory.Count > MaxMsgBufferPoolSize)
            {
                m_MsgFactory.Clear();
                System.GC.Collect();
            }
            m_MsgFactory.Add(id, MsgStructFactory(id));
        }
        return m_MsgFactory[id];
    }
    private TBase MsgStructFactory(MessageID id)
    {
        TBase res = null;
        switch (id)
        {
            // 0 - 1000 general pkg
            case MessageID.G_Position:
                res = new PkgGeneral_UpdatePos();
                break;
            case MessageID.G_Handler:
                res = new PkgGeneral_Handle();
                break;
            case MessageID.G_RoleInfo:
                res = new PkgGeneral_RoleInfo();
                break;
            case MessageID.G_DissolutionRoom:
                res = new PkgGeneral_DissolutionRoom();
                break;


            // 1000 - 1999 server to client pkg
            case MessageID.STC_RegisterResult:
                res = new PkgSToC_RegisteResult();
                break;
            case MessageID.STC_LoginResult:
                res = new PkgSToC_LoginResult();
                break;
            case MessageID.STC_CreateRoleResult:
                res = new PkgSToC_CreateRoleResult();
                break;
            case MessageID.STC_CreateOrEnterRoom:
                res = new PkgSToC_CreateOrEnterRoomResult();
                break;
            case MessageID.STC_RoomList:
                res = new PkgSToC_RoomList();
                break;
            case MessageID.STC_Hurt:
                res = new PkgSToC_Hurt();
                break;
            case MessageID.STC_BeginBattle:
                res = new PkgSToC_BeginBattle();
                break;
            case MessageID.STC_ChangeBulletCompleted:
                res = new PkgSToC_ChangeBulletCompleted();
                break;
            case MessageID.STC_Fire:
                res = new PkgSToC_Fire();
                break;
            case MessageID.STC_RoomDetail:
                res = new PkgStoC_RoomDetail();
                break;

            // 1999 - 2999 client to server pkg
            case MessageID.CTS_PlayerInfo:
                res = new PkgCToS_PlayerInfo();
                break;
            case MessageID.CTS_Register:
                res = new PkgCToS_Register();
                break;
            case MessageID.CTS_Login:
                res = new PkgCToS_Login();
                break;
            case MessageID.CTS_CreateRole:
                res = new PkgCToS_CreateRole();
                break;
            case MessageID.CTS_CreateRoom:
                res = new PkgCToS_CreateRoom();
                break;
            case MessageID.CTS_EnterRoom:
                res = new PkgCToS_EnterRoom();
                break;
            case MessageID.CTS_RoomList:
                res = new PkgCToS_RoomList();
                break;
            case MessageID.CTS_ReadyToBattle:
                res = new PkgCToS_ReadyToBattle();
                break;
            case MessageID.CTS_RequestChangeBullet:
                res = new PkgCToS_RequestChangeBullet();
                break;
            case MessageID.CTS_BulletTrajectory:
                res = new PkgCToS_BulletTrajectory();
                break;
            case MessageID.CTS_ChangePosition:
                res = new PkgCToS_ChangePosition();
                break;
        }

        if (null == res)
        {
            Debug.LogError("can't find target message struct : " + id.ToString());
        }
        return res;
    }
    #endregion

}
