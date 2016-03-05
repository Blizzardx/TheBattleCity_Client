using System;
using System.IO;
using Assets.Scripts.Core.Utils;
using Assets.Scripts.Framework.Network;
using Config;
using Communication;
using UnityEngine;
using System.Collections.Generic;

public class AssetUpdate : Singleton<AssetUpdate>
{
    private string                  m_strRemoteAssetServerURL;
    private const string            m_strVersionConfigName      = "version_txtpkg.bytes";
    private VersionConfig           m_RemoteVersionConfig;
    private VersionConfig           m_LocalVersionConfig;
    private List<AssetFile>         m_DownloadList; 
    private Action<bool, string>    m_CompleteCallBack;
    private Action<float, AssetFile> m_ProcessCallBack;
    private int                     m_iCountingIndex;
    private const int               m_iTriggerSaveRate = 5;
    private Dictionary<string, string>  m_NameKeytoSignMap;

    public void CheckUpdate(Action<bool,string> CallBack,Action<float,AssetFile> processCallBack)
    {
        m_CompleteCallBack = CallBack;
        m_ProcessCallBack = processCallBack;
        m_strRemoteAssetServerURL = "http://112.126.74.237:8080/client/";
        m_RemoteVersionConfig = null;
        m_LocalVersionConfig = null;
        m_DownloadList = new List<AssetFile>();
        m_iCountingIndex = 0;
        m_NameKeytoSignMap = new Dictionary<string, string>();

        //download remote version config
        DownloadRemoteVersionConfig();
    }
    private void DownloadRemoteVersionConfig()
    {
        AssetFile remoteVersionFile = new AssetFile(m_strVersionConfigName,"",m_strRemoteAssetServerURL + m_strVersionConfigName);

        List<AssetFile> tmpDownloadList = new List<AssetFile>();
        tmpDownloadList.Add(remoteVersionFile);

        //trigger download remote versin config
        AssetsDownloader.Instance.BeginDownload(
            tmpDownloadList,
            (file, fileInfo) =>
            {
                m_RemoteVersionConfig = new VersionConfig();
                ThriftSerialize.DeSerialize(m_RemoteVersionConfig, file);
            },
            (e, fileInfo) =>
            {
                m_CompleteCallBack(false, e.Message);
            },
            (process, fileInfo) =>
            {
                
            }, 
            () =>
            {
                if (null == m_RemoteVersionConfig || m_RemoteVersionConfig.VersionList == null)
                {
                    m_CompleteCallBack(false, "");
                }
                else
                {
                    CompareVersion();
                }
            });
    }
    private void CompareVersion()
    {
        m_DownloadList.Clear();
        m_NameKeytoSignMap.Clear();

        m_LocalVersionConfig = ConfigManager.Instance.GetLocalVersionConfig();
        if (null == m_LocalVersionConfig || null == m_LocalVersionConfig.VersionList ||
            m_LocalVersionConfig.VersionList.Count == 0)
        {
            // download all
            for (int i = 0; i < m_RemoteVersionConfig.VersionList.Count; ++i)
            {
                AddToDownloadList(m_RemoteVersionConfig.VersionList[i]);
            }
            m_LocalVersionConfig = new VersionConfig();
            m_LocalVersionConfig.VersionList = new List<VersionConfigElement>();
        }
        else
        {
            for (int i = 0; i < m_RemoteVersionConfig.VersionList.Count; ++i)
            {
                VersionConfigElement remElem = m_RemoteVersionConfig.VersionList[i];
                bool isExit = false;
                for (int j = 0; j < m_LocalVersionConfig.VersionList.Count; ++j)
                {
                    VersionConfigElement localElem = m_LocalVersionConfig.VersionList[j];
                    if (remElem.Name == localElem.Name)
                    {
                        isExit = true;
                        if (remElem.Sign != localElem.Sign)
                        {
                            AddToDownloadList(remElem);
                            //remove date-out 
                            m_LocalVersionConfig.VersionList.RemoveAt(j);
                        }
                        break;
                    }
                }

                if (!isExit)
                {
                    AddToDownloadList(remElem);
                }
            }
        }
        if (m_DownloadList.Count > 0)
        {
            //begin download
            BeginDownload();
        }
        else
        {
            m_CompleteCallBack(true, "");
        }
    }
    private void BeginDownload()
    {
        AssetsDownloader.Instance.BeginDownload
            (   m_DownloadList,
                (filedata, fileInfo) =>
                {
                    AddToLocalVersionConfigList(fileInfo);
                    ++ m_iCountingIndex;
                    TrySave();
                },
                (e, fileData) =>
                {
                    m_CompleteCallBack(false, e.Message);
                },
                (process, fileInfo) =>
                {
                    m_ProcessCallBack(process, fileInfo);
                },
                () =>
                {
                    SaveLocalVersionConfig();
                    m_CompleteCallBack(true, "");
                    Debuger.Log("on complete download all ");
                }
            );
    }
    private void AddToDownloadList(VersionConfigElement elem)
    {
        AssetFile downloadElem = new AssetFile(elem.Name, ConfigManager.Instance.GetConfigPath() + elem.Name,
                   m_strRemoteAssetServerURL + elem.Name);
        m_DownloadList.Add(downloadElem);
        if (m_NameKeytoSignMap.ContainsKey(elem.Name))
        {
            m_CompleteCallBack(false, "");
            return;
        }
        else
        {
            m_NameKeytoSignMap.Add(elem.Name, elem.Sign);
        }
    }
    private void AddToLocalVersionConfigList(AssetFile elem)
    {
        VersionConfigElement element = new VersionConfigElement();
        element.Name = elem.Name;
        element.Sign = m_NameKeytoSignMap[elem.Name];
        m_LocalVersionConfig.VersionList.Add(element);
    }
    private void TrySave()
    {
        if (m_iCountingIndex >= m_iTriggerSaveRate)
        {
            m_iCountingIndex = 0;
            SaveLocalVersionConfig();
        }
    }
    private void SaveLocalVersionConfig()
    {
        ConfigManager.Instance.SaveLocalVersionConfig(m_LocalVersionConfig);
    }
}
public class AssetUpdateManager : Singleton<AssetUpdateManager>
{
    private Action m_CompleteCallBack;
    private bool m_bIsShowWindow;

