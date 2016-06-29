﻿using Common.Component;
using Common.Tool;
using Framework.Common;

namespace Framework.Queue
{
    public class TaskQueue_UnBlock : Singleton<TaskQueue_UnBlock>
    {

        private readonly TemplateQueue<ITask> m_Queue;

        public TaskQueue_UnBlock()
        {
            m_Queue = new TemplateQueue<ITask>();
            m_Queue.Initialize();
        }
        public void Enqueue(ITask msg)
        {
            m_Queue.Enqueue(msg);
        }

        public ITask Dequeue()
        {
            return m_Queue.Dequeue();
        }

        public int GetCount()
        {
            return m_Queue.GetCount();
        }
    }
}
