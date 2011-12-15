using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol.Packets
{
    [PacketOpcode(Opcode.MOVE)]
    class MovePacket : OutPacket
    {
        public int TickId
        { get; set; }
        public int Time
        { get; set; }
        public TilePoint NewPosition
        { get; set; }

        public MovePacket()
            : this(0, 0, new TilePoint())
        { }

        public MovePacket(int tick_id, int time, TilePoint new_position)
        {
            this.TickId = tick_id;
            this.Time = time;
            this.NewPosition = new_position;
        }

        public override byte[] Write()
        {
            ProtocolWriter writer = new ProtocolWriter();

            writer.Write(this.TickId);
            writer.Write(this.Time);
            writer.Write(this.NewPosition);

            return writer.GetBuffer();
        }
    }
}
