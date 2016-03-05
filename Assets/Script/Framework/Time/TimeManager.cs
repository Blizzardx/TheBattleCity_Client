using System;
using UnityEngine;
using System.Collections;

public class TimeManager : Singleton<TimeManager>
{
	private	bool	m_bDoubleSpeed = false;
	private long	m_Now = 0L;

    public void Initialize()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        Now = Convert.ToInt64(ts.TotalMilliseconds);
    }
    public string CheckTime (long time)
    {
        return TimeUtil.TimeFormat(TimeUtil.GetDateTime(time));
    }
    public void ResetTime()
    {
        Initialize();
    }
    public void Update()
    {
        Now += (int)(GetDeltaTime() * 1000);
    }
	public bool DoubleSpeed
	{
		get	{ return m_bDoubleSpeed; }
		set	{ m_bDoubleSpeed = value; }
	}
	public float GetRealTime()
	{
		return Time.realtimeSinceStartup;
	}
	public float GetTime()
	{
		return Time.time;
	}
	public	float GetDeltaTime()
	{
		return	Time.deltaTime;
	}
    public float GetRealDeltaTime()
    {
        return RealTime.deltaTime;
    }
	public void SetTimeScale( float fScale )
	{
		if( DoubleSpeed )
			fScale	*= 2f;

		Time.timeScale = fScale;
	}
	public	void ToggleDoubleSpeed()
	{
		if( DoubleSpeed )
		{
			Time.timeScale *= 0.5f;
			DoubleSpeed	=	false;
		}
		else
		{
			Time.timeScale *= 2f;
			DoubleSpeed	=	true;
		}
	}
	public long Now
	{
		get{ return m_Now; }
		set{ m_Now = value; }
	}

    public string GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy-M-d dddd HH:mm:ss");
    }
    public string GetCurrentTime(string formate)
    {
        return DateTime.Now.ToString(formate);
    }
}