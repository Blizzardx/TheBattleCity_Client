using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// Resource manager
/// </summary>
/// 
//  0     - 3000  tank prefab
//  3000  - 6000  tank material
//  6000  - 10000 tank material
//  10000 - 20000 audio source
//  20000 - 30000 bullet prefab
//  30000 - 40000 bullet materials
//  40000 - 50000 bullet light prefab
//  50000 - 60000 bullet light materials
//  60000 - 70000 explosion prefab
//  70000 - 80000 explosion materials
//  80000 - 90000 explosion animation
//  90000 - 100000 scene prefab
//  100000 - 110000 wall prefab

public enum AssetsType
{
    Text        = 0,
    Texture     = 1,
    Prefab      = 2,
    Audio       = 3,
    Material    = 4,
    Animation_Picture = 5,
    Animation   = 6,
}
public class ConfigStruct
{
    public int          AssetsId;
    public string       ConfigFolderPath;
    public string       ConfigName;
    public AssetsType   ConfigType;
}
public class TypeToIdStruct
{
    public int Type;
    public int AssetsId;
}
public class DownloadResourceElement
{
    public int                          AssetsId;
    public ResourceRequest              Request;
    public bool                         IsFinishedDownload;
    public UnityEngine.Object           ResultObject;
}
public class ResourceManager : MonoBehaviour
{
    private Dictionary<int, UnityEngine.Object>                         m_AssetsStore                       ;
    private Dictionary<int, ConfigStruct>                               m_AssetsIndexStore                  ;
    private Dictionary<string,Dictionary<int, TypeToIdStruct>>          m_TypeToAssetsIndexStore            ;
    private Dictionary<int, List<Texture>>                              m_AnimationStore                    ;
    private Dictionary<string, DownloadResourceElement>                 m_DownLoadList                      ;
    private Dictionary<string, List<Action<UnityEngine.Object>>>        m_LoadAssetCallBackStore            ;
    private static ResourceManager                                      m_Instance;
    private readonly string                                             m_strBulletAssetsIndexStoreName             = "BulletIndex";
    private readonly string                                             m_strBulletTextureAssetsIndexStoreName      = "BulletTextureIndex";
    private readonly string                                             m_strBulletLightAssetsIndexStoreName        = "BulletLightIndex";
    private readonly string                                             m_strBulletLightTextureAssetsIndexStoreName = "BulletLightTextureIndex";
    private readonly string                                             m_strExplosionAssetsIndexStoreName          = "ExplosionIndex";
    private readonly string                                             m_strExplosionMaterialsIndexStoreName       = "ExplosionMaterialsIndex";
    private readonly string                                             m_strExplosionAnimationIndexStoreName       = "ExplosionAnimationIndex";
    private readonly string                                             m_strSceneAssetsIndexStoreName              = "SceneIndex";
    private readonly string                                             m_strWallAssetsIndexStoreName               = "WallIndex";
    private readonly string                                             m_strTankAssetsIndexStoreName               = "TankIndex";
    private readonly string                                             m_strTankMaterialAssetsIndexStoreName       = "TankMaterialIndex";
    private readonly string                                             m_strTankAnimationAssetsIndexStoreName      = "TankAnimationIndex";

