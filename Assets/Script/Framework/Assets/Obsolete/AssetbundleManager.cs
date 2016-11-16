using System;
using System.Collections;
using Common.Component;
using Common.Tool;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework.Asset.Obsolete
{
    public class AssetInfo
    {
        public string assetName;
        public string bundleName;
        public bool isAsync;
        public List<Action<Object>> callBack;
    }
    public class AssetbundleInfo
    {
        public string mainBundleName;
        public HashSet<string> dependecList;
        public List<Action<string,AssetBundle>> callBack;
    }
    public class LoadAssetParam
    {
        public string bundleName;
        public string assetName;
        public Action<Object> callBack;
        public bool isAsync;
    }
    public class LoadBundleParam
    {
        public string bundleName;
        public Action<string, AssetBundle> callBack;
    }
    public class AssetbundleManager : MonoSingleton<AssetbundleManager>
    {
        private Dictionary<string, AssetBundle>             m_LoadedBundleMap;
        private HashSet<string>                             m_LoadingAllAssetbundleMap;
        private Dictionary<string, AssetbundleInfo>         m_LoadingAssetbundleMap; 
        private Dictionary<string, AssetInfo>               m_LoadingAssetMap;
        private Dictionary<string, Object>                  m_LoadedAssetMap; 
        private AssetBundleManifest                         m_Manifest;
        private readonly string                             m_strDataPath           = Application.persistentDataPath + "/Download/resource/";
        private readonly string                             m_strManifestDataPath   = Application.persistentDataPath + "/Download/resource/Windows";
        private List<LoadAssetParam>                        m_LoadAssetList;
        private List<LoadBundleParam>                       m_LoadBundleList;
        private bool                                        m_bIsLoadingManifest;
        private bool                                        m_bIsLoadingAsset;
        private bool m_bIsMarkToClear; 

        #region public interface
        public AssetbundleManager()
        {
            m_LoadedBundleMap = new Dictionary<string, AssetBundle>();
            m_LoadingAllAssetbundleMap = new HashSet<string>();
            m_LoadingAssetbundleMap = new Dictionary<string, AssetbundleInfo>();
            m_LoadingAssetMap = new Dictionary<string, AssetInfo>();
            m_LoadAssetList = new List<LoadAssetParam>();
            m_LoadBundleList = new List<LoadBundleParam>();
            m_LoadedAssetMap = new Dictionary<string, Object>();
            m_bIsLoadingManifest = false;
            m_bIsLoadingAsset = false;
            m_bIsMarkToClear = false;
        }
        public void LoadAsset(string bundleName, string assetName, Action<Object> callBack,bool isAsyc)
        {
            if (CheckBusyLoadAsset(bundleName, assetName, callBack, isAsyc))
            {
                return;
            }
            Debug.Log("begin load asset " + bundleName + " : " + assetName);

            string fullName = bundleName + assetName;
            Object res = null;
            m_LoadedAssetMap.TryGetValue(fullName, out res);
            if (null != res)
            {
                callBack(res);
                return;
            }

            AssetInfo info = null;
            if (m_LoadingAssetMap.TryGetValue(fullName, out info))
            {
                info.isAsync = isAsyc;
                if(!info.callBack.Contains(callBack))
                {
                    info.callBack.Add(callBack);
                }
            }
            else
            {
                info = new AssetInfo();
                info.assetName = assetName;
                info.bundleName = bundleName;
                info.isAsync = isAsyc;
                info.callBack = new List<Action<Object>>(){callBack};
                m_LoadingAssetMap.Add(fullName,info);
            }

            // load asset bundle
            LoadAssetBundle(bundleName, OnAssetbundleLoaded);
        }
        public void LoadAssetBundle(string bundleName,Action<string,AssetBundle> callBack)
        {
            if (CheckBusyLoadBundle(bundleName, callBack))
            {
                return;
            }
            Debug.Log("begin load bundle " + bundleName);
            AssetbundleInfo bundleInfo = null;
            if (m_LoadingAssetbundleMap.TryGetValue(bundleName,out bundleInfo))
            {
                if (!bundleInfo.callBack.Contains(callBack))
                {
                    bundleInfo.callBack.Add(callBack);
                }
                return;
            }

            var bundleList = m_Manifest.GetAllDependencies(bundleName);
            string[] realBundleList = new string[bundleList.Length + 1];
            Array.Copy(bundleList, realBundleList, bundleList.Length);
            realBundleList[bundleList.Length] = bundleName;

            bundleInfo = new AssetbundleInfo();
            bundleInfo.dependecList = new HashSet<string>();
            bundleInfo.mainBundleName = bundleName;
            bundleInfo.callBack = new List<Action<string,AssetBundle>>(){callBack};

            foreach (var name in realBundleList)
            {
                if (!m_LoadedBundleMap.ContainsKey(name))
                {
                    bundleInfo.dependecList.Add(name);
                }
            }
            if (bundleInfo.dependecList.Count == 0)
            {
                AssetBundle bundle = m_LoadedBundleMap[bundleName];
                foreach (var callback in bundleInfo.callBack)
                {
                    callback(bundleName,bundle);
                }
                return;
            }
            m_LoadingAssetbundleMap.Add(bundleName, bundleInfo);

            foreach (var name in bundleInfo.dependecList)
            {
                if (!m_LoadingAllAssetbundleMap.Contains(name))
                {
                    m_LoadingAllAssetbundleMap.Add(name);
                    StartCoroutine(LoadbundlesFromFile(name));
                }
            }
        }
        public void Clear()
        {
            if (!m_bIsLoadingAsset)
            {
                DoClear();
            }
            else
            {
                //
                m_bIsMarkToClear = true;
            }
        }
        public Object GetAsset(string bundleName, string assetName)
        {
            Object obj = null;
            m_LoadedAssetMap.TryGetValue(bundleName + assetName, out obj);
            return obj;
        }
        #endregion

        #region system function
        private void OnAssetbundleLoaded(string bundlename,AssetBundle bundle)
        {
            Debug.Log("On loaded bundle " + bundlename);
            List<AssetInfo> list = new List<AssetInfo>();

            foreach (var elem in m_LoadingAssetMap)
            {
                if (elem.Value.bundleName == bundlename)
                {
                    list.Add(elem.Value);
                }
            }
            foreach (var asset in list)
            {
                if (string.IsNullOrEmpty(asset.assetName))
                {
                    m_LoadingAssetMap.Remove(asset.bundleName + asset.assetName);
                    for (int i = 0; i < asset.callBack.Count; ++i)
                    {
                        asset.callBack[i](null);
                    }
                    continue;
                }
                if (asset.isAsync)
                {
                    StartCoroutine(LoadAssetFromBundle_Async(bundle, asset));
                }
                else
                {
                    LoadAssetFromBundle_Sync(bundle, asset);
                }
            }
        }
        private bool CheckBusyLoadAsset(string bundleName, string assetName, Action<Object> callBack, bool isAsync)
        {
            if (m_Manifest != null)
            {
                return false;
            }
            BeginLoadManifest();
            LoadAssetParam elem = new LoadAssetParam();
            elem.bundleName = bundleName;
            elem.assetName = assetName;
            elem.callBack = callBack;
            elem.isAsync = isAsync;
            m_LoadAssetList.Add(elem);
            return true;
        }
        private bool CheckBusyLoadBundle(string bundleName, Action<string, AssetBundle> callBack)
        {
            if (m_Manifest != null)
            {
                return false;
            }
            BeginLoadManifest();
            LoadBundleParam elem = new LoadBundleParam();
            elem.bundleName = bundleName;
            elem.callBack = callBack;
            m_LoadBundleList.Add(elem);
            return true;
        }
        private void OnManifestLoaded()
        {
            foreach (var elem in m_LoadAssetList)
            {
                LoadAsset(elem.bundleName, elem.assetName, elem.callBack, elem.isAsync);
            }
            foreach (var elem in m_LoadBundleList)
            {
                LoadAssetBundle(elem.bundleName, elem.callBack);
            }
            m_LoadAssetList.Clear();
            m_LoadBundleList.Clear();
        }
        private void BeginLoadManifest()
        {
            if (m_bIsLoadingManifest)
            {
                return;
            }
            m_bIsLoadingManifest = true;
            StartCoroutine(LoadManifest());
        }
        private void AddToLoadedAssetMap(string bundlename, string assetName, Object res)
        {
            string fullName = bundlename + assetName;
            if (m_LoadedAssetMap.ContainsKey(fullName))
            {
                m_LoadedAssetMap[fullName] = res;
            }
            else
            {
                m_LoadedAssetMap.Add(fullName, res);
            }
        }
        private IEnumerator LoadbundlesFromFile(string bundle)
        {
            // mark loading status
            SetLoadingStatus(true);

            string url = "file:///" + m_strDataPath + bundle;
            Debug.Log(url);
            WWW loader = new WWW(url);
            yield return loader;
            if (loader.assetBundle == null)
            {
                Debug.LogError("can't load bundle from file " + bundle);
                loader.Dispose();
                // remark loading status
                SetLoadingStatus(false);
                yield break;
            }

            loader.assetBundle.name = bundle;
            Debug.Log("loaded " + bundle);

            // remove from loading list
            m_LoadingAllAssetbundleMap.Remove(bundle);

            // add to loaded list
            m_LoadedBundleMap.Add(bundle,loader.assetBundle);

            // check is bundle ready
            List<AssetbundleInfo> readyBundleList = new List<AssetbundleInfo>();
            foreach (var elem in m_LoadingAssetbundleMap)
            {
                if (elem.Value.dependecList.Contains(bundle))
                {
                    elem.Value.dependecList.Remove(bundle);
                    if (elem.Value.dependecList.Count == 0)
                    {
                        //add to ready list
                        readyBundleList.Add(elem.Value);
                    }
                }
            }
            foreach (var readyBundle in readyBundleList)
            {
                m_LoadingAssetbundleMap.Remove(readyBundle.mainBundleName);
                foreach (var callback in readyBundle.callBack)
                {
                    callback(readyBundle.mainBundleName,m_LoadedBundleMap[readyBundle.mainBundleName]);
                }
            }

            // remark loading status
            SetLoadingStatus(false);
        }
        private IEnumerator LoadAssetFromBundle_Async(AssetBundle bundle, AssetInfo info)
        {
            // mark loading status
            SetLoadingStatus(true);

            var request = bundle.LoadAssetAsync(info.assetName);
            yield return request;
            m_LoadingAssetMap.Remove(info.bundleName + info.assetName);

            if (null == request.asset)
            {
                Debug.LogError("Error on load asset " + info.assetName + " in bundle " + info.bundleName);
                //yield break;
            }
            else
            {
                // add to cashe
                AddToLoadedAssetMap(info.bundleName, info.assetName, request.asset);
            }

            for (int i = 0; i < info.callBack.Count; ++i)
            {
                info.callBack[i](request.asset);
            }
            
            // remark loading status
            SetLoadingStatus(false);
        }
        private void LoadAssetFromBundle_Sync(AssetBundle bundle, AssetInfo info)
        {
            var obj = bundle.LoadAsset(info.assetName);

            m_LoadingAssetMap.Remove(info.bundleName + info.assetName);

            if (null == obj)
            {
                Debug.LogError("Error on load asset " + info.assetName + " in bundle " + info.bundleName);
            }
            else
            {
                // add to cashe
                AddToLoadedAssetMap(info.bundleName, info.assetName, obj);
            }

            foreach (var callback in info.callBack)
            {
                callback(obj);
            }
        }
        private IEnumerator LoadManifest()
        {
            // mark loading status
            SetLoadingStatus(true);

            string manifestPath = "file:///" + m_strManifestDataPath;
            Debug.Log(manifestPath);
            WWW manifestLoader = new WWW(manifestPath);
            yield return manifestLoader;

            // remark loading status
            SetLoadingStatus(false);

            if (manifestLoader.assetBundle == null)
            {
                Debug.LogError("can't load bundle from file ");
                manifestLoader.Dispose();
                yield break;
            }

            m_Manifest = manifestLoader.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            if (null == m_Manifest)
            {
                Debug.LogError("can't load manifest from bundle");
                yield break;
            }
            // unload
            manifestLoader.assetBundle.Unload(false);

            OnManifestLoaded();
        }
        private void SetLoadingStatus(bool status)
        {
            m_bIsLoadingAsset = status;

            if (!m_bIsLoadingAsset && m_bIsMarkToClear)
            {
                m_bIsMarkToClear = false;
                DoClear();
            }
        }
        private void DoClear()
        {
            foreach (var elem in m_LoadedBundleMap)
            {
                elem.Value.Unload(false);
            }
            m_LoadedBundleMap.Clear();
            m_LoadingAllAssetbundleMap.Clear();
            m_LoadingAssetbundleMap.Clear();
            m_LoadingAssetMap.Clear();
            m_LoadedAssetMap.Clear();
            m_bIsLoadingAsset = false;
            m_bIsMarkToClear = false;
        }
        #endregion
    }
}