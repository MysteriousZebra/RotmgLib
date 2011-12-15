using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    class PacketOpcodeAttribute : Attribute
    {
        public Opcode Opcode
        { get; private set; }

        public PacketOpcodeAttribute(Opcode opcode)
        {
            this.Opcode = opcode;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
