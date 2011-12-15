using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.NOTIFICATION)]
    class NotificationPacket : InPacket
    {
        public int ObjectId
        { get; private set; }
        public string Text
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.ObjectId = reader.ReadInt32();
            this.Text = reader.ReadString();
        }
    }
}
