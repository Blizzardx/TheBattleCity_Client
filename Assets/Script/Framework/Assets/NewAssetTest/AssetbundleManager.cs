using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Framework.Assets.NewAssetTest
{
    internal class AssetbundleManager:MonoBehaviour
    {
        internal class AssetbundleTask
        {
            public AssetbundleTask(string abname, Action<AssetbundleInfo> callback)
            {
                bundleName = abname;
                doneCallback = callback;
            }

            public AssetbundleTask()
            {
                
            }
            public string bundleName;
            public Action<AssetbundleInfo> doneCallback;
        }

        private AssetBundleManifest                                 m_Manifest;
        private HashSet<string>                                     m_LoadingBundleMap;
        private Dictionary<string, AssetbundleInfo>                 m_LoadedBundleMap;
        private Dictionary<string, List<Action<AssetbundleInfo>>>   m_CallbackMap;
        private Queue<AssetbundleTask>                              m_TaskList;
        private bool                                                m_bIsLoadingManifest;
        private bool                                                m_bNeedClear;
        private string                                              m_strDataPath;
        private string                                              m_strManifestDataPath;

        #region public interface
        public void Initialize(string datapath,string manifestName)
        {
            m_LoadedBundleMap = new Dictionary<string, AssetbundleInfo>();
            m_LoadedBundleMap = new Dictionary<string, AssetbundleInfo>();
            m_CallbackMap = new Dictionary<string, List<Action<AssetbundleInfo>>>();
            m_TaskList = new Queue<AssetbundleTask>();
            m_bNeedClear = false;
            m_bIsLoadingManifest = true;
            m_strDataPath = datapath;
            m_strManifestDataPath = datapath + "/" + manifestName;

            // begin load manifest
            StartCoroutine(LoadManifest());
        }
        public void LoadAssetBundle(string assetbundleName,Action<AssetbundleInfo> doneCallback)
        {
            if (m_bIsLoadingManifest)
            {
                m_TaskList.Enqueue(new AssetbundleTask(assetbundleName,doneCallback));
                return;
            }
            HashSet<string> realLoadingList = new HashSet<string>();

            var bundleList = m_Manifest.GetAllDependencies(assetbundleName);
            string[] realBundleList = new string[bundleList.Length + 1];
            Array.Copy(bundleList, realBundleList, bundleList.Length);
            realBundleList[bundleList.Length] = assetbundleName;

            // check is in loaded list
            for (int i = 0; i < realBundleList.Length; ++i)
            {
                string name = realBundleList[i];
                // check is in loaded list
                if (!m_LoadedBundleMap.ContainsKey(name))
                {
                    // check is in loading list
                    if (!m_LoadingBundleMap.Contains(name))
                    {
                        // add to loading list
                        if (!realLoadingList.Contains(name))
                        {
                            realLoadingList.Add(name);
                        }
                        else
                        {
                            // error
                            Debug.LogError("Error on load bundle " + name);
                        }
                    }
                    else
                    {
                        // add done call back
                        if (m_CallbackMap.ContainsKey(name))
                        {
                            m_CallbackMap.Add(name,new List<Action<AssetbundleInfo>>());
                        }
                        m_CallbackMap[name].Add(doneCallback);
                    }
                }
            }

            if (realLoadingList.Count == 0)
            {
                doneCallback(m_LoadedBundleMap[assetbundleName]);
            }
            else
            {
                // add done call back
                m_CallbackMap.Add(assetbundleName, new List<Action<AssetbundleInfo>>() { doneCallback });

                // add to loading list
                foreach (var name in realLoadingList)
                {
                    m_LoadingBundleMap.Add(name);
                    StartCoroutine(BeginLoadAssetbundle(name));
                }
            }
        }
        public AssetbundleInfo GetAssetbundle(string assetbundleName)
        {
            AssetbundleInfo res = null;
            m_LoadedBundleMap.TryGetValue(assetbundleName, out res);
            return res;
        }
        public void ChangeAssetbundleRefrenceCount(string bundleName,bool isAdd)
        {
            // check is bundle is loaded
            AssetbundleInfo info = null;
            m_LoadedBundleMap.TryGetValue(bundleName, out info);
            if (null == info)
            {
                return;
            }
            int deltaValue = isAdd ? 1 : -1;

            // change refreance count
            info.SetRefrenceCount(info.GetRefrenceCount()+ deltaValue);

            // get all dep
            var list = info.GetAllDepBundles();
            for (int i = 0; i < list.Length; ++i)
            {
                string tmpName = list[i];
                AssetbundleInfo tmpInfo = null;
                m_LoadedBundleMap.TryGetValue(tmpName, out tmpInfo);
                if (null != tmpInfo)
                {
                    // change refreance count
                    tmpInfo.SetRefrenceCount(tmpInfo.GetRefrenceCount() + deltaValue);
                }
            }
        }
        public void Clear()
        {
            if (m_bIsLoadingManifest)
            {
                // do nothing
                return;
            }
            if (m_LoadingBundleMap.Count < 0)
            {
                DoClear();
            }
            else
            {
                // trigger to clear
                m_bNeedClear = true;
            }
        }
        public void ReleaseBundle(int leftCount = 0)
        {
            if (leftCount <= m_LoadedBundleMap.Count)
            {
                // nothing to unload
                return;
            }

            int removeCount = m_LoadedBundleMap.Count - leftCount;
            
            List<AssetbundleInfo> removeList = new List<AssetbundleInfo>();
            foreach (var elem in m_LoadedBundleMap)
            {
                if (elem.Value.GetRefrenceCount() == 0)
                {
                    removeList.Add(elem.Value);
                }
                if (removeList.Count >= removeCount)
                {
                    break;
                }
            }
            // do unload & remove
            for (int i = 0; i < removeList.Count; ++i)
            {
                var bundle = removeList[i];
                // remove from list
                m_LoadedBundleMap.Remove(bundle.GetName());
                // unload
                bundle.GetBody().Unload(true);
            }
        }
        #endregion

        #region system function
        private IEnumerator BeginLoadAssetbundle(string bundle)
        {
            string url = "file:///" + m_strDataPath + bundle;
            Debug.Log(url);
            WWW loader = new WWW(url);
            yield return loader;
            if (loader.assetBundle == null)
            {
                Debug.LogError("can't load bundle from file " + bundle);
                loader.Dispose();
                yield break;
            }

            loader.assetBundle.name = bundle;
            Debug.Log("loaded " + bundle);

            AssetbundleInfo bundleInstance = new AssetbundleInfo(bundle,loader.assetBundle, m_Manifest.GetAllDependencies(bundle));

            OnBundleLoaded(bundleInstance);
        }
        private IEnumerator LoadManifest()
        {
            string manifestPath = "file:///" + m_strManifestDataPath;
            Debug.Log(manifestPath);
            WWW manifestLoader = new WWW(manifestPath);
            yield return manifestLoader;
            
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
        private bool CheckBundleComplate(AssetbundleInfo bundleInfo)
        {
            var list = bundleInfo.GetAllDepBundles();
            if (null == list || list.Length == 0)
            {
                return true;
            }
            for (int i = 0; i < list.Length; ++i)
            {
                if (!m_LoadedBundleMap.ContainsKey(list[i]))
                {
                    return false;
                }
            }
            return true;
        }
        private void OnBundleLoaded(AssetbundleInfo bundleInfo)
        {
            // remove from loading list
            m_LoadingBundleMap.Remove(bundleInfo.GetName());

            // add to loaded list
            m_LoadedBundleMap.Add(bundleInfo.GetName(), bundleInfo);

            // update refrence count - add refrence count
            ChangeAssetbundleRefrenceCount(bundleInfo.GetName(), true);

            // check is in callback list
            List<Action<AssetbundleInfo>> callbackList = null;
            m_CallbackMap.TryGetValue(bundleInfo.GetName(), out callbackList);

            if (null != callbackList && callbackList.Count > 0)
            {
                // check complate
                if (CheckBundleComplate(bundleInfo))
                {
                    // remove call 
                    m_CallbackMap.Remove(bundleInfo.GetName());

                    // get all callback & do call back
                    for (int i = 0; i < callbackList.Count; ++i)
                    {
                        // do call back
                        callbackList[i](bundleInfo);
                    }
                }
            }

            if (m_bNeedClear)
            {
                // check can clear
                Clear();
            }
        }
        private void OnManifestLoaded()
        {
            m_bIsLoadingManifest = false;
            AssetbundleTask task = m_TaskList.Dequeue();
            while (null != task)
            {
                LoadAssetBundle(task.bundleName, task.doneCallback);
                task = m_TaskList.Dequeue();
            }
        }
        private void DoClear()
        {
            m_bNeedClear = false;
            foreach (var elem in m_LoadedBundleMap)
            {
                elem.Value.GetBody().Unload(true);
            }
            m_LoadingBundleMap.Clear();
            m_LoadedBundleMap.Clear();
            m_CallbackMap.Clear();
            m_TaskList.Clear();
        }
        #endregion
    }
}
