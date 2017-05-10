using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Common.Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework.Asset
{
    public class AssetManager : MonoSingleton<AssetManager>
    {
        private class BuildInAssetInfo
        {
            public string AssetName;
            public bool IsDestroyed;
            public Object AssetBody;
        }
        private class InBundleAssetInfo
        {
            public string AssetName;
            public bool IsDestroyed;
            public Object AssetBody;
            public int RefrenceCount;
            public string AssetBundleName;
        }

        private class LruAssetInfo
        {
            public string AssetBundleName;
            public string AssetName;

            public LruAssetInfo(string bundleName, string assetName)
            {
                AssetBundleName = bundleName;
                AssetName = assetName;
            }

            public bool IsBuildInAsset()
            {
                return string.IsNullOrEmpty(AssetBundleName);
            }
        }
        // key - name,value - resource
        private Dictionary<string, BuildInAssetInfo>                            m_AssetBuildInMap;
        // key - bundle name ,value -(key-asset name,value-resource)
        private Dictionary<string, Dictionary<string, InBundleAssetInfo>>       m_AssetInBundleMap;

        private LinkedList<LruAssetInfo>                                        m_LRUAssetList;
        private AssetbundleManager                                              m_AssetbundleMgr;

        #region public interface

        private void Awake()
        {
            Initialize("", "", false);
        }
        public void Initialize(string assetbundleDataPath,string manifestName, bool startWithbundle)
        {
            if (startWithbundle)
            {
                m_AssetbundleMgr = gameObject.AddComponent<AssetbundleManager>();
                m_AssetbundleMgr.Initialize(assetbundleDataPath, manifestName);
            }
            m_LRUAssetList = new LinkedList<LruAssetInfo>();
            m_AssetBuildInMap = new Dictionary<string, BuildInAssetInfo>();
            m_AssetInBundleMap = new Dictionary<string, Dictionary<string, InBundleAssetInfo>>();
        }
        public void Clear(bool isDeepth = false)
        {
            if (isDeepth)
            {
                m_LRUAssetList.Clear();
                m_AssetBuildInMap.Clear();
                m_AssetInBundleMap.Clear();
                if (null != m_AssetbundleMgr)
                {
                    m_AssetbundleMgr.Clear();
                }
                Resources.UnloadUnusedAssets();
            }
            else
            {
                ClearByLRU();
            }
        }
        #endregion

        #region build in asset

        public T LoadAsset<T>(string assetName) where T : Object
        {
            var tmpRes = LoadAssetInLoadedMap<T>(assetName);
            if (null != tmpRes)
            {
                return tmpRes;
            }
            tmpRes = Resources.Load<T>(assetName);
            if (tmpRes != null)
            {
                BuildInAssetInfo info = new BuildInAssetInfo();
                info.IsDestroyed = false;
                info.AssetName = assetName;
                info.AssetBody = tmpRes;

                if (m_AssetBuildInMap.ContainsKey(assetName))
                {
                    m_AssetBuildInMap[assetName] = info;
                }
                else
                {
                    m_AssetBuildInMap.Add(assetName, info);
                }
            }
            return tmpRes;
        }
        public void ReleaseAsset(string name)
        {
            BuildInAssetInfo res = null;
            if (m_AssetBuildInMap.TryGetValue(name, out res))
            {
                res.IsDestroyed = true;
            }
        }
        public void LoadAssetAsync<T>(string assetName, Action<string, T> callback) where T : Object
        {
            var tmpRes = LoadAssetInLoadedMap<T>(assetName);
            if (null != tmpRes)
            {
                callback(assetName, tmpRes);
            }
            else
            {
                Action<Object> tmpCallback = o => { callback(assetName, o as T); };
                StartCoroutine(LoadBuildinAssetAsync(assetName, tmpCallback));
            }
        }
        private T LoadAssetInLoadedMap<T>(string name) where T : Object
        {
            BuildInAssetInfo res = null;
            if (m_AssetBuildInMap.TryGetValue(name, out res))
            {
                // update refrence count
                OnAccess(string.Empty,name);
                return res as T;
            }
            return null;
        }
        private IEnumerator LoadBuildinAssetAsync(string assetName,Action<Object> callback)
        {
            var request = Resources.LoadAsync(assetName);
            yield return request;
            if (request.asset != null)
            {
                BuildInAssetInfo info = new BuildInAssetInfo();
                info.IsDestroyed = false;
                info.AssetName = assetName;
                info.AssetBody = request.asset;

                if (m_AssetBuildInMap.ContainsKey(assetName))
                {
                    m_AssetBuildInMap[assetName] = info;
                }
                else
                {
                    m_AssetBuildInMap.Add(assetName, info);
                }
            }
            callback(request.asset);
        }
        #endregion

        #region asset in asset bundle
        public T LoadAssetInLoadedMap<T>(string assetName, string bundleName) where T : Object
        {
            Dictionary<string,InBundleAssetInfo> res = null;
            if (m_AssetInBundleMap.TryGetValue(assetName, out res))
            {
                InBundleAssetInfo tmpRes = null;
                if (res.TryGetValue(assetName, out tmpRes))
                {
                    // update refrence count
                    OnAccess(bundleName, assetName);
                    ++ tmpRes.RefrenceCount;
                    m_AssetbundleMgr.ChangeAssetbundleRefrenceCount(tmpRes.AssetBundleName, true);
                    return tmpRes as T;
                }
            }
            return null;
        }
        public void ReleaseAsset(string assetName, string bundleName)
        {
            Dictionary<string, InBundleAssetInfo> res = null;
            if (m_AssetInBundleMap.TryGetValue(assetName, out res))
            {
                InBundleAssetInfo tmpRes = null;
                if (res.TryGetValue(assetName, out tmpRes))
                {
                    tmpRes.IsDestroyed = true;
                }
            }
        }
        public void LoadAssetAsync<T>(string assetName, string assetBundleName, Action<string, T> callback) where T : Object
        {
            LoadAssetAsync<T>(assetName, assetBundleName, (asset, resAssetName, resAssetBundleName) =>
            {
                callback(resAssetName, asset);
            });
        }
        public void LoadAssetAsync<T>(string assetName, string assetBundleName, Action<T, string, string> callback) where T : Object
        {
            T tmpRes = LoadAssetInLoadedMap<T>(assetName, assetBundleName);
            if (null != tmpRes)
            {
                callback(tmpRes, assetName, assetBundleName);
            }
            else
            {
                Action<AssetbundleInfo> bundleLoadCallBack = (bundle) =>
                {
                    if (bundle == null || bundle.GetBody() == null)
                    {
                        callback(null, assetName, assetBundleName);
                    }
                    else
                    {
                        Action<Object> assetLoadCallback = (tmpAsset) =>
                        {
                            callback(tmpAsset as T, assetName, assetBundleName);
                        };
                        StartCoroutine(LoadAssetFromBundleAsync(assetName, bundle, assetLoadCallback));
                    }
                };
                m_AssetbundleMgr.LoadAssetBundle(assetBundleName, bundleLoadCallBack);
            }
        }
        private IEnumerator LoadAssetFromBundleAsync(string assetName, AssetbundleInfo bundle, Action<Object> callback)
        {
            var request = bundle.GetBody().LoadAssetAsync(assetName);
            yield return request;
            if (request.asset != null)
            {
                if (!m_AssetInBundleMap.ContainsKey(bundle.GetName()))
                {
                    m_AssetInBundleMap.Add(bundle.GetName(), new Dictionary<string, InBundleAssetInfo>());
                }
                if (m_AssetInBundleMap[bundle.GetName()].ContainsKey(assetName))
                {
                    var info = m_AssetInBundleMap[bundle.GetName()][assetName];
                    info.IsDestroyed = false;
                    info.AssetName = assetName;
                    info.AssetBody = request.asset;
                    info.AssetBundleName = bundle.GetName();
                    ++ info.RefrenceCount;
                    m_AssetbundleMgr.ChangeAssetbundleRefrenceCount(bundle.GetName(), true);
                }
                else
                {
                    InBundleAssetInfo info = new InBundleAssetInfo();
                    info.IsDestroyed = false;
                    info.AssetName = assetName;
                    info.AssetBody = request.asset;
                    info.AssetBundleName = bundle.GetName();
                    info.RefrenceCount = 1;

                    m_AssetInBundleMap[bundle.GetName()].Add(assetName, info);
                }
            }
            callback(request.asset);
        }
        #endregion

        #region LRU asset list
        private void OnAccess(string bundleName,string assetName)
        {
            bool isAdded = false;
            var node = m_LRUAssetList.First;
            while (node != null && node.Value != null)
            {
                if (node.Value.AssetBundleName == bundleName && node.Value.AssetName == assetName)
                {
                    var value = node.Value;
                    m_LRUAssetList.Remove(node);
                    m_LRUAssetList.AddFirst(value);
                    isAdded = true;
                    break;
                }
                node = node.Next;
            }
            if (!isAdded)
            {
                m_LRUAssetList.AddFirst(new LruAssetInfo(bundleName, assetName));
            }
        }
        private void ClearByLRU(int leftCount = 0)
        {
            var node = m_LRUAssetList.Last;
            while (node != null && node.Value != null && m_LRUAssetList.Count > leftCount)
            {
                bool isRemove = false;
                if (node.Value.IsBuildInAsset())
                {
                    var res = m_AssetBuildInMap[node.Value.AssetName];
                    if (res.IsDestroyed)
                    {
                        // remove from list & destroy
                        m_AssetInBundleMap.Remove(node.Value.AssetName);
                        Resources.UnloadAsset(res.AssetBody);
                        isRemove = true;
                    }
                }
                else
                {
                    var res = m_AssetInBundleMap[node.Value.AssetBundleName][node.Value.AssetName]; if (res.IsDestroyed)
                    {
                        // remove from list & destroy
                        m_AssetInBundleMap[node.Value.AssetBundleName].Remove(node.Value.AssetName);
                        m_AssetbundleMgr.ChangeAssetbundleRefrenceCount(node.Value.AssetBundleName, false,res.RefrenceCount);
                        isRemove = true;
                    }
                }
                var tmp = node;
                node = node.Previous;
                if (isRemove)
                {
                    m_LRUAssetList.Remove(tmp);
                }
            }
            // release asset bundle
            if (null != m_AssetbundleMgr)
            {
                m_AssetbundleMgr.ReleaseBundle();
            }
        }
        #endregion
    }
}
