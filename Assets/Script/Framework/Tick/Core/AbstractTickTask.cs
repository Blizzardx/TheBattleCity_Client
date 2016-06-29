using System;
using UnityEngine;

namespace Framework.Tick
{

    public abstract class AbstractTickTask : ITickTask
    {
        private long lastTickTime;

        public void Tick()
        {
            long time = (long)(Time.realtimeSinceStartup * 1000.0f);


            if (lastTickTime == 0)
            {
                if (this.FirstRunExecute())
                {
                    try
                    {
                        this.Beat();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
                lastTickTime = 0;
            }
            if (time - lastTickTime < this.GetTickTime())
            {
                return;
            }
            try
            {
                this.Beat();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            if (lastTickTime < time)
            {
                lastTickTime = time;
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

}