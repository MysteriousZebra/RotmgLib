using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.NEW_TICK)]
    class NewTickPacket : InPacket
    {
        public int TickId
        { get; private set; }
        public int TickTime
        { get; private set; }
        public ObjectStatusData[] Statuses
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.TickId = reader.ReadInt32();
            this.TickTime = reader.ReadInt32();
            this.Statuses = reader.ReadVector<ObjectStatusData>(new ReadItemDelegate(reader.ReadObjectStatusData));
        }
    }
}
