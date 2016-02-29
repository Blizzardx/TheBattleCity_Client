using System.Net.Sockets;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Thrift.Protocol;

public class MessageObject
{
    public object       msgValue;
    public MessageID    msgId;
    public Socket       m_ClientSocket;

    public MessageObject()
    {
    }
    public MessageObject(MessageID msgId, object mv,Socket clientSocket = null)
    {
        this.msgValue   = mv;
        this.msgId      = msgId;
        m_ClientSocket = clientSocket;
    }
}
public class MessageCenter
{
    public delegate void MsgCallback(MessageObject eb);

    protected void Initialize()
    {
        m_MsgCallbackStore      = new Dictionary<MessageID, List<MsgCallback>>();
        m_MsgList               = new List<MessageObject>();
        m_DelayMsgList          = new List<MessageObject>();
        m_RemovingStore         = new List<KeyValuePair<MessageID, MsgCallback>>();
        m_bIsProcessingMsgList  = false;
    }
    private Dictionary<MessageID, List<MsgCallback>>                    m_MsgCallbackStore;
    private List<MessageObject>                                         m_MsgList;
    private List<MessageObject>                                         m_DelayMsgList;
    private bool                                                        m_bIsProcessingMsgList;
    private List<KeyValuePair<MessageID, MsgCallback>>                  m_RemovingStore; 

    //regist message
    public void RegistMessage(MessageID msgId, MsgCallback msgCallback)
    {
        if (null == msgCallback)
        {
            Debug.LogError("msg call back can't be null !!!" + msgId.ToString());
        }
        if(!m_MsgCallbackStore.ContainsKey(msgId))
        {
            m_MsgCallbackStore.Add(msgId, new List<MsgCallback>());
            m_MsgCallbackStore[msgId].Add(msgCallback);
        }
        else
        {
            for(int i=0;i<m_MsgCallbackStore[msgId].Count;++i)
            {
                if(m_MsgCallbackStore[msgId][i] == msgCallback )
                {
                    return;
                }
            }
            m_MsgCallbackStore[msgId].Add(msgCallback);
        }
    }
    public void UnregistMessage(MessageID msgId, MsgCallback msgCallback)
    {
        m_RemovingStore.Add(new KeyValuePair<MessageID, MsgCallback>(msgId, msgCallback));
    }
    public void OnTick()
    {
        lock (this)
        {
            if (m_MsgList.Count == 0)
            {
                foreach (MessageObject eb in m_DelayMsgList)
                {
                    m_MsgList.Add(eb);
                }
                m_DelayMsgList.Clear();
                return;
            }
            m_bIsProcessingMsgList = true;

            //process msglist message
            foreach (MessageObject elem in m_MsgList)
            {
                //try
                //{
                    if (m_MsgCallbackStore.ContainsKey(elem.msgId))
                    {
                        foreach(MsgCallback fun in m_MsgCallbackStore[elem.msgId])
                        {
                            if( null != fun )
                            {
                                fun(elem);
                            }
                            else
                            {
                                //log error                        
                                Debug.LogError("null of call back fun" + elem.msgId.ToString()); 
                            }
                        }
                    }
                    else
                    {
                        //empty msg list                    
                        Debug.LogError("empty msg list  " + elem.msgId.ToString());
                    }
                //}
                //catch (Exception e)
                //{
                //    //log error
                //    Debug.LogError("Wrong msg callback" + elem.msgId.ToString() + "error log: "+e.Message);
                //}
            }
            m_MsgList.Clear();
            m_bIsProcessingMsgList = false;

            //do remove
            for (int i = 0; i < m_RemovingStore.Count; ++i)
            {
                List<MsgCallback> targetList = null;
                m_MsgCallbackStore.TryGetValue(m_RemovingStore[i].Key, out targetList);
                if (null != targetList && m_MsgCallbackStore.Count > 0)
                {
                    targetList.Remove(m_RemovingStore[i].Value);
                }
            }
            m_RemovingStore.Clear();
        }
    }
    public void BoradCastCustomMsg(MessageObject msg)
    {
        BroadCastMsg(new List<MessageObject>(){msg});
    }
    public void BroadCastMsg(List<MessageObject> msgs)
    {
        //id to show error 
        MessageID currentId = MessageID.G_Position;

        lock (this)
        {                     
            //process msg
            try
            {
                for (int i = 0; i < msgs.Count; ++i)
                {
                    currentId = msgs[i].msgId;
                    if (!m_MsgCallbackStore.ContainsKey(msgs[i].msgId))
                    {
                        return;
                    }
                    if (m_bIsProcessingMsgList)
                    {
                        m_DelayMsgList.Add(msgs[i]);
                    }
                    else
                    {
                        m_MsgList.Add(msgs[i]);
                    }
                }
            }
            catch
            {
                Debug.LogError("Don't exit msg id " + currentId.ToString());
            }
            msgs = null;
        }
    }
}

public class ClientMessageCenter : MessageCenter
{
    public static ClientMessageCenter GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new ClientMessageCenter();
                m_Instance.Initialize();
            }
            return m_Instance;
        }
    }
    private static ClientMessageCenter m_Instance = null;
}

public class LocalServerMessageCenter : MessageCenter
{
    public static LocalServerMessageCenter GetInstance
    {
        get
        {
            if (null == m_Instance)
            {
                m_Instance = new LocalServerMessageCenter();
                m_Instance.Initialize();
            }
            return m_Instance;
        }
    }
    private static LocalServerMessageCenter m_Instance = null;
}