using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.CREATE)]
    class CreatePacket : OutPacket
    {
        public int ObjectType
        { get; set; }

        public CreatePacket()
            : this(0)
        { }

        public CreatePacket(int object_type)
        {
            this.ObjectType = object_type;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.ObjectType);

            return writer.GetBuffer();
        }
    }
}
