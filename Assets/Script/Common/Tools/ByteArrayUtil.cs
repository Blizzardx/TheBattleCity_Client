namespace Common.Tools
{
    public class ByteArrayUtil
    {
        public static byte[] intToBytes(int x)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(x >> 24);
            bytes[1] = (byte)(x >> 16);
            bytes[2] = (byte)(x >> 8);
            bytes[3] = (byte)(x >> 0);
            return bytes;
        }
        public static byte[] shortToByteArray(short s)
        {
            byte[] targets = new byte[2];

            targets[0] = (byte)(s >> 8);
            targets[1] = (byte)(s & 255);

            return targets;
        }
        public static int bytesToInt(byte[] bytes,int index = 0)
        {
            return (int)((((bytes[index + 0] & 0xFF) << 24) | ((bytes[index + 1] & 0xFF) << 16)
                           | ((bytes[index + 2] & 0xFF) << 8) | ((bytes[index + 3] & 0xFF) << 0)));
        }

        public static short bytesToShort(byte[] bytes, int index = 0)
        {
            return (short)(( ((bytes[index + 0] & 0xFF) << 8) | ((bytes[index + 1] & 0xFF) << 0)));
        }
    }
}