    //system function------------------------
    private void ExcutationJasonStream_Config(string jsonValue)
    {
        try
        {
            Dictionary<string, object> Contect = (Dictionary<string, object>)Json.Deserialize(jsonValue);

            //load config index
            if (Contect.ContainsKey("ConfigIndex"))
            {
                List<object> store = (List<object>)(Contect["ConfigIndex"]);
                foreach (Dictionary<string, object> elem in store)
                {
                    ConfigStruct newElem        = new ConfigStruct();
                    newElem.ConfigFolderPath    = (string)(elem["ConfigFolderPath"]);
                    newElem.AssetsId            = Convert.ToInt32((string)((elem["AssetsId"])));
                    newElem.ConfigName          = (string)(elem["ConfigName"]);
                    newElem.ConfigType          = (AssetsType)(Convert.ToInt32((string)(elem["AssetsType"])));

                    //add to store
                    m_AssetsIndexStore.Add(newElem.AssetsId, newElem);
                }
            }
            else
            {
                //log error
                DebugLog.LogError("Error");
            }

            //load bullet index
            LoadTargetElement(m_strBulletAssetsIndexStoreName, Contect);

            //load bullet texture index
            LoadTargetElement(m_strBulletTextureAssetsIndexStoreName, Contect);

            //load bullet Light index
            LoadTargetElement(m_strBulletLightAssetsIndexStoreName, Contect);

            //load bullet Light texture index
            LoadTargetElement(m_strBulletLightTextureAssetsIndexStoreName, Contect);

            //load explosion prefab
            LoadTargetElement(m_strExplosionAssetsIndexStoreName, Contect);

            //load explosion materials
            LoadTargetElement(m_strExplosionMaterialsIndexStoreName, Contect);

            //load explosion animation
            LoadTargetElement(m_strExplosionAnimationIndexStoreName, Contect);

            //load scene
            LoadTargetElement(m_strSceneAssetsIndexStoreName, Contect);
            
            //load wall
            LoadTargetElement(m_strWallAssetsIndexStoreName, Contect);

            //load tank
            LoadTargetElement(m_strTankAssetsIndexStoreName, Contect);

            //load tank material
            LoadTargetElement(m_strTankMaterialAssetsIndexStoreName, Contect);

            //loas tank animation
            LoadTargetElement(m_strTankAnimationAssetsIndexStoreName, Contect);
        }
        catch
        {
            DebugLog.LogError("error");
        }
    }
    private void LoadTargetElement(string key, Dictionary<string, object> sourceStore )
    {
        if (sourceStore.ContainsKey(key))
        {
            Dictionary<int, TypeToIdStruct> targetStore = new Dictionary<int,TypeToIdStruct>();
            List<object> store = (List<object>)(sourceStore[key]);
            LoadConfigElement(targetStore, store);
            m_TypeToAssetsIndexStore.Add(key, targetStore);
        }
        else
        {
            //log error
            DebugLog.LogError("Error");
        }
    }
    private void LoadConfigElement(Dictionary<int,TypeToIdStruct> targetStore,List<object> sourceStore)
    {
        foreach (Dictionary<string, object> elem in sourceStore)
        {
            TypeToIdStruct newElem = new TypeToIdStruct();
            newElem.Type = Convert.ToInt32((string)(elem["Type"]));
            newElem.AssetsId = Convert.ToInt32((string)(elem["AssetsId"]));

            //add to store
            targetStore.Add(newElem.Type, newElem);
        }
    }
    static public  void LoadAnimation(int assetsId)
    {
        if (m_Instance.m_AnimationStore.ContainsKey(assetsId))
        {
            RelesaseAnimation(assetsId);
        }
        m_Instance.m_AnimationStore.Add(assetsId, new List<Texture>());

        try
        {
            string name = m_Instance.m_AssetsIndexStore[assetsId].ConfigFolderPath + "/" + m_Instance.m_AssetsIndexStore[assetsId].ConfigName + "/";
            for(int i=0;;++i)
            {
                Texture targetTexture = Resources.Load<Texture>(name + i.ToString());
                if( null != targetTexture)
                {
                    m_Instance.m_AnimationStore[assetsId].Add(targetTexture);
                }
                else
                {
                    break;
                }
            }            
        }
        catch
        {
            DebugLog.LogError("error: " + assetsId.ToString());
        }

    }
    static public  void RelesaseAnimation(int assetsId)
    {
        if (m_Instance.m_AnimationStore.ContainsKey(assetsId))
        {
            m_Instance.m_AnimationStore[assetsId] = null;
            m_Instance.m_AnimationStore.Remove(assetsId);
            System.GC.Collect();
        }
    }
    static public List<Texture> GetAnimationStore(int assetsId)
    {
        if (m_Instance.m_AnimationStore.ContainsKey(assetsId))
        {
            return m_Instance.m_AnimationStore[assetsId];
        }
        return null;
    }
    private IEnumerator StartLoadAssets(DownloadResourceElement element,Action<UnityEngine.Object> CallBack)
    {
        ConfigStruct elem = m_Instance.m_AssetsIndexStore[element.AssetsId];
        string path = elem.ConfigFolderPath + "/" + elem.ConfigName;
        AddToCallBackStore(path,CallBack);

        if (m_Instance.m_AssetsStore.ContainsKey(element.AssetsId))
        {
            element.IsFinishedDownload = true;
            element.ResultObject = m_Instance.m_AssetsStore[element.AssetsId];
            ExcuteDownloadStore(path);
            yield return null;
        }
        else if(IsAssetsOnLoading(path))
        {
            yield return null;
        }
        else
        {
            //start load
            element.IsFinishedDownload = false;
            element.ResultObject = null;
            element.Request = Resources.LoadAsync(path);
            
            //add to download list
            m_DownLoadList.Add(path,element);
            
            yield return element.Request;

            element.IsFinishedDownload = true;
            element.ResultObject = element.Request.asset;

            //add to assets store
            if (m_Instance.m_AssetsStore.ContainsKey(element.AssetsId))
            {
                DebugLog.LogError("Error code : " + element.AssetsId.ToString() + path);
            }
            else
            {
                m_Instance.m_AssetsStore.Add(element.AssetsId, element.Request.asset);
                ExcuteDownloadStore(path);
            }
        }
    }
    private void AddToCallBackStore(string Path,Action<UnityEngine.Object> CallBack)
    {
        if( null == CallBack)
        {
            DebugLog.LogError("Call back can't be null !!!");
            return;
        }
        if( !m_LoadAssetCallBackStore.ContainsKey(Path))
        {
            m_LoadAssetCallBackStore.Add(Path, new List<Action<UnityEngine.Object>>());
        }
        m_LoadAssetCallBackStore[Path].Add(CallBack);
    }
    private void DoCallBack(string Path,UnityEngine.Object result)
    {
        if( !m_LoadAssetCallBackStore.ContainsKey(Path) || result == null)
        {
            DebugLog.LogError("Can't find Path: " + Path + " in call back store !!! or result is null !!!");
            return;
        }
        for(int i=0;i<m_LoadAssetCallBackStore[Path].Count;++i)
        {
            m_LoadAssetCallBackStore[Path][i](result);
        }
        m_LoadAssetCallBackStore.Remove(Path);
    }
    private void ExcuteDownloadStore(string path)
    {
        if( m_DownLoadList.ContainsKey(path))
        {
            DoCallBack(path, m_DownLoadList[path].ResultObject);
            m_DownLoadList.Remove(path);
        }
        else
        {
            DebugLog.LogError("Error on excute download store");
        }
    }
    private bool IsAssetsOnLoading(string path)
    {
        return m_DownLoadList.ContainsKey(path);            
    }
    //public interface------------------------
    static public void Destructor()
    {
        m_Instance.m_AssetsStore = null;
        m_Instance.m_AssetsIndexStore = null;
        m_Instance.m_TypeToAssetsIndexStore = null;
        m_Instance.m_AnimationStore = null;
        m_Instance.m_DownLoadList = null;
    }
    public static void Initialize(string configPath)
    {
        m_Instance = GameObject.Find("GameManager").GetComponent<ResourceManager>();

        //set memory
        m_Instance.m_AssetsStore = new Dictionary<int, UnityEngine.Object>();
        m_Instance.m_AssetsIndexStore = new Dictionary<int, ConfigStruct>();
        m_Instance.m_TypeToAssetsIndexStore = new Dictionary<string, Dictionary<int, TypeToIdStruct>>();
        m_Instance.m_AnimationStore = new Dictionary<int, List<Texture>>();
        m_Instance.m_DownLoadList = new Dictionary<string, DownloadResourceElement>();
        m_Instance.m_LoadAssetCallBackStore = new Dictionary<string, List<Action<UnityEngine.Object>>>();
        //load local config file json
        m_Instance.ExcutationJasonStream_Config(Resources.Load<TextAsset>(configPath).text);
    }
    public static void LoadAssetsAsync(int assetsId,Action<UnityEngine.Object> callBack)
    {
        if (!m_Instance.m_AssetsIndexStore.ContainsKey(assetsId))
        {
            return;
        }
        if (m_Instance.m_AssetsStore.ContainsKey(assetsId))
        {
            callBack(m_Instance.m_AssetsStore[assetsId]);
        }
        else
        {
            DownloadResourceElement elem = new DownloadResourceElement();
            elem.AssetsId = assetsId;
            m_Instance.StartCoroutine(m_Instance.StartLoadAssets(elem, callBack));
        }
    }
    public static UnityEngine.Object LoadBuildInAsset(int assetsId)
    {
        if (!m_Instance.m_AssetsIndexStore.ContainsKey(assetsId))
        {
            return null;
        }
        string path = m_Instance.m_AssetsIndexStore[assetsId].ConfigFolderPath + "/" + m_Instance.m_AssetsIndexStore[assetsId].ConfigName;
        return Resources.Load<UnityEngine.Object>(path);
    }
    public static GameObject LoadBuildInUIPrefab(string name)
    {
        return Resources.Load<GameObject>("UI/Prefab/" + name);
    }
    public static void ReleaseAssets(int assetsId)
    {
        if( m_Instance.m_AssetsStore.ContainsKey(assetsId))
        {
            //m_Instance.m_AssetsStore[assetsId] = null;
            m_Instance.m_AssetsStore.Remove(assetsId);
        }
    }
    public static int GetBulletLightAssetsIdByType(int bulletType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletLightAssetsIndexStoreName].ContainsKey(bulletType))
        {
            Debug.LogError("can't find bullet id");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletLightAssetsIndexStoreName][bulletType].AssetsId;
    }
    public static int GetBulletLightTextureAssetsIdByBulletTextureType(int bulletTextureType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletLightTextureAssetsIndexStoreName].ContainsKey(bulletTextureType))
        {
            Debug.LogError("can't find bullet id");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletLightTextureAssetsIndexStoreName][bulletTextureType].AssetsId;
    }
    public static int GetBulletAssetsIdByType(int bulletType)
    {
        if( !m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletAssetsIndexStoreName].ContainsKey(bulletType))
        {
            Debug.LogError("can't find bullet id");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletAssetsIndexStoreName][bulletType].AssetsId;
    }
    public static int GetBulletTextureAssetsIdByBulletTextureType(int bulletTextureType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletTextureAssetsIndexStoreName].ContainsKey(bulletTextureType))
        {
            Debug.LogError("can't find bulletTextureType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strBulletTextureAssetsIndexStoreName][bulletTextureType].AssetsId;
    }
    public static int GetExplosionAssetsIdByType(int ExplosionType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strExplosionAssetsIndexStoreName].ContainsKey(ExplosionType))
        {
            Debug.LogError("can't find ExplosionType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strExplosionAssetsIndexStoreName][ExplosionType].AssetsId;
    }
    public static int GetExplosionMaterialAssetsIdByType(int ExplosionMateiralType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strExplosionMaterialsIndexStoreName].ContainsKey(ExplosionMateiralType))
        {
            Debug.LogError("can't find ExplosionMateiralType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strExplosionMaterialsIndexStoreName][ExplosionMateiralType].AssetsId;
    }
    public static int GetExplosionAnimationAssetsIdByType(int ExplosionAnimType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strExplosionAnimationIndexStoreName].ContainsKey(ExplosionAnimType))
        {
            Debug.LogError("can't find ExplosionAnimType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strExplosionAnimationIndexStoreName][ExplosionAnimType].AssetsId;
    }
    public static int GetSceneIdByType(int SceneType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strSceneAssetsIndexStoreName].ContainsKey(SceneType))
        {
            Debug.LogError("can't find SceneType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strSceneAssetsIndexStoreName][SceneType].AssetsId;
    }
    public static int GetWallIdByType(int WallType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strWallAssetsIndexStoreName].ContainsKey(WallType))
        {
            Debug.LogError("can't find WallType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strWallAssetsIndexStoreName][WallType].AssetsId;
    }
    public static int GetTankByType(int TankType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strTankAssetsIndexStoreName].ContainsKey(TankType))
        {
            Debug.LogError("can't find  TankType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strTankAssetsIndexStoreName][TankType].AssetsId;
    }
    public static int GetTankMaterialByType(int TankMaterialType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strTankMaterialAssetsIndexStoreName].ContainsKey(TankMaterialType))
        {
            Debug.LogError("can't find  TankMaterialType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strTankMaterialAssetsIndexStoreName][TankMaterialType].AssetsId;
    }
    public static int GetTankAnimationByType(int TankAnimationType)
    {
        if (!m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strTankAssetsIndexStoreName].ContainsKey(TankAnimationType))
        {
            Debug.LogError("can't find  TankAnimationType");
            return -1;
        }
        return m_Instance.m_TypeToAssetsIndexStore[m_Instance.m_strTankAssetsIndexStoreName][TankAnimationType].AssetsId;
    }
}
