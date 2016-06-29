using System;
using UnityEngine;
using System.Collections;
using Common.Tool;

public class SceneManager : MonoSingleton<SceneManager>
{
    private SceneBase   m_CurrentScene;
    private bool        m_bIsLoading;

    public void LoadScene<T>() where T : SceneBase
    {
        if (m_bIsLoading)
        {
            Debug.LogWarning("System busy");
            return;
        }
        // try load scen
        if (null != m_CurrentScene)
        {
            // on exit
            m_CurrentScene.Exit();
        }
        m_CurrentScene = Activator.CreateInstance(typeof (T)) as SceneBase;

        // on create
        m_CurrentScene.Create();

        // load empty first
        StartCoroutine(LoadEmptyScene());
    }
    IEnumerator LoadEmptyScene()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Empty");
        StartCoroutine(LoadTargetScene());
    }
    IEnumerator LoadTargetScene()
    {
        var res = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_CurrentScene.GetSceneName());
        
        // on process
        m_CurrentScene.Process(res.progress);
        yield return res;

        // on init
        m_CurrentScene.Init();
    }
}
