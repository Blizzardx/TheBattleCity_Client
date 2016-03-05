using System;
using System.Collections.Generic;

namespace ObjectPool
{
    public class ObjectPool<T> where T : class, new()
    {
        private Stack<T> m_objectStack;

        private Action<T> m_resetAction;
        private Action<T> m_onetimeInitAction;

        public ObjectPool(int initialBufferSize, Action<T> ResetAction = null, Action<T> OnetimeInitAction = null)
        {
            m_objectStack = new Stack<T>(initialBufferSize);
            m_resetAction = ResetAction;
            m_onetimeInitAction = OnetimeInitAction;
        }

        public T New()
        {
            if (m_objectStack.Count > 0)
            {
                T t = m_objectStack.Pop();

                if (m_resetAction != null)
                    m_resetAction(t);

                return t;
            }
            else
            {
                T t = new T();

                if (m_onetimeInitAction != null)
                    m_onetimeInitAction(t);

                return t;
            }
        }

        public void Store(T obj)
        {
            m_objectStack.Push(obj);
        }
    }

    public interface IResetable
    {
        void Reset();
    }

    public class ObjectPoolWithReset<T> where T : class, IResetable, new()
    {
        private Stack<T> m_objectStack;

        private Action<T> m_resetAction;
        private Action<T> m_onetimeInitAction;

        public ObjectPoolWithReset(int initialBufferSize, Action<T> ResetAction = null, Action<T> OnetimeInitAction = null)
        {
            m_objectStack = new Stack<T>(initialBufferSize);
            m_resetAction = ResetAction;
            m_onetimeInitAction = OnetimeInitAction;
        }

        public T New()
        {
            if (m_objectStack.Count > 0)
            {
                T t = m_objectStack.Pop();

                t.Reset();

                if (m_resetAction != null)
                    m_resetAction(t);

                return t;
            }
            else
            {
                T t = new T();

                if (m_onetimeInitAction != null)
                    m_onetimeInitAction(t);

                return t;
            }
        }

        public void Store(T obj)
        {
            m_objectStack.Push(obj);
        }
    }

    public class ObjectPoolWithCollectiveReset<T> where T : class, new()
    {
        private List<T> m_objectList;
        private int m_nextAvailableIndex = 0;

        private Action<T> m_resetAction;
        private Action<T> m_onetimeInitAction;

        public ObjectPoolWithCollectiveReset(int initialBufferSize, Action<T> ResetAction = null, Action<T> OnetimeInitAction = null)
        {
            m_objectList = new List<T>(initialBufferSize);
            m_resetAction = ResetAction;
            m_onetimeInitAction = OnetimeInitAction;
        }

        public T New()
        {
            if (m_nextAvailableIndex < m_objectList.Count)
            {
                // an allocated object is already available; just reset it
                T t = m_objectList[m_nextAvailableIndex];
                m_nextAvailableIndex++;

                if (m_resetAction != null)
                    m_resetAction(t);

                return t;
            }
            else
            {
                // no allocated object is available; create a new one and grow the internal object list
                T t = new T();
                m_objectList.Add(t);
                m_nextAvailableIndex++;

                if (m_onetimeInitAction != null)
                    m_onetimeInitAction(t);

                return t;
            }
        }

        public void ResetAll()
        {
            m_nextAvailableIndex = 0;
        }
    }
}