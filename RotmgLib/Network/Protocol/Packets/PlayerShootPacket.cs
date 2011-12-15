using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.PLAYERSHOOT)]
    class PlayerShootPacket : OutPacket
    {
        public int Time
        { get; set; }
        public byte BulletId
        { get; set; }
        public short ContainerType
        { get; set; }
        public TilePoint StartingPosition
        { get; set; }
        public float Angle
        { get; set; }

        public PlayerShootPacket()
            : this(0, 0, 0, new TilePoint(), 0)
        { }

        public PlayerShootPacket(int time, byte bullet_id, short container_type, TilePoint starting_pos, float angle)
        {
            this.Time = time;
            this.BulletId = bullet_id;
            this.ContainerType = container_type;
            this.StartingPosition = starting_pos;
            this.Angle = angle;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.Time);
            writer.Write(this.BulletId);
            writer.Write(this.ContainerType);
            writer.Write(this.StartingPosition);
            writer.Write(this.Angle);

            return writer.GetBuffer();
        }
    }
}
