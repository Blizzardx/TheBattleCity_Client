using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Core.Utils;
using UnityEngine;

namespace Assets.Scripts.Framework.Network
{

    #region 批量下载 joe
    /// <summary>
    /// 一个素材文件
    /// </summary>
    public class AssetFile
    {
        enum AssetFileType
        {
            NONE = 0,
            TEXT = 1,
            BYTE = 2
        }
        public const int TEXT = 1;
        public const int BYTE = 2;
        public string Url = "";
        public string LocalPath = "";
        public int Type;
        public string Name;

        public bool Exists
        {
            get
            {
                return File.Exists(this.LocalPath);
            }
        }
        /// <summary>
        ///  构造一个下载资源 <see cref="AssetFile"/> class.
        /// </summary>
        /// <param name='Key'>
        /// Key eg>  Configs/LevelUp/config.xml
        /// </param>
        public AssetFile(string name,string localPath, string url)
        {
            Name = name;
            Url = url;
            LocalPath = localPath;

            if (this.LocalPath.IndexOf(".txt") > 0 || this.LocalPath.IndexOf(".xml") > 0)
            {
                this.Type = (int)AssetFileType.TEXT;
            }
            else
            {
                this.Type = (int)AssetFileType.BYTE;
            }
        }
    }
    /// <summary>
    /// 素材批量下载工具
    /// </summary>
    public class AssetsDownloader
    {
        Action<bool> onCompleteCallback = null;
        Action<int> onProgressCallback = null;
        Action<object> onOneDoneCallback = null;

        private List<AssetFile> DList = new List<AssetFile>();
        private int index = 0, len = 0;
        private AssetFile currentAsset;
        private bool SaveToLocalOnFinish = false;
        private bool IsBusy = false;


        #region Singleton
        private static AssetsDownloader sInstance = null;
        public static AssetsDownloader Singleton
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = new AssetsDownloader();
                }
                return sInstance;
            }
        }
        #endregion

        public AssetFile GetCurrentDownloadAsset()
        {
            if (!IsBusy)
            {
                return null;
            }
            return DList[index];
        }
        /// <summary>
        /// 开始下载一个资源队列
        /// </summary>
        /// <param name="dList">目标队列</param>
        /// <param name="onCompleteCallback">完成回调</param>
        /// <param name="onProgressCallback">总进度回调</param>
        /// <param name="onOneDoneCallback">单个任务完成回调</param>
        /// <param name="SaveToLocalOnFinish">完成时是否缓存到本地</param>
        public void StartDownload(List<AssetFile> dList, Action<bool> onCompleteCallback = null, Action<int> onProgressCallback = null, Action<object> onOneDoneCallback = null, bool SaveToLocalOnFinish = true)
        {
            if (IsBusy)
            {
                Debug.Log("Queue is in downloading ,pls wiat it done ");
                return;
            }
            IsBusy = true;

            if (onCompleteCallback != null)
            {
                this.onCompleteCallback = onCompleteCallback;
            }
            else
            {
                this.onCompleteCallback = (bool n) => { };
            }
            if (onProgressCallback != null)
            {
                this.onProgressCallback = onProgressCallback;
            }
            else
            {
                this.onProgressCallback = (int n) => { };
            }

            this.onOneDoneCallback = onOneDoneCallback == null ? (object n) => { } : onOneDoneCallback;

            this.SaveToLocalOnFinish = SaveToLocalOnFinish;


            index = len = 0;
            DList = dList;
            len = DList.Count;
            this.currentAsset = DList[index];
            StartDownloadOne();
        }
        /// <summary>
        /// 开始一个下载任务
        /// </summary>
        private void StartDownloadOne()
        {
            HttpService.Singleton.Load(this.currentAsset.Url, HandleDownloadOne, UpdateProgress, null, 120);

        }
        /// <summary>
        /// 百分比
        /// </summary>
        private int percent = 0;

        /// <summary>
        /// 进度回调
        /// </summary>
        /// <param name="wwwObject"></param>
        private void UpdateProgress(WWW wwwObject)
        {
            percent = (int)(((index * 1.0f) / (len * 1.0f) + wwwObject.progress / (len * 1.0f)) * 100.0f);
            this.onProgressCallback(percent);
        }
        /// <summary>
        /// 单个子任务完成处理
        /// </summary>
        /// <param name="wwwObj"></param>
        /// <param name="param"></param>
        public void HandleDownloadOne(WWW wwwObj, object param)
        {
            if (wwwObj != null && wwwObj.error == null)
            {
                if (SaveToLocalOnFinish)
                {
                    SaveFile(wwwObj, currentAsset.Type);
                }
                onOneDoneCallback(wwwObj);
                this.onProgressCallback((int)(((index * 1.0f) / (len * 1.0f))) * 100);
            }
            else
            {
                Debug.LogWarning("HttpService:Skiped the not exist file: index" + index + " url:" + currentAsset.Url);
                DList.Clear();
                IsBusy = false;
                onCompleteCallback(false);
                return;
            }

            index++;
            if (index < len)
            {
                currentAsset = DList[index];
                StartDownloadOne();
            }
            else
            {
                index = 0;
                DList.Clear();
                IsBusy = false;
                onCompleteCallback(true);
            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="www"></param>
        /// <param name="Type"></param>
        private void SaveFile(WWW www, int Type)
        {
            //完成则写文件
            if (www.progress == 1)
            {
                if (Type == AssetFile.BYTE)
                {
                    if (www.bytes.Length > 0)
                    {
                        FileUtils.SaveFileByteArray(currentAsset.LocalPath, www.bytes);
                    }
                }
                if (Type == AssetFile.TEXT)
                {
                    if (www.bytes.Length > 0)
                    {
                        FileUtils.SaveStringFile(currentAsset.LocalPath, www.text);
                    }
                }
            }
        }
    }
    #endregion
}