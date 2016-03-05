using System;
using System.IO.Compression;
using System.IO;
using NetWork;
using NetFramework;

namespace Cache
{
    public class TranscoderManager
    {
        public static readonly TranscoderManager Instance = new TranscoderManager();

        private const short MAGIC_NUMBER = 0x7A8B;

        private const short COMPRESSION_LEN = 100;

        private ByteBuffer buffer = ByteBuffer.Allocate(256);

        private TranscoderManager()
        {

        }

        public byte[] Encode(ITranscoder transcoder, bool canCompress, object value)
        {
            lock(this)
            {
                buffer.Clear();
                transcoder.Encode(buffer, value);

                byte[] bytes = buffer.ToArray();
                byte compress = 0;
                /*if (canCompress && bytes.Length > COMPRESSION_LEN)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (GZipStream gipStream = new GZipStream(ms, CompressionMode.Compress))
                        {
                            gipStream.Write(bytes, 0, bytes.Length);
                        }
                        bytes = ms.ToArray();
                        compress = 1;
                    }
                }*/

                buffer.Clear();

                buffer.WriteShort(MAGIC_NUMBER);
                buffer.WriteByte(compress);
                buffer.WriteInt(bytes.Length);
                buffer.WriteBytes(bytes);

                byte[] newBytes = buffer.ToArray();
                
                return newBytes;
            }
        }

        public object Decode(ITranscoder transcoder, byte[] bytes,bool isNativeFiile)
        {
            lock(this)
            {
                buffer.Clear();
                if (isNativeFiile)
                {
                    return null;
                }

                buffer.WriteBytes(bytes);

                if(buffer.ReadShort() != MAGIC_NUMBER)
                {
                    throw new ApplicationException("bytes have not used TranscoderManager encode.");
                }
                byte compress = buffer.ReadByte();
                int len = buffer.ReadInt();

                if(compress == 1)
                {
                    byte[] tempBytes = new byte[len];
                    buffer.ReadBytes(tempBytes, 0, len);
                    buffer.Clear();

                    using (MemoryStream ms = new MemoryStream(tempBytes))
                    {
                        ms.Flush();
                        using (GZipStream gipStream = new GZipStream(ms, CompressionMode.Decompress))
                        {
                            using (MemoryStream outBuffer = new MemoryStream())
                            {
                                byte[] block = new byte[1024];
                                while (true)
                                {
                                    int bytesRead = gipStream.Read(block, 0, block.Length);
                                    if (bytesRead <= 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        outBuffer.Write(block, 0, bytesRead);
                                    }
                                }
                                buffer.WriteBytes(outBuffer.ToArray());
                            }
                        }
                    }
                }
                return transcoder.Decode(buffer);
            }
        }

        public bool IsTranscoderEncode(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
            {
                return false;
            }
            byte[] b = new byte[2];
            Array.Copy(bytes, 0, b, 0, b.Length);
            if (ByteArrayUtil.bytesToShort(b) == MAGIC_NUMBER)
            {
                return true;
            }
            return false;
        }
    }
}
