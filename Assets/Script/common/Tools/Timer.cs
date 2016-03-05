using UnityEngine;

// 定时器
class Timer 
{
	private float time = float.MaxValue;
    private float timeInterval;
    private TimerCollection.Callback func = null;
    private TimerCollection.CallbackWithTime funcWithTime = null;
    private bool autoRelease = false;

    public Timer(TimerCollection.Callback _func, TimerCollection.CallbackWithTime _funcWithTime)
    {
		func = _func;
        funcWithTime = _funcWithTime;
        autoRelease = false;
	}
    public Timer(TimerCollection.Callback _func, bool autoR, TimerCollection.CallbackWithTime _funcWithTime)
    {
        func = _func;
        funcWithTime = _funcWithTime;
        autoRelease = autoR;
    }

	public void Start(float _time) {
        timeInterval = _time;
		time = RealTime.time + _time;
	}
	public void Stop() {
		time = float.MaxValue;
	}
	public bool Update() {
        if (RealTime.time >= time)
        {
            time = float.MaxValue;
            if (func != null)
            {
                func();
                return autoRelease;
            }
        }
        else if(funcWithTime != null)
        {
            float _timeRemain = time - RealTime.time;
            funcWithTime(timeInterval - _timeRemain);
        }
        return false;
	}
}

// 定时器管理器
internal class TimerCollection
{
    private static TimerCollection _this = new TimerCollection();

    public static TimerCollection GetInstance()
    {
        return _this;
    }

    public delegate void Callback();

    public delegate void CallbackWithTime(float _value);

    private System.Collections.Generic.LinkedList<Timer> timerList = new System.Collections.Generic.LinkedList<Timer>();

    public Timer Create(Callback func, CallbackWithTime _funcWithTime)
    {
        var timer = new Timer(func, _funcWithTime);
        timerList.AddLast(timer);
        return timer;
    }

    public Timer Create(Callback func, bool autoRelease, CallbackWithTime _funcWithTime)
    {
        Timer t = new Timer(func, autoRelease, _funcWithTime);
        timerList.AddLast(t);
        return t;
    }

    public void Destroy(Timer timer)
    {
        timerList.Remove(timer);
    }

    public void Update()
    {
        var node = timerList.First;
        while (node != null)
        {
            if (node.Value.Update())
            {
                var _tmp = node;
                node = node.Next;
                timerList.Remove(_tmp);
            }
            else
            {
                node = node.Next;
            }
        }
    }
}