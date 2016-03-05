using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Async
{
    /// <summary>
    /// 标识一个异步任务的接口
    /// </summary>
    public interface IAsyncTask
    {
        /// <summary>
        /// 执行异步任务前调用，主线程调用
        /// </summary>
        /// <returns></returns>
        AsyncState BeforeAsyncTask();

        /// <summary>
        /// 执行异步任务时调用，子线程调用，尽可能的减少数据同步问题
        /// </summary>
        /// <returns></returns>
        AsyncState DoAsyncTask();

        /// <summary>
        /// 执行异步任务后调用，主线程调用
        /// </summary>
        /// <returns></returns>
        AsyncState AfterAsyncTask();
    }
}
