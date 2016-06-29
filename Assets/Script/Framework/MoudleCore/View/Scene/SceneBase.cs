using System;
using System.Collections.Generic;
using Framework.Asset;
using Object = UnityEngine.Object;
public enum PerloadAssetType
{
    BuildInAsset,
    AssetBundleAsset,
    AssetBundle,
}
public class PreloadAssetInfo
{
    public string assetName;
    public PerloadAssetType assetType;

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

public class SceneBase
{
    
    private string                      m_strSceneName;
    private List<PreloadAssetInfo>      m_PreloadResList;
    private List<PreloadAssetInfo>      m_BeforloadResList;
    private Action<SceneBase>           m_DoBeforeLoadCompledted;

    #region public interface
    public SceneBase()
    {
        m_PreloadResList = new List<PreloadAssetInfo>();
        m_BeforloadResList = new List<PreloadAssetInfo>();
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
        BeginLoadResource(m_PreloadResList, OnResourceLoadedCallback);
    }
    public void Exit()
    {
        OnExit();
    }
    public void DoBeforeLoad(Action<SceneBase> doneCallback)
    {
        m_DoBeforeLoadCompledted = doneCallback;
        BeginLoadResource(m_BeforloadResList, OnBeforLoadResourceLoadedCallback);
    }
    #endregion

    #region system function

    private void BeforeLoadDone()
    {
        OnBeforeLoadDone();
        m_DoBeforeLoadCompledted(this);
    }
    protected void SetSceneName(string scene)
    {
        m_strSceneName = scene;
    }
    protected void AddBeforeLoadResource(string assetName, PerloadAssetType assetType)
    {
        PreloadAssetInfo res = new PreloadAssetInfo();
        res.assetName = assetName;
        res.assetType = assetType;

        if (!m_BeforloadResList.Contains(res))
        {
            m_BeforloadResList.Add(res);
        }
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
    private void BeginLoadResource(List<PreloadAssetInfo> m_BeforloadResList, Action<string, Object> onBeforLoadResourceLoadedCallback)
    {
        if (null == m_BeforloadResList || m_BeforloadResList.Count == 0)
        {
            onBeforLoadResourceLoadedCallback(string.Empty, null);
        }
        else
        {
            foreach (var elem in m_BeforloadResList)
            {
                if (elem.assetType == PerloadAssetType.BuildInAsset)
                {
                    // load from build in resource
                    ResourceManager.Instance.LoadBuildInResourceAsync(elem.assetName, onBeforLoadResourceLoadedCallback);
                }
                else if (elem.assetType == PerloadAssetType.AssetBundleAsset)
                {
                    // load from assetbundle
                    ResourceManager.Instance.LoadAssetFromBundle(elem.assetName,
                        onBeforLoadResourceLoadedCallback);
                }
                else if (elem.assetType == PerloadAssetType.AssetBundle)
                {
                    // load asset bundle
                    AssetbundleManager.Instance.LoadAssetBundle(elem.assetName, (name, bundle) =>
                    {
                        onBeforLoadResourceLoadedCallback(name, null);
                    });
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
            Completed();
        }
    }
    private void OnBeforLoadResourceLoadedCallback(string assetName, Object obj)
    {
        for (int i = 0; i < m_BeforloadResList.Count; ++i)
        {
            if (m_BeforloadResList[i].assetName == assetName)
            {
                m_BeforloadResList.RemoveAt(i);
                break;
            }
        }
        if (m_BeforloadResList.Count == 0)
        {
            BeforeLoadDone();
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
    protected virtual void OnBeforeLoadDone()
    {
        
    }
}
