using UnityEngine;
using System.Collections.Generic;

public class BulletManager :Singleton<BulletManager>
{
    private Dictionary<string, List<Bullet>> m_BulletStack; 
    private List<Bullet> m_BulletStore;

    public BulletManager()
    {
        m_BulletStack = new Dictionary<string, List<Bullet>>();
        m_BulletStore = new List<Bullet>();
    }
    public void CreateBullet(string name,Vector3 pos,Vector3 dir)
    {
        var obj =ResourceManager.Instance.LoadBuildInResource<GameObject>("Bullet/" + name, AssetType.Moudle);
        if (null == obj)
        {
            Debuger.LogWarning("Can't load bullet : " + name);
            return;
        }

        obj = GameObject.Instantiate(obj);

        Bullet elem = obj.GetComponent<Bullet>();
        if (null == elem)
        {
            Debuger.LogWarning("error on load bullet " + name);
            GameObject.Destroy(obj);
            return;
        }

        elem.transform.position = pos;
        elem.transform.forward = dir;
        elem.gameObject.name = name;
        elem.SetOnDestroy(OnBulletDestroy);
    }

    private void OnBulletDestroy(Bullet elem)
    {
        m_BulletStore.Remove(elem);
        m_BulletStack.Remove(elem.gameObject.name);
    }
}
