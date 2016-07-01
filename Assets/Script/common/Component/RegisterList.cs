using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace Common.Component
{
    public class RegisterList
    {
        private LinkedList<Action> m_UpdateList;
        private LinkedList<Action> m_UpdatingList;
        
        public RegisterList()
        {
            m_UpdateList = new LinkedList<Action>();
            m_UpdatingList = new LinkedList<Action>();
        }
        public void RegisterToUpdateList(Action element)
        {
            if (null != m_UpdatingList)
            {
                foreach (var elem in m_UpdatingList)
                {
                    if (elem == element)
                    {
                        return;
                    }
                }
            }
            foreach (var elem in m_UpdateList)
            {
                if (elem == element)
                {
                    return;
                }
            }
            m_UpdateList.AddLast(element);
        }
        public void UnRegisterFromUpdateList(Action element)
        {
            int count = 0;
            if (null != m_UpdatingList)
            {
                count = m_UpdatingList.Count;
                for (int i = 0; i < count; ++i)
                {
                    var elem = m_UpdatingList.First;
                    m_UpdatingList.RemoveFirst();
                    if (elem.Value == element)
                    {
                        break;
                    }
                    m_UpdatingList.AddLast(elem.Value);
                }
            }
            count = m_UpdateList.Count;
            for (int i = 0; i < count; ++i)
            {
                var elem = m_UpdateList.First;
                m_UpdateList.RemoveFirst();
                if (elem.Value == element)
                {
                    break;
                }
                m_UpdateList.AddLast(elem.Value);
            }
        }
        public void ExcutionUpdateList()
        {
            if (m_UpdateList.Count == 0)
            {
                return;
            }
            do
            {
                var elem = m_UpdateList.First;
                m_UpdateList.RemoveFirst();
                m_UpdatingList.AddLast(elem.Value);

                try
                {
                    // do callback
                    if (null != elem.Value)
                    {
                        elem.Value();
                    }
                    else
                    {
                        //log error                        
                        Debug.LogWarning("null of call back fun");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Wrong msg callback error log: " + e.Message);
                    Debug.LogException(e);
                }
            } while (m_UpdateList.Count != 0);

            m_UpdateList = m_UpdatingList;
            m_UpdatingList = null;
        }
    }
    public class RegisterListTemplate<T>
    {
        private LinkedList<Action<T>> m_UpdateList;
        private LinkedList<Action<T>> m_UpdatingList;

        public RegisterListTemplate()
        {
            m_UpdateList = new LinkedList<Action<T>>();
            m_UpdatingList = new LinkedList<Action<T>>();
        }
        public void RegisterToUpdateList(Action<T> element)
        {
            if (null != m_UpdatingList)
            {
                foreach (var elem in m_UpdatingList)
                {
                    if (elem == element)
                    {
                        return;
                    }
                }
            }
            foreach (var elem in m_UpdateList)
            {
                if (elem == element)
                {
                    return;
                }
            }
            m_UpdateList.AddLast(element);
        }
        public void UnRegisterFromUpdateList(Action<T> element)
        {
            int count = 0;
            if (null != m_UpdatingList)
            {
                count = m_UpdatingList.Count;
                for (int i = 0; i < count; ++i)
                {
                    var elem = m_UpdatingList.First;
                    m_UpdatingList.RemoveFirst();
                    if (elem.Value == element)
                    {
                        break;
                    }
                    m_UpdatingList.AddLast(elem.Value);
                }
            }
            count = m_UpdateList.Count;
            for (int i = 0; i < count; ++i)
            {
                var elem = m_UpdateList.First;
                m_UpdateList.RemoveFirst();
                if (elem.Value == element)
                {
                    break;
                }
                m_UpdateList.AddLast(elem.Value);
            }
        }
        public void ExcutionUpdateList(T param)
        {
            if (m_UpdateList.Count == 0)
            {
                return;
            }
            do
            {
                var elem = m_UpdateList.First;
                m_UpdateList.RemoveFirst();
                m_UpdatingList.AddLast(elem.Value);

                try
                {
                    // do callback
                    if (null != elem.Value)
                    {
                        elem.Value(param);
                    }
                    else
                    {
                        //log error                        
                        Debug.LogWarning("null of call back fun");
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Wrong msg callback error log: " + e.Message);
                    Debug.LogException(e);
                }
            } while (m_UpdateList.Count != 0);

            m_UpdateList = m_UpdatingList;
            m_UpdatingList = null;
        }
    }
    public class RegisterDictionaryTemplate<T>
    {
        private Dictionary<int, List<Action<T>>> m_CallbackStore;
        private Dictionary<int, List<Action<T>>> m_UnRegisterList;
        private Dictionary<int, List<Action<T>>> m_RegisterList;
        private bool m_bIsProcessingList;

        public RegisterDictionaryTemplate()
        {
            m_CallbackStore = new Dictionary<int, List<Action<T>>>();
            m_UnRegisterList = new Dictionary<int, List<Action<T>>>();
            m_RegisterList = new Dictionary<int, List<Action<T>>>();

            m_bIsProcessingList = false;
        }
        public void BeginUpdate()
        {
            m_bIsProcessingList = true;
        }
        public void Update(int id, T param)
        {
            if (m_CallbackStore.ContainsKey(id))
            {
                foreach (Action<T> fun in m_CallbackStore[id])
                {
                    if (null != fun)
                    {
                        fun(param);
                    }
                    else
                    {
                        //log error                        
                        Debug.LogWarning("null of call back fun" + id.ToString());
                    }
                }
            }
            else
            {
                //empty msg list                    
                Debug.LogWarning("empty list  " + id.ToString());
            }
        }
        public void EndUpdate()
        {
            m_bIsProcessingList = false;
            DoUnregister();
        }
        public void RegistEvent(int msgId, Action<T> msgCallback)
        {
            if (null == msgCallback)
            {
                Debug.LogError("msg call back can't be null !!!" + msgId.ToString());
            }
            if (m_bIsProcessingList)
            {
                if (!m_RegisterList.ContainsKey(msgId))
                {
                    m_RegisterList.Add(msgId, new List<Action<T>>());
                    m_RegisterList[msgId].Add(msgCallback);
                }
                else
                {
                    for (int i = 0; i < m_RegisterList[msgId].Count; ++i)
                    {
                        if (m_RegisterList[msgId][i] == msgCallback)
                        {
                            return;
                        }
                    }
                    m_RegisterList[msgId].Add(msgCallback);
                }
            }
            else
            {
                if (!m_CallbackStore.ContainsKey(msgId))
                {
                    m_CallbackStore.Add(msgId, new List<Action<T>>());
                    m_CallbackStore[msgId].Add(msgCallback);
                }
                else
                {
                    for (int i = 0; i < m_CallbackStore[msgId].Count; ++i)
                    {
                        if (m_CallbackStore[msgId][i] == msgCallback)
                        {
                            return;
                        }
                    }
                    m_CallbackStore[msgId].Add(msgCallback);
                }
            }

        }
        public void UnregistEvent(int msgId, Action<T> msgCallback)
        {
            if (m_bIsProcessingList)
            {
                bool hasKey = false;
                foreach (var elem in m_UnRegisterList)
                {
                    if (elem.Key == msgId)
                    {
                        hasKey = true;
                        var tmpList = elem.Value;
                        for (int i = 0; i < tmpList.Count; ++i)
                        {
                            if (tmpList[i] == msgCallback)
                            {
                                return;
                            }
                        }
                    }
                }
                // add to remove store
                if (!hasKey)
                {
                    m_UnRegisterList.Add(msgId, new List<Action<T>>());
                }
                m_UnRegisterList[msgId].Add(msgCallback);
            }
            else
            {
                if (m_CallbackStore.ContainsKey(msgId))
                {
                    m_CallbackStore[msgId].Remove(msgCallback);
                }
            }

        }
        public void UnregistAllEvent(int msgId)
        {
            List<Action<T>> list = null;
            if (m_CallbackStore.TryGetValue(msgId, out list))
            {
                for (int i = 0; i < list.Count; ++i)
                {
                    UnregistEvent(msgId, list[i]);
                }
            }
        }
        public bool IsContainsKey(int id)
        {
            return m_CallbackStore.ContainsKey(id);
        }
        public int GetListCount(int id)
        {
            List<Action<T>> list = null;
            if (m_CallbackStore.TryGetValue(id, out list))
            {
                return list.Count;
            }
            return 0;
        }
        private void DoUnregister()
        {
            // handler add first
            if (m_RegisterList.Count != 0)
            {
                foreach (var elem in m_RegisterList)
                {
                    List<Action<T>> tmpList = null;
                    if (m_CallbackStore.TryGetValue(elem.Key, out tmpList))
                    {
                        foreach (var tmpElem in elem.Value)
                        {
                            for (int i = 0; i < tmpList.Count; ++i)
                            {
                                if (tmpList[i] == tmpElem)
                                {
                                    continue;
                                }
                            }
                            tmpList.Add(tmpElem);
                        }
                    }
                    else
                    {
                        m_CallbackStore.Add(elem.Key, elem.Value);
                    }
                }
            }
            m_RegisterList.Clear();

            //handler remove
            if (m_UnRegisterList.Count != 0)
            {
                foreach (var elem in m_UnRegisterList)
                {
                    var tmpList = elem.Value;
                    List<Action<T>> msgList = null;
                    if (m_CallbackStore.TryGetValue(elem.Key, out msgList))
                    {
                        for (int i = 0; i < tmpList.Count; ++i)
                        {
                            msgList.Remove(tmpList[i]);
                        }
                    }
                }
                m_UnRegisterList.Clear();
            }
        }
    }
    public class RegisterDictionaryTemplate_Linkedlist<T>
    {
        private Dictionary<int, LinkedList<Action<T>>> m_CallbackStore;
        private LinkedList<Action<T>> m_UpdatingList;
        private int m_iCurrentUpdatingListId;

        public RegisterDictionaryTemplate_Linkedlist()
        {
            m_CallbackStore = new Dictionary<int, LinkedList<Action<T>>>();
            m_UpdatingList = new LinkedList<Action<T>>();
        }
        public void Update(int id, T param)
        {
            LinkedList<Action<T>> list = null;
            if (!m_CallbackStore.TryGetValue(id, out list) || list.Count == 0)
            {
                //empty msg list                    
                Debug.LogWarning("empty list  " + id.ToString());
                return;
            }
            m_iCurrentUpdatingListId = id;
            m_UpdatingList = new LinkedList<Action<T>>();
            do
            {
                var elem = list.First;
                list.RemoveFirst();
                m_UpdatingList.AddLast(elem.Value);

                try
                {
                    // do callback
                    if (null != elem.Value)
                    {
                        elem.Value(param);
                    }
                    else
                    {
                        //log error                        
                        Debug.LogWarning("null of call back fun" + id.ToString());
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("Wrong msg callback" + id + "error log: " + e.Message);
                    Debug.LogException(e);
                }
            } while (list.Count != 0);
            if (m_CallbackStore.ContainsKey(id))
            {
                m_CallbackStore[id] = m_UpdatingList;
            }
            m_UpdatingList = null;
            m_iCurrentUpdatingListId = -1;
        }
        public void RegistEvent(int msgId, Action<T> msgCallback)
        {
            LinkedList<Action<T>> list = null;
            if (!m_CallbackStore.TryGetValue(msgId, out list))
            {
                list = new LinkedList<Action<T>>();
                list.AddLast(msgCallback);
                m_CallbackStore.Add(msgId, list);
                return;
            }
            if (m_iCurrentUpdatingListId == msgId && null != m_UpdatingList)
            {
                foreach (var elem in m_UpdatingList)
                {
                    if (elem == msgCallback)
                    {
                        return;
                    }
                }
            }
            foreach (var elem in list)
            {
                if (elem == msgCallback)
                {
                    return;
                }
            }
            list.AddLast(msgCallback);
        }
        public void UnregistEvent(int msgId, Action<T> msgCallback)
        {
            LinkedList<Action<T>> list = null;
            if (!m_CallbackStore.TryGetValue(msgId, out list))
            {
                return;
            }
            if (m_iCurrentUpdatingListId == msgId && null != m_UpdatingList)
            {
                int count = m_UpdatingList.Count;
                for (int i = 0; i < count; ++i)
                {
                    var elem = m_UpdatingList.First;
                    m_UpdatingList.RemoveFirst();

                    if (elem.Value == msgCallback)
                    {
                        break;
                    }
                    m_UpdatingList.AddLast(elem.Value);
                }
            }
            int listcount = list.Count;
            for (int i = 0; i < listcount; ++i)
            {
                var elem = list.First;
                list.RemoveFirst();

                if (elem.Value == msgCallback)
                {
                    break;
                }
                list.AddLast(elem.Value);
            }
            if (list.Count == 0)
            {
                m_CallbackStore.Remove(msgId);
            }
        }
        public void UnregistAllEvent(int msgId)
        {
            LinkedList<Action<T>> list = null;
            if (!m_CallbackStore.TryGetValue(msgId, out list))
            {
                return;
            }
            m_CallbackStore.Remove(msgId);
            list.Clear();
        }
        public bool IsContainsKey(int id)
        {
            return m_CallbackStore.ContainsKey(id);
        }
        public int GetListCount(int id)
        {
            LinkedList<Action<T>> list = null;
            if (m_CallbackStore.TryGetValue(id, out list))
            {
                return list.Count;
            }
            return 0;
        }
        public int GetCallbackListCount()
        {
            return m_CallbackStore.Keys.Count;
        }
    }
}