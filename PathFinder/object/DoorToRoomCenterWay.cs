namespace PathFinder
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorDraw.Geometry;

public class DoorToRoomCenterWay
{

 
    public Connector sc;
    public gPoint center;
    public gPoints way;

    public DoorToRoomCenterWay(Connector sc, gPoint center, gPoints way)
    {
        this.sc = sc;
        this.center = center;
        this.way = way;
     
    }
}
}

