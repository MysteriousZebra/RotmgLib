using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    class StatData
    {
        public byte Type
        { get; set; }
        public int IntData
        { get; set; }
        public string StringData
        { get; set; }

        public StatData()
            : this(0, 0)
        { }

        public StatData(byte type, object data)
        {
            this.Type = type;

            if (type == 31)
                this.StringData = (string)data;
            else
                this.IntData = (int)data;
        }
    }
}
