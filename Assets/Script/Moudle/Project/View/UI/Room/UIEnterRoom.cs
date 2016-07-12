
using Framework.Common;
using Framework.Event;
using NetWork.Auto;
using UnityEngine;

class UIEnterRoom : UIBase
{
    private UIInput m_InputName;
    private RoomInfo m_RoomInfo;

    protected override void OnCreate()
    {
        base.OnCreate();
        SetResourceName("BuildIn/UI/Prefab/Room/UIWindow_EnterRoom", PerloadAssetType.BuildInAsset);
    }

    protected override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(FindChild("Button_Enter")).onClick = OnClickEnter;
        UIEventListener.Get(FindChild("Button_Cancle")).onClick = OnClickCancle;
        m_InputName = GetChildComponent<UIInput>("Input_Name");

        RegisterEvent(EventIdDefine.EnterRoom, OnEnterRoom);
    }

    private void OnEnterRoom(EventElement obj)
    {
        SetBlock(false);
        if (null != obj.eventParam)
        {
            // show tips 

        }
    }

    protected override void OnOpen(object param)
    {
        base.OnOpen(param);
        m_RoomInfo = param as RoomInfo;
    }
    private void OnClickCancle(GameObject go)
    {
        Hide();
    }
    private void OnClickEnter(GameObject go)
    {
        SetBlock(true);
        HandlerManager.Instance.GetHandler<RoomHandler>().EnterRoom(m_InputName.value, m_RoomInfo.Name);
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
}