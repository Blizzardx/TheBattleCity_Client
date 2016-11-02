using UnityEngine;

namespace Assets.Script.Framework.Assets.NewAssetTest
{
    internal class AssetbundleInfo
    {
        private string m_strName;
        private AssetBundle m_Assetbundle;
        private int m_iRefrenceCount;
        private string[] m_DepBundleList;

        public AssetbundleInfo(string name, AssetBundle body, string[] dep)
        {
            m_strName = name;
            m_Assetbundle = body;
            m_DepBundleList = dep;
            m_iRefrenceCount = 0;
        }
        public string GetName()
        {
            return m_strName;
        }
        public AssetBundle GetBody()
        {
            return m_Assetbundle;
        }
        public int GetRefrenceCount()
        {
            return m_iRefrenceCount;
        }
        public void SetRefrenceCount(int count)
        {
            m_iRefrenceCount = count;
        }
        public string[] GetAllDepBundles()
        {
            return m_DepBundleList;
        }
    }
}
