using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.INVSWAP)]
    class InventorySwapPacket : OutPacket
    {
        public int Time
        { get; set; }
        public TilePoint Position
        { get; set; }
        public SlotObject SlotObject1
        { get; set; }
        public SlotObject SlotObject2
        { get; set; }

        public InventorySwapPacket()
            : this(0, new TilePoint(), new SlotObject(), new SlotObject())
        { }

        public InventorySwapPacket(int time, TilePoint position, SlotObject slot_object1, SlotObject slot_object2)
        {
            this.Time = time;
            this.Position = position;
            this.SlotObject1 = slot_object1;
            this.SlotObject2 = slot_object2;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.Time);
            writer.Write(this.Position);
            writer.Write(this.SlotObject1);
            writer.Write(this.SlotObject2);

            return writer.GetBuffer();
        }
    }
}
