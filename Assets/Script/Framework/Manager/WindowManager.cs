using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum WindowType
{
    //main menu
    Welcom,
    RoomList,
    RoomMapPreview,
    CreateRoom,
    SelectMode,
    LANRoomList,
    LANCreateRoom,
    //battle
    Battle,

}
public class WindowElement
{
    public GameObject WindowObject;
    public WindowBase WindowData;
}
public class WindowIndexElement
{
    public WindowIndexElement(string path,Type type)
    {
        this.path = path;
        this.type = type;
    }
    public string path;
    public Type type;
}
public class WindowManager
{
    static private  WindowManager               m_Instance;
    private         GameObject                  m_UIWindowRoot;
    private         Camera                      m_UICamera;
    private         WindowBase                  m_CurrentWindow;
    private         WindowBase                  m_HidingWindow;
    private         List<Action>                m_UpdateStore;
    private List<Action>                        m_RemoveingUpdateList; 
    private Dictionary<WindowType, WindowIndexElement> m_WindowIndexStore;
    private Dictionary<WindowType, WindowElement> m_WindowUsingPool;
   
    //public interface -------------
    static public WindowManager GetInstance
    {
        get
        {
            if( m_Instance == null )
            {
                m_Instance = new WindowManager();
            }
            return m_Instance;
        }
    }
    public void Initialize()
    {
        m_RemoveingUpdateList  = new List<Action>();
        m_UpdateStore = new List<Action>();
        m_UICamera = GameObject.Find("Camera").GetComponent<Camera>();
        m_UIWindowRoot      = GameObject.Find("Camera/LayerWindow");
        m_WindowIndexStore = new Dictionary<WindowType, WindowIndexElement>();
        m_WindowUsingPool = new Dictionary<WindowType, WindowElement>();
        RegisterWindows();
    }
    public GameObject GetUIWindowRoot()
    {
        return m_UIWindowRoot;
    }
    public Camera GetUICamera()
    {
        return m_UICamera;
    }
    public void RegisterWindow(WindowType type,string name,Type windowType)
    {
        WindowIndexElement elem = new WindowIndexElement(name, windowType);
        if( !m_WindowIndexStore.ContainsKey(type))
        {
            m_WindowIndexStore.Add(type, elem);
        }
        else
        {
            m_WindowIndexStore[type] = elem;
        }
    }
    public void OpenWindow(WindowType type,System.Object paramter = null,bool isHideCurrentWindow = true)
    {
        if (isHideCurrentWindow)
        {
            m_HidingWindow = m_CurrentWindow;
        }
        else
        {
            m_HidingWindow = null;
        }

        if( null != m_CurrentWindow )
        {
            m_CurrentWindow.SetDepth(0);   
        }
        if (m_WindowUsingPool.ContainsKey(type))
        {
            m_CurrentWindow = m_WindowUsingPool[type].WindowData;
            m_CurrentWindow.OnInit(type,m_WindowUsingPool[type].WindowObject);
            HideWindowDuringInit();

            m_CurrentWindow.OnOpen(OnOpenWindowFinshed, paramter);
        }
        else
        {
            if (!m_WindowIndexStore.ContainsKey(type))
            {
                DebugLog.LogError("Can't load window : " + type.ToString());
            }
            else
            {
                //load window object
                WindowElement tmpElem = new WindowElement();
                tmpElem.WindowData = WindowFactory(type);
                tmpElem.WindowObject = GameObject.Instantiate(ResourceManager.LoadBuildInUIPrefab(m_WindowIndexStore[type].path)) as GameObject;
                ComponentTool.Attach(GetUIWindowRoot().transform, tmpElem.WindowObject.transform);
                
                //add to using pool
                m_WindowUsingPool.Add(type, tmpElem);

                //set current window
                m_CurrentWindow = tmpElem.WindowData;
                
                //init current window
                m_CurrentWindow.OnInit(type, m_WindowUsingPool[type].WindowObject);
                HideWindowDuringInit();

                // open window
                tmpElem.WindowData.OnOpen(OnOpenWindowFinshed,paramter);
            }
        }
    }
    public void HideWindow(WindowType type)
    {
        if (m_CurrentWindow != null && type == m_CurrentWindow.GetWindowType())
        {
            m_WindowUsingPool[type].WindowObject.SetActive(false);
            m_WindowUsingPool[type].WindowData.OnClose();
            m_CurrentWindow = m_HidingWindow;
        }
        else if( m_WindowUsingPool.ContainsKey(type))
        {
            m_WindowUsingPool[type].WindowObject.SetActive(false);
            m_WindowUsingPool[type].WindowData.OnClose();
        }
    }
    public void HideAllWindow()
    {
        foreach( KeyValuePair<WindowType,WindowElement> elem in m_WindowUsingPool)
        {
            elem.Value.WindowObject.SetActive(false);
            elem.Value.WindowData.OnClose();
        }
        m_CurrentWindow = null;
    }
    public void BasicUpdate()
    {
        if (null != m_UpdateStore && m_UpdateStore.Count > 0)
        {
            DoUpdate();
            DoRemoveUpdateList();
        }
    }
    public void ClearUsingPool()
    {
        foreach(var elem in m_WindowUsingPool)
        {
            GameObject.Destroy(elem.Value.WindowObject);
        }
        m_WindowUsingPool.Clear();
    }
    public WindowBase GetWindow(WindowType type)
    {
        if( m_WindowUsingPool.ContainsKey(type))
        {
            return m_WindowUsingPool[type].WindowData;
        }
        return null;
    }
    public void RegisterToUpdateStore(Action Update)
    {
        if (null == Update)
        {
            return;
        }
        foreach (Action elem in m_UpdateStore)
        {
            if (elem == Update)
            {
                return;
            }
        }
        m_UpdateStore.Add(Update);
    }
    public void UnRegisterFromUpdateStore(Action Update)
    {
        if (null == Update)
        {
            return;
        }
        for (int i = 0; i < m_RemoveingUpdateList.Count; ++i)
        {
            if (m_RemoveingUpdateList[i] == Update)
            {
                return;
            }
        }
        m_RemoveingUpdateList.Add(Update);
    }
    //system function -------------
    private WindowBase WindowFactory(WindowType type)
    {
        WindowBase res = Activator.CreateInstance(m_WindowIndexStore[type].type) as WindowBase;
        return res;
    }
    private void RegisterWindows()
    {
        //main menu
        RegisterWindow(WindowType.Welcom, "MainMenu/Window_Welcom", typeof(Window_Welcome));
        RegisterWindow(WindowType.RoomList, "MainMenu/Window_RoomList", typeof(Window_RoomList));
        RegisterWindow(WindowType.RoomMapPreview, "MainMenu/Window_RoomPreview", typeof(Window_RoomPreview));
        RegisterWindow(WindowType.CreateRoom, "MainMenu/Window_CreateRoom", typeof(Window_CreateRoom));
        RegisterWindow(WindowType.SelectMode, "MainMenu/Window_SelectMode", typeof(Window_SelectMode));
        RegisterWindow(WindowType.LANCreateRoom, "MainMenu/Window_LANCreateRoom", typeof(Window_LANCreateRoom));
        RegisterWindow(WindowType.LANRoomList, "MainMenu/Window_LANRoomList", typeof(Window_LANRoomList));

        //battle
        RegisterWindow(WindowType.Battle, "Battle/Window_Battle", typeof(Window_Battle));
    }
    private void OnOpenWindowFinshed()
    {
        if (null == m_CurrentWindow)
        {
            DebugLog.LogError("current is null");
        }
        else
        {
            GameObject tmpWindow = m_WindowUsingPool[m_CurrentWindow.GetWindowType()].WindowObject;
            UIPanel tmpPanel = tmpWindow.GetComponent<UIPanel>();
            tmpPanel.alpha = 1.0f;
            m_CurrentWindow.SetDepth(50);  
        }
        if (null != m_HidingWindow)
        {
            //hide last window
            HideWindow(m_HidingWindow.GetWindowType());
        }
    }
    private void HideWindowDuringInit()
    {
        if (null == m_CurrentWindow)
        {
            DebugLog.LogError("current is null");
        }
        else
        {
            GameObject tmpWindow = m_WindowUsingPool[m_CurrentWindow.GetWindowType()].WindowObject;
            UIPanel tmpPanel = tmpWindow.GetComponent<UIPanel>();
            tmpPanel.alpha = 0.0f;
        }
    }
    private WindowManager()
    {

    }
    private void DoRemoveUpdateList()
    {
        for (int i = 0; i < m_RemoveingUpdateList.Count; ++i)
        {
            for (int j = 0; j < m_UpdateStore.Count; ++j)
            {
                if (m_UpdateStore[j] == m_RemoveingUpdateList[i])
                {
                    m_UpdateStore.RemoveAt(j);
                    break;
                }
            }
        }
        m_RemoveingUpdateList.Clear();
    }
    private void DoUpdate()
    {
        foreach (Action elem in m_UpdateStore)
        {
            elem();
        }
    }
}
public class WindowBase
{
    protected WindowType m_WindowType;
    protected GameObject m_WindowObject;
    protected UIPanel m_Panel;
    public void OnInit(WindowType type,GameObject windowObj)
    {
        m_WindowType = type;
        m_WindowObject = windowObj;
        m_Panel = m_WindowObject.GetComponent<UIPanel>();
        m_WindowObject.SetActive(true);
    }
    public WindowType GetWindowType()
    {
        return m_WindowType;
    }
    public virtual void OnOpen(Action OnFinshedCallBack,System.Object paramter)
    {

    }
    public virtual void OnClose()
    {

    }
    public virtual void OnDestroy()
    {

    }
    public void SetDepth(int depth)
    {
        if (null != m_Panel)
        {
            //m_Panel.depth = depth;
        }
    }
    public void HideWindow()
    {
        WindowManager.GetInstance.HideWindow(m_WindowType);
    }
    protected T FindChildComponent<T>(string name) where T : Component
    {
        return ComponentTool.FindChildComponent<T>(name, m_WindowObject);
    }
    protected T FindChildComponent<T>() where T : Component
    {
        return m_WindowObject.GetComponent<T>();
    }
    
}