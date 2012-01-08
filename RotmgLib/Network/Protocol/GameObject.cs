using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RotmgLib.Network.Protocol
{
    public class GameObject
    {
        public int ObjectType
        { get; set; }
        public ObjectStatusData Status
        { get; set; }

        public GameObject()
            : this(0, new ObjectStatusData())
        { }

        public GameObject(int object_type, ObjectStatusData status)
        {
            this.ObjectType = object_type;
            this.Status = status;
        }
    }
}
