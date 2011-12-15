using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.CREATE_SUCCESS)]
    class CreateSuccessPacket : InPacket
    {
        public int ObjectId
        { get; private set; }
        public int CharId
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.ObjectId = reader.ReadInt32();
            this.CharId = reader.ReadInt32();
        }
    }
}
