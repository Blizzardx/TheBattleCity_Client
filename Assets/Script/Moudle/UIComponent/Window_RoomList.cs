using UnityEngine;
using System.Collections;

public class Window_RoomList : WindowBase
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

        m_RoomList.Initialize(typeof(RoomListItem), "MainMenu/RoomItem");
        ClientMessageCenter.GetInstance.RegistMessage(MessageID.STC_RoomList, OnRoomListRefresh);

        //send to ask room list
        PkgCToS_RoomList msg = new PkgCToS_RoomList();
        msg.PlayerId = "test";
        ClientSocketManager.GetInstance.SendMsgToRemoteServer(new MessageObject(MessageID.CTS_RoomList, msg));

        m_ButtonCreateRoom.AddCallBack(OnClickCreateRoom);
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
        WindowManager.GetInstance.OpenWindow(WindowType.CreateRoom);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.STC_RoomList, OnRoomListRefresh);
    }
    private void OnRoomListRefresh(MessageObject eb)
    {
        PkgSToC_RoomList roomlist = eb.msgValue as PkgSToC_RoomList;
        m_RoomList.SetData(roomlist.RoomInfoList);
    }
}
public class RoomListItem : ListItemBase
{
    private SubPkg_RoomInfo m_data;
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
            m_data = data as SubPkg_RoomInfo;

            //
            m_LabelRoomName.text = m_data.RoomName;
            m_SpriteStatus.spriteName = m_data.State == 0 ? "Fighting" : "Waitting";
        }
    } 
}