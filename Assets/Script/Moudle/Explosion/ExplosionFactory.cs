using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionFactory 
{
    private static List<GameObject> m_UsingExplosionPool = new List<GameObject>();
    private static Queue<GameObject> m_BackupExplosionPool = new Queue<GameObject>();

    private readonly static int m_nMaxExplosionLimit = 10;
    
    public static GameObject GetExplosionByType(int type)
    {
        GameObject newElem = null;
        if (m_BackupExplosionPool.Count > 0)
        {
            newElem = m_BackupExplosionPool.Dequeue();
            newElem.SetActive(true);
        }
        else
        {
            //load Explosion prefab from resource
           // newElem = ResourceManager.LoadAssets(ResourceManager.GetExplosionAssetsIdByType(type)) as GameObject;
        }
        return newElem;
    }
    public static Material CreateExplosionMaterialByType(int type)
    {
        //load Explosion material from resource
        Material newElem = null;// ResourceManager.LoadAssets(ResourceManager.GetExplosionAssetsIdByType(type)) as Material;
        return new Material(newElem);
    }
    public static void CollectionExplosion(GameObject target)
    {
        m_UsingExplosionPool.Remove(target);
        if (m_UsingExplosionPool.Count > m_nMaxExplosionLimit)
        {
            GameObject.Destroy(target);
        }
        else
        {
            target.SetActive(false);
            m_BackupExplosionPool.Enqueue(target);
        }
    }
}
