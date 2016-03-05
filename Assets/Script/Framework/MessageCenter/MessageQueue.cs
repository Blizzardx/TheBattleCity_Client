using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageQueue
{
    private int maxSize = int.MaxValue;
    private Queue<MessageObject> m_Queue;

    public void Initialize()
    {
        m_Queue = new Queue<MessageObject>(1024);
    }
    public bool Offer(MessageObject message)
    {
        lock (m_Queue)
        {
            if (m_Queue.Count >= maxSize)
            {
                return false;
            }
            m_Queue.Enqueue(message);
            return true;
        }
    }

    public MessageObject Poll()
    {
        lock (m_Queue)
        {
            if (m_Queue.Count == 0)
            {
                return null;
            }
            return m_Queue.Dequeue();
        }
    }
}
