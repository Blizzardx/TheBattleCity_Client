using UnityEngine;
using System.Collections.Generic;
using System;

namespace Common.Component
{
    public class RegisterList
    {
        private List<Action> m_UpdateList;
        private List<Action> m_UnregisterUpdateListStore;
        private bool m_bIsUpdateListBusy;

        #region public interface
        public RegisterList()
        {
            m_UpdateList = new List<Action>();
            m_UnregisterUpdateListStore = new List<Action>();
            m_bIsUpdateListBusy = false;
        }
        public void RegisterToUpdateList(Action element)
        {
            for (int i = 0; i < m_UpdateList.Count; ++i)
            {
                if (element == m_UpdateList[i])
                {
                    CheckRemovingStore(element);
                    return;
                }
            }
            m_UpdateList.Add(element);
        }
        public void UnRegisterFromUpdateList(Action element)
        {
            if (!m_bIsUpdateListBusy)
            {
                m_UpdateList.Remove(element);
            }
            else
            {
                for (int i = 0; i < m_UnregisterUpdateListStore.Count; ++i)
                {
                    if (element == m_UnregisterUpdateListStore[i])
                    {
                        return;
                    }
                }
                m_UnregisterUpdateListStore.Add(element);
            }
        }
        public void ExcutionUpdateList()
        {
            m_bIsUpdateListBusy = true;
            foreach (Action elem in m_UpdateList)
            {
                elem();
            }
            m_bIsUpdateListBusy = false;
            ExcutionUnregister();
        }
        #endregion

        #region system function
        private void CheckRemovingStore(Action element)
        {
            for (int i = 0; i < m_UnregisterUpdateListStore.Count; ++i)
            {
                if (element == m_UnregisterUpdateListStore[i])
                {
                    m_UnregisterUpdateListStore.RemoveAt(i);
                    return;
                }
            }
        }
        private void ExcutionUnregister()
        {
            if (m_UnregisterUpdateListStore.Count == 0)
            {
                return;
            }
            for (int i = 0; i < m_UnregisterUpdateListStore.Count; ++i)
            {
                m_UpdateList.Remove(m_UnregisterUpdateListStore[i]);
            }
            m_UnregisterUpdateListStore.Clear();
        }
        #endregion
    }
    public class RegisterListTemplate<T>
    {
        private List<Action<T>> m_UpdateList;
        private List<Action<T>> m_UnregisterUpdateListStore;
        private bool m_bIsUpdateListBusy;

        #region public interface
        public RegisterListTemplate()
        {
            m_UpdateList = new List<Action<T>>();
            m_UnregisterUpdateListStore = new List<Action<T>>();
            m_bIsUpdateListBusy = false;
        }
        public void RegisterToUpdateList(Action<T> element)
        {
            for (int i = 0; i < m_UpdateList.Count; ++i)
            {
                if (element == m_UpdateList[i])
                {
                    CheckRemovingStore(element);
                    return;
                }
            }
            m_UpdateList.Add(element);
        }
        public void UnRegisterFromUpdateList(Action<T> element)
        {
            if (!m_bIsUpdateListBusy)
            {
                m_UpdateList.Remove(element);
            }
            else
            {
                for (int i = 0; i < m_UnregisterUpdateListStore.Count; ++i)
                {
                    if (element == m_UnregisterUpdateListStore[i])
                    {
                        return;
                    }
                }
                m_UnregisterUpdateListStore.Add(element);
            }
        }
        public void ExcutionUpdateList(T param)
        {
            m_bIsUpdateListBusy = true;
            foreach (Action<T> elem in m_UpdateList)
            {
                elem(param);
            }
            m_bIsUpdateListBusy = false;
            ExcutionUnregister();
        }
        #endregion

        #region system function
        private void CheckRemovingStore(Action<T> element)
        {
            for (int i = 0; i < m_UnregisterUpdateListStore.Count; ++i)
            {
                if (element == m_UnregisterUpdateListStore[i])
                {
                    m_UnregisterUpdateListStore.RemoveAt(i);
                    return;
                }
            }
        }
        private void ExcutionUnregister()
        {
            if (m_UnregisterUpdateListStore.Count == 0)
            {
                return;
            }
            for (int i = 0; i < m_UnregisterUpdateListStore.Count; ++i)
            {
                m_UpdateList.Remove(m_UnregisterUpdateListStore[i]);
            }
            m_UnregisterUpdateListStore.Clear();
        }
        #endregion
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
            DoClear();
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
        private void DoClear()
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
        public int GetCallbackListCount()
        {
            return m_CallbackStore.Count;
        }
    }

}