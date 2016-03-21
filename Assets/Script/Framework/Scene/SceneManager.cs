using System.Reflection.Emit;
using UnityEngine;
using System.Collections;
using System;

public class SceneManager : SingletonTemplateMon<SceneManager>
{
    private GameStateType   m_DefaultStageType;
    private const int       m_TimeOut               = 30000;
    private const int       m_LoadingSceneMinTime   = 2000;
    private Action          m_LoadFinishedCallBack;
    private Action m_LoadInitCallBack;
    private AsyncOperation  m_SceneAsync;
    private int             m_nBeginLoadingTime;
    private int             m_nLoadingTotalTime;
    private string          m_strTargetLoadingSceneName;
    private bool            m_bIsBusy;

    public void Initialize()
    {
        m_DefaultStageType = GameStateType.WorldState;
        m_bIsBusy = false;
    }
    public void LoadScene(string sceneName,Action FinishedCallBack,Action PreExcution,Action InitCallBack)
    {
        if (m_bIsBusy)
        {
            Debuger.Log("Is busy !!! " + sceneName);
            return;
        }

        m_bIsBusy = true;
        m_strTargetLoadingSceneName = sceneName;

        //mark start time
        m_nBeginLoadingTime = (int) (Time.time*1000.0f);
        m_nLoadingTotalTime = 0;

        //push to update store
        UITickTask.Instance.RegisterToUpdateList(BasicUpdate);
        m_LoadInitCallBack = InitCallBack; 
        m_LoadFinishedCallBack = FinishedCallBack;
        WindowManager.Instance.CloseAllWindow();
        ClearScene();

        PreExcution();

        //change scene to empty
        Application.LoadLevel("Empty");

        //begin loading target scene
        StartCoroutine(StartLoadScene(sceneName));
    }
    public bool IsSceneLoadiing()
    {
        return m_bIsBusy;
    }
    public float GetLoadingSceneProcess()
    {
        if (null == m_SceneAsync)
        {
            return 1.0f;
        }
        else
        {
            return m_SceneAsync.progress;
        }
    }
    private IEnumerator StartLoadScene(string targetSceneName)
    {
        m_SceneAsync = Application.LoadLevelAsync(targetSceneName);
        yield return m_SceneAsync;

        //log
        Debuger.Log("load scene " + m_strTargetLoadingSceneName);

        m_LoadInitCallBack();

        //end loading
        EndLoadCheck();
    }
    private void ClearScene()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    private void EndLoadCheck()
    {
        UITickTask.Instance.UnRegisterFromUpdateList(BasicUpdate);
        if (m_nLoadingTotalTime > m_LoadingSceneMinTime)
        {
            EndLoad();
        }
        else
        {
            Invoke("EndLoad", ((m_LoadingSceneMinTime )/1000.0f));
        }
    }
    private void EndLoad()
    {
        //set busy flat
        m_bIsBusy = false;
        
        //call back
        m_LoadFinishedCallBack();
    }
    private void BasicUpdate()
    {
        m_nLoadingTotalTime = (int) (Time.time*1000.0f) - m_nBeginLoadingTime;
        if (m_nLoadingTotalTime >= m_TimeOut)
        {
            Debuger.Log("Load scene time out");
            m_bIsBusy = false;
            Action defaultExcution = () => { WindowManager.Instance.OpenWindow(WindowID.Loading);};
            Action defaultInit = () => { };
            StageManager.Instance.ChangeState(m_DefaultStageType);
            //LoadScene(m_strDefaultScene, m_LoadFinishedCallBack, defaultExcution, defaultInit);
        }
    }
    private void Awake()
    {
        _instance = this;
    }
}
