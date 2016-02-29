using System;
using UnityEngine;
using System.Collections;

public class Window_SelectMode : WindowBase
{
    private Button m_ButtonOnlineMode;
    private Button m_ButtonLANMode;
    private Button m_ButtonConsoleMode;

    public override void OnOpen(System.Action OnFinshedCallBack, object paramter)
    {
        base.OnOpen(OnFinshedCallBack, paramter);
        m_ButtonOnlineMode = FindChildComponent<Button>("Button_OnLine");
        m_ButtonLANMode = FindChildComponent<Button>("Button_LAN");
        m_ButtonConsoleMode = FindChildComponent<Button>("Button_Console");

        m_ButtonOnlineMode.AddCallBack(OnClickOnlineMode);
        m_ButtonLANMode.AddCallBack(OnClickLANMode);
        m_ButtonConsoleMode.AddCallBack(OnClickConsoleMode);

        ClientMessageCenter.GetInstance.RegistMessage(MessageID.C_ConnectToServerDone,ConnectServerDone);
        OnFinshedCallBack();
    }

    public override void OnClose()
    {
        base.OnClose();

        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.C_ConnectToServerDone, ConnectServerDone);
    }

    private void OnClickOnlineMode()
    {
        PlayerData.GetInstance.m_PlayMode = PlayMode.Online;
        ClientSocketManager.GetInstance.Connect(GameManager.GetInstance.m_RemoteIp,
            Convert.ToInt32(GameManager.GetInstance.m_RemotePort), (res, errorInfo) =>
            {
                if (res)
                {
                    ClientMessageCenter.GetInstance.BoradCastCustomMsg(
                        new MessageObject(MessageID.C_ConnectToServerDone, errorInfo));
                }
                else
                {
                    ClientMessageCenter.GetInstance.BoradCastCustomMsg(
                        new MessageObject(MessageID.C_ConnectToServerDone, errorInfo));
                }
            }

            );
    }
    private void OnClickLANMode()
    {
        PlayerData.GetInstance.m_PlayMode = PlayMode.LAN;
        WindowManager.GetInstance.OpenWindow(WindowType.LANRoomList);
        HideWindow();
    }
    private void OnClickConsoleMode()
    {
        
    }
    private void ConnectServerDone(MessageObject msg)
    {
        HideWindow();
        WindowManager.GetInstance.OpenWindow(WindowType.RoomList, PlayMode.Online);
    }
}