using NetWork.Auto;
using UnityEngine;

internal class UIRoomListViewUI: UIListItemBase
{
    private RoomInfo m_Data;
    public override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(m_ObjectRoot).onClick = OnClickItem;
    }

    private void OnClickItem(GameObject go)
    {
        UIManager.Instance.OpenWindow<UIEnterRoom>(UIManager.WindowLayer.Window,m_Data);
    }

    public override void OnData()
    {
        base.OnData();
        m_Data = Data as RoomInfo;

        SetData(m_Data);
    }

    private void SetData(RoomInfo roomInfo)
    {
        
    }
}