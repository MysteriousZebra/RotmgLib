using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    class SlotObject
    {
        public int ObjectId
        { get; set; }
        public byte SlotId
        { get; set; }
        public short ObjectType
        { get; set; }

        public SlotObject()
            : this(0, 0, 0)
        { }

        public SlotObject(int object_id, byte slot_id, short object_type)
        {
            this.ObjectId = object_id;
            this.SlotId = slot_id;
            this.ObjectType = object_type;
        }
    }
}
