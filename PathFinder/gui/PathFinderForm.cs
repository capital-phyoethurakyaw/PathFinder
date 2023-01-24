using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vdIFC;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
/*using ClipperLib;*/
using VectorDraw.Generics;
using VectorDraw.Professional.Constants;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using PathFinder.util;
using System.Runtime.CompilerServices;
using PathFinder.analysis;
using PathFinder.gui;
using static RenderFormats.PrimitiveRender3d;
using System.Threading;
using VectorDraw.SolidModel;
using VectorDraw.Render;
using System.Drawing.Imaging;

namespace PathFinder
{
    public partial class PathFinderForm : Form
    {
        public Info info;
        vdPolyline routeLine = null;
        Thread analysisThread = null;
        private vdRender render;

        public PathFinderForm()
        {
            InitializeComponent();
            this.Visible = true;
            info = new Info();

            roomGroupControl1.groupChangedReceiveMsg += RoomGroupControl1_groupChangedReceiveMsg;
            roomGroupControl1.roomSelectionChangedEventMsg += RoomGroupControl1_roomSelectionChangedEventMsg;
            roomGroupControl1.roomHighlightChangedEventMsg += RoomGroupControl1_roomHighlightChangedEventMsg;
            sequenceSettingControlcs1.sequenceGroupChangedReceiveMsg += sequenceSettingControlcs1_sequenceGroupChangedReceiveMsg;
            sequenceSettingControlcs1.sequenceNameChangedReceiveMsg += SequenceSettingControlcs1_sequenceNameChangedReceiveMsg;
            sequenceSettingControlcs1.sequenceSelectionChangedEventHanlder += SequenceSettingControlcs1_sequenceSelectionChangedEventHanlder;
            sequenceSettingControlcs1.sequenceHighlightChangedReceiveMsg += SequenceSettingControlcs1_sequenceHighlightChangedReceiveMsg;
            mainRouteControl1.mainRouteSelectionChangedEventMsg += MainRouteControl1_mainRouteSelectionChangedEventMsg;
            mainRouteControl1.mainRouteHighlightChangedEventMsg += MainRouteControl1_mainRouteHighlightChangedEventMsg;
            algorithComboBox.SelectedIndex = 0;
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void SequenceSettingControlcs1_sequenceHighlightChangedReceiveMsg(object sender, List<Room> rooms)
        {
            vectorDrawBaseControl1.Redraw();
        }

        private void SequenceSettingControlcs1_sequenceSelectionChangedEventHanlder(object sender, EventArgs e)
        {
            vectorDrawBaseControl1.Redraw();
        }

        private void MainRouteControl1_mainRouteHighlightChangedEventMsg(object sender, List<Room> e)
        {
            vectorDrawBaseControl1.Redraw();
        }

        private void MainRouteControl1_mainRouteSelectionChangedEventMsg(object sender, List<Room> e)
        {
            vectorDrawBaseControl1.Redraw();
        }

        private void RoomGroupControl1_roomHighlightChangedEventMsg(object sender, List<Room> e)
        {
            vectorDrawBaseControl1.Redraw();
        }

        private void SequenceSettingControlcs1_sequenceNameChangedReceiveMsg(object sender, EventArgs e)
        {
            analysisRouteControl1.setSequence();
        }

        private void RoomGroupControl1_roomSelectionChangedEventMsg(object sender, List<Room> e)
        {
            vectorDrawBaseControl1.Redraw();
        }



        private void RoomGroupControl1_groupChangedReceiveMsg(object sender, EventArgs e)
        {
            sequenceSettingControlcs1.setRoomGroups(this.info);
            this.sequenceSettingControlcs1.setSequenceGroups(info.sequenceGroups);
        }

        private void sequenceSettingControlcs1_sequenceGroupChangedReceiveMsg(object sender, EventArgs e)
        {
            info.setsequenceGroup();
            analysisRouteControl1.setSequence(info, this.vectorDrawBaseControl1.ActiveDocument);
        }


        public void setData()
        {
            List<RoomTable> Rlist1 = new List<RoomTable>();

            this.roomGroupControl1.setRoomList(info.floor.roomList);
            this.sequenceSettingControlcs1.setRoomList(info.floor.roomList);
            this.mainRouteControl1.setRoomList(info.floor.roomList);



            IFCToCAD.setRoomRelation(info.floor, info.roomRelations, this.vectorDrawBaseControl1.ActiveDocument);

            IFCToCAD.setRelationObstacleWithRoom(info.floor);





            List<Connector> connectors = new List<Connector>();
            foreach (Connector c in info.floor.connectorList)
            {
                if (c.roomList.Count > 2)
                {
                    c.shape.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeDoubleHatch);
                    c.shape.PenColor = new vdColor(Color.Blue);
                }
                if (c.roomList.Count == 0)
                {
                    c.shape.PenColor = new vdColor(Color.Green);
                    c.shape.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeHatchCross);

                }
                if (c.roomList.Count == 1)
                {
                    c.shape.PenColor = new vdColor(Color.Magenta);
                    c.shape.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeHatchDiagCross);

                }
            }

