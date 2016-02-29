using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BulletManager
{
    private static BulletManager                m_Instance;
    private List<BulletHandler>                 m_BulletStore;
    private Dictionary<int,UnityEngine.Object>  m_SourceObjectStore;
    private List<int>                           m_LoadingList;
    private Action                              m_LoadFinishedCallBack;

    public static BulletManager GetInstance
    {
        get
        {
            if( null == m_Instance)
            {
                m_Instance = new BulletManager();
            }
            return m_Instance;
        }
    }
    public static void Destructor()
    {
        m_Instance.ClearData();
        m_Instance.m_BulletStore = null;
        m_Instance = null;
        System.GC.Collect();
    }
    public void Initialize()
    {
        m_BulletStore = new List<BulletHandler>();
        m_SourceObjectStore = new Dictionary<int, UnityEngine.Object>();
    }
    public void ClearData()
    {
        for(int i=0;i<m_BulletStore.Count;++i)
        {
            m_BulletStore[i].Destructor();
            m_BulletStore[i] = null;
        }
        m_BulletStore.Clear();

        foreach(KeyValuePair<int,UnityEngine.Object> elem in m_SourceObjectStore)
        {
            ResourceManager.ReleaseAssets(elem.Key);
        }
        m_SourceObjectStore.Clear();

    }
    public void PreLoadAssets(List<int> assetsIdStore,Action FinishedCallBack)
    {
        m_LoadingList = assetsIdStore;
        m_LoadFinishedCallBack = FinishedCallBack;

        //pre load bullet at initialize of game
        for(int i=0;i<assetsIdStore.Count;++i)
        {
            PreLoadAssets(assetsIdStore[i]);
        }
    }
    private void PreLoadAssets(int assetsId)
    {
        ResourceManager.LoadAssetsAsync(assetsId, (UnityEngine.Object source) =>
        {
            m_SourceObjectStore.Add(assetsId, source);
            m_LoadingList.Remove(assetsId);
            if( m_LoadingList.Count == 0)
            {
                // do finished call back
                m_LoadFinishedCallBack();
            }
        });
    }
    public List<SubPkg_BulletTrajectoryElement> PushBullet(int BulletType, int BulletTextureType, int mark, Vector3 position, Vector3 speed, float hurtPercent)
    {
        try
        {
            List<SubPkg_BulletTrajectoryElement> res = MapManager.GetInstance.CaculateBullet(position, speed, TimeManager.GetDuringTimeStartFire(), 2);
            if (null != res)
            {
                int bulletAssetsId = ResourceManager.GetBulletAssetsIdByType(BulletType);
                int bulletMaterialsAssetsId = ResourceManager.GetBulletTextureAssetsIdByBulletTextureType(BulletTextureType);
                GameObject tmp = GameObject.Instantiate(m_SourceObjectStore[bulletAssetsId]) as GameObject;
                Material tmpTexture = m_SourceObjectStore[bulletMaterialsAssetsId] as Material;
                BulletHandler newHandler = new BulletHandler();
                BulletRender newRender = tmp.GetComponent<BulletRender>();


                newHandler.Intialize(position, speed, BulletType, (int)(hurtPercent), mark, newRender, tmpTexture, res);
                m_BulletStore.Add(newHandler);
            }
            return res;
        }
        catch
        {
            DebugLog.LogError("error on load bullet or other component");
        }
        return null;
    }
    public void CreateBulletLight(int BulletType,int BulletTextureType,Vector3 position)
    {
        int bulletLightAssetsId             = ResourceManager.GetBulletLightAssetsIdByType(BulletType);
        int bulletLightMaterialsAssetsId    = ResourceManager.GetBulletLightTextureAssetsIdByBulletTextureType(BulletTextureType);

        GameObject tmp          = GameObject.Instantiate(m_SourceObjectStore[bulletLightAssetsId]) as GameObject;
        tmp.transform.position  = position;
        Material tmpTexture     = m_SourceObjectStore[bulletLightMaterialsAssetsId] as Material;
        FireLightAnimation fireLightAnim = tmp.GetComponent<FireLightAnimation>();
        fireLightAnim.Initialize(tmpTexture);
    }
    public void FixedUpdate()
    {
        for(int i=0;i<m_BulletStore.Count;++i)
        {
            m_BulletStore[i].FixedUpdate();
            if( m_BulletStore[i].IsDestroyed())
            {
                //trigger explosion animation
                ExplosionManager.GetInstance.ShowExplostion(m_BulletStore[i].GetPosition(), 0);

                //play sound
                AudioManager.GetInstance.PlayEffectSound(EffectAudioDefine.AudioEffectSoundType.Explosion, m_BulletStore[i].GetPosition());

                //release from bullet using store
                GameObject tmpElement = m_BulletStore[i].GetBulletObjet();
                m_BulletStore.Remove(m_BulletStore[i]);

                //destroy bullet
                BulletFactory.CollectionBullet(tmpElement);
            }
        }
    }
}
