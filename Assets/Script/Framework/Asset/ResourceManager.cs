using System.Collections;
using System.IO;
using Assets.Scripts.Core.Utils;
using Communication;
using Thrift.Protocol;
using Thrift.Transport;
using UnityEngine;
using System.Collections.Generic;
using System;
using Object = UnityEngine.Object;

public enum AssetType
{
    None,
    UI,
    Animation,
    Audio,
    Map,
    Texture,
    Moudle,
    Tank,
}

public enum LoadType
{
    BuildIn,
    Download,
}
public class LoadResourceElement
{
    public string           realPath;
    public ResourceRequest  request;
    public bool             isFinishedDownload;
}
public class ResourceManager : SingletonTemplateMon<ResourceManager>
{
    private Dictionary<string, UnityEngine.Object>                      m_AssetStore;
    private Dictionary<string, List<Action<UnityEngine.Object>>>        m_AssetLoadCallBackStore;
    private List<string>                                                m_LoadingStore;
    
    #region public interface
    public void Initialize()
    {
        m_AssetStore                = new Dictionary<string, UnityEngine.Object>();
        m_AssetLoadCallBackStore    = new Dictionary<string, List<Action<UnityEngine.Object>>>();
        m_LoadingStore              = new List<string>();
    }
    public void Destructor()
    {
        m_AssetStore.Clear();
        m_AssetLoadCallBackStore.Clear();
        m_LoadingStore.Clear();
    }
    public T LoadBuildInResource<T>(string path,AssetType type ) where T : UnityEngine.Object
    {
        string realPath = GetRealPath(path, type,LoadType.BuildIn);
        
        UnityEngine.Object res = null;
        if (m_AssetStore.TryGetValue(realPath, out res))
        {
            if (null != res)
            {
                return res as T;
            }
            else
            {
                m_AssetStore.Remove(realPath);
            }
        }

        res = Resources.Load<T>(realPath);
        if (m_AssetStore.ContainsKey(realPath))
        {
            m_AssetStore[realPath] = res;
        }
        else
        {
            m_AssetStore.Add(realPath, res);
        }
        return res as T;
    }
    public void LoadBuildInAssetsAsync(string path, AssetType type, Action<UnityEngine.Object> CallBack)
    {
        StartCoroutine(StartLoadBuildInResource(path, type, CallBack));
    }
    public void LoadDownloadAsset(string path, AssetType type, Action<Object> CallBack)
    {
        StartCoroutine(StartLoadDownloadResource(path, type, CallBack));
    }
    IEnumerator LoadWWW(string path,Action<WWW> CallBack)
    {
        WWW www = new WWW(path);
        yield return www;
        CallBack(www);
    }
    public void LoadStreamingAssetsData(string _path, Action<WWW> _callBack)
    {
        StartCoroutine(LoadWWW(_path,_callBack));
    }
    public void DecodeStreamAssetTemplate<T>(string path, Action<bool, T> callBack) where T : TBase, new()
    {
        Action<WWW> callBackdef = (www) =>
        {
            byte[] data = www.bytes;

            if (null != data)
            {
                T template = new T();
                ThriftSerialize.DeSerialize(template, data);
                callBack(true, template);
            }
            else
            {
                callBack(false,new T());
            }
        };

        StartCoroutine(LoadWWW(path, callBackdef));
    }
    public static bool DecodePersonalDataTemplate<T>(string path, ref T template) where T : TBase, new()
    {
        byte[] data = FileUtils.ReadByteFile(path);

        if (null != data)
        {
            template = new T();
            ThriftSerialize.DeSerialize(template, data);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DecodeBuildInTemplate<T>(string path, ref T template) where T : TBase, new()
    {
        path = "BuildIn/" + path;
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        
        if (textAsset != null)
        {
            byte[] data = textAsset.bytes;

            if (null != data)
            {
                template = new T();
                ThriftSerialize.DeSerialize(template, data);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debuger.LogError("error ");
        }
        return false;
    }
    public bool IsEditor()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor ||  Application.platform == RuntimePlatform.OSXEditor)
        {
            return true;
        }
        return false;
    }
    public string GetStreamAssetPath()
    {
        string path = string.Empty;
        
        if (Application.platform == RuntimePlatform.Android)
        {
            // android
            path = "jar:file://" + Application.dataPath + "!/assets";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // ios
            path = @"file://" + Application.dataPath + "/Raw";
            
        }
        else
        {
            path = @"file://" + Application.streamingAssetsPath;
        }
        return path;
    }
    public void ExecuteActionNextFrame(Action _action)
    {
        StartCoroutine(ExecuteAction(_action));
    }
    #endregion

    #region system function
    private IEnumerator ExecuteAction(Action _action)
    { 
        yield return new  WaitForSeconds(0.1f);
        _action();
    }
    private IEnumerator StartLoadBuildInResource(string path, AssetType type, Action<UnityEngine.Object> CallBack)
    {
        string realPath = GetRealPath(path, type, LoadType.BuildIn);
        UnityEngine.Object res = null;
        if (m_AssetStore.TryGetValue(realPath, out res))
        {
            if (null != res)
            {
                CallBack(res);
                yield return null;
            }
            else
            {
                m_AssetStore.Remove(realPath);
            }
        }

        //register to call back store
        RegisterToCallBackStore(realPath, CallBack);

        if (IsAssetsOnLoading(realPath))
        {
            yield return null;
        }
        else
        {
            //add to download list
            RegisterToLoadingStore(realPath);

            //start load
            ResourceRequest result = Resources.LoadAsync(realPath);

            yield return result;

            res = result.asset;
            if (m_AssetStore.ContainsKey(realPath))
            {
                m_AssetStore[realPath] = res;
            }
            else
            {
                m_AssetStore.Add(realPath, res);
            }

            UnRegisterFromLoadingStore(realPath);
            DoResourceCallBack(realPath, res);
        }
    }
    private IEnumerator StartLoadDownloadResource(string path, AssetType type, Action<UnityEngine.Object> CallBack)
    {
        string realPath = GetRealPath(path, type, LoadType.Download);
        UnityEngine.Object res = null;
        m_AssetStore.TryGetValue(realPath, out res);
        if (null != res)
        {
            CallBack(res);
            yield return null;
        }

        //register to call back store
        RegisterToCallBackStore(realPath, CallBack);

        if (IsAssetsOnLoading(realPath))
        {
            yield return null;
        }
        else
        {
            //add to download list
            RegisterToLoadingStore(realPath);
            object request = null;
            bool isAssetBundle = true;
            if (isAssetBundle)
            {
                byte[] data = FileUtils.ReadByteFile(realPath);
                AssetBundleCreateRequest a = AssetBundle.LoadFromMemoryAsync(data);
                request = a;
            }
            else
            {
                WWW a = new WWW(realPath);
                request = a;
            }

            yield return request;

            if (isAssetBundle)
            {
                AssetBundleCreateRequest a = request as AssetBundleCreateRequest;
                res = a.assetBundle.LoadAsset(GetBundleName(realPath));
            }
            else
            {
                WWW a = request as WWW;
                switch (type)
                {
                    case AssetType.Audio:
                        res = a.audioClip;
                        break;
                        case AssetType.Texture:
                        res = a.texture;
                        a.texture.Compress(true);
                        break;
                }
            }

            if (m_AssetStore.ContainsKey(realPath))
            {
                m_AssetStore[realPath] = res;
            }
            else
            {
                m_AssetStore.Add(realPath, res);
            }

            UnRegisterFromLoadingStore(realPath);
            DoResourceCallBack(realPath, res);
        }
    }
    private void RegisterToCallBackStore(string realPath,Action<UnityEngine.Object> callBack)
    {
        if (m_AssetLoadCallBackStore.ContainsKey(realPath))
        {
            for (int i = 0; i < m_AssetLoadCallBackStore[realPath].Count; ++i)
            {
                if (m_AssetLoadCallBackStore[realPath][i] == callBack)
                {
                    return;
                }
            }
            m_AssetLoadCallBackStore[realPath].Add(callBack);
        }
        else
        {
            m_AssetLoadCallBackStore[realPath] = new List<Action<Object>>();
            m_AssetLoadCallBackStore[realPath].Add(callBack);
        }
    }
    private void DoResourceCallBack(string realPath,UnityEngine.Object result)
    {
        for (int i = 0; i < m_AssetLoadCallBackStore[realPath].Count; ++i)
        {
            m_AssetLoadCallBackStore[realPath][i](result);
        }
        m_AssetLoadCallBackStore[realPath].Clear();
    }
    private void RegisterToLoadingStore(string realPath)
    {
        m_LoadingStore.Add(realPath);
    }
    private void UnRegisterFromLoadingStore(string realPath)
    {
        m_LoadingStore.Remove(realPath);
    }
    private bool IsAssetsOnLoading(string realPath)
    {
        for (int i = 0; i < m_LoadingStore.Count; ++i)
        {
            if (realPath == m_LoadingStore[i])
            {
                return true;
            }
        }
        return false;
    }
    private string GetRealPath(string path, AssetType type,LoadType loadType)
    {
        switch (type)
        {
           case AssetType.UI:
                path = "UI/Prefab/" + path;
                break;
            case AssetType.Audio:
                path = "Audio/" + path;
                break;
            case AssetType.Map:
                path = "Moudle/Map/" + path;
                break;
            case AssetType.Moudle:
                path = "Moudle/" + path;
                break;
                case AssetType.Tank:
                path = "Moudle/Tank/" + path;
                break;
            default:
            {
                // do nothing
            }
                break;
        }
        if (loadType == LoadType.BuildIn)
        {
            path = "BuildIn/" + path;
        }
        else
        {
            path = Application.persistentDataPath + "/Resource/" + path;
            switch (type)
            {
                case AssetType.UI:
                    path += ".pkg";
                    break;
                case AssetType.Animation:
                    path += ".pkg";
                    break;
                case AssetType.Audio:
                    path += ".mp3";
                    break;
                case AssetType.Map:
                    path += ".pkg";
                    break;
                case AssetType.Texture:
                    path += ".png";
                    break;
                default:
                    {
                        // do nothing
                    }
                    break;
            }
        }
        return path;
    }
    private string GetBundleName(string name)
    {
        int startIndex = name.LastIndexOf('/');
        if (startIndex > 0)
        {
            startIndex += 1;
        }
        int length = name.LastIndexOf('.') - startIndex;
        string res= name.Substring(startIndex, length);
        Debuger.Log(res);
        return res;
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }
    #endregion
}
