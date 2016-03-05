using NetWork;

namespace Cache
{
    public interface ITranscoder
    {
        void Encode(ByteBuffer buffer, object value);

        object Decode(ByteBuffer buffer);
    }
}
