using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PathFinder.analysis;
using PathFinder.util;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using static VectorDraw.Render.OpenGL.OpenGLImports;

namespace PathFinder
{

   
    public class Room : RoomAndGroupObject
    {
        public int guid;
     
        public List<vdPolyline> shapeList = new List<vdPolyline>();
        public vdPolyline roomBoundary = new vdPolyline() ;
        public  gPoint centerPoint= new gPoint() ;
        public List<Connector> connectorList = new List<Connector>();
        public List<Connector> connListU = new List<Connector>();
        public List<Connector> connListA = new List<Connector>();
        public List<Obstacle> obstacles = new List<Obstacle>();

        public List<Room> relationRoomList = new List<Room>();
        public VectorDraw.Professional.vdFigures.vdText text = new VectorDraw.Professional.vdFigures.vdText();

        public List<DoorToDoorWay> doorToDoorWays = new List<DoorToDoorWay>();
        public List<DoorToRoomCenterWay> doorToRoomCenterWays = new List<DoorToRoomCenterWay>();


        List<GroupPoint> gpsH ;
        List<GroupPoint> gpsM  ;



        public Room(string name , int guid , List<vdPolyline> shapeList) :base() {
            this.guid = guid;
            this.name = name;
            this.shapeList = shapeList;
            if (shapeList.Count > 0)
            {
                this.roomBoundary = shapeList[0];
                //shapeList.Remove(this.roomBoundary);
                centerPoint = this.roomBoundary.BoundingBox.MidPoint;
            }
            text.TextString = name;
            foreach (vdPolyline poly in shapeList) {
                poly.XProperties.Add("Room", this);
            }
        }


        public gPoints getShortDistanceConnectorToConnector(Connector firstConn, Connector lastConn, int type, vdDocument doc)
        {
            foreach (DoorToDoorWay fdd in this.doorToDoorWays) {
                if (firstConn == fdd.sc && firstConn == fdd.ec) return fdd.way;
                else if(firstConn == fdd.ec && firstConn == fdd.sc) return fdd.way.Clone(true,true);
            }
             gPoints way =  AnalysisShortDistance.getShortDistanceConnectorToConnector(this, firstConn, lastConn, doc);
             DoorToDoorWay findWayDoorToDoor = new DoorToDoorWay(firstConn, lastConn, way);
            this.doorToDoorWays.Add(findWayDoorToDoor);
            return findWayDoorToDoor.way;
        }

        public void setText(vdDocument doc)
        {
            text.Height = 200;
            text.TextString = this.name;
            text.Update();
            double cen = text.BoundingBox.Width / 2.0;
            text.InsertionPoint = new VectorDraw.Geometry.gPoint(centerPoint.x - cen, centerPoint.y + 100, centerPoint.z);
            text.SetUnRegisterDocument(doc);
            text.setDocumentDefaults();
        }

        public List<GroupPoint> getPoints2()
        {
            if (gpsH != null) return gpsH;
            double gapWithWall = 0;
            double gapBetweenPoints = 400; //500

            double x1 = this.roomBoundary.BoundingBox.Left + gapWithWall;
            double x2 = this.roomBoundary.BoundingBox.Right - gapWithWall;
            double y1 = this.roomBoundary.BoundingBox.Bottom + gapWithWall;
            double y2 = this.roomBoundary.BoundingBox.Top - gapWithWall;
            double width = this.roomBoundary.BoundingBox.Width - gapWithWall * 2;
            double height = this.roomBoundary.BoundingBox.Height - gapWithWall * 2;
            int countW = (int)(width / gapBetweenPoints);
            int countH = (int)(height / gapBetweenPoints);

            List<GroupPoint> points = new List<GroupPoint>();

            List<vdPolyline> offsetShapeList = new List<vdPolyline>();
            List<vdPolyline> offsetObstacleShapeList = new List<vdPolyline>();


            vdCurves offssetCureves1 = roomBoundary.getOffsetCurve(100);
            vdPolyline offssetPolyline1 = new vdPolyline();
            offssetPolyline1.VertexList.AddRange(offssetCureves1[0].GetGripPoints());



            foreach (vdPolyline poly in shapeList)
            {
                if (poly == roomBoundary) continue;
                vdCurves offssetCureves2 = poly.getOffsetCurve(200);
                vdPolyline offssetPolyline = new vdPolyline();
                offssetPolyline.VertexList.AddRange(offssetCureves2[0].GetGripPoints());
                offsetShapeList.Add(offssetPolyline);
            }

            foreach (Obstacle obstacle in this.obstacles)
            {
                vdCurves offssetCureves2 = obstacle.shape.getOffsetCurve(100);
                vdPolyline offssetPolyline = new vdPolyline();
                offssetPolyline.VertexList.AddRange(offssetCureves2[0].GetGripPoints());
                offsetObstacleShapeList.Add(offssetPolyline);
            }





            for (int i = 0; i < countW + 1; i++)
            {
                for (int j = 0; j < countH + 1; j++)
                {
                    int x = (int)(x1 + gapBetweenPoints * i);
                    double y = (int)(y1 + gapBetweenPoints * j);
                    gPoint p = new gPoint(x, y);
                    bool isInBoundary = CadUtil.contains(offssetPolyline1.VertexList, p);
                    bool isInObstacle = false;

                    double dis2 = -1;
                    foreach (vdPolyline poly in offsetShapeList)
                    {
                        isInObstacle = CadUtil.contains(poly.VertexList, p);
                        if (isInObstacle) break;
                    }

                    if (!isInObstacle)
                    {
                        foreach (vdPolyline poly in offsetObstacleShapeList)
                        {
                            isInObstacle = CadUtil.contains(poly.VertexList, p);
                            if (isInObstacle) break;
                        }
                    }
                    

                    if (isInBoundary && !isInObstacle)
                    {
                        points.Add(new GroupPoint(this.guid, p, true));
                    }
                }
            }




            return points;
        }

        public List<GroupPoint> getPointsLikeMachine()
        {
            if (gpsM != null) return gpsM;

            gPoints gps = new gPoints();
            vdCurves offssetCureves1 = null;
            
            if (roomBoundary.IsClockwise()) offssetCureves1 = roomBoundary.getOffsetCurve(-500);
            else offssetCureves1 =  roomBoundary.getOffsetCurve(500);

            if (offssetCureves1 != null && offssetCureves1.Count > 0)
            {
                int co = (int)(offssetCureves1[0].Length() / 2000);
                gps.AddRange(offssetCureves1[0].GetGripPoints());
                gps.AddRange(offssetCureves1[0].Divide(co));
            }

            foreach (vdPolyline poly in shapeList)
            {
                if (poly == roomBoundary) continue;
                vdCurves offssetCureves2 = null;
                if (roomBoundary.IsClockwise()) offssetCureves2 = poly.getOffsetCurve(-300);
                else offssetCureves2 = poly.getOffsetCurve(300);
                if (offssetCureves2 != null && offssetCureves2.Count > 0)
                {
                    int co = (int)(offssetCureves2[0].Length() / 4000);
                    gps.AddRange(offssetCureves2[0].GetGripPoints());
                    gps.AddRange(offssetCureves2[0].Divide(co));
                }
            }
            List<GroupPoint> points = new List<GroupPoint>();
            foreach(gPoint gp in gps) {
               points.Add(new GroupPoint(this.guid, gp, true));
            }
            return points;
        }

        override public string ToString() {
            return this.name;
        }
    }
}
