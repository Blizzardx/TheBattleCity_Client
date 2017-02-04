using System;
using System.Collections.Generic;
using Framework.Asset;
using Object = UnityEngine.Object;
public enum PerloadAssetType
{
    BuildInAsset,
    AssetBundleAsset,
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

public abstract class SceneBase
{
    private List<PreloadAssetInfo>      m_LoadResList;
    private List<PreloadAssetInfo>      m_BeforloadResList;
    private Action<SceneBase>           m_DoBeforeLoadCompledted;

    #region public interface
    public SceneBase()
    {
        m_LoadResList = new List<PreloadAssetInfo>();
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
        BeginLoadResource(m_LoadResList, OnResourceLoadedCallback);
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
    protected void AddLoadResource(string assetName,PerloadAssetType assetType)
    {
        PreloadAssetInfo res = new PreloadAssetInfo();
        res.assetName = assetName;
        res.assetType = assetType;

        if (!m_LoadResList.Contains(res))
        {
            m_LoadResList.Add(res);
        }
    }
    private void Completed()
    {
        OnCompleted();
    }
    private void BeginLoadResource(List<PreloadAssetInfo> resList, Action<string, Object> onBeforLoadResourceLoadedCallback)
    {
        if (null == resList || resList.Count == 0)
        {
            onBeforLoadResourceLoadedCallback(string.Empty, null);
        }
        else
        {
            foreach (var elem in resList)
            {
                if (elem.assetType == PerloadAssetType.BuildInAsset)
                {
                    // load from build in resource
                    AssetManager.Instance.LoadAssetAsync(elem.assetName, onBeforLoadResourceLoadedCallback);
                }
                else if (elem.assetType == PerloadAssetType.AssetBundleAsset)
                {
                    // load from assetbundle
                    string assetBundleName = AssetbundleHelper.GetBundleNameByAssetName(elem.assetName);
                    AssetManager.Instance.LoadAssetAsync(elem.assetName, assetBundleName,
                        onBeforLoadResourceLoadedCallback);
                }
            }
        }
    }
    private void OnResourceLoadedCallback(string assetName,Object obj)
    {
        for (int i = 0; i < m_LoadResList.Count; ++i)
        {
            if (m_LoadResList[i].assetName == assetName)
            {
                m_LoadResList.RemoveAt(i);
                break;
            }
        }
        if (m_LoadResList.Count == 0)
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

    public abstract string GetSceneName();
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
