using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public bool m_bIsShowDebugMsg;
    public string m_RemoteIp;
    public string m_RemotePort;
    public string m_LocalIp;
    public string m_LocalPort;
    private static GameManager m_Instance;
    private List<Action> m_UpdateStore;
 
    public static GameManager GetInstance 
    {
        get
        {
            return m_Instance;
        }
    }
    void Awake()
    {
        m_Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
	// Use this for initialization
	void Start ()
    {
        m_UpdateStore = new List<Action>();
        ResourceManager.Initialize("Config/Config");
        
        PlayerData.GetInstance.Initialize();

        WindowManager.GetInstance.Initialize();
        AudioManager.GetInstance.Initialize();
        SceneManager.GetInstance.Initialize();
        SceneManager.GetInstance.ChangeSceneTo(SceneType.MainMenu);
        TimeManager.Initialize();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //update time !!!! do this at first
        TimeManager.Update();

        ClientMessageCenter.GetInstance.OnTick();
        SceneManager.GetInstance.BasicUpdate();
        AudioManager.GetInstance.BasicUpdate();
        WindowManager.GetInstance.BasicUpdate();

	    foreach (Action updateElem in m_UpdateStore)
	    {
	        updateElem();
	    }
	}
    void OnDestroy()
    {
        AudioManager.GetInstance.Destructor();
        ResourceManager.Destructor();
    }

    public void RegisterToUpdateStore(Action method)
    {
        foreach (Action elem in m_UpdateStore)
        {
            if (elem == method)
            {
                return;
            }
        }
        m_UpdateStore.Add(method);
    }

    public void UnRegisterFromUpdateStore(Action method)
    {
        for (int i = 0; i < m_UpdateStore.Count; ++i)
        {
            if (m_UpdateStore[i] == method)
            {
                m_UpdateStore.RemoveAt(i);
                return;
            }
        }
    }
}