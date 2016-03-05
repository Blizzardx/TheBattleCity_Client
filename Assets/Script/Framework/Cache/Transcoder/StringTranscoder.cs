using NetWork;

namespace Cache.Transcoder
{
    public class StringTranscoder : ITranscoder
    {
        public void Encode(ByteBuffer buffer, object value)
        {
            string str = (string)value;
            buffer.WriteString(str);
        }

        public object Decode(ByteBuffer buffer)
        {
            return buffer.ReadString();
        }
    }
}
