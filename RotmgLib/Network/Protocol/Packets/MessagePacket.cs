using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.MESSAGE)]
    class MessagePacket : OutPacket
    {
        public MessagePacket()
        { }

        public override byte[] Write()
        {
            return new byte[0];
        }
    }
}
