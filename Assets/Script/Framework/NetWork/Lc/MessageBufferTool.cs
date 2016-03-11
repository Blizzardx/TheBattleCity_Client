using System;
using System.ComponentModel;
using Communication;
using NetFramework;
using Thrift.Protocol;
using UnityEngine;
using System.Collections.Generic;

/**
 * 
 * message define 
 * 
 * message id[4] + message prefix[4] + message header length[2] + message header body[...] + message body length[4] + message body[...]
 * 
 * [(4) + (4) + (2) + (...) + (4) + {...)]
 * 
 **/

public class MessageBufferTool
{
    readonly static public int              MAXLength               = 2048 ;
    readonly static public int              MaxMsgBufferSize        = 256;
    private byte[]                          m_RecieveBuffer         ;
    private byte[]                          m_SendBuffer            ;
    private int                             m_SendBufferSize;
    private Dictionary<int, Type>           m_MessageMapIdToType;
    private Dictionary<Type, int>           m_MessageMapTypeToId;
    private bool                            m_bIsWaitingPkgComplete;
    private int                             m_nCurrentDecodeIndex;
    private List<byte>                      m_DecodingBuffer; 
 
    #region public interface
    public void Initialize()
    {
        m_RecieveBuffer         = new byte[MAXLength];
        m_SendBuffer            = new byte[MAXLength];
        m_DecodingBuffer        = new List<byte>(MAXLength);
        m_SendBufferSize        = 0;
        m_MessageMapIdToType    = new Dictionary<int, Type>();
        m_MessageMapTypeToId    = new Dictionary<Type, int>();
        m_bIsWaitingPkgComplete = false;
        m_nCurrentDecodeIndex   = 0;
    }
    public byte[] GetSendBuffer()
    {
        return m_SendBuffer;
    }
    public int GetSendBufferSize()
    {
        return m_SendBufferSize;
    }
    public byte[] GetRecieveBuffer()
    {
        return m_RecieveBuffer;
    }
    public void ClearBuffer()
    {
        for (int i = 0; i < MAXLength; ++i)
        {
            m_RecieveBuffer[i] = 0;
            m_SendBuffer[i] = 0;
        }
    }
    public void EncodeGamePackage(object messageBody)
    {
        // clear send buffer
        ResetSendBuffer();

        //
        int messageId = 0;
        if (!m_MessageMapTypeToId.TryGetValue(messageBody.GetType(), out messageId))
        {
            Debuger.LogError("Can't encode message " + messageId);
            return;
        }

        // push message id
        Array.Copy(ByteArrayUtil.intToBytes(messageId),0,m_SendBuffer,m_SendBufferSize,4);
        m_SendBufferSize += 4;

        // push prefix
        m_SendBufferSize += 4;

        //push header (default length = 0,body = null)
        Array.Copy(ByteArrayUtil.shortToByteArray(0),0, m_SendBuffer, m_SendBufferSize,2);
        m_SendBufferSize += 2;

        // push message body length
        TBase Message = messageBody as TBase;
        byte[] tmpSendBody = ThriftSerialize.Serialize(Message);
        Array.Copy(ByteArrayUtil.intToBytes(tmpSendBody.Length),0,m_SendBuffer, m_SendBufferSize,4);
        m_SendBufferSize += 4;

        Array.Copy(tmpSendBody, 0, m_SendBuffer, m_SendBufferSize, tmpSendBody.Length);
        m_SendBufferSize += tmpSendBody.Length;

        Debuger.Log("Send msg:" + messageBody.GetType().ToString());
    }
    public void RecieveMsg(int size)
    {
        if (m_DecodingBuffer.Count + size > m_DecodingBuffer.Capacity)
        {
            //reset capacity
            m_DecodingBuffer.Capacity = m_DecodingBuffer.Count + size;
        }

        //push source byte stream
        //m_DecodingBuffer.InsertRange(m_DecodingBuffer.Count, m_RecieveBuffer);
        for (int i = 0; i < size; ++i)
        {
            m_DecodingBuffer.Add(m_RecieveBuffer[i]);
        }

        do
        {
            m_nCurrentDecodeIndex = DecodeGamePackage(m_nCurrentDecodeIndex, size);

        } while (!m_bIsWaitingPkgComplete && m_nCurrentDecodeIndex < size && m_nCurrentDecodeIndex != -1);
        
        if (m_nCurrentDecodeIndex != -1)
        {
            m_DecodingBuffer.RemoveRange(0, m_nCurrentDecodeIndex);
            m_nCurrentDecodeIndex = 0;
        }
        else
        {
            m_DecodingBuffer.Clear();
            m_nCurrentDecodeIndex = 0;
        }
    }
    public void RegisterMessage(Dictionary<int, Type> MessageMapIdToType, Dictionary<Type, int> MessageMapTypeToId)
    {
        m_MessageMapIdToType = MessageMapIdToType;
        m_MessageMapTypeToId = MessageMapTypeToId;
    }
    #endregion

    #region system function
    private int DecodeGamePackage(int index,int size)
    {
        m_bIsWaitingPkgComplete = false;

        int initIndex = index;

        if (IsOutOfSize(index, size, 4))
        {
            m_bIsWaitingPkgComplete = true;
            return initIndex;
        }
        //decode message id
        int messageId = ByteArrayUtil.bytesToInt(m_DecodingBuffer.ToArray(), index);
        index += 4;

        if (IsOutOfSize(index, size, 4))
        {
            m_bIsWaitingPkgComplete = true;
            return initIndex;
        }
        //skip header
        index += 4;

        if (IsOutOfSize(index, size, 2))
        {
            m_bIsWaitingPkgComplete = true;
            return initIndex;
        }
        short headerLength = ByteArrayUtil.bytesToShort(m_DecodingBuffer.ToArray(), index);
        index += 2;

        if (IsOutOfSize(index, size, headerLength))
        {
            m_bIsWaitingPkgComplete = true;
            return initIndex;
        }
        //skip header
        index += headerLength;

        if (IsOutOfSize(index, size, 4))
        {
            m_bIsWaitingPkgComplete = true;
            return initIndex;
        }
        //get message length
        int messageLength = ByteArrayUtil.bytesToInt(m_DecodingBuffer.ToArray(), index);
        index += 4;

        if (IsOutOfSize(index, size, messageLength))
        {
            m_bIsWaitingPkgComplete = true;
            return initIndex;
        }
        byte[] messageBody = new byte[messageLength];
        Array.Copy(m_DecodingBuffer.ToArray(), index, messageBody, 0, messageLength);

        //update index
        index += messageLength;

        TBase message = null;
        Type tmpType;
        if (!m_MessageMapIdToType.TryGetValue(messageId, out tmpType))
        {
            Debuger.LogError("Can't decode message " + messageId);
            return -1;
        }
        message = Activator.CreateInstance(tmpType) as TBase;
        ThriftSerialize.DeSerialize(message, messageBody);

        //broad cast
        MessageManager.Instance.AddToMessageQueue(new MessageObject(messageId, message));

        Debuger.Log("Rec msg:" + message.GetType().Name);

        return index;
    }
    private void ResetSendBuffer()
    {
        m_SendBufferSize = 0;
    }
    private bool IsOutOfSize(int index, int size, int length)
    {
        return index + length > size;
    }

    #endregion
}
