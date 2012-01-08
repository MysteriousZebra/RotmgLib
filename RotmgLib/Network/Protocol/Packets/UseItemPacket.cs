using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.USEITEM)]
    class UseItemPacket : OutPacket
    {
        public int Time
        { get; set; }
        public int ObjectId
        { get; set; }
        public byte SlotId
        { get; set; }
        public TilePoint ItemUsePosition
        { get; set; }

        public UseItemPacket()
            : this(0, 0, 0, new TilePoint())
        { }

        public UseItemPacket(int time, int object_id, byte slot_id, TilePoint item_use_position)
        {
            this.Time = time;
            this.ObjectId = object_id;
            this.SlotId = slot_id;
            this.ItemUsePosition = item_use_position;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.Time);
            writer.Write(this.ObjectId);
            writer.Write(this.SlotId);
            writer.Write(this.ItemUsePosition);

            return writer.GetBuffer();
        }
    }
}
