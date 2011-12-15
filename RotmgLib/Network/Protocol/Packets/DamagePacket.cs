using System;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.DAMAGE)]
    class DamagePacket : InPacket
    {
        public int TargetId
        { get; private set; }
        public byte ConditionEffect
        { get; private set; }
        public ushort DamageAmount
        { get; private set; }
        public byte BulletId
        { get; private set; }
        public int ObjectId
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.TargetId = reader.ReadInt32();
            this.ConditionEffect = reader.ReadByte();
            this.DamageAmount = reader.ReadUInt16();
            this.BulletId = reader.ReadByte();
            this.ObjectId = reader.ReadInt32();
        }
    }
}
