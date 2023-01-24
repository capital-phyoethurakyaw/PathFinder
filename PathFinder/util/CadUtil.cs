namespace PathFinder.util
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VectorDraw.Geometry;
    using VectorDraw.Professional.Constants;
    using VectorDraw.Professional.vdFigures;

    public class CadUtil
    {

        public static bool contains(Vertexes _pts, gPoint pt)
        {
            bool isIn = false;

            int NumberOfPoints = _pts.Count;
            if (true)
            {
                int i, j = 0;
                for (i = 0, j = NumberOfPoints - 1; i < NumberOfPoints; j = i++)
                {
                    if (
                    (
                    ((_pts[i].y <= pt.y) && (pt.y <= _pts[j].y)) || ((_pts[j].y <= pt.y) && (pt.y <= _pts[i].y))
                    ) &&
                    (pt.x <= (_pts[j].x - _pts[i].x) * (pt.y - _pts[i].y) / (_pts[j].y - _pts[i].y) + _pts[i].x)
                    )
                    {
                        isIn = !isIn;
                    }
                }
            }
            return isIn;
        }
        public static bool contains2(Vertexes _pts, gPoint pt)
        {
            bool isIn = false;

            int NumberOfPoints = _pts.Count;
            if (true)
            {
                int i, j = 0;
                for (i = 0, j = NumberOfPoints - 1; i < NumberOfPoints; j = i++)
                {
                    if (
                    (
                    ((_pts[i].y < pt.y) && (pt.y < _pts[j].y)) || ((_pts[j].y < pt.y) && (pt.y < _pts[i].y))
                    ) &&
                    (pt.x < (_pts[j].x - _pts[i].x) * (pt.y - _pts[i].y) / (_pts[j].y - _pts[i].y) + _pts[i].x)
                    )
                    {
                        isIn = !isIn;
                    }
                }
            }
            return isIn;
        }

        public static void setColorPath(vdPolyline polyline, Color color, VdConstLineWeight vdConstLineWeight)
        {
            polyline.PenColor = new VectorDraw.Professional.vdObjects.vdColor(color);
            polyline.LineWeight = vdConstLineWeight;
        }

        public static void setArrow(vdPolyline polyline, Color color, VdConstLineWeight vdConstLineWeight)
        {
            polyline.PenColor = new VectorDraw.Professional.vdObjects.vdColor(color);
        }

    }
}
