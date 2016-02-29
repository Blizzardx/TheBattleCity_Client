using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.Collections;

public class LANServerLogic
{
    private string m_strRoomName;
    private PkgStoC_RoomDetail m_RoomDetail;
    private List<Socket> m_ClientList;
    private int readyCount = 0;

    public void InitLAVServerLogic()
    {
        m_RoomDetail = new PkgStoC_RoomDetail();
        m_RoomDetail.RoomDetailList = new List<SubPkg_RoomDetail>();
        m_ClientList = new List<Socket>();
    }
    public void RegisterMsgHandler()
    {
        LocalServerMessageCenter.GetInstance.RegistMessage(MessageID.CTS_RoomList, CTS_RoomList_Handler);
        LocalServerMessageCenter.GetInstance.RegistMessage(MessageID.CTS_EnterRoom, CTS_EnterRoom_Handler);
        LocalServerMessageCenter.GetInstance.RegistMessage(MessageID.CTS_CreateRoom, CTS_CreateRoom_Handler);
        LocalServerMessageCenter.GetInstance.RegistMessage(MessageID.CTS_ReadyToBattle, CTS_ReadyToBattle_Handler);
        LocalServerMessageCenter.GetInstance.RegistMessage(MessageID.G_Handler, G_Handler_Handler);
        LocalServerMessageCenter.GetInstance.RegistMessage(MessageID.CTS_BulletTrajectory, CTS_BulletTrajectory_Handler);
    }
    public void UnRegisterMsgHandler()
    {
        LocalServerMessageCenter.GetInstance.UnregistMessage(MessageID.CTS_RoomList, CTS_RoomList_Handler);
        LocalServerMessageCenter.GetInstance.UnregistMessage(MessageID.CTS_EnterRoom, CTS_EnterRoom_Handler);
        LocalServerMessageCenter.GetInstance.UnregistMessage(MessageID.CTS_ReadyToBattle, CTS_ReadyToBattle_Handler);
        LocalServerMessageCenter.GetInstance.UnregistMessage(MessageID.G_Handler, G_Handler_Handler);
        LocalServerMessageCenter.GetInstance.UnregistMessage(MessageID.CTS_BulletTrajectory, CTS_BulletTrajectory_Handler);
    }
    private void CTS_RoomList_Handler(MessageObject eb)
    {
        PkgSToC_RoomList tmpRoomList = new PkgSToC_RoomList();
        tmpRoomList.RoomInfoList = new List<SubPkg_RoomInfo>();
        if (! string.IsNullOrEmpty(m_strRoomName))
        {
            SubPkg_RoomInfo tmpRom = new SubPkg_RoomInfo();
            tmpRom.MapId = 0;
            tmpRom.RoomName = m_strRoomName;
            tmpRom.State = 1;
            tmpRoomList.RoomInfoList.Add(tmpRom);
        }
        ServerSocketManager.GetInstance.SendMsgToClient(eb.m_ClientSocket, new MessageObject(MessageID.STC_RoomList, tmpRoomList));
    }
    private void CTS_EnterRoom_Handler(MessageObject eb)
    {
        PkgCToS_EnterRoom clientMsg = eb.msgValue as PkgCToS_EnterRoom;
        m_strRoomName = clientMsg.RoomName;
        SubPkg_RoomDetail tmpPlayer = new SubPkg_RoomDetail();
        tmpPlayer.PlayerName = clientMsg.PlayerName;
        tmpPlayer.Position = 1;
        
        AddToRoomList(tmpPlayer);
        AddToClientSocketStore(eb.m_ClientSocket);

        BroadCastmsgToAllClient(new MessageObject(MessageID.STC_RoomDetail, m_RoomDetail));
    }
    private void CTS_CreateRoom_Handler(MessageObject eb)
    {
        PkgCToS_CreateRoom clientMsg = eb.msgValue as PkgCToS_CreateRoom;
        m_strRoomName = clientMsg.RoomName;
        SubPkg_RoomDetail tmpSelf = new SubPkg_RoomDetail();
        tmpSelf.PlayerName = clientMsg.PlayerName;
        tmpSelf.Position = 0;
        AddToRoomList(tmpSelf);
        AddToClientSocketStore(eb.m_ClientSocket);
        ServerSocketManager.GetInstance.SendMsgToClient(eb.m_ClientSocket, new MessageObject(MessageID.STC_RoomDetail, m_RoomDetail));
    }
    private void CTS_ReadyToBattle_Handler(MessageObject eb)
    {
        ++ readyCount;
        if (readyCount == m_ClientList.Count)
        {
            PkgSToC_BeginBattle tmpMsg = new PkgSToC_BeginBattle();
            tmpMsg.BeginTimeUTC = TimeManager.ClientTimeMSec + 3000;
            BroadCastmsgToAllClient(new MessageObject(MessageID.STC_BeginBattle, tmpMsg));
        }
    }
    private void G_Handler_Handler(MessageObject eb)
    {
        BroadCastmsgToAllClientExceptTarget(eb.m_ClientSocket, eb);
    }
    private void CTS_BulletTrajectory_Handler(MessageObject eb)
    {
        //BroadCastmsgToAllClientExceptTarget(eb.m_ClientSocket, eb);
    }

    #region system function

    private void BroadCastmsgToAllClient(MessageObject eb)
    {
        for (int i = 0; i < m_ClientList.Count; ++i)
        {
            ServerSocketManager.GetInstance.SendMsgToClient(m_ClientList[i], eb);    
        }
    }
    private void BroadCastmsgToAllClientExceptTarget(Socket target, MessageObject eb)
    {
        for (int i = 0; i < m_ClientList.Count; ++i)
        {
            if (m_ClientList[i] == target)
            {
                continue;
            }
            ServerSocketManager.GetInstance.SendMsgToClient(m_ClientList[i], eb);
        }
    }
    private void AddToClientSocketStore(Socket client)
    {
        for (int i = 0; i < m_ClientList.Count; ++i)
        {
            if (m_ClientList[i] == client)
            {
                return;
            }
        }
        m_ClientList.Add(client);
    }
    private void AddToRoomList(SubPkg_RoomDetail elem)
    {
        for (int i = 0; i < m_RoomDetail.RoomDetailList.Count; ++i)
        {
            if (m_RoomDetail.RoomDetailList[i] == elem)
            {
                return;
            }
        }
        m_RoomDetail.RoomDetailList.Add(elem);
    }
    #endregion
}
