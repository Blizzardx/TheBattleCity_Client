using System;
using Framework.Common;
using Framework.Event;
using UnityEngine;

public class UIRoom:UIBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetResourceName("BuildIn/UI/Prefab/Room/UIWindow_Room", PerloadAssetType.BuildInAsset);
    }
    protected override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(FindChild("Button_Create")).onClick = OnClickCreateRoom;
        UIEventListener.Get(FindChild("Button_Search")).onClick = OnClickSearchRoom;
        UIEventListener.Get(FindChild("Button_Refresh")).onClick = OnClickRefreshRoom;
        UIEventListener.Get(FindChild("Button_Back")).onClick = OnClickBack;

        RegisterEvent(EventIdDefine.CreateRoom, OnCreateRoom);
        RegisterEvent(EventIdDefine.SearchRoom, OnSearchRoom);
        RegisterEvent(EventIdDefine.EnterRoom, OnEnterRoom);

        RegisterModelEvent(RoomModel.KeyRoomList,OnRoomList,ModelManager.Instance.GetModel<RoomModel>());
    }
    private void OnEnterRoom(EventElement obj)
    {

    }
    private void OnSearchRoom(EventElement obj)
    {
        throw new NotImplementedException();
    }
    private void OnRoomList(EventElement obj)
    {
        throw new NotImplementedException();
    }
    private void OnCreateRoom(EventElement obj)
    {
        throw new NotImplementedException();
    }

    private void OnClickBack(GameObject go)
    {
        Hide();
        HandlerManager.Instance.GetHandler<RoomHandler>().BackToMainMenu();
    }

    private void OnClickRefreshRoom(GameObject go)
    {
        HandlerManager.Instance.GetHandler<RoomHandler>().GetRoomList();
        SetBlock(true);
    }

    private void OnClickSearchRoom(GameObject go)
    {
        UIManager.Instance.OpenWindow<UISearchRoom>(UIManager.WindowLayer.Window);
    }

    private void OnClickCreateRoom(GameObject go)
    {
        UIManager.Instance.OpenWindow<UICreateRoom>(UIManager.WindowLayer.Window);
    }

    private void SetBlock(bool status)
    {
        
    }
}