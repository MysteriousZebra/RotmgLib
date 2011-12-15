using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.SHOOT)]
    class ShootPacket : InPacket
    {
        public byte BulletId
        { get; private set; }
        public int OwnerId
        { get; private set; }
        public short ContainerType
        { get; private set; }
        public TilePoint StartingPosition
        { get; private set; }
        public float Angle
        { get; private set; }
        public short Damage
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.BulletId = reader.ReadByte();
            this.OwnerId = reader.ReadInt32();
            this.ContainerType = reader.ReadInt16();
            this.StartingPosition = reader.ReadTilePoint();
            this.Angle = reader.ReadSingle();
            this.Damage = reader.ReadInt16();
        }
    }
}
