using Framework.Common;
using Framework.Event;
using UnityEngine;

public class UILoading : UIBase
{
    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/Loading/UIWindow_Loading";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
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
