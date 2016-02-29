using UnityEngine;
using System.Collections;

public class Window_RoomPreview : WindowBase
{
    private Button m_ButtonClose;
    private Button m_ButtonEnter;
    private UILabel m_LabelRoomName;
    private SubPkg_RoomInfo m_RoomInfo;
    public override void OnOpen(System.Action OnFinshedCallBack, object paramter)
    {
        base.OnOpen(OnFinshedCallBack, paramter);
        m_ButtonEnter = FindChildComponent<Button>("Button_Enter");
        m_ButtonClose = FindChildComponent<Button>("Button_Close");

        m_ButtonEnter.AddCallBack(OnClickEnter);
        m_ButtonClose.AddCallBack(OnClickClose);

        m_RoomInfo = paramter as SubPkg_RoomInfo;

        ClientMessageCenter.GetInstance.RegistMessage(MessageID.STC_RoomDetail, OnEnterRoom);

        OnFinshedCallBack();
    }
    private void OnClickEnter()
    {
        //send msg to server
        PkgCToS_EnterRoom msg = new PkgCToS_EnterRoom();
        msg.RoomName = m_RoomInfo.RoomName;
        msg.PlayerName = PlayerData.GetInstance.PlayerName;

        ClientSocketManager.GetInstance.SendMsgToRemoteServer(new MessageObject(MessageID.CTS_EnterRoom, msg));
    }
    public void OnClickClose()
    {
        WindowManager.GetInstance.HideWindow(WindowType.RoomMapPreview);
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
