using System;
using System.Collections.Generic;

namespace Common.Math
{
    public class LRU_K<T>
    {
        public class Element<T>
        {
            public T key;
            public int index;
        }

        private LinkedList<Element<T>> m_LruList;
        private int m_iK;
        private int m_iCount;
        private Action<T> m_OnDestroyCalBack;

        public LRU_K(int k, int count, Action<T> callback)
        {
            m_iK = k;
            m_iCount = count;
            m_LruList = new LinkedList<Element<T>>();
            m_OnDestroyCalBack = callback;
        }

        public void Access(T k)
        {
            List<T> removingList = new List<T>();
            var it = m_LruList.First;
            while (it != null)
            {
                if (it.Value.key.Equals(k))
                {
                    ++ it.Value.index;
                    if (it.Value.index >= m_iK)
                    {
                        it.Value.index = 0;
                        m_LruList.Remove(it);
                        m_LruList.AddFirst(it);

                        while (m_LruList.Count > m_iCount)
                        {
                            // trigger to remove
                            var removeElem = m_LruList.Last.Value;
                            m_LruList.RemoveLast();
                            removingList.Add(removeElem.key);
                        }
                    }
                    break;
                }
                it = it.Next;
            }
            Element<T> elem = new Element<T>();
            elem.key = k;
            elem.index = 1;
            m_LruList.AddLast(elem);

            for (int i = 0; i < removingList.Count; ++i)
            {
                OnRemove(removingList[i]);
            }
        }

        public void Clear()
        {
            m_LruList.Clear();
        }

        public void ResetK(int k)
        {
            m_iK = k;
        }

        public void ResetCount(int count)
        {
            m_iCount = count;
        }

        private void OnRemove(T key)
        {
            if (null != m_OnDestroyCalBack)
            {
                m_OnDestroyCalBack(key);
            }
        }
    }

}