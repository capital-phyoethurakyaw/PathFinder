namespace PathFinder
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorDraw.Geometry;

public class DoorToDoorWay
{
 public   Connector sc;
        public Connector ec;
        public gPoints way;

    public DoorToDoorWay(Connector sc, Connector ec, gPoints way) { 
        this.sc = sc;
        this.ec = ec;   
        this.way = way;     
        this.way = way;     
    }
}
}