    public void CheckUpdate(Action doneCallBack,bool havWindow = true)
    {
        m_bIsShowWindow = havWindow;
        m_CompleteCallBack = doneCallBack;
        TryConnect();
    }
    private void TryConnect()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            //使用的是网络运营商提供的网络，提示是否需要切换到wifi状态下//
            if (m_bIsShowWindow)
            {
                TipManager.Instance.Alert("提示","当前不是wifi，确定要继续更新吗？","确定","取消", (res) =>
                {
                    if (res)
                    {
                        BeginCheck();
                        return;
                    }
                    else
                    {
                        Application.Quit();
                    }
                });
            }
        }
        else if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (m_bIsShowWindow)
            {
                TipManager.Instance.Alert("提示", "网络连接失败，请重试", "确定", "取消", (res) =>
                {
                    if (res)
                    {
                        BeginCheck();
                        return;
                    }
                    else
                    {
                        Application.Quit();
                    }
                });
            }
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            BeginCheck();
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            BeginCheck();
        }
    }
    private void BeginCheck()
    {
        AssetUpdate.Instance.CheckUpdate(CheckResult,OnProcess);
    }
    private void OnProcess(float arg1, AssetFile arg2)
    {
        Debuger.Log("process: " + arg1 + " currentFile: " + arg2.Name);
        if (m_bIsShowWindow)
        {
        }
    }
    private void CheckResult(bool isSucceed,string errorTip)
    {
        if (isSucceed)
        {
            m_CompleteCallBack();
            return;
        }
        else
        {
            if (m_bIsShowWindow)
            {
                TipManager.Instance.Alert("提示", "网络连接失败，请重试", "确定", "取消", (res) =>
                {
                    if (res)
                    {
                        BeginCheck();
                        return;
                    }
                    else
                    {
                        Application.Quit();
                    }
                });
            }
            Debuger.LogError(errorTip);
        }
    }
    private void Retry()
    {
        BeginCheck();
    }
}
