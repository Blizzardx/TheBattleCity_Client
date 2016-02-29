using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ExplosionManager 
{
    private static ExplosionManager m_Instance;
    public static ExplosionManager GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new ExplosionManager();
            }
            return m_Instance;
        }
    }
    private ExplosionManager() { }
    private List<ExplosionHandler>              m_ExplosionStore;
    private Dictionary<int, UnityEngine.Object> m_SourceObjectStore;
    private readonly int                        m_nStoreCountMax;

    public void Initialize()
    {
        m_ExplosionStore = new List<ExplosionHandler>();
        m_SourceObjectStore = new Dictionary<int, UnityEngine.Object>();

        List<int> loadingstore = new List<int>();
        loadingstore.Add(ResourceManager.GetExplosionAssetsIdByType(0));
        loadingstore.Add(ResourceManager.GetExplosionAssetsIdByType(1));
        loadingstore.Add(ResourceManager.GetExplosionMaterialAssetsIdByType(0));

        foreach(int id in loadingstore)
        {
            LoadAsset(id);
        }
        ResourceManager.LoadAnimation(ResourceManager.GetExplosionAnimationAssetsIdByType(0));
        ResourceManager.LoadAnimation(ResourceManager.GetExplosionAnimationAssetsIdByType(1));
    }
    public static void Destructor()
    {
        m_Instance.ClearData();
        m_Instance.m_ExplosionStore = null;
        m_Instance = null;
        System.GC.Collect();
    }
    private void LoadAsset(int assetsId)
    {
        m_SourceObjectStore.Add(assetsId,ResourceManager.LoadBuildInAsset(assetsId));
    }
    public void ClearData()
    {
        for (int i = 0; i < m_ExplosionStore.Count; ++i)
        {
            m_ExplosionStore[i].Destructor();
            m_ExplosionStore[i] = null;
        }
        m_ExplosionStore.Clear();

        foreach (KeyValuePair<int, UnityEngine.Object> elem in m_SourceObjectStore)
        {
            ResourceManager.ReleaseAssets(elem.Key);
        }
        m_SourceObjectStore.Clear();
        ResourceManager.RelesaseAnimation(ResourceManager.GetExplosionAnimationAssetsIdByType(0));
        ResourceManager.RelesaseAnimation(ResourceManager.GetExplosionAnimationAssetsIdByType(1));
    }
    public void ShowExplostion(Vector3 pos,int ExplosionType)
    {
        try
        {
            int AssetsId = ResourceManager.GetExplosionAssetsIdByType(ExplosionType);
            int MaterialsAssetsId = ResourceManager.GetExplosionMaterialAssetsIdByType(0);
            int animAssetsId = ResourceManager.GetExplosionAnimationAssetsIdByType(ExplosionType);
            
            GameObject tmp = GameObject.Instantiate(m_SourceObjectStore[AssetsId]) as GameObject;
            Material tmpTexture = m_SourceObjectStore[MaterialsAssetsId] as Material;
            ExplosionHandler newHandler = tmp.GetComponent<ExplosionHandler>();

            newHandler.Initialize(pos, tmpTexture, ResourceManager.GetAnimationStore(animAssetsId),OnDestroyCallback);
            m_ExplosionStore.Add(newHandler);

        }
        catch
        {
            DebugLog.LogError("error on load bullet or other component");
        }     
    }
    private void OnDestroyCallback(ExplosionHandler element)
    {
        GameObject.Destroy(element.gameObject);
    }
}
