using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.PLAYERTEXT)]
    class PlayerTextPacket : OutPacket
    {
        public string Text
        { get; set; }

        public PlayerTextPacket()
            : this("")
        { }

        public PlayerTextPacket(string text)
        {
            this.Text = text;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.Text);

            return writer.GetBuffer();
        }
    }
}
