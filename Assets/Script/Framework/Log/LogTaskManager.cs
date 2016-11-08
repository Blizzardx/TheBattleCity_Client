using System;
using Common.Component;
using Common.Tool;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Framework.Async;
using Framework.Network;
using NetWork;

namespace Framework.Log
{
    public enum TaskType
    {
        ReadFile,
        WriteFile,
        DelFile,
        RenameFile,
        UploadFile,
        PrintDirectory,
    }

    public class FileOperationElem
    {
        public string m_strFilePath;
        public object m_strParam;
    }
    public class LogTaskItem
    {
        public TaskType         m_Type;
        public object           m_Param;
        public Action<object>   m_ParamCallback;
    }

    public class LogAsyncTaskHandler : IAsyncTask
    {
        private bool m_bIsRunning;
        private LogTaskItem m_CurrentTask;

        public bool IsRunning()
        {
            return m_bIsRunning;
        }

        public void Start(LogTaskItem item)
        {
            if (m_bIsRunning)
            {
                return;
            }
            m_bIsRunning = true;
            m_CurrentTask = item;
            AsyncManager.Instance.ExecuteAsyncTask(this);
        }
        public void Start_Quick(LogTaskItem item)
        {
            m_CurrentTask = item;
        }
        public AsyncState BeforeAsyncTask()
        {
            m_bIsRunning = true;
            return AsyncState.DoAsync;
        }
        public AsyncState DoAsyncTask()
        {
            switch (m_CurrentTask.m_Type)
            {
                case TaskType.ReadFile:
                    OnExec_ReadFile();
                    break;
                case TaskType.WriteFile:
                    OnExec_Writefile();
                    break;
                case TaskType.DelFile:
                    OnExec_DelFile();
                    break;
                case TaskType.RenameFile:
                    OnExec_RenameFile();
                    break;
                case TaskType.UploadFile:
                    OnExec_UploadFile();
                    break;
                case TaskType.PrintDirectory:
                    OnExec_PrinitDirectory();
                    break;
            }
            return AsyncState.AfterAsync;
        }
        public AsyncState AfterAsyncTask()
        {
            m_bIsRunning = false;
            switch (m_CurrentTask.m_Type)
            {
                case TaskType.ReadFile:
                    OnEnd_ReadFile();
                    break;
                case TaskType.WriteFile:
                    OnEnd_WriteFile();
                    break;
                case TaskType.DelFile:
                    OnEnd_DelFile();
                    break;
                case TaskType.RenameFile:
                    OnEnd_RenameFile();
                    break;
                case TaskType.UploadFile:
                    OnEnd_UploadFile();
                    break;
                case TaskType.PrintDirectory:
                    OnEnd_PrinitDirectory();
                    break;
            }
            return AsyncState.Done;
        }

