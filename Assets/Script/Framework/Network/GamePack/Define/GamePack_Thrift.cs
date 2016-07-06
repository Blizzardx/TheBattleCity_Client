using System;
using Common.Tools;
using Communication;
using Framework.Common;
using Framework.Message;
using Thrift.Protocol;
using UnityEngine;

namespace Framework.Network.GamePack
{
    public class GamePack_Thrift : IGamePack
    {
        public enum Status
        {
            Id,
            Prefix,
            HeaderLength,
            Header,
            BodyLength,
            Body,
        }

        // recieve
        private int     m_iMessageId;
        private short   m_iHeaderLength;
        private byte[]  m_Header;
        private int     m_iBodyLength;
        private TBase   m_Body;
        private bool    m_bWithError;
        private Status  m_Status;
        private ByteBuffer m_RevBuff;
        private MessageElement m_RecievedMsg;

        private const int RECIEVE_BUFFER_SIZE   = 64*1024*4;
        private const int MESSAGE_ID_SIZE       = 4;
        private const int HEADER_LENGTH_SIZE    = 2;
        private const int BODY_LENGTH_SIZE      = 4;

        public GamePack_Thrift()
        {
            m_RevBuff = new ByteBuffer(RECIEVE_BUFFER_SIZE);
        }
        public void ClearBuffer()
        {
            m_RecievedMsg = null;
            m_iMessageId = 0;
            m_Header = null;
            m_iHeaderLength = 0;
            m_iBodyLength = 0;
            m_Body = null;
            m_bWithError = false;
            m_Status = Status.Id;
            m_RevBuff.Flip();
        }
        public void AddToBuffer(byte[] source, int size)
        {
            if (m_RevBuff.Length() + size > m_RevBuff.Capcity())
            {
                m_RevBuff.Compact();
                m_RevBuff.Flip();
            }
            long pos = m_RevBuff.Position();
            m_RevBuff.SetPosition(m_RevBuff.Length());
            m_RevBuff.Put(source, size);
            m_RevBuff.SetPosition(pos);
        }

        #region decode
        /**
        * 
        * message define 
        * 
        * message id[4] + message prefix[4] + message header length[2] + message header body[...] + message body length[4] + message body[...]
        * 
        * [(4) + (4) + (2) + (...) + (4) + {...)]
        * 
        **/
        public IMessage Decode()
        {
            while (m_RecievedMsg == null)
            {
                switch (m_Status)
                {
                    case Status.Id:
                        if (!DecodeId())
                        {
                            return null;
                        }
                        break;
                    case Status.Prefix:
                        if (!DecodePrefix())
                        {
                            return null;
                        }
                        break;
                    case Status.HeaderLength:
                        if (!DecodeHeaderLength())
                        {
                            return null;
                        }
                        break;
                    case Status.Header:
                        if (!DecodeHeader())
                        {
                            return null;
                        }
                        break;
                    case Status.BodyLength:
                        if (!DecodeBodyLength())
                        {
                            return null;
                        }
                        break;
                    case Status.Body:
                        if (!DecodeBody())
                        {
                            return null;
                        }
                        break;
                }

            }
            var res = m_RecievedMsg;
            m_RecievedMsg = null;
            var currentTime = DateTime.Now;
            Debug.Log("rec msg " + res.GetMessageBody().ToString() + " at time " + currentTime.Second + " : " + currentTime.Millisecond);
            return res;
        }

        private bool DecodeId()
        {
            if (m_RevBuff.Remaining() < MESSAGE_ID_SIZE)
            {
                return false;
            }
            byte[] msgId = new byte[MESSAGE_ID_SIZE];
            m_RevBuff.Read(msgId);
            m_iMessageId = ByteArrayUtil.bytesToInt(msgId);
            m_Status = Status.Prefix;
            return true;
        }

        private bool DecodePrefix()
        {
            if (m_RevBuff.Remaining() < MESSAGE_ID_SIZE)
            {
                return false;
            }
            byte[] prefix = new byte[MESSAGE_ID_SIZE];
            m_RevBuff.Read(prefix);
            m_Status = Status.HeaderLength;
            return true;
        }

        private bool DecodeHeaderLength()
        {
            if (m_RevBuff.Remaining() < HEADER_LENGTH_SIZE)
            {
                return false;
            }
            byte[] headerLength = new byte[HEADER_LENGTH_SIZE];
            m_RevBuff.Read(headerLength);
            m_iHeaderLength = ByteArrayUtil.bytesToShort(headerLength);
            m_Status = Status.Header;
            return true;
        }

        private bool DecodeHeader()
        {
            if (m_RevBuff.Remaining() < m_iHeaderLength)
            {
                return false;
            }
            m_Status = Status.BodyLength;
            return true;
        }

        private bool DecodeBodyLength()
        {
            if (m_RevBuff.Remaining() < BODY_LENGTH_SIZE)
            {
                return false;
            }
            byte[] bodyLength = new byte[BODY_LENGTH_SIZE];
            m_RevBuff.Read(bodyLength);
            m_iBodyLength = ByteArrayUtil.bytesToInt(bodyLength);
            m_Status = Status.Body;
            return true;
        }

        private bool DecodeBody()
        {
            if (m_RevBuff.Remaining() < m_iBodyLength)
            {
                return false;
            }
            byte[] body = new byte[m_iBodyLength];
            m_RevBuff.Read(body);
            try
            {

                m_Body = null;
                Type tmpType;
                if (!ThriftMessageHelper.Get_REQ_ID_MSG().TryGetValue(m_iMessageId, out tmpType))
                {
                    Debug.LogError("Can't decode message " + m_iMessageId);
                }
                else
                {
                    m_Body = Activator.CreateInstance(tmpType) as TBase;
                    ThriftSerialize.DeSerialize(m_Body, body);
                    m_RecievedMsg = new MessageElement(m_iMessageId, m_Body);
                }
            }
            catch (Exception e)
            {
                m_RecievedMsg = null;
                Debug.LogError("error on decode message " + m_iMessageId);
                Debug.LogException(e);
            }
            m_Status = Status.Id;
            return true;
        }
        #endregion

        #region encode
        /**
         * 
         * message define 
         * 
         * message id[4] + message prefix[4] + message header length[2] + message header body[...] + message body length[4] + message body[...]
         * 
         * [(4) + (4) + (2) + (...) + (4) + {...)]
         * 
         **/
        public byte[] Encode(IMessage source)
        {
            MessageElement msg = source as MessageElement;

            int msgId = msg.GetMessageId();
            TBase msgBody = msg.GetMessageBody() as TBase;

            byte[] byteMsgBody = ThriftSerialize.Serialize(msgBody);

            int size = MESSAGE_ID_SIZE + 4 + HEADER_LENGTH_SIZE + 0 + BODY_LENGTH_SIZE + byteMsgBody.Length;
            byte[] sendBuffer = new byte[size];
            int index = 0;

            //
            int messageId = msgId;

            // push message id
            Array.Copy(ByteArrayUtil.intToBytes(messageId), 0, sendBuffer, index, 4);
            index += 4;

            // push prefix
            index += 4;

            //push header (default length = 0,body = null)
            Array.Copy(ByteArrayUtil.shortToByteArray(0), 0, sendBuffer, index, 2);
            index += 2;

            // push message body length
            Array.Copy(ByteArrayUtil.intToBytes(byteMsgBody.Length), 0, sendBuffer, index, 4);
            index += 4;

            Array.Copy(byteMsgBody, 0, sendBuffer, index, byteMsgBody.Length);
            Debug.Log("send msg" + msgBody.ToString());
            return sendBuffer;
        }
        #endregion
    }
}
