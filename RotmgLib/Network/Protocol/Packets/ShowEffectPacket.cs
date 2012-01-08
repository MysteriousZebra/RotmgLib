using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.SHOW_EFFECT)]
    class ShowEffectPacket : InPacket
    {
        public byte EffectType
        { get; private set; }
        public int TargetObjectId
        { get; private set; }
        public TilePoint Position1
        { get; private set; }
        public TilePoint Position2
        { get; private set; }
        public int Colour // fuck american-english!
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.EffectType = reader.ReadByte();
            this.TargetObjectId = reader.ReadInt32();
            this.Position1 = reader.ReadTilePoint();
            this.Position2 = reader.ReadTilePoint();
            this.Colour = reader.ReadInt32();
        }
    }
}
