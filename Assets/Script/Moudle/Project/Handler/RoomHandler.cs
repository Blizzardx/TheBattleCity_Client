using Framework.Common;
using Framework.Event;
using Framework.Message;
using Framework.Network;
using NetWork.Auto;

public class RoomHandler:HandlerBase
{
    private BattleManager m_BattleMgr;
    public override void OnCreate()
    {
        base.OnCreate();
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_CreateRoom, OnCreateRoom);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_EnterRoom, OnEnterRoom);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_RoomList, OnRoomList);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_SearchRoom, OnSearchRoom);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_SyncPlayerInfo, OnSyncPlayerInfo);


        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleEnd, OnBattleEnd);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleBegin, OnBattleBegin);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleLogicFrame, OnBattleLogicFrame);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BeginLoadBattle, OnBeginLoadBattle);
    }

    #region room
    public void CreateRoom(string playerName,string mapName,string roomName,int memberCount)
    {
        CSCreateRoom msg = new CSCreateRoom();
        msg.PlayerName = playerName;
        msg.RoomInformation = new RoomInfo();
        msg.RoomInformation.MapName = mapName;
        msg.RoomInformation.Name = roomName;
        msg.RoomInformation.RoomMemberCount = memberCount;
        NetworkManager.Instance.SendMsgToServer(msg);
    }
    public void EnterRoom(string playerName,string roomName)
    {
        CSEnterRoom msg = new CSEnterRoom();
        msg.PlayerName = playerName;
        msg.RoomName = roomName;
        NetworkManager.Instance.SendMsgToServer(msg);
    }
    public void SearchRoom(string roomName)
    {
        CSSearchRoom msg = new CSSearchRoom();
        msg.Name = roomName;
        NetworkManager.Instance.SendMsgToServer(msg);
    }
    public void GetRoomList()
    {
        CSRoomList msg = new CSRoomList();
        msg.RequestCount = 100;
        NetworkManager.Instance.SendMsgToServer(msg);
    }
    public void BackToMainMenu()
    {
        if (NetworkManager.Instance.IsConnect())
        {
            NetworkManager.Instance.Disconnect();
        }
        UIManager.Instance.OpenWindow<UIMainMenu>(UIManager.WindowLayer.Window);
    }
    public void BattleLoadEnd()
    {
        CSBattleLoadEnd msg = new CSBattleLoadEnd();

        NetworkManager.Instance.SendMsgToServer(msg);
    }
    private void OnCreateRoom(IMessage obj)
    {
        MessageElement pkg = obj as MessageElement;
        SCCreateRoom msg = pkg.GetMessageBody() as SCCreateRoom;
        if (!msg.IsSucceed)
        {
            EventDispatcher.Instance.BroadcastAsync(EventIdDefine.CreateRoom,msg.ErrorInfo);
        }
        else
        {
            HandlerModelData<PlayerModel>(PlayerModel.KeyPlayerId, msg.PlayerUid);
            EventDispatcher.Instance.BroadcastAsync(EventIdDefine.CreateRoom);
        }
    }
    private void OnEnterRoom(IMessage obj)
    {
        MessageElement pkg = obj as MessageElement;
        SCEnterRoom msg = pkg.GetMessageBody() as SCEnterRoom;

        if (!msg.IsSucceed)
        {
            EventDispatcher.Instance.BroadcastAsync(EventIdDefine.EnterRoom, msg.ErrorInfo);
        }
        else
        {
            HandlerModelData<PlayerModel>(PlayerModel.KeyPlayerId, msg.PlayerUid);
            EventDispatcher.Instance.BroadcastAsync(EventIdDefine.EnterRoom);
        }
    }
    private void OnRoomList(IMessage obj)
    {
        MessageElement pkg = obj as MessageElement;
        SCRoomList msg = pkg.GetMessageBody() as SCRoomList;

        HandlerModelData<RoomModel>(RoomModel.KeyRoomList, msg.RoomList);
    }
    private void OnSearchRoom(IMessage obj)
    {
        MessageElement pkg = obj as MessageElement;
        SCSearchRoom msg = pkg.GetMessageBody() as SCSearchRoom;
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.SearchRoom, msg.RoomInformation);
    }
    private void OnSyncPlayerInfo(IMessage obj)
    {
        MessageElement pkg = obj as MessageElement;
        SCSyncPlayerInfo msg = pkg.GetMessageBody() as SCSyncPlayerInfo;

        HandlerModelData<RoomModel>(RoomModel.KeyPlayerList, msg.PlayerInfomation);
    }

    #endregion

    #region battle
    private void OnBattleBegin(IMessage obj)
    {
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.BattleBegin);
    }
    private void OnBattleEnd(IMessage obj)
    {
        EventDispatcher.Instance.Broadcast(EventIdDefine.BattleEnd);
    }
    private void OnBattleLogicFrame(IMessage obj)
    {
        MessageElement msgElement = obj as MessageElement;
        SCBattleLogicFrame msg = msgElement.GetMessageBody() as SCBattleLogicFrame;

        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.BattleLogicFrame, msg);
    }
    private void OnBeginLoadBattle(IMessage obj)
    {
        m_BattleMgr = new BattleManager();
        m_BattleMgr.Initialize();
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.BeginLoadBattle);
    }
    #endregion
}