using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Async
{
    /// <summary>
    /// 线程安全的系统的消息队列
    /// </summary>
    public class SystemMessageQueue
    {
        public static readonly SystemMessageQueue Instance = new SystemMessageQueue();

        private Queue<IInternalMessage> messageQueue = new Queue<IInternalMessage>();

        /// <summary>
        /// 将一个系统消息加入到队列中
        /// </summary>
        /// <param name="internalMessage">系统消息对象</param>
        public void Offer(IInternalMessage internalMessage)
        {
            lock (this)
            {
                messageQueue.Enqueue(internalMessage);
            }
        }

        /// <summary>
        /// 尝试从队列中拿出一个系统消息
        /// </summary>
        /// <returns></returns>
        public IInternalMessage Poll()
        {
            lock (this)
            {
                if (messageQueue.Count == 0)
                {
                    return null;
                }
                return messageQueue.Dequeue();
            }
        }

        public int GetCount()
        {
            return messageQueue.Count;
        }
    }
}
