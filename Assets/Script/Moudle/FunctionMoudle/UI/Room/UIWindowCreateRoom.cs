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

    private void OnClickCancle(GameObject go)
    {
        Hide();
    }
    private void OnClickCreate(GameObject go)
    {
        WorldLogic.Instance.CreateRoom(m_InputPlayerName.value, m_InputRoomName.value);
    }
}
