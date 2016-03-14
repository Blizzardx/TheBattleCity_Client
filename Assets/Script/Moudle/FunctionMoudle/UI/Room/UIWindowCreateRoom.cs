using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NetWork.Auto;
using UnityEngine;

public class UIWindowCreateRoom : WindowBase
{
    private UIInput m_InputRoomName;
    private UIInput m_InputPlayerName;
    private GameObject m_ButtonOk;
    private GameObject m_ButtonCancle;

    public override void OnInit()
    {
        base.OnInit();

        m_InputRoomName = FindChildComponent<UIInput>("Input_RoomName");
        m_InputPlayerName = FindChildComponent<UIInput>("Input_Name");
        m_ButtonOk = FindChild("Button_Create");
        m_ButtonCancle = FindChild("Button_Cancle");

        UIEventListener.Get(m_ButtonOk).onClick = OnClickCreate;
        UIEventListener.Get(m_ButtonCancle).onClick = OnClickCancle;
    }

    public override void OnOpen(object param)
    {
        base.OnOpen(param);
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_CreateRoom,OnCreateRoom);
    }

    public override void OnClose()
    {
        base.OnClose();
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_CreateRoom, OnCreateRoom);
    }

    private void OnClickCancle(GameObject go)
    {
        Hide();
    }
    private void OnClickCreate(GameObject go)
    {
        CSCreateRoom client = new CSCreateRoom();
        client.PlayerName = m_InputPlayerName.value;
        client.RoomInformation = new RoomInfo();
        client.RoomInformation.MapName = "Scene_0";
        client.RoomInformation.RoomMemberCount = 2;
        client.RoomInformation.Name = m_InputRoomName.value;

        NetWorkManager.Instance.SendMsgToServer(client);
    }
    private void OnCreateRoom(MessageObject msg)
    {
        if (msg.msgValue is SCCreateRoom)
        {
            SCCreateRoom server = msg.msgValue as SCCreateRoom;

            if (server.IsSucceed)
            {
                
            }
            else
            {
                TipManager.Instance.Alert(server.ErrorInfo);
            }
        }
    }
}
