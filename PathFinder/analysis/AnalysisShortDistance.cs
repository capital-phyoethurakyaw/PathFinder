namespace PathFinder.analysis
{
   
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using PathFinder.util;
    using VectorDraw.Geometry;
    using VectorDraw.Professional.Constants;
    using VectorDraw.Professional.vdCollections;
    using VectorDraw.Professional.vdFigures;
    using VectorDraw.Professional.vdObjects;

    public class AnalysisShortDistance
    {
        private static readonly object console;

        public static Room setShortDistance(Sequence sequence, Info info, vdDocument doc, bool isRoomCenter, int type, bool isMainRoute, bool isMinmumRoute) {

            
            foreach (RoomRelation rr in info.roomRelations)
            {
                if (rr.sRoom == sequence.sRoom && rr.eRoom == sequence.eRoom)
                {
                    foreach (ArrayList list in rr.roomLists)
                    {
                        List<Room> roomList = new List<Room>();
                        foreach (Room r in list) roomList.Add(r);
                        SubSequence sequence1 = new SubSequence(roomList);
                        sequence.subSequences.Add(sequence1);
                    }
                    break;
                }
            }
            foreach (SubSequence subSequence in sequence.subSequences)
            {
                gPoints ps = null;
                if (type  == 0) ps = AnalysisShortDistance.getShortDistanceDD(subSequence.roomList, info, doc, isRoomCenter);
                else if (type == 1) ps = AnalysisShortDistance.getShortDistanceLikeHuman(subSequence.roomList, info, doc, isRoomCenter);
                else ps = AnalysisShortDistance.getShortDistanceLikeMachine(subSequence.roomList, info, doc);
                subSequence.setRouteLine(ps);
               
            }
            sequence.setShortestPath(isMainRoute, isMinmumRoute, info.mainRoutes);

            return sequence.eRoom;
        }

        public static gPoints getShortDistanceDD(List<Room> roomList, Info info,vdDocument doc, bool isRoomCenter)
        {
            gPoints way = new gPoints();
            if (isRoomCenter)
            {
                Room firstRoom = roomList[0];
                Room room02 = roomList[1];
                Connector firstConnector = FindUtil.findConnector(info.floor, firstRoom, room02);
                gPoints way1 = getShortDistanceRoomCenterToConnector(firstRoom, firstConnector, true);
                way.AddRange(way1);
            }
            for (int i = 0; i < roomList.Count - 2; i++)
            {
                Room room1 = roomList[i];
                Room room2 = roomList[i + 1];
                Connector conn1 = FindUtil.findConnector(info.floor, room1, room2);
                //room 1 conn1
                Room room3 = roomList[i + 2];
                Connector conn2 = FindUtil.findConnector(info.floor, room2, room3);
                //room 3 conn2
                gPoints way2  =  room2.getShortDistanceConnectorToConnector(conn1, conn2, 0, doc);
                way.AddRange(way2);
            }
            if (isRoomCenter)
            {
                Room room11 = roomList[roomList.Count - 2];
                Room lastRoom = roomList[roomList.Count - 1];
                Connector lastConnector = FindUtil.findConnector(info.floor, room11, lastRoom);
                gPoints way3 = getShortDistanceRoomCenterToConnector(lastRoom, lastConnector, false);
                way.AddRange(way3);
            }
            return way;
        }

        public static gPoints getShortDistanceRoomCenterToConnector(Room room, Connector connector, bool isStartFromRoom)
        {
            List<GroupPoint> gps = new List<GroupPoint>();
            gps.AddRange(room.getPoints2());
            GroupPoint firstGP = new GroupPoint(room.guid, room.centerPoint, false);
            GroupPoint lastGP = new GroupPoint(connector.guid, new gPoint(connector.shape.BoundingBox.MidPoint.x, connector.shape.BoundingBox.MidPoint.y), false);
            gps.Insert(0, firstGP);
            gps.Add(lastGP);
            double[,] adjMatrix = new double[gps.Count, gps.Count];
            for (int i = 0; i < gps.Count - 1; i++)
            {
                for (int j = i + 1; j < gps.Count; j++)
                {
                    double distance = gps[i].point.Distance2D(gps[j].point);
                    if (distance > 450)
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
            if (!isStartFromRoom) ps.Reverse();
            return ps;

        }


        public static gPoints getShortDistanceConnectorToConnector(Room room, Connector firstConn, Connector lastConn, vdDocument doc)
        {
            List<GroupPoint> gps = new List<GroupPoint>();
            gps.AddRange(room.getPoints2());
            GroupPoint firstGP = new GroupPoint(firstConn.guid, new gPoint(firstConn.shape.BoundingBox.MidPoint.x, firstConn.shape.BoundingBox.MidPoint.y), false);
            GroupPoint lastGP = new GroupPoint(lastConn.guid, new gPoint(lastConn.shape.BoundingBox.MidPoint.x, lastConn.shape.BoundingBox.MidPoint.y), false);

            gps.Insert(0, firstGP);
            gps.Add(lastGP);

            double[,] adjMatrix = new double[gps.Count, gps.Count];
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





        public static gPoints getShortDistanceLikeHuman(List<Room> roomList, Info info, vdDocument doc, bool isRoomCenter)
        {
      
            List<Connector> connectorList = new List<Connector>();
            List<GroupPoint> gps = new List<GroupPoint>();
           
           
            
            if (isRoomCenter)//isRoomCenter
            {
                Room firstRoom = roomList.First();

                gps.AddRange(firstRoom.getPoints2());
            }
            
            for (int i = 0; i < roomList.Count - 1; i++)
            {
                Room room1 = roomList[i];
                Room room2 = roomList[i + 1];
                Connector conn = FindUtil.findConnector(info.floor, room1, room2);
                connectorList.Add(conn);
                //  if( i != 0  ) gps.AddRange(room1.getPoints());
                gps.AddRange(conn.getPoints2());
                if (i != roomList.Count - 2)
                {


                    gps.AddRange(room2.getPoints2());
                }
            }

            if (isRoomCenter)//isRoomCenter
            {
                Room firstRoom = roomList.Last();
                gps.AddRange(firstRoom.getPoints2());
            }


            if (isRoomCenter)//isRoomCenter
            {
                Room firstRoom = roomList.First();
                Room lastRoom = roomList.Last();
                gps.Insert(0, new GroupPoint(firstRoom.guid, firstRoom.centerPoint, true));
                gps.Add(new GroupPoint(lastRoom.guid, lastRoom.centerPoint, true));
            }
            else {

                Connector firstConn = connectorList[0];
                Connector lastConn = connectorList.Last();
                GroupPoint firstGP = new GroupPoint(firstConn.guid, new gPoint(firstConn.shape.BoundingBox.MidPoint.x, firstConn.shape.BoundingBox.MidPoint.y), false);
                GroupPoint lastGP = new GroupPoint(lastConn.guid, new gPoint(lastConn.shape.BoundingBox.MidPoint.x, lastConn.shape.BoundingBox.MidPoint.y), false);
                gps.Insert(0, firstGP);
                gps.Add(lastGP);
            }





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

        public static gPoints getShortDistanceLikeMachine(List<Room> roomList, Info info, vdDocument doc)
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

            

            Connector firstConn = connectors[0];
            Connector lastConn = connectors.Last();

            GroupPoint firstGP = new GroupPoint(firstConn.guid, new gPoint(firstConn.shape.BoundingBox.MidPoint.x, firstConn.shape.BoundingBox.MidPoint.y), false);
            GroupPoint lastGP = new GroupPoint(lastConn.guid, new gPoint(lastConn.shape.BoundingBox.MidPoint.x, lastConn.shape.BoundingBox.MidPoint.y), false);

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
