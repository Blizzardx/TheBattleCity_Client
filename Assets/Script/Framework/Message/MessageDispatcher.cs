using System;
using Common.Component;
using Common.Tool;
using Framework.Common;
using Framework.Queue;
using UnityEngine;

namespace Framework.Message
{
    public class MessageDispatcher : Singleton<MessageDispatcher>
    {
        private RegisterListTemplate<IMessage>                 m_AllMessageListenerList; 
        private RegisterDictionaryTemplate<IMessage>           m_MsgCallList; 

        public MessageDispatcher()
        {
            m_MsgCallList           = new RegisterDictionaryTemplate<IMessage>();
            m_AllMessageListenerList = new RegisterListTemplate<IMessage>();

        }
        public void Update()
        {
            m_MsgCallList.BeginUpdate();
            int errorId = 0;
            try
            {
                //process msglist message
                for (int i = 0; i < 16; ++i)
                {
                    var elem = MessageQueue.Instance.Dequeue() ;
                    if (null == elem)
                    {
                        break;
                    }

                    m_AllMessageListenerList.ExcutionUpdateList(elem);

                    errorId = elem.GetMessageId();
                    m_MsgCallList.Update(elem.GetMessageId(), elem);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                //log error
                Debug.LogError("Wrong msg callback" + errorId + "error log: " + e.Message);
            }
            m_MsgCallList.EndUpdate();
        }

        public void BroadcastMessage(int id, object msgValue)
        {
            BroadcastMessage(new MessageElement(id,msgValue));
        }
        public void BroadcastMessage(IMessage msgBody)
        {
            if (!m_MsgCallList.IsContainsKey(msgBody.GetMessageId()))
            {
                return;
            }
            MessageQueue.Instance.Enqueue(msgBody);
        }
        public void RegisterAllMessageListener(Action<IMessage> msgCallBack)
        {
            m_AllMessageListenerList.RegisterToUpdateList(msgCallBack);
        }
        public void UnRegisterAllMesssageListener(Action<IMessage> msgCallBack)
        {
            m_AllMessageListenerList.UnRegisterFromUpdateList(msgCallBack);
        }
        public void RegistMessage(int msgId, Action<IMessage> msgCallback)
        {
            m_MsgCallList.RegistEvent(msgId, msgCallback);
        }
        public void UnregistMessage(int msgId, Action<IMessage> msgCallback)
        {
            m_MsgCallList.UnregistEvent(msgId, msgCallback);
        }
        public void UnregistMessageAll(int msgId)
        {
            m_MsgCallList.UnregistAllEvent(msgId);
        }
    }
}
