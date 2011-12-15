using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    abstract class OutPacket
    {
        public abstract byte[] Write();
    }
}
