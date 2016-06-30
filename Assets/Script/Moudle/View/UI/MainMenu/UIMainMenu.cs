using Framework.Event;
using UnityEngine;

public class UIMainMenu:UIBase
{

    protected override void OnCreate()
    {
        base.OnCreate();
        SetResourceName("BuildIn/UI/Prefab/MainMenu/Window_SelectMode", PerloadAssetType.BuildInAsset);
    }
    protected override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(FindChild("Button_OnLine")).onClick = OnClickOnLineMode;
        UIEventListener.Get(FindChild("Button_LAN")).onClick = OnClickLANMode;
        UIEventListener.Get(FindChild("Button_Console")).onClick = OnClickConsoleMode;

        EventDispatcher.Instance.RegistEvent(EventIdDefine.Connected, SocketConnected);
        EventDispatcher.Instance.RegistEvent(EventIdDefine.ConnectError, SocketConnectError);
    }

    protected override void OnClose()
    {
        base.OnClose();
        EventDispatcher.Instance.UnregistEvent(EventIdDefine.Connected, SocketConnected);
        EventDispatcher.Instance.UnregistEvent(EventIdDefine.ConnectError, SocketConnectError);
    }

    private void SocketConnectError(EventElement obj)
    {
        UIManager.Instance.CloseWindow<UILoading>();
        Debug.Log("connect error");
    }
    private void SocketConnected(EventElement obj)
    {
        UIManager.Instance.CloseWindow<UILoading>();
        Debug.Log("connected server");
        UIManager.Instance.OpenWindow<UIRoom>(UIManager.WindowLayer.Window);
    }
    private void OnClickOnLineMode(GameObject go)
    {
        var handler = HandlerManager.Instance.GetHandler<MainMenuHandler>();
        if (!handler.ConnectToServer())
        {
            return;
        }
        UIManager.Instance.OpenWindow<UILoading>(UIManager.WindowLayer.Tip);
    }
    private void OnClickLANMode(GameObject go)
    {

    }
    private void OnClickConsoleMode(GameObject go)
    {

    }
}
