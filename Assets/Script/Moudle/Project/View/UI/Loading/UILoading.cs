using Framework.Common;
using Framework.Event;
using UnityEngine;

public class UILoading : UIBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetResourceName("BuildIn/UI/Prefab/Loading/UIWindow_Loading",PerloadAssetType.BuildInAsset);
        //SetResource(Resources.Load("BuildIn/UI/Prefab/Loading/UIWindow_Loading") as GameObject);
    }

    protected override void OnInit()
    {
        base.OnInit();
        RegisterEvent(EventIdDefine.LoadingSceneProcess, OnProcess);
    }

    private void OnProcess(EventElement obj)
    {
        Debug.Log("UILoading process " + (float)obj.eventParam);
    }
}
