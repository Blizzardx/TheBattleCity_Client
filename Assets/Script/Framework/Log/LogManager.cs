using Common.Tool;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using Object = UnityEngine.Object;

namespace Framework.Log
{
    public class LogManager : Singleton<LogManager>, ILogHandler
    {
        private List<string> m_LogStore;
        private bool m_bIsEnalbeRecord;
        private string m_strCurrentLogSavePath;
        private string m_strLogCashePath;
        private int m_MaxLogFileSize;
        private int m_iLogStoreCount;
        private int m_iLogHistoryCount;
        private LogTaskManage m_TaskManager;
        private Queue<string> m_LogCasheList;
        private bool m_bIsPrintDirectoryBeforupload;
        private bool m_bIsEnableTimeoutAutoUpload;
        private float m_InitSystemTimeout;
        private const string m_strLogSnapshotHeader = "log_";

        #region public interface

        public void Initialize
            (bool isEnalbeLogToConsole = true,
            bool isEnableRecord = true,
            string logCashePath = "",
            int logQueueCount = 1024,
            int logHistoryCount = 1,
            int maxLogFileSize = 3 * 1024 * 1024,// 3m
            bool isPrintDirectoryBeforUpload = true,
            bool isEnableInitSystemTimeoutAutoUpload = true,
            float timeout = 30.0f
            )
        {
            m_InitSystemTimeout = timeout;
            m_bIsEnableTimeoutAutoUpload = isEnableInitSystemTimeoutAutoUpload;

            // begin counting initialize
            MarktoBeginInitializeApplication();

            m_bIsPrintDirectoryBeforupload = isPrintDirectoryBeforUpload;

            if (isEnalbeLogToConsole)
            {
                Application.logMessageReceived += HandleLog;
            }
            else
            {
                Debug.logger.logHandler = this;
            }
            m_iLogStoreCount = logQueueCount;
            m_TaskManager = new LogTaskManage();
            m_TaskManager.CheckInit();
            m_LogStore = new List<string>();
            if (string.IsNullOrEmpty(logCashePath))
            {
                m_strLogCashePath = Application.persistentDataPath + "/Logcashe";
            }
            else
            {
                m_strCurrentLogSavePath = logCashePath;
            }
            m_strCurrentLogSavePath = m_strLogCashePath + "/CurrentLog.txt";
            m_bIsEnalbeRecord = isEnableRecord;
            m_MaxLogFileSize = maxLogFileSize;
            m_iLogHistoryCount = logHistoryCount;

            // init log history cashe
            InitLogHistory();

            //check size
            if (CheckSize())
            {
                ClearCashe();
            }

            //check time
            m_LogStore.Add(TimeControl.Instance.GetCurrentTime());
            SaveToFileSystem();
        }
        private void HandleLog(string condition, string stackTrace, LogType type)
        {
            if (!m_bIsEnalbeRecord)
            {
                return;
            }
            StringBuilder res = new StringBuilder();
            // time
            res.Append(TimeControl.Instance.GetCurrentTime());
            // type
            res.Append(" " + type.ToString());
            // content
            res.Append(" " + condition);
            // stacktrace
            if (type == LogType.Error || type == LogType.Assert || type == LogType.Exception)
            {
                res.Append(" " + stackTrace);
            }
            m_LogStore.Add(res.ToString());

            if (m_LogStore.Count >= m_iLogStoreCount)
            {
                SaveToFileSystem();
            }
        }
        public void Distructor()
        {
            SaveToFileSystem();
            if (m_TaskManager != null)
            {
                m_TaskManager.QuickFinishedAllTask();
            }
        }
        public void Update()
        {
            TimeoutUpdate();
            m_TaskManager.Tick();
        }
        public void UploadLog(string url = "")
        {
            if (m_bIsPrintDirectoryBeforupload)
            {
                // print directory info befor upload
                PrintDirectory();
            }

            // save cashe to disk befor upload
            SaveToFileSystem();

            List<string> fileList = new List<string>();
            int count = m_LogCasheList.Count;
            int index = 0;
            while (index < count)
            {
                var elem = m_LogCasheList.Dequeue();
                fileList.Add(elem);
                m_LogCasheList.Enqueue(elem);
                ++index;
            }
            fileList.Add(m_strCurrentLogSavePath);

            // set default url when url is null&empty
            url = string.IsNullOrEmpty(url) ? "http://10.12.7.35/client-upload-tools/upload.do" : url;

            // send to task list to upload
            for (int i = 0; i < fileList.Count; ++i)
            {
                string realName = fileList[i];
                realName = realName.Replace('\\', '/');
                realName = realName.Substring(realName.LastIndexOf('/')+1);
                if (!realName.StartsWith(m_strLogSnapshotHeader))
                {
                    realName = GenLogFileName(null,false);
                }
                //else
                //{
                //    string preFix = m_strLogCashePath + "/";
                //    realName = realName.Substring(realName.LastIndexOfAny(preFix.ToCharArray()));
                //}
               // realName = realName.Replace(".txt", ".bytes");
                m_TaskManager.StartTask(TaskType.UploadFile, new object[] { url, fileList[i], realName }, null);
            }
        }
        public void OnApplicationInitializeSucceed()
        {
            MarktoInitializeApplicationSucceed();
        }
        #endregion

