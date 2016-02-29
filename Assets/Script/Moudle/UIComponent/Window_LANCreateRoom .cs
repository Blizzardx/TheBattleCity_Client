using System;
using UnityEngine;
using System.Collections.Generic;

public class Window_LANCreateRoom : WindowBase
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
        ClientMessageCenter.GetInstance.RegistMessage(MessageID.C_ConnectToLANServerDone, ConnectServerDone);
    

        OnFinshedCallBack();
    }
    private void OnClickCreate()
    {
        if (string.IsNullOrEmpty(m_InputRoomName.value))
        {
            return;
        }
        //create room
        LocalServerManager.GetInstance.CreateRoom(m_InputRoomName.value, "");

        ClientSocketManager.GetInstance.Connect(GameManager.GetInstance.m_LocalIp, Convert.ToInt32(GameManager.GetInstance.m_LocalPort), (res, errorInfo) =>
        {
            if (res)
            {
                ClientMessageCenter.GetInstance.BoradCastCustomMsg(
                    new MessageObject(MessageID.C_ConnectToLANServerDone, errorInfo));
            }
            else
            {
                ClientMessageCenter.GetInstance.BoradCastCustomMsg(
                    new MessageObject(MessageID.C_ConnectToLANServerDone, errorInfo));
            }
        }
    );
    }
    public void OnClickClose()
    {
        HideWindow();
    }
    public override void OnClose()
    {
        base.OnClose();
        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.STC_RoomDetail, OnEnterRoom);
        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.C_ConnectToLANServerDone, ConnectServerDone);
    }
    private void ConnectServerDone(MessageObject eb)
    {
        //send msg to server
        PkgCToS_CreateRoom msg = new PkgCToS_CreateRoom();
        msg.RoomName = m_InputRoomName.value;
        msg.PlayerName = PlayerData.GetInstance.PlayerName;

        ClientSocketManager.GetInstance.SendMsgToRemoteServer(new MessageObject(MessageID.CTS_CreateRoom, msg));
    }
    private void OnEnterRoom(MessageObject eb)
    {
        PkgStoC_RoomDetail roomDetial = eb.msgValue as PkgStoC_RoomDetail;
        PlayerData.GetInstance.BattleRoomDetail = roomDetial.RoomDetailList;
        SceneManager.GetInstance.ChangeSceneTo(SceneType.Battle);
    }
}
