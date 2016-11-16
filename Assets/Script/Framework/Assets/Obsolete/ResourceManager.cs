using System;
using System.Collections;
using Common.Tool;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Framework.Asset.Obsolete
{
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        private Dictionary<string, Object>                  m_LoadedAssetMap;
        private Dictionary<string, List<Action<string,Object>>>    m_LoadingAssetMap;
        private bool m_bIsLoading;
        private bool m_bIsMarkToClear;

        public ResourceManager()
        {
            m_LoadedAssetMap = new Dictionary<string, Object>();
            m_LoadingAssetMap = new Dictionary<string, List<Action<string, Object>>>();
        }
        public void Clear()
        {
            if (m_bIsLoading)
            {
                m_bIsMarkToClear = true;
            }
            else
            {
                DoClear();
            }
        }
        public T LoadBuildInResourceSync<T>(string assetName) where T: Object
        {
            Object res = null;
            m_LoadedAssetMap.TryGetValue(assetName, out res);
            if (null == res)
            {
                res = Resources.Load(assetName);
            }
            if (null == res)
            {
                Debug.LogError("can't load asset in build in asset " + assetName);
                return null;
            }
            return res as T;
        }
        public void LoadBuildInResourceAsync(string assetName, Action<string,Object> callback)
        {
            Object res = null;
            m_LoadedAssetMap.TryGetValue(assetName, out res);
            if (null != res)
            {
                callback(assetName,res);
                return;
            }
            List<Action<string,Object>> list = null;
            if (m_LoadingAssetMap.TryGetValue(assetName, out list))
            {
                list.Add(callback);
                return;
            }
            list = new List<Action<string,Object>>() {callback};
            m_LoadingAssetMap.Add(assetName, list);
            
            // begin load asset
            StartCoroutine(LoadAsset(assetName));
        }
        public void LoadAssetFromBundle(string assetName, Action<string, Object> callback,bool isAsync = true)
        {
            AssetbundleManager.Instance.LoadAsset("", assetName, (obj) =>
            {
                // do call back
                callback(assetName, obj);
            }, isAsync);
        }
        private IEnumerator LoadAsset(string assetName)
        {
            // mark loading status
            SetLoadingStatus(true);

            var request = Resources.LoadAsync(assetName);
            yield return request;

            if (request.asset == null)
            {
                Debug.LogError("can't load asset in build in asset " + assetName);
            }

            List<Action<string,Object>> list = null;
            if (!m_LoadingAssetMap.TryGetValue(assetName, out list))
            {
                Debug.LogError("error on Async load resource " + assetName);
            }
            else
            {
                // remove from loading list
                m_LoadingAssetMap.Remove(assetName);

                // add to loaded list
                if (m_LoadedAssetMap.ContainsKey(assetName))
                {
                    Debug.Log("update resource " + assetName);
                    m_LoadedAssetMap[assetName] = request.asset;
                }
                else
                {
                    m_LoadedAssetMap.Add(assetName, request.asset);
                }

                // do callback
                foreach (var elem in list)
                {
                    elem(assetName,request.asset);
                }
            }
            // mark loading status
            SetLoadingStatus(false);
        }
        private void DoClear()
        {
            m_LoadedAssetMap = new Dictionary<string, Object>();
            m_LoadingAssetMap = new Dictionary<string, List<Action<string, Object>>>();
        }
        private void SetLoadingStatus(bool status)
        {
            m_bIsLoading = status;
            if (!m_bIsLoading && m_bIsMarkToClear)
            {
                m_bIsMarkToClear = false;
                DoClear();
            }
        }
    }
}