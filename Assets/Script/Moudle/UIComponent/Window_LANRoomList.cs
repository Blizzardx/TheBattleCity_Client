using UnityEngine;
using System.Collections.Generic;

public class Window_LANRoomList : WindowBase
{
    private UIList m_RoomList;
    private UIInput m_InputPlayerName;
    private Button m_ButtonCreateRoom;

    public override void OnOpen(System.Action OnFinshedCallBack, object paramter)
    {
        base.OnOpen(OnFinshedCallBack, paramter);

        m_InputPlayerName   = FindChildComponent<UIInput>("Input_Name");
        m_ButtonCreateRoom = FindChildComponent<Button>("Button_CreateRoom");
        m_RoomList          = FindChildComponent<UIList>("RoomList");

        m_RoomList.Initialize(typeof(RoomListItem), "RoomItem");

        m_ButtonCreateRoom.AddCallBack(OnClickCreateRoom);
        OnRoomListRefresh();
        OnFinshedCallBack();
    }
    public void ResetPlayerName()
    {
        //reset playername
        PlayerData.GetInstance.PlayerName = m_InputPlayerName.value;
    }
    public override void OnClose()
    {
        ResetPlayerName();

        base.OnClose();

    }
    private void OnClickCreateRoom()
    {
        WindowManager.GetInstance.OpenWindow(WindowType.LANCreateRoom);
    }
    private void OnRoomListRefresh()
    {
        HostData[] roomList = LocalServerManager.GetInstance.GetRoomList();
        List<HostData> tmpRoomList = new List<HostData>(roomList);
        m_RoomList.SetData(tmpRoomList);
    }
}
public class LANRoomListItem : ListItemBase
{
    private HostData         m_data;
    private UILabel         m_LabelRoomName;
    private UISprite        m_SpriteStatus;

    public override void OnInit()
    {
        base.OnInit();
        m_LabelRoomName = FindChildComponent<UILabel>("Label_RoomName");
        m_SpriteStatus = FindChildComponent<UISprite>("Sprite_Status");

        //set background music
        AudioManager.GetInstance.PlayBackgroundSound(BackgroundAudioDefine.AudioBackgroundSoundType.Menu);
    }
    public override void OnClick(GameObject sender)
    {
        Window_RoomList window = WindowManager.GetInstance.GetWindow(WindowType.RoomList) as Window_RoomList;
        window.ResetPlayerName();
        WindowManager.GetInstance.OpenWindow(WindowType.RoomMapPreview, m_data, false);
    }
    public override void OnData(object data)
    {
        base.OnData(data);
        if( null != data)
        {
            //set data
            m_data = data as HostData;

            //
            m_LabelRoomName.text = m_data.gameName;
            //m_SpriteStatus.spriteName = m_data.State == 0 ? "Fighting" : "Waitting";
            m_SpriteStatus.spriteName ="Waitting";
        }
    } 
}