        #region sytem function 
        private void InitLogHistory()
        {
            List<FileInfo> tmpList = new List<FileInfo>();
            m_LogCasheList = new Queue<string>();
            DirectoryInfo info = Directory.CreateDirectory(m_strLogCashePath);
            var file = info.GetFiles();
            for (int i = 0; i < file.Length; ++i)
            {
                string elem = file[i].Name;
                if (elem.StartsWith(m_strLogSnapshotHeader))
                {
                    //add to tmp list for sort
                    tmpList.Add(file[i]);
                }
            }
            tmpList.Sort(SortFilesByCreateTime);
            // sort files by time
            for (int i = 0; i < tmpList.Count; ++i)
            {
                string elem = m_strLogCashePath + "/" + tmpList[i].Name;
                // add to queue
                m_LogCasheList.Enqueue(elem);
            }
        }
        private int SortFilesByCreateTime(FileInfo x, FileInfo y)
        {
            if (x.LastWriteTime > y.LastWriteTime)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        private void ClearCashe()
        {
            // get new file name & save
            string newName = m_strLogCashePath + "/" + GenLogFileName(GetCurrentLogCreateTime().ToString("yyyyMMdd-HH:mm:ss"));
            m_TaskManager.StartTask(TaskType.RenameFile, new object[] { m_strCurrentLogSavePath, newName }, null);
            // add to log cashe
            m_LogCasheList.Enqueue(newName);
            // check cashe size
            while (m_LogCasheList.Count > m_iLogHistoryCount)
            {
                string fileName = m_LogCasheList.Dequeue();
                m_TaskManager.StartTask(TaskType.DelFile, fileName, null);
            }
        }
        private void SaveToFileSystem()
        {
            if (m_LogStore == null || m_LogStore.Count == 0)
            {
                // do noting
                return;
            }
            FileOperationElem elem = new FileOperationElem();
            elem.m_strFilePath = m_strCurrentLogSavePath;
            elem.m_strParam = m_LogStore;
            m_TaskManager.StartTask(TaskType.WriteFile, elem, null);
            // do not use m_LogStore.Clear();
            m_LogStore = new List<string>();
        }
        private bool CheckSize()
        {
            return FileUtils.GetFileLength(m_strCurrentLogSavePath) > m_MaxLogFileSize;
        }
        private string GenLogFileName(string createTime,bool withEndTime = true)
        {
            string deviceId = SystemInfo.deviceUniqueIdentifier;
            string deviceName = SystemInfo.deviceName;
            string endTime = withEndTime ? "-" + DateTime.Now.ToString("yyyyMMdd-HH:mm:ss") : "-Now";
            string strCreateTime = string.IsNullOrEmpty(createTime) ? "" : "_" + createTime;
            string res = m_strLogSnapshotHeader + "_" + deviceName + "_" + deviceId + strCreateTime + endTime + ".txt";
            res = res.Replace(' ', '_');
            res = res.Replace(':', '_');
            return res;
        }
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            HandleLog(string.Format(format, args), "", logType);
        }
        public void LogException(Exception exception, Object context)
        {
            HandleLog(context != null ? context.ToString() : string.Empty, exception.StackTrace, LogType.Exception);
        }
        private void PrintDirectory()
        {
            object[] paramList = new object[] { Application.persistentDataPath, "" ,m_LogStore};

            m_TaskManager.StartTask(TaskType.PrintDirectory, paramList, PrintDirectoryCallback);
        }
        private void PrintDirectoryCallback(object content)
        {
            Debug.Log("Write file info to log");
            object[] paramList = content as object[];
            List<string> tmpLogStore = paramList[2] as List<string>;
            tmpLogStore.Add(paramList[1] as string);
        }
        private DateTime GetCurrentLogCreateTime()
        {
            FileInfo file = new FileInfo(m_strCurrentLogSavePath);
            return file.CreationTime;
        }
        #endregion

        #region Crash Log Collection

        private bool m_bIsMarkToInit;
        private float m_fStartTime;

        private void TimeoutUpdate()
        {
            if (!m_bIsMarkToInit)
            {
                return;
            }
            if (Time.realtimeSinceStartup - m_fStartTime > m_InitSystemTimeout)
            {
                m_bIsMarkToInit = false;
                UploadLog();
            }
        }
        private void MarktoBeginInitializeApplication()
        {
            if (!m_bIsEnableTimeoutAutoUpload)
            {
                return;
            }
            Debug.Log("Log: MarktoBeginInitializeApplication");
            m_bIsMarkToInit = true;
            m_fStartTime = Time.realtimeSinceStartup;
        }
        private void MarktoInitializeApplicationSucceed()
        {
            Debug.Log("Log: MarktoInitializeApplicationSucceed");
            m_bIsMarkToInit = false;
        }
        #endregion
    }

}