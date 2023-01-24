using ClipperLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClipperLib;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.SolidModel;
using VectorDraw.Professional.CommandActions;

namespace vdIFCViewer
{
    using static RenderFormats.PrimitiveRender3d;
    using Polygon = List<IntPoint>;
    using Polygons = List<List<IntPoint>>;

 

    internal class Union
    {

      
        public static vdPolyline getBoundary(vdPolyline poly1, vdPolyline poly2) {

            Polygons subjects = new Polygons();
            Polygons clips = new Polygons();
            Polygons solution = new Polygons();

            List<IntPoint> points1 = new List<IntPoint>();
            foreach (VectorDraw.Geometry.Vertex vertex in poly1.VertexList)
            {
                points1.Add(new IntPoint(vertex.x, vertex.y));
            }

            List<IntPoint> points2 = new List<IntPoint>();
            foreach (VectorDraw.Geometry.Vertex vertex in poly2.VertexList)
            {
                points2.Add(new IntPoint(vertex.x, vertex.y));
            }

            subjects.Add(points1);
            clips.Add(points2);

            Clipper c = new Clipper();
            c.AddPaths(subjects, PolyType.ptSubject, true);
            c.AddPaths(clips, PolyType.ptClip, true);
            solution.Clear();


            bool succeeded = c.Execute(ClipType.ctUnion, solution, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            //   foreach (List<IntPoint> pts in solution) {
            List<IntPoint> pts = solution[0]; // 
            vdPolyline poly = new vdPolyline();

                foreach (IntPoint pt in pts)
                {
                 poly.VertexList.Add(pt.X, pt.Y, 0, 0);

                }
            poly.Flag = VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE;
            //  }
            return poly;
        }
       
    
    }
}
