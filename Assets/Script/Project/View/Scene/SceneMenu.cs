using Framework.Common;
using UnityEngine;
using Framework.Event;

public class SceneMenu : SceneBase
{
    public override string GetSceneName()
    {
        return "SceneMenu";
    }

    protected override void OnCreate()
    {
        base.OnCreate();

        AddLoadResource("BuildIn/UI/Prefab/MainMenu/Window_Welcom", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/MainMenu/Window_SelectMode", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/Room/UIWindow_Room", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/Room/UIWindow_CreateRoom", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/Room/UIWindow_EnterRoom", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/Room/UIWindow_SearchRoom", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/Room/UIWindow_WaitBattle", PerloadAssetType.BuildInAsset);

        UIManager.Instance.OpenWindow<UILoading>(UIManager.WindowLayer.Tip);
    }

    protected override void OnInit()
    {
        base.OnInit();
        Debug.Log("OnInit  SceneMenu");
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();
        Debug.Log("loaded  SceneMenu");
        UIManager.Instance.CloseWindow<UILoading>();
        UIManager.Instance.OpenWindow<UIWelcome>(UIManager.WindowLayer.Window);
    }

    protected override void OnProcess(float process)
    {
        base.OnProcess(process);
        Debug.Log("Process " + process);
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.LoadingSceneProcess, process);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}
