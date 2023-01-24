namespace PathFinder.util
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using vdIFC;
    using VectorDraw.Geometry;
    using VectorDraw.Professional.Control;
    using VectorDraw.Professional.vdCollections;
    using VectorDraw.Professional.vdFigures;
    using VectorDraw.Professional.vdObjects;
    using VectorDraw.Professional.vdPrimaries;

    internal class IFCToCAD
    {
        public static void getIFCToCAD(vdIFCBuildingStorey storey,Floor floor, vdDocument doc ) {

            foreach (vdIFCProduct product in storey.Products)
            {
                foreach (vdFigure figure in product.Entities)
                {
                    if (product.IFCType == "IfcSpace")
                    {

                        //1인실_TYP2

                      

                        ///////////////////////////////////////////////////////
                        vdIFCProperties props = product.PropertiesGroup.FindName("" + "Pset_SpaceCommon");
                        vdXProperty pro = props.Properties.FindName("Reference");
                        /////////////////////////////////////////////////////////////////////
                    

                        vdLayer layer = getVdLayer(pro.PropValue.ToString(), doc);//rooom name
                        if (layer == null)
                        {
                            layer = new vdLayer(doc, pro.PropValue.ToString());
                        }
                        vdFigure vp = (vdFigure)figure.Clone(null);

                        if (vp is vdSectionPath) //shape type : vdSectionPath or vdPolyface ....etc
                        {
                            vdSectionPath sectionPath = (vdSectionPath)vp;
                            bool isPlane = true; // 2d plane room or 3d extrude room
                            if (isPlane)
                            {
                                vdPolyCurves poc = sectionPath.Sections;
                                List<vdPolyline> shapeList = new List<vdPolyline>();
                                foreach (vdCurves cs in poc)
                                {
                                    foreach (vdCurve c in cs)
                                    {
                                        vdCurve c1 = (vdCurve)c.Clone(null);
                                        c1.Layer = layer;
                                        c1.Transformby(sectionPath.Placement);
                                        Matrix mat = new Matrix();
                                        mat.TranslateMatrix(0, 0, 0);
                                        //mat.TranslateMatrix(0, 0, storey.Elevation);
                                        c1.Transformby(mat);
                                        doc.Model.Entities.AddItem(c1);
                                        shapeList.Add((vdPolyline)c1);
                                    }
                                }
                                Room room = new Room(pro.PropValue.ToString(), product.Id, shapeList);////////////
                                 
                                room.setText(doc);
                                floor.roomList.Add(room);/////////////
                            }
                            else
                            {
                               sectionPath.Layer = layer;
                               doc.Model.Entities.AddItem(sectionPath);
                            }
                        }
                        else if (vp is vdPolyface)
                        {
                            vdPolyface polyface = (vdPolyface)vp;
                        }
                    }
                    else if (product.IFCType == "IfcDoor")
                    {
                        gPoint lrPoint = product.BoundingBox.LowerRight;
                         lrPoint.z = 0; //set zero plan
                        //vdRect rect = new vdRect(vectorDrawBaseControl1.ActiveDocument, lrPoint, -product.BoundingBox.Width, product.BoundingBox.Height, 0);
                        vdRect rect = new vdRect(doc, lrPoint, -product.BoundingBox.Width, product.BoundingBox.Height, 0);
                        vdPolyline poly = rect.AsPolyline();
                        doc.Model.Entities.AddItem(poly);
                        Connector connector = new Connector(  poly, product.Id, true);
                        floor.connectorList.Add(connector);
                    }

                    else if (product.IFCType == "IfcWallStandardCase")
                    {
                        vdFigure vp = (vdFigure)figure.Clone(null);

                        Console.WriteLine(vp.ToString());

                    }
                    else if (product.IFCType == "IfcBuildingElementProxy")
                    {
                        //implement
                    }
                    else if (product.IFCType == "IfcFurnishingElement")
                    {
                        gPoint lrPoint = product.BoundingBox.LowerRight;
                        lrPoint.z = 0;
                        vdRect rect = new vdRect(doc, lrPoint, -product.BoundingBox.Width, product.BoundingBox.Height, 0);
                        vdPolyline poly = rect.AsPolyline();
                        doc.Model.Entities.AddItem(poly);
                        Obstacle obstacle = new Obstacle(poly, product.Id);
                        floor.obstacles.Add(obstacle);

                    }
                    else if (product.IFCType == "?????") //check ifc type
                    {
                        //implement
                    }
                    else  //check ifc type
                    {
                        //implement
                    }
                }
            }
        }


        public static vdLayer getVdLayer(string name, vdDocument doc)
        {
            foreach (vdLayer layer in doc.Layers)
            {

                if (layer.Name == name)
                {
                    return layer;
                }
            }
            return null;
        }



        public static void  setRelationObstacleWithRoom(Floor floor) {

            foreach (Obstacle obstacle in floor.obstacles) {
                foreach (Room room in floor.roomList)
                {
                    bool isIn = false;
                    foreach (vdPolyline poly in room.shapeList)
                    {
                        foreach (Vertex v in obstacle.shape.VertexList) {
                            isIn =   CadUtil.contains2(poly.VertexList, v);
                            if (isIn) {
                              if(!room.obstacles.Contains(obstacle))room.obstacles.Add(obstacle);


                                Console.WriteLine(room.name + " isIn ");
                                break;
                            }
                        }
                    }
                    if (isIn) break;
                }

            }


        }
        public static void setRoomRelation(Floor floor, List<RoomRelation> roomRelations, vdDocument doc)
        {
            //find rooms related without connector
            for (int i = 0; i < floor.roomList.Count - 1; i++)
            {
                Room r1 = (Room)floor.roomList[i];
                for (int j = i + 1; j < floor.roomList.Count; j++)
                {
                    Room r2 = (Room)floor.roomList[j];
                    gPoints points = new gPoints();
                    bool isRelation = false;
                    foreach (vdFigure s1 in r1.shapeList)
                    {
                        foreach (vdFigure s2 in r2.shapeList)
                        {
                            isRelation = s1.IntersectWith(s2, VectorDraw.Professional.Constants.VdConstInters.VdIntOnBothOperands, points);
                            if (isRelation) break;
                        }
                        if (isRelation) break;
                    }
                    if (isRelation)
                    {


                        vdPolyline poly = new vdPolyline();
                        poly.VertexList.AddRange(points);
                        double width = poly.BoundingBox.Width;
                        double height = poly.BoundingBox.Height;
 ;
                        if (width < 10) {
                            width = 30;
                        }
                        if (height < 10)
                        {
                            height = 30;
                        }

                        vdRect rect = new vdRect(doc);
                        rect.Width = width;
                        rect.Height = height;
                        gPoint gp = rect.InsertionPoint = new gPoint(poly.BoundingBox.MidPoint.x - rect.Width / 2.0, poly.BoundingBox.MidPoint.y - rect.Height / 2.0);
                        rect.InsertionPoint = gp;
                        rect.PenColor = new vdColor(Color.Yellow);
                        rect.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                        doc.Model.Entities.AddItem(rect);
                        Connector c = new Connector(rect.AsPolyline(), rect.Id, false);
                        floor.connectorList.Add(c);
                    }
                }
            }
            /////////

            foreach (Connector connector in floor.connectorList)
            {
                foreach (Room room in floor.roomList)
                {
                    foreach (vdPolyline shape in room.shapeList)
                    {
                        gPoints points = new gPoints();
                        vdCurves cu;
                        bool isRelation;
                        if (connector.shape.IsClockwise()) cu = connector.shape.getOffsetCurve(5);
                        else cu = connector.shape.getOffsetCurve(-5);    

                        if (cu.Count > 0) { 
                            isRelation = cu[0].IntersectWith(shape, VectorDraw.Professional.Constants.VdConstInters.VdIntOnBothOperands, points);
                            }else {
                            isRelation = connector.shape.IntersectWith(shape, VectorDraw.Professional.Constants.VdConstInters.VdIntOnBothOperands, points);

                        }
                            
                            if (isRelation)
                            {

                            if (points.Length() > 500)
                            {

                                connector.roomList.Add(room);
                                connector.roomPolylinesList.Add(shape);
                                room.connectorList.Add(connector);
                            }
                                break;
                            }
                        
                    }
                }
            }

            foreach (Room room in floor.roomList)
            {
                foreach (Connector connector in room.connectorList)
                {
                    foreach (Room room1 in connector.roomList)
                    {
                        if (room != room1) room.relationRoomList.Add(room1);
                    }
                }
            }


            roomRelations.Clear();
            for (int i = 0; i < floor.roomList.Count; i++)
            {
                Room room1 = floor.roomList[i];
                //All possible routes from startPt to EndPt is retrieved and stored in HistoryList
                for (int j = 0; j < floor.roomList.Count; j++)
                {
                    Room room2 = floor.roomList[j];
                    if (i == j) continue;
                    RoomRelation rr = new RoomRelation(room1, room2);
                    roomRelations.Add(rr);
                }
            }
        }
    }
}
