using NetWork.Auto;
using UnityEngine.WSA;

internal class RoomListViewUI: UIListItemBase
{
    private RoomInfo m_Data;
    public override void OnInit()
    {
        base.OnInit();
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