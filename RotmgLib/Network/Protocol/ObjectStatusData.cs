using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    class ObjectStatusData
    {
        public int ObjectId
        { get; set; }
        public TilePoint TilePosition
        { get; set; }
        public StatData[] Data
        { get; set; }

        public ObjectStatusData()
            : this(0, new TilePoint(), new StatData[0])
        { }

        public ObjectStatusData(int object_id, TilePoint tile_position, StatData[] data)
        {
            this.ObjectId = object_id;
            this.TilePosition = tile_position;
            this.Data = data;
        }
    }
}
