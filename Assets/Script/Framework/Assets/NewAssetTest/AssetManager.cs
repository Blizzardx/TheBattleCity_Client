using System.Collections.Generic;
using UnityEngine;
using Common.Math;
using Common.Tool;

namespace Assets.Script.Framework.Assets.NewAssetTest
{
    public class AssetManager : MonoSingleton<AssetManager>
    {
        private class AssetInfo
        {
            public string Name;
            public int RefrenceCount;
            public bool IsDestroyed;
            public string FromBundleName;
            public Object AssetBody;
        }

        private LinkedList<AssetInfo>           m_LRUAssetList;
        private Dictionary<string, AssetInfo>   m_LRUAssetFindMap;

        private Dictionary<string, Object>      m_AssetBuildInMap;
        private Dictionary<string, Object>      m_AssetInBundleMap;
        private AssetbundleManager              m_AssetbundleMgr;

        public void Initialize(string assetbundleDataPath,string manifestName, bool startWithbundle)
        {
            if (startWithbundle)
            {
                m_AssetbundleMgr = gameObject.AddComponent<AssetbundleManager>();
                m_AssetbundleMgr.Initialize(assetbundleDataPath, manifestName);
            }
            m_LRUAssetList = new LinkedList<AssetInfo>();
            m_LRUAssetFindMap = new Dictionary<string, AssetInfo>();
            m_AssetBuildInMap = new Dictionary<string, Object>();
            m_AssetInBundleMap = new Dictionary<string, Object>();
            
        }
        // sync
        // build in
        public T LoadAsset<T>(string name) where T : Object
        {
            Object res = null;
            if (m_AssetBuildInMap.TryGetValue(name, out res))
            {
                // update refrence count
                OnAccess(m_LRUAssetFindMap[name]);
            }
            return res as T;
        }
        public void ReleaseAsset(string name)
        {
            AssetInfo res = null;
            if (m_LRUAssetFindMap.TryGetValue(name, out res))
            {
                res.IsDestroyed = true;
            }
        }
        // in asset bundle
        public T LoadAsset<T>(string name,string bundleName) where T : Object
        {
            Object res = null;
            if (m_AssetBuildInMap.TryGetValue(name, out res))
            {
                // update refrence count
                OnAccess(m_LRUAssetFindMap[name]);
            }
            return res as T;
        }
        public void ReleaseAsset(string name, string bundleName)
        {
            AssetInfo res = null;
            if (m_LRUAssetFindMap.TryGetValue(name, out res))
            {
                res.IsDestroyed = true;
            }
        }
        // async

        // LRU
        private void OnAccess(AssetInfo info)
        {
            string name = info.Name;
            if (m_LRUAssetFindMap.ContainsKey(name))
            {
                var node = m_LRUAssetList.First;
                while (node != null)
                {
                    if (node.Value.Name == name)
                    {
                        var tmpValue = node.Value;
                        m_LRUAssetList.Remove(node);
                        m_LRUAssetList.AddFirst(tmpValue);
                        break;
                    }
                    node = node.Next;
                }
            }
            else
            {
                m_LRUAssetFindMap.Add(name, info);
                m_LRUAssetList.AddFirst(info);
            }
        }
        
    }
}
