using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BulletFactory 
{
    private static List<GameObject> m_UsingBulletPool   = new List<GameObject>();
    private static Queue<GameObject> m_BackupBulletPool = new Queue<GameObject>();

    private readonly static int m_nMaxBulletLimit = 20;

    public static void GetBulletByType(int type,Action<UnityEngine.Object> CallBack)
    {
        GameObject newElem = null;
        if( m_BackupBulletPool.Count > 0)
        {
            newElem = m_BackupBulletPool.Dequeue();
            newElem.SetActive(true);
            CallBack(newElem);
        }
        else
        {
            //load bullet prefab from resource
            ResourceManager.LoadAssetsAsync(ResourceManager.GetBulletAssetsIdByType(type), CallBack);
        }
    }
    public static void GetBulletLightType(int type, Action<UnityEngine.Object> CallBack)
    {
        ResourceManager.LoadAssetsAsync(ResourceManager.GetBulletLightAssetsIdByType(type), CallBack);
    }
    public static void GetBulletMaterialByType(int type, Action<UnityEngine.Object> CallBack)
    {
        ResourceManager.LoadAssetsAsync(ResourceManager.GetBulletTextureAssetsIdByBulletTextureType(type), CallBack);
    }
    public static void GetBulletLightMaterialsByType(int type, Action<UnityEngine.Object> CallBack)
    {
        ResourceManager.LoadAssetsAsync(ResourceManager.GetBulletLightTextureAssetsIdByBulletTextureType(type), CallBack);
    }
    public static void CollectionBullet(GameObject target)
    {
        m_UsingBulletPool.Remove(target);
        if (m_UsingBulletPool.Count + m_BackupBulletPool.Count+1 > m_nMaxBulletLimit)
        {
            GameObject.Destroy(target);
        }
        else
        {
            target.SetActive(false);
            m_BackupBulletPool.Enqueue(target);            
        }
    }
}
