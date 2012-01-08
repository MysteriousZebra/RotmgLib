using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    public class TradeItem
    {
        public int Item
        { get; set; }
        public int SlotType
        { get; set; }
        public bool Tradeable
        { get; set; }
        public bool Included
        { get; set; }

        public TradeItem()
            : this(0, 0, false, false)
        { }

        public TradeItem(int item, int slot_type, bool tradeable, bool included)
        {
            this.Item = item;
            this.SlotType = slot_type;
            this.Tradeable = tradeable;
            this.Included = included;
        }
    }
}
