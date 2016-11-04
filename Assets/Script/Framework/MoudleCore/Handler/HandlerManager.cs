using System;
using System.Collections.Generic;
using Common.Tool;
using UnityEngine;

public class HandlerManager : Singleton<HandlerManager>
{
    private HandlerBase[] m_HanderList;
    private const int MAX_EMPTY_INDEX_COUNT = 10;

    private void AutoRegister()
    {
        var list = ReflectionManager.Instance.GetTypeByBase(typeof(HandlerBase));
        List<HandlerBase> tmpInstanceList = new List<HandlerBase>(list.Count);
        for (int i = 0; i < list.Count; ++i)
        {
            var elem = list[i];
            HandlerBase modelInstance = Activator.CreateInstance(elem) as HandlerBase;
            tmpInstanceList.Add(modelInstance);
        }
        // check correct
        HashSet<int> tmpHashset = new HashSet<int>();

        int max = 1;
        for (int i = 0; i < tmpInstanceList.Count; ++i)
        {
            int index = tmpInstanceList[i].GetIndex();
            if (tmpHashset.Contains(index))
            {
                Debug.LogError(tmpInstanceList[i].GetType().Name + " index conflict " + index);
            }
            tmpHashset.Add(index);
            max = index > max ? index : max;
        }
        m_HanderList = new HandlerBase[max+1];
        for (int i = 0; i < tmpInstanceList.Count; ++i)
        {
            var elemInstance = tmpInstanceList[i];
            if (elemInstance.GetIndex() < 0)
            {
                Debug.LogError(elemInstance.GetType().Name + " index error " + elemInstance.GetIndex());
                continue;
            }
            m_HanderList[elemInstance.GetIndex()] = elemInstance;
        }
        int emptyIndexCount = 0;
        // show empty
        for (int i = 0; i < m_HanderList.Length; ++i)
        {
            if (null == m_HanderList[i])
            {
                Debug.LogWarning("empty unused handler index " + i);
                ++emptyIndexCount;
            }
        }
        if (emptyIndexCount > MAX_EMPTY_INDEX_COUNT)
        {
            Debug.LogError(" handler map have too many empty index !!! " + emptyIndexCount);
        }
        // initialize
        for (int i = 0; i < m_HanderList.Length; ++i)
        {
            var instance = m_HanderList[i];
            if (instance != null)
            {
                instance.Create();
            }
        }
    }
    public T GetHandler<T>(int index) where T : HandlerBase
    {
        CheckInit();

        if (index < 0 || index >= m_HanderList.Length)
        {
            return null;
        }
        return m_HanderList[index] as T;

    }
    public void CheckInit()
    {
        if (null != m_HanderList)
        {
            return;
        }
        AutoRegister();
    }
    public void Destroy()
    {
        for (int i = 0; i < m_HanderList.Length; ++i)
        {
            m_HanderList[i].Destroy();
        }
        m_HanderList = null;
    }
}
