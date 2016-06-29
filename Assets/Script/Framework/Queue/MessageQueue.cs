using Common.Component;
using Common.Tool;
using Framework.Common;

namespace Framework.Queue
{
    public class MessageQueue : Singleton<MessageQueue>
    {
        private readonly TemplateQueue<IMessage>  m_Queue;

        public MessageQueue()
        {
            m_Queue = new TemplateQueue<IMessage>();
            m_Queue.Initialize();
        }
        public void Enqueue(IMessage msg)
        {
            m_Queue.Enqueue(msg);
        }

        public IMessage Dequeue()
        {
            return m_Queue.Dequeue();
        }
    }
}
