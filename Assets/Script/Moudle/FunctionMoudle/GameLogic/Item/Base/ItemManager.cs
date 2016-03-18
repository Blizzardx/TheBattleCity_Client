using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemRegisterInfo
{
    public ItemRegisterInfo(int id, string path,Action<object> handler)
    {
        this.path = path;
        this.id = id;
        this.handler = handler;
    }
    public string path;
    public int id;
    public Action<object> handler;
}
public class ItemManager :Singleton<ItemManager>
{
    private Dictionary<int, ItemRegisterInfo> m_ItemTemplateMap;
    private List<ItemBase> m_CurrentItemStore;

    public ItemManager()
    {
        m_ItemTemplateMap = new Dictionary<int, ItemRegisterInfo>();
        m_CurrentItemStore = new List<ItemBase>();
    }
    public void AddItem(int id,Vector3 pos,int posId)
    {
        ItemRegisterInfo template = null;
        if(!m_ItemTemplateMap.TryGetValue(id,out template))
        {
            Debuger.LogWarning("Can't load item by id " + id);
            return;
        }
        //load item 
        var obj = ResourceManager.Instance.LoadBuildInResource<GameObject>(template.path, AssetType.Moudle);
        if(null == obj)
        {
            Debuger.LogWarning("Can't load obj by path " + template.path);
            return;
        }

        //instance
        obj = GameObject.Instantiate(obj);
        //set pos
        obj.transform.position = pos;
        obj.transform.localScale = Vector3.one;

        //get handler
        ItemBase handler = obj.GetComponent<ItemBase>();
        if(null == handler)
        {
            //
            Debuger.LogWarning("Can't load item handler on path " + template.path);
            GameObject.Destroy(obj);
            return;
        }

        //reset id & position id
        handler.m_iId = id;
        handler.m_iPosId = posId;

        //add to store
        m_CurrentItemStore.Add(handler);
    }
    public void RemoveItem(int id,int posId)
    {
        for(int i=0;i<m_CurrentItemStore.Count;++i)
        {
            if(m_CurrentItemStore[i].m_iId == id && m_CurrentItemStore[i].m_iPosId == posId)
            {
                GameObject.Destroy(m_CurrentItemStore[i].gameObject);
                // do remove
                m_CurrentItemStore.RemoveAt(i);
                break;
            }
        }
    }
    public void UseItem(int id,object param)
    {
        // find item handler
        ItemRegisterInfo template = null;
        if (m_ItemTemplateMap.TryGetValue(id,out template))
        {
            template.handler(param);
        }
    }
    public List<int> GetItemList()
    {
        List<int> res = new List<int>();
        foreach(var elem in m_ItemTemplateMap)
        {
            res.Add(elem.Key);
        }
        return res;
    }

    #region register
    public void RegisterItem(int id,string resPath,Action<object> handler)
    {
        if(m_ItemTemplateMap.ContainsKey(id) )
        {
            Debuger.LogWarning("item id register error " + id + " path " + resPath);
            return;
        }
        m_ItemTemplateMap.Add(id, new ItemRegisterInfo(id,resPath,handler));
    }
    #endregion
}
