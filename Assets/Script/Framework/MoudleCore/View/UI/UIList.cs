using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Common.Component;

public class UIList:MonoBehaviour
{
    public class ListData
    {
        public ListData(GameObject root, UIListItemBase handler,UIList rootMgr)
        {
            m_ObjectRoot = root;
            m_Handler = handler;
            m_Handler.m_ObjectRoot = root;
            m_Handler.m_RootMgr = rootMgr;
        }
        public GameObject m_ObjectRoot;
        public UIListItemBase m_Handler;
    }
    private UIScrollView        m_ScrollView;
    private UIGrid              m_Grid;
    private UITable             m_Table;
    private UIPanel             m_UIPanel;
    private List<ListData> m_ChildElementList;
    private GameObject          m_ChildElementTemplate;
    private bool                m_bIsInit;
    public static readonly int  m_ChildCountMax = 20;
    private Vector3             m_InitPos;
    private Vector2             m_InitPanelOffset;
    private Type                m_HandlerType;
    private int                 m_nCurrentSelectedIndex;
    private GameObject          m_ObjectListRoot;
    private GameObject          m_ObjectGridRoot;
    private GameObject          m_ObjectTableRoot;
    private GameObject          m_ObjectPanelRoot;

    private const string        m_strElementName        = "Element";
    private const string        m_strGridRoot           = "GridRoot";
    private const string        m_strTableRoot          = "TableRoot";
    private const string        m_strOtherRoot          = "OtherRoot";
    private const string        m_strPanelRoot          = "PanelRoot";

    public void InitUIList<T>() where T : UIListItemBase
    {
        if (m_bIsInit)
        {
            return;
        }

        m_ObjectListRoot = gameObject;
        m_ObjectPanelRoot = ComponentTool.FindChild(m_strPanelRoot, m_ObjectListRoot);
        m_ObjectGridRoot = ComponentTool.FindChild(m_strGridRoot, m_ObjectPanelRoot);
        m_ObjectTableRoot = ComponentTool.FindChild(m_strTableRoot, m_ObjectPanelRoot);
        m_ChildElementTemplate = ComponentTool.FindChild(m_strElementName, m_ObjectPanelRoot);

        m_HandlerType = typeof(T);
        m_ChildElementList = new List<ListData>();
        m_Grid = m_ObjectGridRoot == null ? null : m_ObjectGridRoot.GetComponent<UIGrid>();
        m_Table = m_ObjectTableRoot == null ? null : m_ObjectTableRoot.GetComponent<UITable>();
        m_UIPanel = m_ObjectPanelRoot.GetComponent<UIPanel>();
        m_ScrollView = m_ObjectPanelRoot.GetComponent<UIScrollView>();
        m_ChildElementList.Add(new ListData(m_ChildElementTemplate, Activator.CreateInstance(m_HandlerType) as UIListItemBase,this));
        m_ChildElementList[0].m_Handler.OnInit();
        m_ChildElementTemplate.gameObject.SetActive(false);
        m_InitPos = new Vector3(m_ObjectGridRoot.transform.localPosition.x, m_ObjectGridRoot.transform.localPosition.y, m_ObjectGridRoot.transform.localPosition.z);
        m_InitPanelOffset = new Vector2(m_UIPanel.clipOffset.x,m_UIPanel.clipOffset.y);
        m_bIsInit = true;
    }
    public void SetData<T>(List<T> content)
    {
        if (null == content)
        {
            return;
        }
        //reset position
        m_ObjectPanelRoot.transform.localPosition = m_InitPos;
        m_UIPanel.clipOffset = m_InitPanelOffset;

        int index = 0;
        for (index = 0; index < content.Count && index < m_ChildElementList.Count; ++index)
        {
            m_ChildElementList[index].m_ObjectRoot.SetActive(true);
            m_ChildElementList[index].m_Handler.Data= content[index];
            m_ChildElementList[index].m_Handler.OnData();
        }
        if (index < content.Count && index >= m_ChildElementList.Count)
        {
            for (int i = index; i < content.Count; ++i, ++index)
            {
                m_ChildElementList.Add(CreateChild());
                m_ChildElementList[i].m_Handler.m_iIndex = i;
                m_ChildElementList[i].m_Handler.Data = content[index];
                m_ChildElementList[i].m_Handler.OnData();
            }
        }
        else if (index >= content.Count && index < m_ChildElementList.Count)
        {
            for (int i = index; i < m_ChildElementList.Count && m_ChildElementList.Count >= m_ChildCountMax; )
            {
                GameObject.Destroy(m_ChildElementList[i].m_ObjectRoot);
                m_ChildElementList.RemoveAt(i);
            }
            for (int i = index; i < m_ChildElementList.Count; ++i)
            {
                m_ChildElementList[i].m_ObjectRoot.SetActive(false);
            }
        }

        m_Grid.Reposition();
    }
    public void SetSelectedIndex(int index)
    {
        for (int i = 0; i < m_ChildElementList.Count; ++i)
        {
            m_ChildElementList[i].m_Handler.IsSelected = i == index;
            m_ChildElementList[i].m_Handler.OnSelected();
        }
        m_nCurrentSelectedIndex = index;
    }
    public object GetSelected()
    {
        if (m_nCurrentSelectedIndex < 0 || m_nCurrentSelectedIndex >= m_ChildElementList.Count)
        {
            return null;
        }
        return m_ChildElementList[m_nCurrentSelectedIndex].m_Handler.Data;
    }
    public void ExcutionCustomFunction(object param)
    {
        for (int i = 0; i < m_ChildElementList.Count; ++i)
        {
            m_ChildElementList[i].m_Handler.CustomerFunction(param);
        }
    }
    public void ExcutionCustomFunction(List<object> param)
    {
        for (int i = 0; i < m_ChildElementList.Count && i<param.Count; ++i)
        {
            m_ChildElementList[i].m_Handler.CustomerFunction(param[i]);
        }
    }
    private ListData CreateChild()
    {
        GameObject tmpchild = GameObject.Instantiate(m_ChildElementTemplate) as GameObject;
        ListData result = new ListData(tmpchild, Activator.CreateInstance(m_HandlerType) as UIListItemBase,this);
        result.m_Handler.OnInit();
        ComponentTool.Attach(m_ObjectGridRoot.transform,tmpchild.transform);
        return result;
    }
}

public class UIListItemBase
{
    public UIList       m_RootMgr;
    public GameObject   m_ObjectRoot;
    public int          m_iIndex;

    virtual public void OnInit()
    {
        
    }
    virtual public void OnData()
    {
        
    }
    virtual public void OnSelected()
    {
        
    }
    public object Data
    {
        set; get;
    }
    public bool IsSelected
    {
        set; get;
    }
    protected T FindChildComponent<T>(string childName) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(childName, m_ObjectRoot);
    }
    protected GameObject FindChild(string childName)
    {
        return ComponentTool.FindChild(childName, m_ObjectRoot);
    }
    public virtual void CustomerFunction(object param)
    {
        
    }
}