using System;
using UnityEngine;
using System.Collections.Generic;

public class MessageObject
{
    public object   msgValue;
    public int      msgId;

    public MessageObject()
    {
    }
    public MessageObject(int msgId, object mv)
    {
        this.msgValue = mv;
        this.msgId = msgId;
    }
}
public class MessageManager : Singleton<MessageManager>
{
    private MessageQueue                                        m_MsgList;
    private RegisterListTemplate<MessageObject>                 m_AllMessageListenerList; 
    private RegisterDictionaryTemplate<MessageObject>           m_MsgCallList; 

    public void Initialize()
    {
        m_MsgCallList = new RegisterDictionaryTemplate<MessageObject>();
        m_MsgList               = new MessageQueue();
        m_AllMessageListenerList = new RegisterListTemplate<MessageObject>();

        m_MsgList.Initialize();
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
                MessageObject elem = m_MsgList.Poll();
                if (null == elem)
                {
                    break;
                }

                m_AllMessageListenerList.ExcutionUpdateList(elem);

                errorId = elem.msgId;
                m_MsgCallList.Update(elem.msgId, elem);
            }
        }
        catch (Exception e)
        {
            //log error
            Debuger.LogError("Wrong msg callback" + errorId + "error log: " + e.Message);
        }
        m_MsgCallList.EndUpdate();
    }
    public void AddToMessageQueue(MessageObject msgBody)
    {
        if (!m_MsgCallList.IsContainsKey(msgBody.msgId))
        {
            return;
        }
        m_MsgList.Offer(msgBody);
    }
    public void RegisterAllMessageListener(Action<MessageObject> msgCallBack)
    {
        m_AllMessageListenerList.RegisterToUpdateList(msgCallBack);
    }
    public void UnRegisterAllMesssageListener(Action<MessageObject> msgCallBack)
    {
        m_AllMessageListenerList.UnRegisterFromUpdateList(msgCallBack);
    }
    public void RegistMessage(int msgId, Action<MessageObject> msgCallback)
    {
        m_MsgCallList.RegistEvent(msgId, msgCallback);
    }
    public void UnregistMessage(int msgId, Action<MessageObject> msgCallback)
    {
        m_MsgCallList.UnregistEvent(msgId, msgCallback);
    }
    public void UnregistMessageAll(int msgId)
    {
        m_MsgCallList.UnregistAllEvent(msgId);
    }
}
