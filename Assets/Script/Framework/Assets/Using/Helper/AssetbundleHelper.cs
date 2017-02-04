
namespace Framework.Asset
{
    public class AssetbundleHelper
    {
        private static IAssetbundleHelper m_Helper = new AssetbundleHelper_Json();
        public static string GetBundleNameByAssetName(string assetName)
        {
            return m_Helper.GetBundleNameByAssetName(assetName);
        }

        public static string[] GetAssetsNameByBundleName(string bundleName)
        {
            return m_Helper.GetAssetsNameByBundleName(bundleName);
        }
    }

  
    
}
