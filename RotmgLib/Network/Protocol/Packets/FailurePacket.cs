using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.FAILURE)]
    class FailurePacket : InPacket
    {
        public int ErrorId
        { get; private set; }
        public string ErrorDescription
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.ErrorId = reader.ReadInt32();
            this.ErrorDescription = reader.ReadString();
        }
    }
}
