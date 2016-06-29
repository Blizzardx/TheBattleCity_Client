using System;
using Common.Tool;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Object = UnityEngine.Object;

public class AssetBundleLoadInfo
{
    public string m_strBundleName;
    public string m_strAssetName;
}
public class AssetbundleManager : MonoSingleton<AssetbundleManager>
{
    private Queue<AssetBundleLoadInfo>              m_CurrentLoadingAssetList;
    private Queue<string>                           m_CurrentLoadingBundleList;
    private AssetBundleLoadInfo                     m_CurrentLoadingAsset;
    private Dictionary<string, AssetBundle>         m_LoadedBundleMap;
    private Dictionary<string, Object>              m_LoadedAssetMap;
    private Dictionary<string, Action<Object>>      m_AssetCallBackMap;
    private AssetBundle                             m_AssetbundeIndex;
    private AssetBundleManifest                     m_Manifest;
    private readonly string                         m_strDataPath           = Application.streamingAssetsPath + "/resource/";
    private readonly string                         m_strManifestDataPath   = Application.streamingAssetsPath + "/resource/Windows";
    //private LRU_K<string>                           m_LruMgr;
    //private Queue<string>                           m_RemovingBundleList;

    public AssetbundleManager()
    {
        m_CurrentLoadingAssetList = new Queue<AssetBundleLoadInfo>();
        m_CurrentLoadingBundleList = new Queue<string>();
        m_LoadedBundleMap = new Dictionary<string, AssetBundle>();
        m_AssetCallBackMap = new Dictionary<string, Action<Object>>();
        m_LoadedAssetMap = new Dictionary<string, Object>();
        //m_LruMgr = new LRU_K<string>(3, 20, OnRemovingBundle);
        //m_RemovingBundleList = new Queue<string>();
    }
    public void Clear()
    {
        m_CurrentLoadingAsset = null;
        m_CurrentLoadingAssetList.Clear();
        m_CurrentLoadingBundleList.Clear();
        m_AssetCallBackMap.Clear();
        m_LoadedAssetMap.Clear();
        foreach (var elem in m_LoadedBundleMap)
        {
            elem.Value.Unload(true);
        }
        m_LoadedBundleMap.Clear();
    }
    public void ReleaseAssetBundle(string bundleName)
    {
        if (!m_LoadedBundleMap.ContainsKey(bundleName))
        {
            return;
        }
        m_LoadedBundleMap[bundleName].Unload(true);
        m_LoadedBundleMap.Remove(bundleName);

        List<string> removingList = new List<string>();
        foreach (var elem in m_LoadedAssetMap)
        {
            if (elem.Key.StartsWith(bundleName))
            {
                removingList.Add(elem.Key);
            }
        }
        foreach (var elem in removingList)
        {
            m_LoadedAssetMap.Remove(elem);
        }
    }
    public void LoadAsset(string bundleName, string assetName,Action<Object> callBack)
    {
        // mark this bundle and asset
        //m_LruMgr.Access(bundleName);

        AssetBundleLoadInfo asset = new AssetBundleLoadInfo();
        asset.m_strBundleName = bundleName;
        asset.m_strAssetName = assetName;
        string realName = bundleName + assetName;

       
        if (m_LoadedAssetMap.ContainsKey(realName))
        {
            callBack(m_LoadedAssetMap[realName]);
        }
        else
        {
            if (m_AssetCallBackMap.ContainsKey(realName))
            {
                m_AssetCallBackMap[realName] += callBack;
            }
            else
            {
                m_AssetCallBackMap.Add(realName, callBack);
            }

            m_CurrentLoadingAssetList.Enqueue(asset);
        }
    }
    private void BeginLoadAssetBundle()
    {
        if (m_CurrentLoadingBundleList.Count > 0)
        {
            StartCoroutine(LoadBundleFromFile(m_CurrentLoadingBundleList.Dequeue()));
        }
        else
        {
            // trigger to load current asset
            StartCoroutine(LoadAssetFromBundle());
        }
    }
    private IEnumerator LoadAssetFromBundle()
    {
        if (null == m_CurrentLoadingAsset)
        {
            yield break;
        }
        string realName = m_CurrentLoadingAsset.m_strBundleName + m_CurrentLoadingAsset.m_strAssetName;
        if (!m_AssetCallBackMap.ContainsKey(realName))
        {
            // trigger to begin next download
            m_CurrentLoadingAsset = null;
            yield break;
        }
        if (!m_LoadedBundleMap.ContainsKey(m_CurrentLoadingAsset.m_strBundleName))
        {
            Debug.LogError("error on load asset bundle " + m_CurrentLoadingAsset.m_strBundleName);
            yield break;
        }
        AssetBundle bundle = m_LoadedBundleMap[m_CurrentLoadingAsset.m_strBundleName];

        Debug.Log("begin load asset " + m_CurrentLoadingAsset.m_strAssetName + " from bundle " +
                  m_CurrentLoadingAsset.m_strBundleName);
        var request = bundle.LoadAssetAsync(m_CurrentLoadingAsset.m_strAssetName);
        yield return request;
        if (null == request.asset)
        {
            Debug.LogError("Error on load asset " + m_CurrentLoadingAsset.m_strAssetName);
        }
        // broadcast resource
        if (m_LoadedAssetMap.ContainsKey(realName))
        {
            m_LoadedAssetMap[realName] = request.asset;
        }
        else
        {
            m_LoadedAssetMap.Add(realName, request.asset);
        }
        var callBack = m_AssetCallBackMap[realName];
        m_AssetCallBackMap.Remove(realName);
        callBack(request.asset);
        // trigger to begin next download
        m_CurrentLoadingAsset = null;
    }
    private IEnumerator LoadBundleFromFile(string name)
    {
        Debug.Log("begin load assetbundle " + name);
        if (null == m_AssetbundeIndex || null == m_Manifest)
        {
            string manifestPath = "file://" + m_strManifestDataPath;
            WWW manifestLoader = new WWW(manifestPath);
            yield return manifestLoader;

            if (manifestLoader.assetBundle == null)
            {
                Debug.LogError("can't load bundle from file ");
                manifestLoader.Dispose();
            }
            else
            {
                m_AssetbundeIndex = manifestLoader.assetBundle;
                m_Manifest = manifestLoader.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                if (null == m_Manifest)
                {
                    Debug.LogError("can't load manifest from bundle");
                    yield break;
                }
            }
        }
        if (m_LoadedBundleMap.ContainsKey(name))
        {
            var dependencies = m_Manifest.GetDirectDependencies(name);
            foreach (var path in dependencies)
            {
                m_CurrentLoadingBundleList.Enqueue(path);
            }
            BeginLoadAssetBundle();
            yield break;
        }
        string url = "file://" + m_strDataPath + name;
        WWW loader = new WWW(url);
        yield return loader;
        if (loader.assetBundle == null)
        {
            Debug.LogError("can't load bundle from file " + name);
            loader.Dispose();
        }
        else
        {
            loader.assetBundle.name = name;
            if (m_LoadedBundleMap.ContainsKey(name))
            {
                m_LoadedBundleMap[name] = loader.assetBundle;
            }
            else
            {
                m_LoadedBundleMap.Add(name, loader.assetBundle);
            }
            var dependencies = m_Manifest.GetAllDependencies(name);
            foreach (var path in dependencies)
            {
                m_CurrentLoadingBundleList.Enqueue(path);
            }
            BeginLoadAssetBundle();
        }
    }
    void Update()
    {
        if (m_CurrentLoadingAsset == null && m_CurrentLoadingAssetList.Count > 0)
        {
            m_CurrentLoadingAsset = m_CurrentLoadingAssetList.Dequeue();
            m_CurrentLoadingBundleList.Enqueue(m_CurrentLoadingAsset.m_strBundleName);
            BeginLoadAssetBundle();
        }
        //if (m_RemovingBundleList.Count > 0)
        //{
        //    // do release bundle
        //    int index = 3;
        //    while (index <= 0 || m_RemovingBundleList.Count <= 0)
        //    {
        //        ReleaseAssetBundle(m_RemovingBundleList.Dequeue());
        //        -- index;
        //    }
        //}
    }
    private void OnRemovingBundle(string key)
    {
        //m_RemovingBundleList.Enqueue(key);
    }
}
