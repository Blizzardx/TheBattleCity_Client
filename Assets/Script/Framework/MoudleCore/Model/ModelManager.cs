using System;
using System.Collections.Generic;
using Common.Tool;
using UnityEngine;

public class ModelManager :Singleton<ModelManager>
{
    private ModelBase[] m_ModelList;
    private const int MAX_EMPTY_INDEX_COUNT = 10;

    private void AutoRegister()
    {
        var list = ReflectionManager.Instance.GetTypeByBase(typeof(ModelBase));
        List<ModelBase> tmpInstanceList = new List<ModelBase>(list.Count);
        for (int i = 0; i < list.Count; ++i)
        {
            var elem = list[i];
            ModelBase modelInstance = Activator.CreateInstance(elem) as ModelBase;
            tmpInstanceList.Add(modelInstance);
        }
        // check correct
        HashSet<int> tmpHashset = new HashSet<int>();

        int max = 0;
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
        m_ModelList = new ModelBase[max + 1];
        for (int i = 0; i < tmpInstanceList.Count; ++i)
        {
            var elemInstance = tmpInstanceList[i];
            if (elemInstance.GetIndex() < 0)
            {
                Debug.LogError(elemInstance.GetType().Name + " index error " + elemInstance.GetIndex());
                continue;
            }
            m_ModelList[elemInstance.GetIndex()] = elemInstance;
        }
        int emptyIndexCount = 0;
        // show empty
        for (int i = 0; i < m_ModelList.Length; ++i)
        {
            if (null == m_ModelList[i])
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
        for (int i = 0; i < m_ModelList.Length; ++i)
        {
            var instance = m_ModelList[i];
            if (instance != null)
            {
                Debug.Log("On Create Model " + instance.GetType().Name);
                instance.Create();
            }
        }
    }
    public T GetModel<T>(int index) where T : ModelBase
    {
        CheckInit();
        if (index < 0 || index >= m_ModelList.Length)
        {
            Debug.LogError("Error index");
            return null;
        }
        return m_ModelList[index] as T;
    }
    public void CheckInit()
    {
        if (null != m_ModelList)
        {
            return;
        }
        AutoRegister();
    }
    public void Destroy()
    {
        for (int i = 0; i < m_ModelList.Length; ++i)
        {
            var instance = m_ModelList[i];
            Debug.Log("On Destroy Model " + instance.GetType().Name);
            instance.Destroy();
        }
        m_ModelList = null;
    }
}
