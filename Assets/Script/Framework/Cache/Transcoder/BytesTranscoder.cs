using NetWork;


namespace  Cache.Transcoder
{
    public class BytesTranscoder:ITranscoder
    {
        public void Encode(ByteBuffer buffer, object value)
        {
            byte[] byteBuf = (byte[])value;
            buffer.WriteInt(byteBuf.Length);
            buffer.WriteBytes(byteBuf);
        }

        public object Decode(ByteBuffer buffer)
        {
            byte[] bytes = new byte[buffer.ReadInt()];
            buffer.ReadBytes(bytes, 0, bytes.Length);
            return bytes;
        }
    }
}