            foreach (Connector c in connectors)
            {
                info.floor.connectorList.Remove(c);
            }



            ReadWriteUtil.readRoomGroupList(info.floor, info.roomGroups);
            ReadWriteUtil.readSequenceList(info.floor, info.roomGroups, info.sequenceGroups);
            ReadWriteUtil.readMainRouteList(info.floor, info.mainRoutes);

            this.roomGroupControl1.setRoomGroups(info);
            this.sequenceSettingControlcs1.setRoomGroups(info);
            this.sequenceSettingControlcs1.setSequenceGroups(info.sequenceGroups);
            this.analysisRouteControl1.setSequence(info, this.vectorDrawBaseControl1.ActiveDocument);
            this.mainRouteControl1.setMainRoutes(info);


            var rtList = new List<RouteTable>();
            int count = 0;
            foreach (RoomRelation rr in info.roomRelations)
            {
                string n1 = rr.sRoom.name;
                string n2 = rr.eRoom.name;
                if (rr.roomLists.Count > 0)
                {

                    foreach (ArrayList list in rr.roomLists)
                    {
                        string rooms = "";
                        foreach (Room r in list) rooms += r.name + ",";
                        rooms = rooms.Substring(0, rooms.Length - 1);
                        rtList.Add(new RouteTable()
                        {
                            Index = count++,
                            SRoom = n1,
                            ERoom = n2,
                            Route = rooms
                        }); ;
                    }
                }
            }

            info.setsequenceGroup();

            dataGridView1.DataSource = rtList;


            this.tabPage2.Text = info.floor.name;

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();

            sd.Filter = "AutoCAD|*.dwg";
            List<vdLayer> newLayers = new List<vdLayer>();
            List<vdPolyline> newPolylines = new List<vdPolyline>();  
            List<vdCircle> newCircles = new List<vdCircle>();

