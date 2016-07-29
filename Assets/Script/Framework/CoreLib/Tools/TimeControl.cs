using System;
using Common.Tool;
using UnityEngine;
using System.Collections;

namespace Common.Tool
{

    public class TimeControl : Singleton<TimeControl>
    {
        private long m_Now = 0L;

        public void Initialize()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            m_Now = Convert.ToInt64(ts.TotalMilliseconds);
        }
        public void SyncTime(long serverTime)
        {
            m_Now = serverTime;
        }
        public long Now
        {
            get { return m_Now; }
        }
        public void Update()
        {
            m_Now += (int)(Time.unscaledDeltaTime * 1000);
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


}