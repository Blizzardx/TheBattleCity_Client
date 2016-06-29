using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Tools
{
    class ByteBuffer
    {
        private MemoryStream memBuf;

        public ByteBuffer(int maxSize)
        {
            this.memBuf = new MemoryStream(maxSize);
        }

        public void Put(byte[] data, int length)
        {
            memBuf.Write(data, 0, length);
        }

        public void Put(byte[] data)
        {
            memBuf.Write(data, 0, data.Length);
        }

        public void Read(byte[] data)
        {
            memBuf.Read(data, 0, data.Length);
        }

        public byte ReadByte()
        {
            int n = memBuf.ReadByte();
            return Convert.ToByte(n);
        }

        public void Poll(int count)
        {
            for (int i = 0; i < count; i++)
            {
                memBuf.ReadByte();
            }
        }

        public void Flip()
        {
            this.memBuf.Seek(0, SeekOrigin.Begin);
        }

        public void Compact()
        {
            if (memBuf.Position == 0)
            {
                return;
            }

            long _remaining = this.Remaining();
            if (_remaining <= 0)
            {
                this.Clear();
                return;
            }

            byte[] _leftData = new byte[_remaining];
            this.Read(_leftData);
            this.Clear();
            this.Put(_leftData);
        }

        public void Clear()
        {
            memBuf.Seek(0, SeekOrigin.Begin);
            memBuf.SetLength(0);
        }

        public long Remaining()
        {
            return memBuf.Length - memBuf.Position;
        }

        public bool HasRemaining()
        {
            return memBuf.Length > memBuf.Position;
        }

        public long Position()
        {
            return memBuf.Position;
        }

        public long Length()
        {
            return memBuf.Length;
        }
         
        public int Capcity()
        {
            return memBuf.Capacity;
        }
        public void SetPosition(long position)
        {
            memBuf.Seek(position, SeekOrigin.Begin);
        }

        public void Jump(long offset)
        {
            memBuf.Seek(memBuf.Position + offset, SeekOrigin.Begin);
        }
    }
}
