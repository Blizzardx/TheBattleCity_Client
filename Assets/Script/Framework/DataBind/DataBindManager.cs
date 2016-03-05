using System;
using UnityEngine;
using System.Collections.Generic;

public class DataBindElement
{
    public DataBindElement(object listener, DataBindType bindType, Action<object> callBack)
    {
        this.callBack = callBack;
        this.listener = listener;
        this.bindType = bindType;
    }

    public Action<object>   callBack;
    public object           listener;
    public DataBindType     bindType;
}
public class DataBindManager : Singleton<DataBindManager>
{
    private Dictionary<DataBindKey, List<DataBindElement>>                  m_DataBindStore;
    private Dictionary<int, Action<object>>                                 m_MessageHandlerStore;
    private Dictionary<DataBindType, Action<object, object>>                m_TypeHandlerStore;

    #region public interface
    public void Initialize()
    {
        m_DataBindStore = new Dictionary<DataBindKey, List<DataBindElement>>();
        m_TypeHandlerStore = new Dictionary<DataBindType, Action<object, object>>();
        m_MessageHandlerStore = new Dictionary<int, Action<object>>();

        MessageManager.Instance.RegisterAllMessageListener(MessageCallBack);
        DataBindDefine.Instance.RegisterBindTypeHandler();
        DataBindDefine.Instance.RegisterBindMessageHandler();
    }
    public void RegisterToDataBindStore(DataBindKey key, DataBindElement value)
    {
        if (!m_DataBindStore.ContainsKey(key))
        {
            m_DataBindStore.Add(key, new List<DataBindElement>());
        }
        List<DataBindElement> list = m_DataBindStore[key];
        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i].callBack == value.callBack &&
                list[i].listener == value.listener )
            {
                return;
            }
        }
        list.Add(value);
    }
    public void UnRegisterFromDataBindStore(DataBindKey key, DataBindElement value)
    {
        if (!m_DataBindStore.ContainsKey(key))
        {
            return;
        }
        List<DataBindElement> list = m_DataBindStore[key];
        list.Remove(value);
    }
    public void RegisterMsgHandler(int msgId, Action<object> callback)
    {
        if (!m_MessageHandlerStore.ContainsKey(msgId))
        {
            m_MessageHandlerStore.Add(msgId, callback);
        }
    }
    public void RegisterTypeHandler(DataBindType type, Action<object, object> handler)
    {
        if (!m_TypeHandlerStore.ContainsKey(type))
        {
            m_TypeHandlerStore.Add(type,handler);
        }
    }
    public void UnRegisterMsgHandler(int msgId)
    {
        if (m_MessageHandlerStore.ContainsKey(msgId))
        {
            m_MessageHandlerStore.Remove(msgId);
        }
    }
    public void ClearBindMap()
    {
        m_MessageHandlerStore.Clear();
    }
    #endregion

    #region system function
    public void BindExcution(DataBindKey key, object value)
    {
        List<DataBindElement> tmpList = null;
        if (m_DataBindStore.TryGetValue(key, out tmpList))
        {
            for (int i = 0; i < tmpList.Count; ++i)
            {
                DataBindElement elem = tmpList[i];

                m_TypeHandlerStore[elem.bindType](elem.listener, value);

                if (null != elem.callBack)
                {
                    elem.callBack(value);
                }
            }
        }
    }
    private void MessageCallBack(MessageObject obj)
    {
        Action<object> handler = null;
        if (m_MessageHandlerStore.TryGetValue(obj.msgId, out handler))
        {
            handler(obj.msgValue);
        }
    }
    #endregion
}
