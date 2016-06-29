using UnityEngine;
using Common.Tool;

public class AppManager : SingletonTemplateMon<AppManager>
{
    public bool m_bIsShowDebugMsg;

    void Awake()
    {
        _instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        DontDestroyOnLoad(this);        
        
    }
	void Start () 
    {
	    GameManager.Instance.Initialize();
	}
	void Update () 
    {
	    GameManager.Instance.Update();
	}
    void OnDestroy()
    {
        
    }
    void OnApplicationQuit()
    {
        GameManager.Instance.OnAppQuit();
    }
}
