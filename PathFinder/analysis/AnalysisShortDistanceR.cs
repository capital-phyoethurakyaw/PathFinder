namespace PathFinder.analysis
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PathFinder.util;
    using VectorDraw.Geometry;
    using VectorDraw.Professional.Constants;
    using VectorDraw.Professional.vdCollections;
    using VectorDraw.Professional.vdFigures;
    using VectorDraw.Professional.vdObjects;

    public class AnalysisShortDistanceR
    {
        public static gPoints getShortDistance2(List<Room> roomList, Info info, vdDocument doc)
        {

            List<Connector> connectorList = new List<Connector>();
            List<GroupPoint> gps = new List<GroupPoint>();
            for (int i = 0; i < roomList.Count - 1; i++)
            {
                Room room1 = roomList[i];
                Room room2 = roomList[i + 1];
                Connector conn = FindUtil.findConnector(info.floor, room1, room2);
                connectorList.Add(conn);
                //  if( i != 0  ) gps.AddRange(room1.getPoints());
                gps.AddRange(conn.getPoints2());   // getPointsLikeMachine
                if (i != roomList.Count - 2)
                {
                    gps.AddRange(room2.getPoints2());
                }
            }


            /*
             
          foreach (GroupPoint p in gps) {

              vdCircle circ = new vdCircle();
              doc.Model.Entities.AddItem(circ);
              circ.SetUnRegisterDocument(doc); circ.setDocumentDefaults();
              circ.PenColor.SystemColor = Color.Red; // Using system color
              circ.Center = p.point;
              circ.Radius = 2;
          }
            doc.Model.Entities.Update();
            doc.Update();
            doc.Redraw(true);
   */


            Room firstConn = roomList[0];
            Room lastConn = roomList.Last();

            GroupPoint firstGP = new GroupPoint(firstConn.guid, new gPoint(firstConn.roomBoundary.BoundingBox.MidPoint.x, firstConn.roomBoundary.BoundingBox.MidPoint.y), false);
            GroupPoint lastGP = new GroupPoint(lastConn.guid, new gPoint(lastConn.roomBoundary.BoundingBox.MidPoint.x, lastConn.roomBoundary.BoundingBox.MidPoint.y), false);


            //set start end point
            gps.Insert(0, firstGP);
            gps.Add(lastGP);


            double[,] adjMatrix = new double[gps.Count, gps.Count]; // adding start point , endpoint;

            for (int i = 0; i < gps.Count - 1; i++)
            {
                for (int j = i + 1; j < gps.Count; j++)
                {
                    double distance = gps[i].point.Distance2D(gps[j].point);
                    if (distance > 700)
                    {
                        adjMatrix[i, j] = double.MaxValue;
                        adjMatrix[j, i] = double.MaxValue;
                    }
                    else
                    {
                        adjMatrix[i, j] = distance;
                        adjMatrix[j, i] = distance;
                    }
                    if (gps[i].guid != gps[j].guid && gps[i].isRoom && gps[j].isRoom) // two point between two roooms- > NoN
                    {
                        adjMatrix[i, j] = double.MaxValue;
                        adjMatrix[j, i] = double.MaxValue;
                    }

                    if (!gps[i].isRoom && !gps[j].isRoom) //  two point between two doors - > NoN
                    {
                        adjMatrix[i, j] = double.MaxValue;
                        adjMatrix[j, i] = double.MaxValue;
                    }
                }
            }

            double[] result = Dijkstra.analysis(0, gps.Count - 1, adjMatrix);
            gPoints ps = new gPoints();
            for (int i = 0; i < result.Length - 1; i++)
            {
                GroupPoint gp = gps[(int)result[i]];
                ps.Add(gp.point);
            }


            return ps;
        }

        public static gPoints getShortDistance(List<Room> roomList, Info info, vdDocument doc)
        {

            List<Connector> connectors = new List<Connector>();
            List<Room> rooms = new List<Room>();
            List<GroupPoint> gps = new List<GroupPoint>();
            ArrayList list = new ArrayList();
            for (int i = 0; i < roomList.Count - 1; i++)
            {
                Room room1 = roomList[i];
                Room room2 = roomList[i + 1];
                Connector conn = FindUtil.findConnector(info.floor, room1, room2);
                connectors.Add(conn);
                //  if( i != 0  ) gps.AddRange(room1.getPoints());
                gps.AddRange(conn.getPoints());
                if (i != roomList.Count - 2)
                {
                    rooms.Add(room2);
                    gps.AddRange(room2.getPointsLikeMachine());
                }
            }

            /*
          foreach (GroupPoint p in gps) {
               // if (p.point == null) continue;
              vdCircle circ = new vdCircle();
              doc.Model.Entities.AddItem(circ);
              circ.SetUnRegisterDocument(doc); circ.setDocumentDefaults();
              circ.PenColor.SystemColor = Color.Red; // Using system color
              circ.Center = p.point;
              circ.Radius = 100;
             
          }
            */
            doc.Model.Entities.Update();
            doc.Update();
            doc.Redraw(true);



            Room firstConn = roomList[0];
            Room lastConn = roomList.Last();

            GroupPoint firstGP = new GroupPoint(firstConn.guid, new gPoint(firstConn.roomBoundary.BoundingBox.MidPoint.x, firstConn.roomBoundary.BoundingBox.MidPoint.y), false);
            GroupPoint lastGP = new GroupPoint(lastConn.guid, new gPoint(lastConn.roomBoundary.BoundingBox.MidPoint.x, lastConn.roomBoundary.BoundingBox.MidPoint.y), false);


            //set start end point
            gps.Insert(0, firstGP);
            gps.Add(lastGP);

            double[,] adjMatrix = new double[gps.Count, gps.Count]; // adding start point , endpoint;

            for (int i = 0; i < gps.Count - 1; i++)
            {
                for (int j = i + 1; j < gps.Count; j++)
                {
                    double distance = gps[i].point.Distance2D(gps[j].point);
                    vdLine line = new vdLine(doc, gps[i].point, gps[j].point);
                    bool isIntersect = false;
                    if (gps[i].guid == gps[j].guid && gps[i].isRoom && gps[j].isRoom)
                    {

                        foreach (Room r in roomList)
                        {
                            foreach (vdPolyline po in r.shapeList)
                            {
                                gPoints ps2 = new gPoints();
                                isIntersect = po.IntersectWith(line, VdConstInters.VdIntOnBothOperands, ps2);
                                if (isIntersect) break;
                            }
                            if (isIntersect) break;
                        }
                    }
                    else if (gps[i].guid == gps[j].guid && !gps[i].isRoom && !gps[j].isRoom) // same door 
                    {
                        isIntersect = false;
                    }
                    else if (gps[i].guid != gps[j].guid && !gps[i].isRoom && !gps[j].isRoom) // other doors
                    {
                        isIntersect = true;
                    }
                    else
                    {

                        foreach (Room r in roomList)
                        {
                            foreach (vdPolyline po in r.shapeList)
                            {
                                gPoints ps1 = new gPoints();
                                isIntersect = po.IntersectWith(line, VdConstInters.VdIntOnBothOperands, ps1);


                                if (isIntersect)
                                {
                                    int pCount = ps1.Count;
                                    foreach (gPoint p in ps1)
                                    {
                                        foreach (Connector connector in connectors)
                                        {
                                            if (CadUtil.contains(connector.offShape.VertexList, p))
                                            {

                                                pCount--;
                                                break;
                                            }
                                        }
                                    }
                                    if (pCount == 0) isIntersect = false;
                                }

                                if (isIntersect) break;
                            }
                            if (isIntersect) break;
                            //
                        }
                    }

                    if (distance > 10000) isIntersect = true;

                    if (isIntersect)  //  && gps[i].isRoom && gps[j].isRoom) // two point between two roooms- > NoN
                    {
                        adjMatrix[i, j] = double.MaxValue;
                        adjMatrix[j, i] = double.MaxValue;
                    }
                    else
                    {
                        adjMatrix[i, j] = distance;
                        adjMatrix[j, i] = distance;
                    }
                }
            }

            double[] result = Dijkstra.analysis(0, gps.Count - 1, adjMatrix);
            gPoints ps = new gPoints();
            for (int i = 0; i < result.Length - 1; i++)
            {
                GroupPoint gp = gps[(int)result[i]];
                ps.Add(gp.point);
            }


            return ps;
        }
    }
}
