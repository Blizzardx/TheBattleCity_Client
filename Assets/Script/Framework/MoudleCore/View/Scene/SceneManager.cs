﻿using System;
using UnityEngine;
using System.Collections;
using Common.Tool;
using Framework.Asset;

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
        m_bIsLoading = true;
        var newScene = Activator.CreateInstance(typeof(T)) as SceneBase;

        Debug.Log("begin load scene " + newScene.ToString());

        // on create
        newScene.Create();

        newScene.DoBeforeLoad(BeginLoadScene);
    }
    public SceneBase GetCurrentScene()
    {
        return m_CurrentScene;
    }
    private void BeginLoadScene(SceneBase scene)
    {
        // try load scen
        if (null != m_CurrentScene)
        {
            // on exit
            Debug.Log("Unload scene " + m_CurrentScene.ToString());
            m_CurrentScene.Exit();
        }
        m_CurrentScene = scene;

        // load empty first
        StartCoroutine(LoadEmptyScene());
    }
    IEnumerator LoadEmptyScene()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Empty");
        yield return Clear();
        yield return LoadTargetScene();
        m_bIsLoading = false;
    }
    IEnumerator LoadTargetScene()
    {
        var res = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_CurrentScene.GetSceneName());
        
        // on process
        m_CurrentScene.Process(res.progress);
        yield return res;

        Debug.Log("loaded scene " + m_CurrentScene.ToString());

        // on init
        m_CurrentScene.Init();
    }
    IEnumerator Clear()
    {
        yield return Resources.UnloadUnusedAssets();
        AssetManager.Instance.Clear();
    }
}
