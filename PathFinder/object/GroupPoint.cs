namespace PathFinder
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using VectorDraw.Geometry;

    public class GroupPoint
{
       public int guid = 0;
        public gPoint point = new gPoint();
        public bool isRoom = false;
       public GroupPoint(int guid, gPoint point, bool isRoom)
        {
            this.guid = guid;
            this.point = point;
            this.isRoom = isRoom;   
        }   
    }
}
