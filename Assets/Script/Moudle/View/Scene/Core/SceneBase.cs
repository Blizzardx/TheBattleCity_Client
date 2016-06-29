using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneBase
{
    private string          m_strSceneName;
    private List<string>    m_PreloadResList;

    #region public interface
    public SceneBase()
    {
        m_PreloadResList = new List<string>();
    }
    public void Create()
    {
        OnCreate();
    }
    public void Process(float process)
    {
        OnProcess(process);
    }
    public void Init()
    {
        OnInit();
        BeginLoadResource();
    }
    public void OnComplted()
    {
        OnCompleted();
    }
    public void Exit()
    {
        OnExit();
    }
    #endregion

    #region system function
    protected void SetSceneName(string scene)
    {
        m_strSceneName = scene;
    }
    protected void AddPreloadResource(string res)
    {
        if (!m_PreloadResList.Contains(res))
        {
            m_PreloadResList.Add(res);
        }
    }
    public string GetSceneName()
    {
        return m_strSceneName;
    }
    private void BeginLoadResource()
    {

    }
    #endregion

    protected virtual void OnCreate()
    {
        
    }
    protected virtual void OnProcess(float process)
    {
        
    }
    protected virtual void OnInit()
    {
        
    }
    protected virtual void OnCompleted()
    {
        
    }
    protected virtual void OnExit()
    {
        
    }
}
