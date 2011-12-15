using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.TEXT)]
    class TextPacket : InPacket
    {
        public string Name
        { get; private set; }
        public int ObjectId
        { get; private set; }
        public short ObjectType
        { get; private set; }
        public int Texture1Id
        { get; private set; }
        public int Texture2Id
        { get; private set; }
        public int NumStars
        { get; private set; }
        public string Recipient
        { get; private set; }
        public string Text
        { get; private set; }
        public string CleanText
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.Name = reader.ReadString();
            this.ObjectId = reader.ReadInt32();
            this.ObjectType = reader.ReadInt16();
            this.Texture1Id = reader.ReadInt32();
            this.Texture2Id = reader.ReadInt32();
            this.NumStars = reader.ReadInt32();
            this.Recipient = reader.ReadString();
            this.Text = reader.ReadString();
            this.CleanText = reader.ReadString();
        }
    }
}
