using System;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.UPDATE)]
    class UpdatePacket : InPacket
    {
        public TilePoint[] Tiles
        { get; private set; }
        public GameObject[] NewObjects
        { get; private set; }
        public int[] Drops
        { get; private set; }

        public override void Read(byte[] packet)
        {
            ProtocolReader reader = new ProtocolReader(packet);

            this.Tiles = reader.ReadVector<TilePoint>(new ReadItemDelegate(reader.ReadTilePoint));
            this.NewObjects = reader.ReadVector<GameObject>(new ReadItemDelegate(reader.ReadGameObject));
            this.Drops = reader.ReadVector<int>(new ReadItemDelegate(() => (object)reader.ReadInt32())); // idk why the fuck I have to use lambda
        }
    }
}
