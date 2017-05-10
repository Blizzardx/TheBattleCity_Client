namespace Framework.Asset
{
    public interface IAssetbundleHelper
    {
        string GetBundleNameByAssetName(string assetName);

        string[] GetAssetsNameByBundleName(string bundleName);
    }
}
