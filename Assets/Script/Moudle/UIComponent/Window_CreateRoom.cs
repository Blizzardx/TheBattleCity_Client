using UnityEngine;
using System.Collections;

public class Window_CreateRoom : WindowBase
{
    private Button m_ButtonClose;
    private Button m_ButtonCreate;
    private UIInput m_InputRoomName;

    public override void OnOpen(System.Action OnFinshedCallBack, object paramter)
    {
        base.OnOpen(OnFinshedCallBack, paramter);
        m_ButtonCreate = FindChildComponent<Button>("Button_CreateRoom");
        m_ButtonClose = FindChildComponent<Button>("Button_Close");
        m_InputRoomName = FindChildComponent<UIInput>("Input_RoomName");

        m_ButtonCreate.AddCallBack(OnClickCreate);
        m_ButtonClose.AddCallBack(OnClickClose);

        ClientMessageCenter.GetInstance.RegistMessage(MessageID.STC_RoomDetail, OnEnterRoom);
    

        OnFinshedCallBack();
    }
    private void OnClickCreate()
    {
        if (string.IsNullOrEmpty(m_InputRoomName.value))
        {
            return;
        }

        //send msg to server
        PkgCToS_CreateRoom msg = new PkgCToS_CreateRoom();
        msg.RoomName = m_InputRoomName.value;
        msg.PlayerName = PlayerData.GetInstance.PlayerName;

        ClientSocketManager.GetInstance.SendMsgToRemoteServer(new MessageObject(MessageID.CTS_CreateRoom, msg));
    }
    public void OnClickClose()
    {
        HideWindow();
    }
    public override void OnClose()
    {
        base.OnClose();
        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.STC_RoomDetail, OnEnterRoom);
    }
    private void OnEnterRoom(MessageObject eb)
    {
        PkgStoC_RoomDetail roomDetial = eb.msgValue as PkgStoC_RoomDetail;
        PlayerData.GetInstance.BattleRoomDetail = roomDetial.RoomDetailList;
        SceneManager.GetInstance.ChangeSceneTo(SceneType.Battle);
    }
}
