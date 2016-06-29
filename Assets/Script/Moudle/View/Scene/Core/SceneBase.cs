using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneBase
{
    public enum PerloadAssetType
    {
        BuildIn,
        AssetBundle,
    }
    public class PreloadAssetInfo
    {
        public string               assetName;
        public PerloadAssetType     assetType;

        public override bool Equals(object obj)
        {
            if (!(obj is PreloadAssetInfo))
            {
                return false;
            }
            PreloadAssetInfo left = obj as PreloadAssetInfo;
            if (left.assetName == assetName && left.assetType == assetType)
            {
                return true;
            }
            return false;
        }
    }
    private string                      m_strSceneName;
    private List<PreloadAssetInfo>      m_PreloadResList;

    #region public interface
    public SceneBase()
    {
        m_PreloadResList = new List<PreloadAssetInfo>();
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
    protected void AddPreloadResource(string assetName,PerloadAssetType assetType)
    {
        PreloadAssetInfo res = new PreloadAssetInfo();
        res.assetName = assetName;
        res.assetType = assetType;

        if (!m_PreloadResList.Contains(res))
        {
            m_PreloadResList.Add(res);
        }
    }
    private void Completed()
    {
        OnCompleted();
    }
    public string GetSceneName()
    {
        return m_strSceneName;
    }
    private void BeginLoadResource()
    {
        if (null == m_PreloadResList || m_PreloadResList.Count == 0)
        {
            Completed();
        }
        else
        {
            foreach (var elem in m_PreloadResList)
            {
                if (elem.assetType == PerloadAssetType.BuildIn)
                {
                    // load from build in resource
                    Framework.Asset.ResourceManager.Instance.LoadBuildInResourceAsync(elem.assetName, OnResourceLoadedCallback);
                }
                else if(elem.assetType == PerloadAssetType.AssetBundle)
                {
                    // load from assetbundle
                    Framework.Asset.ResourceManager.Instance.LoadAssetFromBundle(elem.assetName,
                        OnResourceLoadedCallback);
                }
            }
        }
    }
    private void OnResourceLoadedCallback(string assetName,Object obj)
    {
        for (int i = 0; i < m_PreloadResList.Count; ++i)
        {
            if (m_PreloadResList[i].assetName == assetName)
            {
                m_PreloadResList.RemoveAt(i);
                break;
            }
        }
        if (m_PreloadResList.Count == 0)
        {
            OnCompleted();
        }
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
