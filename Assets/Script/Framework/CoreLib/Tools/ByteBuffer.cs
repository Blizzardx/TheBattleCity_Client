using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Common.Tools
{
    public class ByteBuffer
    {
        private byte[] m_buf;
        private int m_currentLength;
        private int m_currentPosition;
        private int m_capacity;

        public ByteBuffer(int capacity)
        {
            m_buf = new byte[capacity];
            m_capacity = capacity;
        }

        public ByteBuffer(byte[] bytes)
        {
            m_buf = bytes;
            m_capacity = bytes.Length;
        }

        public int Length
        {
            get { return m_currentLength; }
        }

        public int Position
        {
            get { return m_currentPosition; }
            set { m_currentPosition = value; }
        }

        public int Capacity
        {
            get { return m_capacity; }
        }

        public void PushByte(byte b)
        {
            m_buf[m_currentLength++] = b;
        }

        public void PushBytes(byte[] bytes)
        {
            bytes.CopyTo(m_buf, m_currentLength);
            m_currentLength += bytes.Length;
        }

        public void PushBytes(byte[] bytes, int size)
        {
            Array.Copy(bytes, 0, m_buf, m_currentLength, size);
            m_currentLength += size;
        }

        public byte[] Read(int len)
        {
            byte[] bytes = new byte[len];
            Array.Copy(m_buf, m_currentPosition, bytes, 0, len);
            m_currentPosition += len;
            return bytes;
        }

        public byte ReadByte()
        {
            return m_buf[m_currentPosition++];
        }

        public void Flip()
        {
            //todo
        }

        public void Compact()
        {
            if (m_currentPosition == 0)
            {
                return;
            }

            int _remaining = this.Remaining();
            if (_remaining <= 0)
            {
                this.Clear();
                return;
            }

            byte[] leftData = Read(_remaining);
            Clear();
            PushBytes(leftData);
        }

        public void Clear()
        {
            m_buf = new byte[m_capacity];
            m_currentLength = 0;
            m_currentPosition = 0;
        }

        public int Remaining()
        {
            return m_currentLength - m_currentPosition;
        }

        public bool HasRemaining()
        {
            return m_currentLength > m_currentPosition;
        }

        public void Jump(int offset)
        {
            m_currentPosition = offset;
        }
    }
    //class ByteBuffer
    //{
    //    private MemoryStream memBuf;

    //    public ByteBuffer(int maxSize)
    //    {
    //        this.memBuf = new MemoryStream(maxSize);
    //    }

    //    public void Put(byte[] data, int length)
    //    {
    //        memBuf.Write(data, 0, length);
    //    }

    //    public void Put(byte[] data)
    //    {
    //        memBuf.Write(data, 0, data.Length);
    //    }

    //    public void Read(byte[] data)
    //    {
    //        memBuf.Read(data, 0, data.Length);
    //    }

    //    public byte ReadByte()
    //    {
    //        int n = memBuf.ReadByte();
    //        return Convert.ToByte(n);
    //    }

    //    public void Poll(int count)
    //    {
    //        for (int i = 0; i < count; i++)
    //        {
    //            memBuf.ReadByte();
    //        }
    //    }

    //    public void Flip()
    //    {
    //        this.memBuf.Seek(0, SeekOrigin.Begin);
    //    }
    //    public int Capcity()
    //    {
    //        return memBuf.Capacity;
    //    }
    //    public void Compact()
    //    {
    //        if (memBuf.Position == 0)
    //        {
    //            return;
    //        }

    //        long _remaining = this.Remaining();
    //        if (_remaining <= 0)
    //        {
    //            this.Clear();
    //            return;
    //        }

    //        byte[] _leftData = new byte[_remaining];
    //        this.Read(_leftData);
    //        this.Clear();
    //        this.Put(_leftData);
    //    }

    //    public void Clear()
    //    {
    //        memBuf.Seek(0, SeekOrigin.Begin);
    //        memBuf.SetLength(0);
    //    }

    //    public long Remaining()
    //    {
    //        return memBuf.Length - memBuf.Position;
    //    }

    //    public Boolean HasRemaining()
    //    {
    //        return memBuf.Length > memBuf.Position;
    //    }

    //    public long Position()
    //    {
    //        return memBuf.Position;
    //    }

    //    public long Length()
    //    {
    //        return memBuf.Length;
    //    }

    //    public void SetPosition(long position)
    //    {
    //        memBuf.Seek(position, SeekOrigin.Begin);
    //    }

    //    public void Jump(long offset)
    //    {
    //        memBuf.Seek(memBuf.Position + offset, SeekOrigin.Begin);
    //    }
    //}
}
