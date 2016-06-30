using Framework.Common;
using Framework.Message;
using NetWork.Auto;

public class RoomHandler:HandlerBase
{
    public override void OnCreate()
    {
        base.OnCreate();
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_CreateRoom, OnCreateRoom);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_EnterRoom, OnEnterRoom);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_RoomList, OnRoomList);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_SearchRoom, OnSearchRoom);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_SyncPlayerInfo, OnSyncPlayerInfo);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BeginLoadBattle, OnBeginLoadBattle);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleBegin, OnBattleBegin);
        MessageDispatcher.Instance.RegistMessage(MessageIdConstants.SC_BattleEnd, OnBattleEnd);
    }

    public void CreateRoom()
    {
        CSCreateRoom msg = new CSCreateRoom();
        msg.PlayerName = "";
        msg.RoomInformation = new RoomInfo();
        msg.RoomInformation.MapName = "";
        msg.RoomInformation.Name = "";
        msg.RoomInformation.RoomMemberCount = 1;
    }
    public void EnterRoom()
    {
        CSEnterRoom msg = new CSEnterRoom();
        msg.PlayerName = "";
        msg.RoomName = "";
    }
    public void SearchRoom()
    {
        CSSearchRoom msg = new CSSearchRoom();
        msg.Name = "";
        
    }
    private void OnCreateRoom(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnEnterRoom(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnRoomList(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnSearchRoom(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnSyncPlayerInfo(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnBeginLoadBattle(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnBattleBegin(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
    private void OnBattleEnd(IMessage obj)
    {
        throw new System.NotImplementedException();
    }
}