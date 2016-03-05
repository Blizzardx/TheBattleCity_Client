using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class AbstractTickTask : ITickTask
{
    private long    lastTickTime;

    public void Tick()
    {
        if (lastTickTime == 0)
        {
            if (this.FirstRunExecute())
            {
                try
                {
                    this.Beat();
                }catch(Exception e){
                    Debuger.LogError(e);
                }
            }
			lastTickTime = TimeManager.Instance.Now;
        }
		if (TimeManager.Instance.Now - lastTickTime < this.GetTickTime())
        {
            return;
        }
        try
        {
			this.Beat();
        }
        catch (Exception e)
        {


        }
		if(lastTickTime < TimeManager.Instance.Now)
		{
			lastTickTime = TimeManager.Instance.Now;
		}
    }

	public void SetLastTickTime(long lastTickTime)
	{
		this.lastTickTime = lastTickTime;
	}

    /// <summary>
    /// 程序第一次启动时是否执行
    /// </summary>
    /// <returns></returns>
    protected abstract bool FirstRunExecute();

    /// <summary>
    /// tick任务的间隔时间，单位毫秒
    /// </summary>
    /// <returns></returns>
    protected abstract int GetTickTime();

    /// <summary>
    /// 触发任务
    /// </summary>
    protected abstract void Beat();
}