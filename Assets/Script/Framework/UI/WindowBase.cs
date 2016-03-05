
using System;
using UnityEngine;
using System.Collections.Generic;

public class WindowComponentKey
{
    public const string m_strLabel      = "Label";
    public const string m_strTexture    = "Texture";
    public const string m_strSprite     = "Sprite";
    public const string m_strUIList     = "UIList";

    public Type GetTypeByKey(string key)
    {
        if (key == m_strLabel)
        {
            return typeof (UILabel);
        }
        if (key == m_strTexture)
        {
            return typeof(UITexture);
        }
        if (key == m_strSprite)
        {
            return typeof(UISprite);
        }
        if (key == m_strUIList)
        {
            return typeof(UIList);
        }
        return null;
    }
}
public class PanelStruct
{
    public UIPanel m_Panel;
    public int m_defaultDeepth;
}
public class WindowBase
{
    public int                  m_ID { get; private set; }
    public GameObject           m_ObjectRoot { get; private set; }
    private int                 m_iDeepth;
    private List<PanelStruct>   m_AllPanelsUnderWindow;
    private int                 m_iMaxDeepth;

    #region public interface
    public void Initialize(int id, GameObject root)
    {
        m_ID = id;
        m_ObjectRoot = root;
        m_iDeepth = 0;
        List<UIPanel> tmpPanelList = new List<UIPanel>();
        m_AllPanelsUnderWindow = new List<PanelStruct>();
        ComponentTool.FindAllChildComponents(m_ObjectRoot.transform, ref tmpPanelList);
        for (int i = 0; i < tmpPanelList.Count; ++i)
        {
            PanelStruct elem = new PanelStruct();
            elem.m_Panel = tmpPanelList[i];
            elem.m_defaultDeepth = tmpPanelList[i].depth;
            m_AllPanelsUnderWindow.Add(elem);
        }
    }
    public int ResetDeepth(int deepth)
    {
        int maxDeepth = -1;
        if (deepth == m_iDeepth)
        {
            //do nothing
            return maxDeepth;
        }
        
        m_iDeepth = deepth;
        for (int i = 0; i < m_AllPanelsUnderWindow.Count; ++i)
        {
            m_AllPanelsUnderWindow[i].m_Panel.depth = m_AllPanelsUnderWindow[i].m_defaultDeepth + m_iDeepth;
            if (m_AllPanelsUnderWindow[i].m_defaultDeepth + m_iDeepth > maxDeepth)
            {
                maxDeepth = m_AllPanelsUnderWindow[i].m_defaultDeepth + m_iDeepth;
            }
        }
        m_iMaxDeepth = maxDeepth;
        return maxDeepth;
    }
    public void Hide()
    {
        WindowManager.Instance.HideWindow(m_ID);
    }
    public void Close()
    {
        WindowManager.Instance.CloseWindow(m_ID);
    }
    public bool IsOpen()
    {
        return m_ObjectRoot.activeSelf;
    }
    public int GetMaxDeepthValue()
    {
        return m_iMaxDeepth;
    }
    #endregion

    #region virtual function
    virtual public void OnInit()
    {
        
    }
    virtual public void OnOpen(object param)
    {
        
    }
    virtual public void OnClose()
    {
        
    }
    virtual public void OnHide()
    {

    }
    #endregion

    #region system function
    protected T FindChildComponent<T>(string childName) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(childName, m_ObjectRoot);
    }
    protected GameObject FindChild(string childName)
    {
        return ComponentTool.FindChild(childName, m_ObjectRoot);
    }
    protected void AddChildElementClickEvent(UIEventListener.VoidDelegate handler, string childName, string rootName = "")
    {
        if (string.IsNullOrEmpty(rootName))
        {
            UIEventListener.Get(FindChild(childName)).onClick = handler;
        }
        else
        {
            UIEventListener.Get(ComponentTool.FindChild(childName, FindChild(rootName))).onClick = handler;
        }
    }
    protected void InitUIComponent<T>(ref T component,string name)where T : Component
    {
        string realName = name.Substring(2);
        component = FindChildComponent<T>(realName);
    }
    #endregion
}

public class WindowContentBase
{
    protected GameObject m_ObjectRoot;

    #region public interface
    public WindowContentBase(GameObject root)
    {
        m_ObjectRoot = root;
    }
    public void SetActive(bool status)
    {
        if (status)
        {
            //attation excution order
            m_ObjectRoot.SetActive(true);
            OnActive();
        }
        else
        {
            //attation excution order
            OnHide();
            m_ObjectRoot.SetActive(false);
        }
    }
    #endregion

    #region virtual function
    virtual public void Init()
    {
        
    }
    virtual protected void OnActive()
    {
        
    }
    virtual protected void OnHide()
    {
        
    }
    virtual protected void OnClose()
    {
        
    }
    #endregion

    #region system function
    protected T FindChildComponent<T>(string childName) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(childName, m_ObjectRoot);
    }
    protected GameObject FindChild(string childName)
    {
        return ComponentTool.FindChild(childName, m_ObjectRoot);
    }
    #endregion
}