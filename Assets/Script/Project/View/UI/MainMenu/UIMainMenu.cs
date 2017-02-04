using Framework.Common;
using Framework.Event;
using UnityEngine;

public class UIMainMenu:UIBase
{
    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/MainMenu/Window_SelectMode";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
    }
    protected override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(FindChild("Button_OnLine")).onClick = OnClickOnLineMode;
        UIEventListener.Get(FindChild("Button_LAN")).onClick = OnClickLANMode;
        UIEventListener.Get(FindChild("Button_Console")).onClick = OnClickConsoleMode;
        AudioPlayer.Instance.StopAllAudio();
        AudioPlayer.Instance.PlayAudio("BuildIn/Audio/music_menu", Vector3.zero, true);
        RegisterEvent(EventIdDefine.Connected, SocketConnected);
        RegisterEvent(EventIdDefine.ConnectError, SocketConnectError);
    }
    private void SocketConnectError(EventElement obj)
    {
        UIManager.Instance.CloseWindow<UILoading>();
    }
    private void SocketConnected(EventElement obj)
    {
        UIManager.Instance.CloseWindow<UILoading>();
        Close();
        UIManager.Instance.OpenWindow<UIRoom>(UIManager.WindowLayer.Window);
    }
    private void OnClickOnLineMode(GameObject go)
    {
        var handler = HandlerManager.Instance.GetHandler<MainMenuHandler>(MainMenuHandler.Index);
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
