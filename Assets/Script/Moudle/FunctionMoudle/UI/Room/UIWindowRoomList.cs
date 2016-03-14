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

    }

    public override void OnOpen(object param)
    {
        base.OnOpen(param);
        RegisterMsg();

        RequestRoomList();
    }

    public override void OnClose()
    {
        base.OnClose();
        UnRegisterMsg();
    }

    private void OnClickRefresh(GameObject go)
    {
        RequestRoomList();
    }
    private void OnClickSearch(GameObject go)
    {
        
    }
    private void OnClickCreate(GameObject go)
    {
        WindowManager.Instance.OpenWindow(WindowID.CreateRoom);
    }
    private void RequestRoomList()
    {
        //send msg to request room list
        CSRoomList client = new CSRoomList();
        client.RequestCount = 100;

        NetWorkManager.Instance.SendMsgToServer(client);
    }
    private void RegisterMsg()
    {
        MessageManager.Instance.RegistMessage(MessageIdConstants.SC_RoomList, OnRoomList);
    }
    private void UnRegisterMsg()
    {
        MessageManager.Instance.UnregistMessage(MessageIdConstants.SC_RoomList, OnRoomList);
    }
    private void OnRoomList(MessageObject obj)
    {
        if (obj.msgValue is SCRoomList)
        {
            SCRoomList roomList = obj.msgValue as SCRoomList;
            m_ListRoom.SetData(roomList.RoomList);
        }
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
    }

    public override void OnData()
    {
        base.OnData();
        m_RoomInfo = (RoomInfo) (Data);
        m_LabelRoomName.text = m_RoomInfo.Name;
    }
}