using Framework.Common;

namespace Framework.Message
{
    public class MessageElement : IMessage
    {
        private object msgBody;
        private int msgId;

        public MessageElement(int msgId, object mv)
        {
            this.msgBody = mv;
            this.msgId = msgId;
        }
        public int GetMessageId()
        {
            return msgId;
        }

        public object GetMessageBody()
        {
            return msgBody;
        }

        public void SetMessageBody(object msg)
        {
            msgBody = msg;
        }
    }
}
