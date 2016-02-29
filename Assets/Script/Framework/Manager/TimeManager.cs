using UnityEngine;
using System.Collections;
using System;

public class TimeManager 
{

    static private TimeManager m_Instance;
    static public TimeManager GetInstance
    {
        get
        {
            if( null == m_Instance)
            {
                m_Instance = new TimeManager();
            }
            return m_Instance;
        }
    }
    private TimeManager()
    {

    }

    private double m_fStartTime;
    private float  m_fDuringTime;

    static public void Initialize()
    {
        GetInstance.InitTimeSystm();
    }
    static public void ResetStartTime(double ServerStartFireUITTime)
    {
        GetInstance.m_fStartTime        = ServerStartFireUITTime;
        GetInstance.m_fDuringTime       = (float)(ClientTimeMSec - ServerStartFireUITTime);
    }    
    static public float GetDuringTimeStartFire()
    {
        return m_Instance.m_fDuringTime;
    }
    static public void Update()
    {
        GetInstance.TimeUpdate();
    }
    private void InitTimeSystm()
    {
        m_fStartTime = 0.0f;
    }
    private void TimeUpdate()
    {
        m_fDuringTime = (float)(ClientTimeMSec - m_fStartTime);
    }
    /// <summary>i
    ///  client UTC Time second
    /// </summary> 
    public static int ClientTimeSec
    {
        get
        {
            TimeSpan t = System.DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (int)t.TotalSeconds;
        }
    }
    /// <summary>i
    ///  client UTC Time MSecond
    /// </summary> 
    public static double ClientTimeMSec
    {
        get
        {
            TimeSpan t = System.DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return t.TotalMilliseconds;
        }
    }
}