            if (sd.ShowDialog(this) == DialogResult.OK)
            {
                short count = 10;
                foreach (SequenceGroup sequenceGroup in this.info.sequenceGroups)
                {
                    count++;
                    vdLayer layer = this.vectorDrawBaseControl1.ActiveDocument.Layers.Add(sequenceGroup.definedSequence.name);
                    newLayers.Add(layer);


                    vdPolyline poly = new vdPolyline(this.vectorDrawBaseControl1.ActiveDocument, sequenceGroup.getShortestPath());
                    newPolylines.Add(poly);

                    layer.PenColor.ColorIndex = count; 
                    poly.Layer = layer;
                   
                    Console.WriteLine("color" + layer.PenColor.SystemColor.ToString());   
                    layer.Name = sequenceGroup.ToString();
                    
                    this.vectorDrawBaseControl1.ActiveDocument.ActiveLayOut.Entities.AddItem(poly);
                    poly.SetUnRegisterDocument(this.vectorDrawBaseControl1.ActiveDocument);
                    poly.setDocumentDefaults();
                    //poly.PenColor = layer.PenColor; // Using system color
                    poly.LineWeight = VdConstLineWeight.LW_50;

                    List<vdPolyline> polylines2 = writeTriangle(poly);
                    foreach(vdPolyline polyline in polylines2)
                    {

                        polyline.Layer = layer;
                        this.vectorDrawBaseControl1.ActiveDocument.ActiveLayOut.Entities.AddItem(polyline);
                        polyline.SetUnRegisterDocument(this.vectorDrawBaseControl1.ActiveDocument);
                        polyline.setDocumentDefaults();
                        
                    }      
                   

                    List<vdPolyline> polylines = sequenceGroup.getShortestPaths(this.vectorDrawBaseControl1.ActiveDocument);
                      Console.WriteLine("polyline c" + polylines.Count);
                    if (polylines.Count == 0) continue;

                        vdPolyline first = polylines.First();

                        vdCircle vdCircle = new vdCircle((this.vectorDrawBaseControl1.ActiveDocument));
                        newCircles.Add(vdCircle);
                        vdCircle.Layer = layer;
                        this.vectorDrawBaseControl1.ActiveDocument.Model.Entities.AddItem(vdCircle);
                        vdCircle.SetUnRegisterDocument(this.vectorDrawBaseControl1.ActiveDocument);
                        vdCircle.setDocumentDefaults();
                        vdCircle.Center = first.getStartPoint();
                        poly.PenColor.SystemColor = Color.Green; //Using system color 
                        vdCircle.Radius = 800;
                        vdCircle.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);

                        vdPolyline last = polylines.Last();
                        vdCircle vdCircle2 = new vdCircle((this.vectorDrawBaseControl1.ActiveDocument));
                        newCircles.Add(vdCircle2);
                        vdCircle.Layer = layer;
                        this.vectorDrawBaseControl1.ActiveDocument.Model.Entities.AddItem(vdCircle2);
                        vdCircle2.SetUnRegisterDocument(this.vectorDrawBaseControl1.ActiveDocument);
                        vdCircle2.setDocumentDefaults();
                        vdCircle2.Center = last.getEndPoint();
                        poly.PenColor.SystemColor = Color.Green;  //Using system color 
                        vdCircle2.Radius = 800;
                        vdCircle2.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                    
                    foreach (vdPolyline poly2 in polylines)
                    {
                        if (poly2 != null)
                        {
                            vdCircle circle3 = new vdCircle(this.vectorDrawBaseControl1.ActiveDocument);
                            newCircles.Add(circle3);
                            circle3.Center = poly2.getStartPoint();
                            circle3.Radius = 400;
                            circle3.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                            circle3.PenColor.Red = 255; circle3.PenColor.Green = 0; circle3.PenColor.Blue = 0;
                            this.vectorDrawBaseControl1.ActiveDocument.Model.Entities.AddItem(circle3);
                            vdCircle circle4 = new vdCircle(this.vectorDrawBaseControl1.ActiveDocument);
                            newCircles.Add(circle4);
                            circle4.Center = poly.getEndPoint();
                            circle4.Radius = 400;
                            circle4.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                            circle4.PenColor.Red = 255; circle4.PenColor.Green = 0; circle4.PenColor.Blue = 0;
                            this.vectorDrawBaseControl1.ActiveDocument.Model.Entities.AddItem(circle4);
                        }
                    }
                }


                this.vectorDrawBaseControl1.ActiveDocument.Update();

                this.vectorDrawBaseControl1.ActiveDocument.SaveAs(sd.FileName);

