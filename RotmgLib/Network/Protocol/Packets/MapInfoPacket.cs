using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.MAPINFO)]
    class MapInfoPacket : InPacket
    {
        public int Width
        { get; private set; }
        public int Height
        { get; private set; }
        public string Name
        { get; private set; }
        public uint FP
        { get; private set; }
        public int Background
        { get; private set; }
        public string[] ExtraXml
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.Width = reader.ReadInt32();
            this.Height = reader.ReadInt32();
            this.Name = reader.ReadString();
            this.FP = reader.ReadUInt32();
            this.Background = reader.ReadInt32();
            this.ExtraXml = reader.ReadVector<string>(new ReadItemDelegate(reader.ReadLongString));
        }
    }
}
