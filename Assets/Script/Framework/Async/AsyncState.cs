using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Async
{
    /// <summary>
    /// 异步任务的状态
    /// </summary>
    public enum AsyncState
    {
        /// <summary>
        /// 异步任务开始前状态
        /// </summary>
        BeforeAsync = 1,

        /// <summary>
        /// 异步任务开始状态
        /// </summary>
        DoAsync = 2,

        /// <summary>
        /// 异步任务结束后状态
        /// </summary>
        AfterAsync = 3,

        /// <summary>
        /// 中止异步任务状态
        /// </summary>
        Done
    }
}
