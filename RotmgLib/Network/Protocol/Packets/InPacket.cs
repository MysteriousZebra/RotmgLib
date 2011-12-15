using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace RotmgLib.Network.Protocol.Packets
{
    abstract class InPacket
    {
        public abstract void Read(byte[] packet);

        public static InPacket Parse(Opcode opcode, byte[] packet)
        {
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                if (t.IsSubclassOf(typeof(InPacket)))
                    if (!t.IsAbstract)
                        foreach (object attribute in t.GetCustomAttributes(true))
                            if (attribute is PacketOpcodeAttribute)
                                if (((PacketOpcodeAttribute)attribute).Opcode == opcode)
                                {
                                    InPacket in_packet = (InPacket)t.GetConstructor(new Type[0]).Invoke(new object[0]);
                                    in_packet.Read(packet);

                                    return in_packet;
                                }

            return null;
        }
    }
}
