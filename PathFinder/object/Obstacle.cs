using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PathFinder.util;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;

namespace PathFinder { 

public class Obstacle
{
        public string name;
        public int guid;
        public vdPolyline shape;


        private vdPolyline boundaryPolyline;
        private vdPolyline offsetBoundaryPolyline;
        private int id;

        public Obstacle(vdPolyline shape, int id)
        {
            this.shape = shape;
            this.id = id;
        }

        public Obstacle(string name, vdPolyline shape, int guid)
        {

            this.name = name;
            this.shape = shape;
            this.guid = guid;
            vdCurves offssetCureves1 = null;

            if (shape.IsClockwise()) offssetCureves1 = shape.getOffsetCurve(5);
            else offssetCureves1 = shape.getOffsetCurve(-5);

            if (offssetCureves1 != null && offssetCureves1.Count > 0) ;

            offsetBoundaryPolyline = new vdPolyline(shape.Document, offssetCureves1[0].GetGripPoints());


            this.offsetBoundaryPolyline = new vdPolyline(shape.Document, offssetCureves1[0].GetGripPoints());

        }
    }
}
