using System;
using Framework.Common;
using Framework.Event;
using UnityEngine;

public class UIRoom:UIBase
{
    private UIList m_RoomList;
    
    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/Room/UIWindow_Room";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
    }
    protected override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(FindChild("Button_Create")).onClick = OnClickCreateRoom;
        UIEventListener.Get(FindChild("Button_Search")).onClick = OnClickSearchRoom;
        UIEventListener.Get(FindChild("Button_Refresh")).onClick = OnClickRefreshRoom;
        UIEventListener.Get(FindChild("Button_Back")).onClick = OnClickBack;
        m_RoomList = GetChildComponent<UIList>("UIList");
        m_RoomList.InitUIList<UIRoomListViewUI>();
        RegisterEvent(EventIdDefine.CreateRoom, OnCreateRoom);
        RegisterEvent(EventIdDefine.SearchRoom, OnSearchRoom);
        RegisterEvent(EventIdDefine.EnterRoom, OnEnterRoom);

        RegisterModelEvent(RoomModel.KeyRoomList,OnRoomList,ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index));

        SetBlock(true);
        HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index).GetRoomList();
    }
    private void OnEnterRoom(EventElement obj)
    {

    }
    private void OnSearchRoom(EventElement obj)
    {
    }
    private void OnRoomList(EventElement obj)
    {
        SetBlock(false);
        UpdateRoomList();
    }
    private void OnCreateRoom(EventElement obj)
    {
        UIManager.Instance.CloseWindow<UILoading>();
        if (obj.eventParam != null)
        {
            string errorInfo = obj.eventParam as string;
            Debug.LogError(errorInfo);
        }
        else
        {
            UIManager.Instance.OpenWindow<UIWaitBattle>(UIManager.WindowLayer.Window);
        }
    }

    private void OnClickBack(GameObject go)
    {
        Hide();
        HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index).BackToMainMenu();
    }

    private void OnClickRefreshRoom(GameObject go)
    {
        SetBlock(true);
        HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index).GetRoomList();
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
        if (status)
        {
            UIManager.Instance.OpenWindow<UILoading>(UIManager.WindowLayer.Window);
        }
        else
        {
            UIManager.Instance.CloseWindow<UILoading>();
        }
    }

    private void UpdateRoomList()
    {
        var list = ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetRoomList();
        
        m_RoomList.SetData(list);
    }
}