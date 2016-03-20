using NetWork.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WorldLogic : LogicBase<WorldLogic>
{
    private int                 m_PlayerUid;
    private List<PlayerInfo>    m_PlayerList;
    private UIWindowWaitBattle  m_UIWindowWaitBattle;
    

    public override void StartLogic()
    {
        m_PlayerList = new List<PlayerInfo>();

        //register event
        RegisterEvent();

        if(!PlayerDataMode.Instance.isConnected)
        {
            //connet to server 
            ConnectToServer();
        }
        else
        {
            WindowManager.Instance.HideAllWindow();
            WindowManager.Instance.OpenWindow(WindowID.RoomList);
        }
    }

    public override void EndLogic()
    {
        UnRegisterEvent();
    }
    private void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTED, OnConnected);
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTEERROR, OnConnectError);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_EnterRoom, OnEnterRoom);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_CreateRoom, OnCreateRoom);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_SyncPlayerInfo, OnSyncPlayerInfo);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_BeginLoadBattle, OnBeginLoadBattle);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_RoomList, OnRoomList);

    }
    private void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTED, OnConnected);
        MessageManager.Instance.UnregistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTEERROR, OnConnectError);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_EnterRoom, OnEnterRoom);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_CreateRoom, OnCreateRoom);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_SyncPlayerInfo, OnSyncPlayerInfo);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_BeginLoadBattle, OnBeginLoadBattle);
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_RoomList, OnRoomList);
    }
    private void ConnectToServer()
    {
        //connect to test server
        NetWorkManager.Instance.Connect("120.25.176.42", 8000);
        //NetWorkManager.Instance.Connect("192.168.1.4", 8000);
    }

    #region request
    public void EnterRoom(string playerName,string roomName)
    {
        // send msg
        CSEnterRoom client = new CSEnterRoom();
        client.PlayerName = playerName;
        client.RoomName = roomName;
        NetWorkManager.Instance.SendMsgToServer(client);
        // show wait window
        WindowManager.Instance.OpenWindow(WindowID.Wait);
    }
    public void CreateRoom(string playerName, string roomName)
    {
        CSCreateRoom client = new CSCreateRoom();
        client.PlayerName = playerName;
        client.RoomInformation = new RoomInfo();
        client.RoomInformation.MapName = "Scene_0";
        client.RoomInformation.RoomMemberCount = 2;
        client.RoomInformation.Name = roomName;

        NetWorkManager.Instance.SendMsgToServer(client);

        // show wait window
        WindowManager.Instance.OpenWindow(WindowID.Wait);
    }
    public void GetRoomList()
    {
        //send msg to request room list
        CSRoomList client = new CSRoomList();
        client.RequestCount = 100;

        NetWorkManager.Instance.SendMsgToServer(client);

        // show wait window
        WindowManager.Instance.OpenWindow(WindowID.Wait);
    }
    #endregion

    #region message
    private void OnConnected(MessageObject msg)
    {
        PlayerDataMode.Instance.isConnected = true;
        WindowManager.Instance.HideAllWindow();
        WindowManager.Instance.OpenWindow(WindowID.RoomList);
    }
    private void OnConnectError(MessageObject msg)
    {
        TipManager.Instance.Alert("Waring", "Can't connect to server", "Retry", (res) =>
        {
            WindowManager.Instance.OpenWindow(WindowID.Wait);
            ConnectToServer();
        });
    }
    private void OnEnterRoom(MessageObject msg)
    {
        WindowManager.Instance.HideWindow(WindowID.Wait);
        if (msg.msgValue is SCEnterRoom)
        {
            SCEnterRoom server = msg.msgValue as SCEnterRoom;
            if (server.IsSucceed)
            {
                m_PlayerUid = server.PlayerUid;
                WindowManager.Instance.HideAllWindow();
                WindowManager.Instance.OpenWindow(WindowID.WaitBattle);
                m_UIWindowWaitBattle = WindowManager.Instance.GetWindow(WindowID.WaitBattle) as UIWindowWaitBattle;
            }
            else
            {
                TipManager.Instance.Alert(server.ErrorInfo);
            }
        }
    }
    private void OnCreateRoom(MessageObject msg)
    {
        WindowManager.Instance.HideWindow(WindowID.Wait);
        if (msg.msgValue is SCCreateRoom)
        {
            SCCreateRoom server = msg.msgValue as SCCreateRoom;

            if (server.IsSucceed)
            {
                m_PlayerUid = server.PlayerUid;
                WindowManager.Instance.HideAllWindow();
                WindowManager.Instance.OpenWindow(WindowID.WaitBattle);
                m_UIWindowWaitBattle = WindowManager.Instance.GetWindow(WindowID.WaitBattle) as UIWindowWaitBattle;
                PlayerDataMode.Instance.isCreater = true;
            }
            else
            {
                TipManager.Instance.Alert(server.ErrorInfo);
            }
        }
    }
    private void OnSyncPlayerInfo(MessageObject msg)
    {
        WindowManager.Instance.HideWindow(WindowID.Wait);
        if (msg.msgValue is SCSyncPlayerInfo)
        {
            SCSyncPlayerInfo server = msg.msgValue as SCSyncPlayerInfo;
            bool needUpdate = true;
            for(int i=0;i<m_PlayerList.Count;++i)
            {
                if(m_PlayerList[i].Uid == server.PlayerInfomation.Uid)
                {
                    m_PlayerList[i] = server.PlayerInfomation;
                    needUpdate = false;
                    break;
                }
            }
            if(needUpdate)
            {
                m_PlayerList.Add(server.PlayerInfomation);
            }
            m_UIWindowWaitBattle.UpdatePlayer(m_PlayerList);
        }
    }
    private void OnBeginLoadBattle(MessageObject msg)
    {
        PlayerDataMode.Instance.m_PlayerList = m_PlayerList;
        PlayerDataMode.Instance.playerUid = m_PlayerUid;
        StageManager.Instance.ChangeState(GameStateType.BattleState);
    }
    private void OnRoomList(MessageObject msg)
    {
        WindowManager.Instance.HideWindow(WindowID.Wait);
        if (msg.msgValue is SCRoomList)
        {
            SCRoomList roomList = msg.msgValue as SCRoomList;
            UIWindowRoomList window = WindowManager.Instance.GetWindow(WindowID.RoomList) as UIWindowRoomList;
            window.ResetRoomList(roomList.RoomList);
            
        }
    }
    #endregion
}
