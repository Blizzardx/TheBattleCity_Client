using Framework.Common;

namespace Framework.Network.GamePack
{
    public interface IGamePack
    {
        void ClearBuffer();
        void AddToBuffer(byte[] source,int size);
        IMessage Decode();
        byte[] Encode(IMessage source);
    }
}