                foreach (vdCircle circle in newCircles) {
                    this.vectorDrawBaseControl1.ActiveDocument.ActiveLayOut.Entities.RemoveItem(circle);
                }
                foreach (vdPolyline poly in newPolylines)
                {
                    this.vectorDrawBaseControl1.ActiveDocument.ActiveLayOut.Entities.RemoveItem(poly);
                }
                foreach (vdLayer layer in newLayers)
                {
                    this.vectorDrawBaseControl1.ActiveDocument.Layers.RemoveItem(layer);
                }
                this.vectorDrawBaseControl1.ActiveDocument.Update();
                this.vectorDrawBaseControl1.ActiveDocument.Redraw(true);




            }

        }

        public void importIFC(vdIFCBuildingStorey storey, vdDocument doc)
        {
            IFCToCAD.getIFCToCAD(storey, info.floor, doc);
            this.vectorDrawBaseControl1.ActiveDocument.Redraw(true);
            this.vectorDrawBaseControl1.ActiveDocument.ZoomExtents();
            this.setData();
        }

        private void vectorDrawBaseControl1_DrawAfter(object sender, VectorDraw.Render.vdRender render)
        {

            Graphics gr = this.vectorDrawBaseControl1.ActiveDocument.GlobalRenderProperties.GraphicsContext.MemoryGraphics;
            Rectangle rc = new Rectangle(new Point(0, 0), this.vectorDrawBaseControl1.ActiveDocument.GlobalRenderProperties.GraphicsContext.MemoryBitmap.Size);
            rc.Inflate(1, 1);
            Font font = new Font("Verdana", 20);
            ;


            try
            {
                drawText(info.floor, font, Color.White, render);
                drawRoute(render);
                selectRooms(render);

            }
            catch (Exception ex) {

            }
        }

        public void drawText(Floor floor, Font font, Color color, VectorDraw.Render.vdRender render)
        {
            try
            {
                foreach (Room room in floor.roomList)
                {
                    room.text.PenColor.SystemColor = color;
                    room.text.Update();
                    room.text.Draw(render);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        public List<vdPolyline> writeTriangle(vdPolyline polyline)
        {


            int count = (int)(polyline.Length() / 2000);
            Console.WriteLine("gpsCount" + count);
            Console.WriteLine("Lenght" + polyline.Length());

            List<vdPolyline> polylines = new List<vdPolyline>();    
            gPoints gps = polyline.Divide(count);
            if (gps != null)
            {
                Console.WriteLine("gps.Length" + gps.Length());

                foreach (gPoint gp in gps)
                {


                        int index = polyline.SegmentIndexFromPoint(gp, 0.1);
                        vdCurve curve = polyline.GetSegmentAtPoint(gp);
                        polyline.IsClockwise();
                        gPoint firstPoint = curve.getStartPoint();
                        gPoint lastPoint = curve.getEndPoint();



                        Vector v = new Vector(firstPoint, lastPoint);
                        double angle = v.Angle2DDirection() - Math.PI / 2.0;

                        Console.WriteLine("gps.Angle" + angle);
                        vdPolyline poly = new vdPolyline(this.vectorDrawBaseControl1.ActiveDocument);

                        poly.VertexList.Add(new VectorDraw.Geometry.gPoint(-100, -100));
                        poly.VertexList.Add(new VectorDraw.Geometry.gPoint(100, -100));
                        poly.VertexList.Add(new VectorDraw.Geometry.gPoint(0, 100));
                        poly.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                        poly.PenColor = new vdColor(Color.Yellow);
                        poly.LineWeight = VdConstLineWeight.LW_25;
                        poly.Flag = VdConstPlineFlag.PlFlagCLOSE;
                        Matrix m = new Matrix();
                        m.RotateZMatrix(angle);
                        m.TranslateMatrix(gp);
                        poly.Transformby(m);
                        //poly.Draw(render);
                        polylines.Add(poly);
                    
                }

            }

            return polylines;
        }
        public void drawRoute(VectorDraw.Render.vdRender render)
        {
            try
            {

                if (info.route != null) info.route.Draw(render);
                List<vdPolyline> polylines = writeTriangle(info.route);
                foreach(vdPolyline polyline in polylines) polyline.Draw(render);  
               
               
                        if (info.routes != null)
                {

                    vdPolyline first = info.routes.First();
                    if (first != null)
                    {
                        vdCircle circle11 = new vdCircle(this.vectorDrawBaseControl1.ActiveDocument);
                        circle11.Center = first.getStartPoint();
                        circle11.Radius = 800;
                        circle11.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                        circle11.PenColor.Red = 0; circle11.PenColor.Green = 0; circle11.PenColor.Blue = 255;
                        circle11.Draw(render);
                    }
                    vdPolyline last = info.routes.Last();
                    if (last != null)
                    {
                        vdCircle circle22 = new vdCircle(this.vectorDrawBaseControl1.ActiveDocument);
                        circle22.Center = last.getEndPoint();
                        circle22.Radius = 800;
                        circle22.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                        circle22.PenColor.Red = 0; circle22.PenColor.Green = 0; circle22.PenColor.Blue = 255;
                        circle22.Draw(render);

                    }



                    foreach (vdPolyline poly in info.routes)
                    {
                        if (poly != null)
                        {
                            poly.Draw(render);
                            vdCircle circle1 = new vdCircle(this.vectorDrawBaseControl1.ActiveDocument);
                            circle1.Center = poly.getStartPoint();
                            circle1.Radius = 500;
                            circle1.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                            circle1.PenColor.Red = 255; circle1.PenColor.Green = 0; circle1.PenColor.Blue = 0;

                            circle1.Draw(render);


                            vdCircle circle2 = new vdCircle(this.vectorDrawBaseControl1.ActiveDocument);
                            circle2.Center = poly.getEndPoint();
                            circle2.Radius = 500;
                            circle2.HatchProperties = new VectorDraw.Professional.vdObjects.vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                            circle2.PenColor.Red = 255; circle2.PenColor.Green = 0; circle2.PenColor.Blue = 0;

                            circle2.Draw(render);


                            vdPolyline divide = new vdPolyline(this.vectorDrawBaseControl1.ActiveDocument);




                        }
                    }
                }


            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void selectRooms(VectorDraw.Render.vdRender render)
        {
            try
            {

                if (info.selectRooms != null)
                {
                    foreach (Room room in info.selectRooms)
                    {
                        if (room.roomBoundary != null)
                        {

                            vdPolyline polyline = (vdPolyline)room.roomBoundary.Clone(this.vectorDrawBaseControl1.ActiveDocument);
                            polyline.LineWeight = VdConstLineWeight.LW_50;
                            polyline.PenColor.Red = 0; polyline.PenColor.Green = 255; polyline.PenColor.Blue = 0;

                            polyline.Draw(render);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public vdDocument getDoc() {

            return this.vectorDrawBaseControl1.ActiveDocument;

        }



        private void vectorDrawBaseControl1_GripSelectionModified(object sender, vdLayout layout, vdSelection gripSelection)
        {
            info.selectRooms.Clear();
            string text = "";
            for (int i = 0; i < gripSelection.Count; i++) {
                if (gripSelection[i] is vdPolyline)
                {
                    vdPolyline poly = (vdPolyline)gripSelection[i];
                    Object ob = poly.XProperties["Room"];
                    if (ob != null)
                    {
                        Room room = (Room)ob;
                        text += room.name + ",";
                        info.selectRooms.Add(room);
                    }
                }
            }
            if (text.Length > 0) text = text.Substring(0, text.Length - 1);
            this.seletionLabel.Text = text;
            this.vectorDrawBaseControl1.Redraw();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rw;
            List<vdPolyline> clipopers = new List<vdPolyline>();
            List<vdPolyline> obstacle = new List<vdPolyline>();
            List<Connector> connectorList = new List<Connector>();
            double[,] doorCenterPoints = new double[2, 4];

            rw = this.dataGridView1.SelectedCells[0].RowIndex;

            String rowval = this.dataGridView1.Rows[rw].Cells[3].Value.ToString();
            String[] roomNames = rowval.Split(',');




            List<Room> roomList = new List<Room>();
            string rooms = "";
            for (int iter = 0; iter < roomNames.Length; iter++)
            {
                String spstr1 = roomNames[iter];
                Room room1 = FindUtil.findRoom(info.floor, spstr1);
                roomList.Add(room1);
                rooms += spstr1 + Protocol.Delimiter_Rooms;
            }
            rooms = rooms.Substring(0, rooms.Length - 1);
            gPoints ps = null;
            bool isRoomCenter = false;
            if (this.toolStripComboBox1.SelectedIndex == 0) isRoomCenter = true;


            if (this.algorithComboBox.SelectedIndex == 0) ps = AnalysisShortDistance.getShortDistanceLikeHuman(roomList, this.info, this.vectorDrawBaseControl1.ActiveDocument, isRoomCenter);
            else ps = AnalysisShortDistance.getShortDistanceLikeMachine(roomList, this.info, this.vectorDrawBaseControl1.ActiveDocument);

            rooms += ":" + (Math.Round(ps.Length() / 10)) / 100.0 + "M";
            this.setInformation(rooms);

            info.route = new vdPolyline(this.vectorDrawBaseControl1.ActiveDocument, ps);
            info.route.PenColor = new vdColor(Color.Red);
            info.route.LineWeight = VdConstLineWeight.LW_50;

            info.routes = new List<vdPolyline>();
            this.vectorDrawBaseControl1.ActiveDocument.Model.Entities.Update();
            this.vectorDrawBaseControl1.ActiveDocument.Update();
            this.vectorDrawBaseControl1.ActiveDocument.Redraw(true);
        }

        public void setInformation(string text) {
            seletionLabel.Text = text;
        }

        private void vectorDrawBaseControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) {
                info.route = new vdPolyline();
                info.routes = new List<vdPolyline>();
                info.selectRooms.Clear();
                this.vectorDrawBaseControl1.ActiveDocument.Update();
                this.vectorDrawBaseControl1.ActiveDocument.Redraw(true);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            info.route = new vdPolyline();
            info.routes = new List<vdPolyline>();
            this.vectorDrawBaseControl1.ActiveDocument.Update();
            this.vectorDrawBaseControl1.ActiveDocument.Redraw(true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cADFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "*";
            {
                if (sd.ShowDialog(this) == DialogResult.OK)
                {
                    this.vectorDrawBaseControl1.ActiveDocument.SaveAs(sd.FileName);
                    foreach (SequenceGroup sequenceGroup in this.info.sequenceGroups)
                    {

                        vdLayer layer = this.vectorDrawBaseControl1.ActiveDocument.Layers.Add(sequenceGroup.definedSequence.name);
                        vdPolyline poly = new vdPolyline(this.vectorDrawBaseControl1.ActiveDocument, sequenceGroup.getShortestPath());
                        poly.Layer = layer;
                        this.vectorDrawBaseControl1.ActiveDocument.ActiveLayOut.Entities.AddItem(poly);
                        poly.SetUnRegisterDocument(this.vectorDrawBaseControl1.ActiveDocument);
                        poly.setDocumentDefaults();
                        poly.PenColor.SystemColor = Color.Red; // Using system color

                    }

                    
                }
            }
        }
        public class RouteTable
        {
            public int Index { get; set; }
            public string SRoom { get; set; }
            public string ERoom { get; set; }
            public string Route { get; set; }
        }

        public class RoomTable
        {
            public Room Room { get; set; }
        }

    }

}




