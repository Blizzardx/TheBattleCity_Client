using Framework.Common;
using Framework.Event;
using UnityEngine;

class SceneOnlineBattle : SceneBase
{
    public override string GetSceneName()
    {
        return "OnLineBattle";
    }
    protected override void OnCreate()
    {
        base.OnCreate();
        AddLoadResource("BuildIn/UI/Prefab/Battle/UIWindow_Battle", PerloadAssetType.BuildInAsset);
        AddLoadResource("BuildIn/UI/Prefab/Battle/Player_Info", PerloadAssetType.BuildInAsset);

        // load tank
        var list = ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetPlayerInfoList();
        for (int i = 0; i < list.Count; ++i)
        {
            AddLoadResource("BuildIn/Moudle/Tank/" + list[i].meshName, PerloadAssetType.BuildInAsset);
        }

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
        UIManager.Instance.OpenWindow<UIBattle>(UIManager.WindowLayer.Window);
        HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index).BattleLoadEnd();
    }
    protected override void OnProcess(float process)
    {
        base.OnProcess(process);
        Debug.Log("Process " + process);
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.LoadingSceneProcess, process);
    }
}