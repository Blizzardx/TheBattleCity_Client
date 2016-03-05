using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Async
{
    /// <summary>
    /// 异步任务的消息
    /// </summary>
    public class AsyncTaskMessage : IInternalMessage
    {
        //临时定义，标识异步在消息队列中的ID
        public const int ASYNC_MESSAGE_ID = 100;

        public AsyncState State { get; set; }

        public IAsyncTask AsyncTask { get; set; }

        public int GetMessageId()
        {
            return ASYNC_MESSAGE_ID;
        }
    }
}
