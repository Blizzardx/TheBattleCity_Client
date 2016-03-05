using Assets.Scripts.Core.Utils;
using UnityEngine;
using System.Collections.Generic;

public class LogManager :Singleton<LogManager>
{

    private List<string>        m_LogStore;
    private bool                m_bIsEnalbeRecord;
    private long                m_lWriteRate;
    private string              m_strSavePath;
    private long                m_lLastTriggerTime;
    private int                 m_MaxLogSize;

	#region public interface
    public void Initialize(bool isEnalbeLog=true,bool isEnableRecord=true,float writeRate = 30.0f)
    {
        m_LogStore          = new List<string>();
		m_strSavePath       = Application.persistentDataPath + "/log.txt";
		m_bIsEnalbeRecord   = isEnableRecord;
	    m_lLastTriggerTime  = TimeManager.Instance.Now;
	    m_lWriteRate        = (long)(writeRate*1000.0f);
        m_MaxLogSize = 3 * 1024 * 1024;// 3m

        //check size
        if (CheckSize())
        {
            FileUtils.DeleteFile(m_strSavePath);
        }

        //check time
        m_LogStore.Add(TimeManager.Instance.GetCurrentTime());
        SaveToFileSystem();

        Debuger.Initialize(OnLogTrigger, isEnalbeLog);
    }
    public void OnQuit()
    {
        SaveToFileSystem();
    }
    #endregion

    #region sytem function 
	private void SaveToFileSystem()
	{
        FileUtils.WriteStringFile(m_strSavePath, m_LogStore);
        m_LogStore.Clear();
    }
    private void OnLogTrigger(string message)
    {
        if (!m_bIsEnalbeRecord)
        {
            return;
        }
        m_LogStore.Add(message);
        if (TimeManager.Instance.Now - m_lLastTriggerTime > m_lWriteRate)
        {
            SaveToFileSystem();
            m_lLastTriggerTime = TimeManager.Instance.Now;
        }
    }

    private bool CheckSize()
    {
        string filecontext =  FileUtils.ReadStringFile(m_strSavePath);
        if (string.IsNullOrEmpty(filecontext))
        {
            return false;
        }
        return filecontext.Length*2 > m_MaxLogSize;
    }
    #endregion
}
