using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    public delegate object ReadItemDelegate();
    
    class ProtocolReader
    {
        private byte[] m_Packet;
        private int    m_Offset;

        public ProtocolReader(byte[] packet)
        {
            this.m_Packet = packet;
            this.m_Offset = 0;
        }

        public byte ReadByte()
        {
            return this.m_Packet[this.m_Offset++];
        }

        public bool ReadBool()
        {
            return Convert.ToBoolean(this.ReadByte());
        }

        public sbyte ReadSByte()
        {
            return unchecked((sbyte)this.ReadByte());
        }

        public byte[] ReadBytes(int size)
        {
            byte[] buffer = new byte[size];

            Array.Copy(this.m_Packet, this.m_Offset, buffer, 0, size);
            this.m_Offset += size;

            return buffer;
        }

        public short ReadInt16()
        {
            byte[] buffer = this.ReadBytes(2);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToInt16(buffer, 0);
        }

        public ushort ReadUInt16()
        {
            byte[] buffer = this.ReadBytes(2);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToUInt16(buffer, 0);
        }

        public int ReadInt32()
        {
            byte[] buffer = this.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToInt32(buffer, 0);
        }

        public uint ReadUInt32()
        {
            byte[] buffer = this.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToUInt32(buffer, 0);
        }

        public float ReadSingle()
        {
            byte[] buffer = this.ReadBytes(4);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToSingle(buffer, 0);
        }

        public double ReadDouble()
        {
            byte[] buffer = this.ReadBytes(8);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);

            return BitConverter.ToDouble(buffer, 0);
        }

        public T[] ReadVector<T>(ReadItemDelegate read_item)
        {
            T[] vector = new T[this.ReadInt16()];

            for (int i = 0; i < vector.Length; i++)
                vector[i] = (T)read_item();

            return vector;
        }

        public TilePoint ReadTilePoint()
        {
            return new TilePoint(this.ReadSingle(), this.ReadSingle());
        }

        public string ReadString()
        {
            return Encoding.UTF8.GetString(this.ReadBytes(this.ReadInt16()));
        }

        public string ReadLongString()
        {
            return Encoding.UTF8.GetString(this.ReadBytes(this.ReadInt32()));
        }

        public StatData ReadStatData()
        {
            byte type = this.ReadByte();

            if (type == 31)
                return new StatData(type, this.ReadString());

            return new StatData(type, this.ReadInt32());
        }

        public ObjectStatusData ReadObjectStatusData()
        {
            return new ObjectStatusData(this.ReadInt32(), this.ReadTilePoint(), this.ReadVector<StatData>(new ReadItemDelegate(this.ReadStatData)));
        }

        public GameObject ReadGameObject()
        {
            return new GameObject(this.ReadInt32(), this.ReadObjectStatusData());   
        }

        public SlotObject ReadSlotObject()
        {
            return new SlotObject(this.ReadInt32(), this.ReadByte(), this.ReadInt16());
        }

        public TradeItem ReadTradeItem()
        {
            return new TradeItem(this.ReadInt32(), this.ReadInt32(), this.ReadBool(), this.ReadBool());
        }
    }
}
