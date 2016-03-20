using NetWork.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIWindowEnterRoom : WindowBase
{
    private UIInput m_InputPlayerName;
    private GameObject m_ButtonEnter;
    private GameObject m_ButtonCancle;
    private string m_RoomName;

    public override void OnInit()
    {
        base.OnInit();
        m_InputPlayerName = FindChildComponent<UIInput>("Input_Name");
        m_ButtonEnter = FindChild("Button_Enter");
        m_ButtonCancle = FindChild("Button_Cancle");

        UIEventListener.Get(m_ButtonEnter).onClick = OnClickEnter;
        UIEventListener.Get(m_ButtonCancle).onClick = OnClickCancle;

    }
    public override void OnOpen(object param)
    {
        base.OnOpen(param);
        m_RoomName = param as string;
    }
    public override void OnClose()
    {
        base.OnClose();
    }

    private void OnClickCancle(GameObject go)
    {
        Hide();
    }
    private void OnClickEnter(GameObject go)
    {
        WorldLogic.Instance.EnterRoom(m_InputPlayerName.value, m_RoomName);
    }
}
