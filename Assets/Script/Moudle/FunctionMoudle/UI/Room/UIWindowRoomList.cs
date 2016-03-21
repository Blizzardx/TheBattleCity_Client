using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetWork.Auto;
using UnityEngine;

public class UIWindowRoomList : WindowBase
{
    private GameObject m_ButtonCreate;
    private GameObject m_ButtonSearch;
    private GameObject m_ButtonRefresh;
    private UIList m_ListRoom;

    public override void OnInit()
    {
        base.OnInit();
        m_ButtonCreate = FindChild("Button_Create");
        m_ButtonRefresh = FindChild("Button_Refresh");
        m_ButtonSearch = FindChild("Button_Search");
        m_ListRoom = FindChildComponent<UIList>("UIList");
        m_ListRoom.InitUIList<RoomListItem>();

        UIEventListener.Get(m_ButtonCreate).onClick = OnClickCreate;
        UIEventListener.Get(m_ButtonSearch).onClick = OnClickSearch;
        UIEventListener.Get(m_ButtonRefresh).onClick = OnClickRefresh;
        AddChildElementClickEvent(OnClickBack, "Button_Back");
    }

    private void OnClickBack(GameObject go)
    {
        StageManager.Instance.ChangeState(GameStateType.LoginState);
    }

    public override void OnOpen(object param)
    {
        base.OnOpen(param);

        RequestRoomList();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    private void OnClickRefresh(GameObject go)
    {
        RequestRoomList();
    }
    private void OnClickSearch(GameObject go)
    {
        WindowManager.Instance.OpenWindow(WindowID.SearchRoom);
    }
    private void OnClickCreate(GameObject go)
    {
        WindowManager.Instance.OpenWindow(WindowID.CreateRoom);
    }
    private void RequestRoomList()
    {
        WorldLogic.Instance.GetRoomList();
    }

    public void ResetRoomList(List<RoomInfo> roomList)
    {
        m_ListRoom.SetData(roomList);
    }
}

public class RoomListItem : UIListItemBase
{
    private RoomInfo m_RoomInfo;
    private UILabel m_LabelRoomName;

    public override void OnInit()
    {
        base.OnInit();
        m_LabelRoomName = FindChildComponent<UILabel>("Label");
        UIEventListener.Get(m_ObjectRoot).onClick = OnClick;
    }

    private void OnClick(GameObject go)
    {
        WindowManager.Instance.OpenWindow(WindowID.EnterRoom, m_LabelRoomName.text);
    }

    public override void OnData()
    {
        base.OnData();
        m_RoomInfo = (RoomInfo) (Data);
        m_LabelRoomName.text = m_RoomInfo.Name;
    }
}