using Framework.Common;
using Framework.Event;
using UnityEngine;

class SceneOnlineBattle : SceneBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetSceneName("OnLineBattle");
        AddPreloadResource("BuildIn/UI/Prefab/Battle/UIWindow_Battle", PerloadAssetType.BuildInAsset);
        AddPreloadResource("BuildIn/UI/Prefab/Battle/Player_Info", PerloadAssetType.BuildInAsset);

        UIManager.Instance.CloseAllWindow();
        UIManager.Instance.OpenWindow<UILoading>(UIManager.WindowLayer.Tip);
    }

    protected override void OnInit()
    {
        base.OnInit();
        Debug.Log("OnInit  SceneOnlineBattle");
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();
        Debug.Log("loaded  SceneOnlineBattle");
        UIManager.Instance.CloseWindow<UILoading>();

        HandlerManager.Instance.GetHandler<RoomHandler>().BattleLoadEnd();
    }

    protected override void OnProcess(float process)
    {
        base.OnProcess(process);
        Debug.Log("Process " + process);
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.LoadingSceneProcess, process);
    }
}