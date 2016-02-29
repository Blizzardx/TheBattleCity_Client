using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIList : MonoBehaviour 
{
    private UIScrollView    m_ScrollView;
    private UIGrid          m_Gride;
    private Type            m_ItemType;
    private string          m_ItemName;
    private List<ListItemBase> m_ItemStore;

    public void Initialize(Type itemType,string name)
    {
        m_ScrollView = GetComponent<UIScrollView>();
        m_Gride = GetComponent<UIGrid>();

        m_ItemType = itemType;
        m_ItemName = name;
        m_ItemStore = new List<ListItemBase>();
    }
    public void SetData<T>(List<T> content)
    {
        int currentCount = m_ItemStore.Count;
        if (content.Count > currentCount)
        {
            for (int i = 0; i < content.Count - currentCount; ++i)
            {
                GameObject viewRoot = GameObject.Instantiate(ResourceManager.LoadBuildInUIPrefab(m_ItemName)) as GameObject;
                ComponentTool.Attach(transform, viewRoot.transform);
                ListItemBase tmpElem = ItemFactory();

                UIDragScrollView comp = viewRoot.GetComponent<UIDragScrollView>();
                comp.scrollView = m_ScrollView;

                //set root data
                tmpElem.m_ViewRoot = viewRoot;

                //add mouse click event
                tmpElem.m_ViewRoot.AddComponent<MouseEvent>();
                tmpElem.m_ViewRoot.GetComponent<MouseEvent>().OnClickCallBack += tmpElem.OnClick;

                //do initialize
                tmpElem.OnInit();

                //add to list store
                m_ItemStore.Add(tmpElem);
            }
        }
        else
        {
            for (int i = 0; i < currentCount - content.Count; ++i)
            {
                GameObject.Destroy(m_ItemStore[i].m_ViewRoot);
            }
            m_ItemStore.RemoveRange(0, currentCount - content.Count);
        }
        //set data
        for (int i = 0; i < content.Count; ++i)
        {
            m_ItemStore[i].OnData(content[i]);
        }

        //resort
        m_Gride.Reposition();
    }
    private ListItemBase ItemFactory()
    {
        return Activator.CreateInstance(m_ItemType) as ListItemBase;
    }
}
public class ListItemBase
{
    public GameObject m_ViewRoot;
    virtual public void OnInit()
    {

    }
    virtual public void OnData(System.Object data)
    {

    }
    virtual public void OnClick(GameObject sender)
    {

    }
    protected T FindChildComponent<T>(string name) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(name, m_ViewRoot);
    }
    protected T FindChildComponent<T>() where T : Component
    {
        return m_ViewRoot.GetComponent<T>();
    }
}