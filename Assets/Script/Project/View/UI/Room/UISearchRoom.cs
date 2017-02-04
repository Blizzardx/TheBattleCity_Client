

class UISearchRoom : UIBase
{
    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/Room/UIWindow_SearchRoom";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
    }
}