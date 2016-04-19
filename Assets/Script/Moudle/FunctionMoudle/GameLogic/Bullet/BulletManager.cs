using UnityEngine;
using System.Collections.Generic;

public class BulletManager :Singleton<BulletManager>
{
    private const int m_iBulletColletionMaxCount = 10;
    private Dictionary<string, List<Bullet>> m_BulletStack; 
    private List<Bullet> m_BulletStore;

    public BulletManager()
    {
        m_BulletStack = new Dictionary<string, List<Bullet>>();
        m_BulletStore = new List<Bullet>();
    }
    public void CreateBullet(int playerUid,string name,Vector3 pos,Vector3 dir)
    {
        var obj = TryGetBulletFromStack(name);

        if (null == obj)
        {
            obj =ResourceManager.Instance.LoadBuildInResource<GameObject>("Bullet/" + name, AssetType.Moudle);

            if (null == obj)
            {
                Debuger.LogWarning("Can't load bullet : " + name);
                return;
            }

            obj = GameObject.Instantiate(obj);
        }

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
        elem.SetOnCollection(OnBulletCollection);
        elem.SetFirePlayerUId(playerUid);
        elem.gameObject.SetActive(true);
        elem.Reset();
    }
    private void OnBulletDestroy(Bullet elem)
    {
        List<Bullet> list = null;
        if (m_BulletStack.TryGetValue(elem.name, out list))
        {
            for(int i=0;i<list.Count;++i)
            {
                if(list[i] == elem)
                {
                    list.RemoveAt(i);
                    break;
                }
            }
        }
    }
    private void OnBulletCollection(Bullet elem)
    {
        CollectionBullet(elem);
    }
    private void CollectionBullet(Bullet elem)
    {
        elem.gameObject.SetActive(false);

        m_BulletStore.Remove(elem);

        // check colletion count
        if (m_BulletStack.ContainsKey(elem.name))
        {
            if (m_BulletStack[elem.name].Count < m_iBulletColletionMaxCount)
            {
                m_BulletStack[elem.name].Add(elem);
            }
            else
            {
                GameObject.Destroy(elem.gameObject);
            }
        }
        else
        {
            List<Bullet> bulletStore = new List<Bullet>();
            bulletStore.Add(elem);
            m_BulletStack.Add(elem.name, bulletStore);
        }
    }

    private GameObject TryGetBulletFromStack(string name)
    {
        List<Bullet> list = null;
        if (m_BulletStack.TryGetValue(name, out list))
        {
            if (list.Count > 0)
            {
                Bullet res = list[0];
                list.RemoveAt(0);
                return res.gameObject;
            }
        }
        return null;
    }
}
