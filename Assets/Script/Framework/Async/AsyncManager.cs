using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Framework.Async
{
    /// <summary>
    /// 异步任务管理器
    /// </summary>
    public class AsyncManager
    {
        public static readonly AsyncManager Instance = new AsyncManager();

        /// <summary>
        /// 以BeforeAsyncTask状态开始执行一个异步任务，必须由主线程来调用
        /// </summary>
        /// <param name="asyncTask">异步任务对象</param>
        public void ExecuteAsyncTask(IAsyncTask asyncTask)
        {
            this.ExecuteAsyncTask(AsyncState.BeforeAsync, asyncTask);
        }

        /// <summary>
        /// 指定异步任务状态，并从指定的状态开始执行,必须由主线程来调用
        /// </summary>
        /// <param name="state">指定异步任务的状态</param>
        /// <param name="asyncTask">异步任务对象</param>
        public void ExecuteAsyncTask(AsyncState state, IAsyncTask asyncTask)
        {
            switch (state)
            {
                case AsyncState.BeforeAsync:
                    {
                        //异步开始前执行，由主线程调用
                        AsyncState newState = asyncTask.BeforeAsyncTask();
                        if (newState == AsyncState.BeforeAsync)
                        {
                            throw new ApplicationException(string.Format("asyncTask:{0} [BeforeAsyncTask] infinite loop.", asyncTask.GetType().FullName));
                        }
                        this.ExecuteAsyncTask(newState, asyncTask);
                        break;
                    }
                case AsyncState.DoAsync:
                    {
                        //需要异步执行，委托线程池来执行该任务
                        ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncExecute), asyncTask);
                        break;
                    }
                case AsyncState.AfterAsync:
                    {
                        AsyncState newState = asyncTask.AfterAsyncTask();
                        if (newState == AsyncState.AfterAsync)
                        {
                            throw new ApplicationException(string.Format("asyncTask:{0} [AfterAsyncTask] infinite loop.", asyncTask.GetType().FullName));
                        }
                        this.ExecuteAsyncTask(newState, asyncTask);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 线程池调用
        /// </summary>
        /// <param name="o"></param>
        private void AsyncExecute(object o)
        {
            IAsyncTask asyncTask = o as IAsyncTask;
            AsyncState newState = asyncTask.DoAsyncTask();

            AsyncTaskMessage message = new AsyncTaskMessage();
            message.State = newState;
            message.AsyncTask = asyncTask;
            SystemMessageQueue.Instance.Offer(message);
        }
    }
}
