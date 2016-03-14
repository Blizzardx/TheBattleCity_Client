using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class WorldLogic : LogicBase<WorldLogic>
{
    public override void StartLogic()
    {
        //register event
        RegisterEvent();

        //connet to server 
        ConnectToServer();
    }

    public override void EndLogic()
    {
        UnRegisterEvent();
    }

    private void RegisterEvent()
    {
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTED, OnConnected);
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTEERROR, OnConnectError);
    }
    private void UnRegisterEvent()
    {
        MessageManager.Instance.UnregistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTED, OnConnected);
        MessageManager.Instance.UnregistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTEERROR, OnConnectError);
    }
    private void ConnectToServer()
    {
        //connect to test server
        NetWorkManager.Instance.Connect("192.168.1.4", 8000);
    }
    private void OnConnected(MessageObject msg)
    {
        WindowManager.Instance.HideAllWindow();
        WindowManager.Instance.OpenWindow(WindowID.RoomList);
    }
    private void OnConnectError(MessageObject msg)
    {
        TipManager.Instance.Alert("Waring", "Can't connect to server", "Retry", (res) =>
        {
            WindowManager.Instance.OpenWindow(WindowID.Wait);
            ConnectToServer();
        });
    }
}
