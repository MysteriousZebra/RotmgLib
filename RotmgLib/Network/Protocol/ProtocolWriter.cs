using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RotmgLib.Network.Protocol
{
    public delegate void WriteItemDelegate<T>(T item);

    class ProtocolWriter
    {
        private MemoryStream m_Stream;

        public ProtocolWriter()
        {
            this.m_Stream = new MemoryStream();
        }

        public byte[] GetBuffer()
        {
            return this.m_Stream.GetBuffer();
        }

        public void Write(byte[] value)
        {
            this.m_Stream.Write(value, 0, value.Length);
        }

        public void Write(int value) // copy for most data types
        {
            byte[] buffer = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            this.Write(buffer);
        }

        public void Write(uint value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            this.Write(buffer);
        }

        public void Write(ushort value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            this.Write(buffer);
        }

        public void Write(short value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            this.Write(buffer);
        }

        public void Write(float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            this.Write(buffer);
        }

        public void Write(double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            this.Write(buffer);
        }

        public void Write(byte value)
        {
            this.Write(new byte[] { value });
        }

        public void Write(sbyte value)
        {
            this.Write(unchecked((byte)value));
        }

        public void Write(bool value)
        {
            this.Write(BitConverter.GetBytes(value));
        }

        public void Write(string value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);

            this.Write((short)buffer.Length);
            this.Write(buffer);
        }

        public void Write<T>(T[] vector, WriteItemDelegate<T> write_item)
        {
            this.Write((short)vector.Length);

            foreach (T item in vector)
                write_item(item);
        }

        public void Write(TilePoint value)
        {
            this.Write(value.X);
            this.Write(value.Y);
        }

        public void Write(StatData value)
        {
            this.Write(value.Type);

            if (value.Type == 31)
                this.Write(value.StringData);
            else
                this.Write(value.IntData);
        }

        public void Write(ObjectStatusData value)
        {
            this.Write(value.ObjectId);
            this.Write(value.TilePosition);
            this.Write<StatData>(value.Data, new WriteItemDelegate<StatData>(this.Write));
        }

        public void Write(GameObject value)
        {
            this.Write(value.ObjectType);
            this.Write(value.Status);
        }

        public void Write(SlotObject value)
        {
            this.Write(value.ObjectId);
            this.Write(value.SlotId);
            this.Write(value.ObjectType);
        }

        public void Write(TradeItem value)
        {
            this.Write(value.Item);
            this.Write(value.SlotType);
            this.Write(value.Tradeable);
            this.Write(value.Included);
        }
    }
}
