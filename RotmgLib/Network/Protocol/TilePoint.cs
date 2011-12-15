using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    public class TilePoint
    {
        public float X
        { get; set; }
        public float Y
        { get; set; }

        public TilePoint()
            : this(0, 0)
        { }

        public TilePoint(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
