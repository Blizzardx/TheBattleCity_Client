using UnityEngine;
using System.Collections;

public class DebugLog 
{
    static private DebugLog m_Instance;
    private DebugLog()
    {

    }
    static public DebugLog GetInstance
    {
        get
        {
            if( null == m_Instance )
            {
                m_Instance = new DebugLog();
            }
            return m_Instance;
        }
    }

    public static void Log(string log)
    {
        //NGUIDebug.Log(log);
        Debug.Log(log);
    }
    public static void LogError(string log)
    {
        Debug.LogError(log);
    }
    public static void LogWarning(string log)
    {
        Debug.LogWarning(log);
    }
}
