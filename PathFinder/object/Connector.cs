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

namespace PathFinder
{
   public class Connector
    {
        public int guid;
        public vdPolyline shape;
        public vdPolyline offShape;
        public bool isReal = true;
        public List<Room> roomList = new List<Room>();
        public List<vdPolyline> roomPolylinesList = new List<vdPolyline>();

         
        // List<Room> 

        public Connector(vdPolyline shape, int guid,  bool isReal) {
            this.shape = shape;
         this.guid = guid;
          //  this.shape.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
            this.isReal = isReal;
            vdCurves offssetCureves1 = null;
      
            if (shape.IsClockwise()) offssetCureves1 = shape.getOffsetCurve(5);
            else offssetCureves1 = shape.getOffsetCurve(-5);

            this.offShape = new vdPolyline(shape.Document,  offssetCureves1[0].GetGripPoints());
        }

        public   vdPolyline findRoomPoly( Room room)
        {
            foreach (vdPolyline poly in room.shapeList)
            {
                foreach (vdPolyline poly2 in this.roomPolylinesList)
                {
                    if (poly == poly2) return poly2;
                }   
            }
            return null;
        }

        public List<GroupPoint> getPoints2()
        {
            double gapX = 50;
            double gapY = 50;
            double gap = 60;
            List<GroupPoint> points = new List<GroupPoint>();
            if (!isReal)
            {
                 gapX = 500;
                 gapY = 500;
                double centerX = this.shape.BoundingBox.MidPoint.x;
                double centerY = this.shape.BoundingBox.MidPoint.y;

                double wi = this.shape.BoundingBox.Width;
                double he = this.shape.BoundingBox.Height;


                int dis = 200;

                if (wi > 30)
                {
                    int count = (int)Math.Floor((wi - 2 * gapX) / dis);
                    double x1 = this.shape.BoundingBox.Left + gapX;
                    for (int i = 0; i < count; i++)
                    {
                        gPoint p = new gPoint(x1+ dis * i, centerY);
                        points.Add(new GroupPoint(this.guid, p, false));
                        bool isInConnector = CadUtil.contains(this.shape.VertexList, p);
                        bool isInObstacle = false;

                        double dis2 = 0;
                        foreach (GroupPoint point in points)
                        {
                            isInObstacle = CadUtil.contains(this.shape.VertexList, p);
                            if (isInObstacle) break;
                        }


                    }
                }
                if(he > 30)
                {
                    int count = (int)Math.Floor((he- 2* gapY) / dis);
                    double y1 = this.shape.BoundingBox.Bottom + gapY;
                    for (int i = 0; i < count; i++)
                    {
                        gPoint p = new gPoint(centerX, y1 + dis * i);
                        points.Add(new GroupPoint(this.guid, p, false));
                        bool isConnector = CadUtil.contains(this.shape.VertexList, p);
                        bool isInObstacle = false;

                        double dis2 = 0;
                        foreach (GroupPoint point in points)
                        {
                            isInObstacle = CadUtil.contains(this.shape.VertexList, p);
                            if (isInObstacle) break;
                        }

                        if (isConnector && !isInObstacle)
                        {
                            points.Add(new GroupPoint(this.guid, p, true));
                        }
                    }
                }

            }
            else
            {



                
                double x1 = this.shape.BoundingBox.Left + gapX;
                double x2 = this.shape.BoundingBox.Right - gapX;
                double y1 = this.shape.BoundingBox.Bottom + gapY;
                double y2 = this.shape.BoundingBox.Top - gapY;
                double width = this.shape.BoundingBox.Width - gapX * 2;
                double height = this.shape.BoundingBox.Height - gapY * 2;
                int countW = (int)(width / gap);
                int countH = (int)(height / gap);
                if (countW == 0)
                {
                    gapX = 10;
                    countW = 2;
                }
                if (countH == 0)
                {
                    gapY = 10;
                    countH = 2;
                }
             
                for (int i = 1; i < countW; i++)
                {
                    for (int j = 1; j < countH; j++)
                    {
                        double x = x1 + gapX * i;
                        double y = y1 + gapY * j;
                        gPoint p = new gPoint(x, y);
                        bool isIn = CadUtil.contains(this.shape.VertexList, p);

                        if (isIn) points.Add(new GroupPoint(this.guid, p, false));
                    }
                }
               
            }
            //if(!this.isReal) 

            points.Add(new GroupPoint(this.guid, this.shape.BoundingBox.MidPoint, false));
            return points;
        }

        public List<GroupPoint> getPoints()
        {
          
            List<GroupPoint> points = new List<GroupPoint>();
            
                gPoints gps = new gPoints();
                vdCurves offssetCureves1 = null;
                if (shape.IsClockwise()) offssetCureves1 = shape.getOffsetCurve(0);//-10
            else offssetCureves1 = shape.getOffsetCurve(0);//10
            gPoints gs1 = offssetCureves1[0].GetGripPoints();

               // foreach (gPoint p in gs1) gps.Add(p);

                foreach (gPoint gp in gps)
                {

                    points.Add(new GroupPoint(this.guid, gp, false));
                }

                points.Add(new GroupPoint(this.guid, this.shape.BoundingBox.MidPoint, false));
           
               
            return points;
        }

        public override string ToString()
        {
            string text = "";
            foreach (Room r in roomList)
            {
                text += r.name + Protocol.Delimiter_Rooms;
            }
            if (text.Length > 0) text = text.Substring(0, text.Length - 1);
            return text;
        }


    }
}
