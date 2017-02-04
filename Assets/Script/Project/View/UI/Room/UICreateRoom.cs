
using Framework.Common;
using Framework.Event;
using UnityEngine;

class UICreateRoom : UIBase
{
    private UIInput m_InputRoomName;
    private UIInput m_InputPlayerName;

    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/Room/UIWindow_CreateRoom";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
    }
    protected override void OnCreate()
    {
        base.OnCreate();

        RegisterEvent(EventIdDefine.CreateRoom, OnCreateRoom);
    }

    private void OnCreateRoom(EventElement obj)
    {
        Hide();
    }

    protected override void OnInit()
    {
        base.OnInit();
        m_InputRoomName = GetChildComponent<UIInput>("Input_RoomName");
        m_InputPlayerName = GetChildComponent<UIInput>("Input_Name");
        UIEventListener.Get(FindChild("Button_Create")).onClick = OnClickOk;
        UIEventListener.Get(FindChild("Button_Cancle")).onClick = OnClickCancle;
    }

    private void OnClickCancle(GameObject go)
    {
        Hide();
    }

    private void OnClickOk(GameObject go)
    {
        UIManager.Instance.OpenWindow<UILoading>(UIManager.WindowLayer.Window);
        HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index).CreateRoom(m_InputPlayerName.value,"map_1", m_InputRoomName.value,1);
    }
}