using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServerCore
{
    public class SendBufferHelper
    {
        public static ThreadLocal<SendBuff> CurrentBuffer = new ThreadLocal<SendBuff>(() => { return null; });

        public static int ChunkSize { get; set; } = 65535 * 100;

        public static ArraySegment<byte> Open(int reserveSize)
        {
            if (CurrentBuffer.Value == null)
                CurrentBuffer.Value = new SendBuff(ChunkSize);

            if (CurrentBuffer.Value.FreeSize < reserveSize)
                CurrentBuffer.Value = new SendBuff(ChunkSize);

            return CurrentBuffer.Value.Open(reserveSize);
        }

        public static ArraySegment<byte> Close(int usedSize)
        {
            return CurrentBuffer.Value.Close(usedSize);
        }

    }

    public class SendBuff
    {
        byte[] _buffer;
        int _usedSize = 0;

        public SendBuff(int chunkSize)
        {
            _buffer = new byte[chunkSize];
        }

        public int FreeSize { get { return _buffer.Length - _usedSize; } }

        public ArraySegment<byte> Open(int reserveSize)
        {
            return new ArraySegment<byte>(_buffer, _usedSize, reserveSize);
        }

        public ArraySegment<byte> Close(int usedSize)
        {
            ArraySegment<byte> segment = new ArraySegment<byte>(_buffer, _usedSize, usedSize);
            _usedSize += usedSize;

            return segment;
        }
    }
}
