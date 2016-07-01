using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Component;
using UnityEngine;

namespace Framework.Event
{
    public class EventDispatchTool
    {
        private RegisterDictionaryTemplate_Linkedlist<EventElement>    m_MsgCallList;
        private TemplateQueue<EventElement>                 m_EventQueue;

        public EventDispatchTool()
        {
            m_MsgCallList = new RegisterDictionaryTemplate_Linkedlist<EventElement>();
            m_EventQueue = new TemplateQueue<EventElement>();
            m_EventQueue.Initialize();
        }
        public void Broadcast(int id, object param)
        {
            Broadcast(new EventElement(id, param));
        }
        public void Broadcast(EventElement eventBody)
        {
            int errorId = 0;
            try
            {
                if (null != eventBody)
                {
                    errorId = eventBody.eventId;
                    m_MsgCallList.Update(eventBody.eventId, eventBody);
                }
            }
            catch (Exception e)
            {
                //log error
                Debug.LogError("Wrong msg callback" + errorId + "error log: " + e.Message);
            }
        }
        public void BroadcastAsync(int id, object param)
        {
            BroadcastAsync(new EventElement(id, param));
        }
        public void BroadcastAsync(EventElement eventBody)
        {
            if (!m_MsgCallList.IsContainsKey(eventBody.eventId))
            {
                return;
            }
            m_EventQueue.Enqueue(eventBody);
        }
        public void RegistEvent(int id, Action<EventElement> eventCallback)
        {
            m_MsgCallList.RegistEvent(id, eventCallback);
        }
        public void UnregistEvent(int id, Action<EventElement> eventCallback)
        {
            m_MsgCallList.UnregistEvent(id, eventCallback);
        }
        public void UnregistEventAll(int id)
        {
            m_MsgCallList.UnregistAllEvent(id);
        }
        public void Update()
        {
            int errorId = 0;
            try
            {
                //process msglist message
                for (int i = 0; i < 16; ++i)
                {
                    var elem = m_EventQueue.Dequeue();
                    if (null == elem)
                    {
                        break;
                    }
                    errorId = elem.eventId;
                    m_MsgCallList.Update(elem.eventId, elem);
                }
            }
            catch (Exception e)
            {
                //log error
                Debug.LogError("Wrong msg callback" + errorId + "error log: " + e.Message);
                Debug.LogException(e);
            }
        }
        public int GetCallbackListCount()
        {
            return m_MsgCallList.GetCallbackListCount();
        }
    }
}