        #region handler
        // read file
        private void OnExec_ReadFile()
        {

        }
        private void OnEnd_ReadFile()
        {
            if (m_CurrentTask.m_ParamCallback != null)
            {
                m_CurrentTask.m_ParamCallback(m_CurrentTask.m_Param);
            }
        }
        // write file
        private void OnExec_Writefile()
        {
            if (!(m_CurrentTask.m_Param is FileOperationElem))
            {
                return;
            }
            FileOperationElem fileElem = m_CurrentTask.m_Param as FileOperationElem;
            var content = fileElem.m_strParam as List<string>;
            // is reset create time
            bool isresetCreatetime = !File.Exists(fileElem.m_strFilePath);

            FileUtils.WriteStringFile(fileElem.m_strFilePath, content);
            FileInfo info = new FileInfo(fileElem.m_strFilePath);
            if (isresetCreatetime)
            {
                info.CreationTime = DateTime.Now;
            }
            else
            {
                info.LastWriteTime = DateTime.Now;
            }
        }
        private void OnEnd_WriteFile()
        {
            if (m_CurrentTask.m_ParamCallback != null)
            {
                m_CurrentTask.m_ParamCallback(m_CurrentTask.m_Param);
            }
        }
        //delete file
        private void OnExec_DelFile()
        {
            string filePath = m_CurrentTask.m_Param as string;
            FileUtils.DeleteFile(filePath);
        }
        private void OnEnd_DelFile()
        {
            if (m_CurrentTask.m_ParamCallback != null)
            {
                m_CurrentTask.m_ParamCallback(m_CurrentTask.m_Param);
            }
        }
        //rename file
        private void OnExec_RenameFile()
        {
            try
            {
                object[] paramlist = m_CurrentTask.m_Param as object[];
                string sourceFileName = paramlist[0] as string;
                string DestinationFileName = paramlist[1] as string;
                FileInfo fileInfo = new FileInfo(sourceFileName);
                fileInfo.MoveTo(DestinationFileName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        private void OnEnd_RenameFile()
        {
            if (m_CurrentTask.m_ParamCallback != null)
            {
                m_CurrentTask.m_ParamCallback(m_CurrentTask.m_Param);
            }
        }
        // upload file
        private void OnExec_UploadFile()
        {
            try
            {
                object[] paramlist = m_CurrentTask.m_Param as object[];
                string url = paramlist[0] as string;
                string filePath = paramlist[1] as string;
                string fileName = paramlist[2] as string;

                byte[] data = FileUtils.ReadByteFile(filePath);
                if (null == data)
                {
                    return;
                }
                Debug.Log("Trigger To Upload log "+ fileName);
                // upload file
                HttpManager.Instance.UploadFile(url, fileName, data, null,"1.0");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        private void OnEnd_UploadFile()
        {
            if (m_CurrentTask.m_ParamCallback != null)
            {
                m_CurrentTask.m_ParamCallback(m_CurrentTask.m_Param);
            }
        }
        // print directory

        private void OnExec_PrinitDirectory()
        {
            object[] paramlist = m_CurrentTask.m_Param as object[];
            string path = paramlist[0] as string;
            paramlist[1]= PrintDirectory(path);
        }
        private void OnEnd_PrinitDirectory()
        {
            if (m_CurrentTask.m_ParamCallback != null)
            {
                m_CurrentTask.m_ParamCallback(m_CurrentTask.m_Param);
            }
        }
        #endregion

        #region Directory printer
        private string PrintDirectory(string path)
        {
            StringBuilder tmpS = new StringBuilder();
            if (!Directory.Exists(path))
            {
                tmpS.Append("Path do not exist " + path);
                return tmpS.ToString();
            }
            DirectoryInfo info = new DirectoryInfo(path);
            tmpS.Append("begin print directory ===== " + path);
            tmpS.Append('\n');

            DoPrintDirectory(info, tmpS);

            tmpS.Append("end print directory ===== " + path);
            tmpS.Append('\n');
            return tmpS.ToString();
        }
        private void DoPrintDirectory(DirectoryInfo info, StringBuilder content)
        {
            var allFiles = info.GetFiles();
            for (int i = 0; i < allFiles.Length; ++i)
            {
                DoPrintFile(allFiles[i], content);
            }
            var alldir = info.GetDirectories();
            for (int i = 0; i < alldir.Length; ++i)
            {
                DoPrintDirectory(alldir[i], content);
            }
        }
        private void DoPrintFile(FileInfo info, StringBuilder content)
        {
            content.Append("Name: ");
            content.Append(info.FullName);
            content.Append(" ");

            content.Append("CreateTime: ");
            content.Append(info.CreationTime);
            content.Append(" ");

            content.Append("LastAccessTime: ");
            content.Append(info.LastAccessTime);
            content.Append(" ");

            content.Append("LastWriteTime: ");
            content.Append(info.LastWriteTime);
            content.Append(" ");

            content.Append("Length: ");
            content.Append(info.Length);
            content.Append(" ");

            content.Append("\n");
        }
        #endregion
    }

    public class LogTaskManage
    {
        private LogAsyncTaskHandler         m_Handler;
        private TemplateQueue<LogTaskItem>  m_TaskQueue;

        public void CheckInit()
        {
            if (null == m_TaskQueue)
            {
                m_TaskQueue = new TemplateQueue<LogTaskItem>();
                m_TaskQueue.Initialize(false);
            }
            if (null == m_Handler)
            {
                m_Handler = new LogAsyncTaskHandler();
            }

        }
        public void StartTask(TaskType type, object param, Action<object> onCallback)
        {
            LogTaskItem item = new LogTaskItem();
            item.m_Type = type;
            item.m_Param = param;
            item.m_ParamCallback = onCallback;
            StartTask(item);
        }
        public void StartTask(LogTaskItem item)
        {
            CheckInit();
            m_TaskQueue.Enqueue(item);
        }
        public void QuickFinishedAllTask()
        {
            //if (m_Handler.IsRunning())
            //{
            //    // save to another file and merger at next start time 
            //    //return;
            //}
            m_Handler = new LogAsyncTaskHandler();
            while (true)
            {
                var elem = m_TaskQueue.Dequeue();
                if (null == elem)
                {
                    break;
                }
                m_Handler.Start_Quick(elem);
                m_Handler.BeforeAsyncTask();
                m_Handler.DoAsyncTask();
                m_Handler.AfterAsyncTask();
            }
        }
        public void Tick()
        {
            if (m_Handler.IsRunning())
            {
                return;
            }
            var elem = m_TaskQueue.Dequeue();
            if (null == elem)
            {
                return;
            }
            m_Handler.Start(elem);
        }
    }

}

