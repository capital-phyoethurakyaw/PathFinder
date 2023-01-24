namespace PathFinder.util
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal class ReadWriteUtil
    {
        public static void saveSequence(List<SequenceGroup> sequenceGroups)
        {
            try
            {
                string text = "id,Name,Rooms,Frequence";
                foreach (SequenceGroup sg in sequenceGroups)
                {
                    text += "\n" + sg.id + ",";
                    text += sg.definedSequence.name + ",";
                    text += sg.definedSequence.getNames() + ",";
                    text += sg.definedSequence.frequency;
                }
                string path = Info.fileInfo.Directory.FullName;
                string fileName = Info.fileInfo.Name.Replace(Info.fileInfo.Extension, "_sg.csv");
                string file = path + "\\" + fileName;
                if (File.Exists(file)) File.Delete(file);
                StreamWriter writer = new StreamWriter(file, false, Encoding.Unicode);
                writer.Write(text);   //저장될 string
                writer.Close();
            }
            catch
            {

            }
        }


        public static void saveRoomGroupList(List<RoomGroup> roomGroups)
        {
            string path = Info.fileInfo.Directory.FullName;
            string fileName = Info.fileInfo.Name.Replace(Info.fileInfo.Extension, "_rg.csv");
            string file = path + "\\" + fileName;
            string text = "Name,Order,Rooms";
            foreach (RoomGroup roomGroup in roomGroups)
            {
                text += "\n" + roomGroup.name + "," + roomGroup.getOrder() + "," + roomGroup.getRooms();
            }
            StreamWriter writer = new StreamWriter(file, false, Encoding.Unicode);
            writer.Write(text);   //저장될 string
            writer.Close();
        }
        public static void saveMainRouteList(List<MainRoute> mainRoutes)
        {
            string path = Info.fileInfo.Directory.FullName;
            string fileName = Info.fileInfo.Name.Replace(Info.fileInfo.Extension, "_mr.csv");
            string file = path + "\\" + fileName;
            string text = "Name,Rooms";
            foreach (MainRoute mainRoute in mainRoutes)
            {
                text += "\n" + mainRoute.name + "," + mainRoute.getRooms();
            }
           
            StreamWriter writer = new StreamWriter(file, false, Encoding.Unicode);

            writer.Write(text);    
            writer.Close();
        }

        public static void saveResult(Info info)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "CSV Documents|*.csv";
            DialogResult dialogResult =  sf.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            StringBuilder sb = new StringBuilder();
         
            for (int i = 0; i < info.sequenceGroups.Count; i++)
            {
                SequenceGroup sequenceGroup = (SequenceGroup)info.sequenceGroups[i];
                int frequence = sequenceGroup.definedSequence.frequency;
                double distance = sequenceGroup.getPathLength();
                string text = (i) + "\t"+"SequenceGroup" + "\t"+ sequenceGroup.definedSequence.name+  "\t" + sequenceGroup.definedSequence.name + "\t" + distance + "\t" + frequence + "\t" + (distance * frequence) + "\n";
                sb.Append(text);
                foreach (Sequence sequence in sequenceGroup.sequences)
                {
                        double dis = 0;
                        dis = sequence.getshortestDistance();
                        string sequenceShortestSubSequence = "";
                        if (sequence.shortestSubSequence != null) sequenceShortestSubSequence = sequence.shortestSubSequence.ToString();
                        text = "\t" + "\t" + "Sequence" + "\t" + sequence.sRoom + "\t" + sequence.eRoom + "\t" + dis
                            + "\t" + "Shortest SubSequence Index("+ sequence.getShortestSubSequenceIndex()+")\t" + sequenceShortestSubSequence + "\n";
                        sb.Append(text);
                        int c = 0;
                        foreach (SubSequence subSequence in sequence.subSequences)
                        {
                            text = "\t" + "\t" + "\t" + "SubSequence" + "\t" + c++ +"\t" + subSequence.ToString() + "\t" +subSequence.getDistanceValue()+ "\t" + subSequence.isRoute+ "\n";
                            sb.Append(text);
                        }
                    
                }
            }
            StreamWriter writer= new StreamWriter(sf.FileName, false, Encoding.Unicode);
            writer.Write(sb.ToString());
            writer.Close();
        }

        public static void saveJPG(Info info)
        {
            //test
            PathFinderBimForm pf = new PathFinderBimForm();
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG Image|*.jpeg";
            if (sf.ShowDialog(pf) == DialogResult.OK)
            {
                //pf.vdFramedControl1.ActiveDocument.vectorDrawBaseControl1.ActiveDocument.SaveAs(sf.FileName);
            }

        }

        public static void SortColumnOrder(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dgv.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dgv.SortedColumn;
            ListSortDirection direction;

            if (oldColumn == newColumn)
            {
                // Sorting by the same column: toggle between ASC and DESC:
                direction = dgv.SortOrder == SortOrder.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                // Sorting by a new column.

                // Choose the default direction based on the column name:
                switch (dgv.Columns[e.ColumnIndex].Name)
                {
                    case "Room":
                        direction = ListSortDirection.Descending;
                        break;
                    default:
                        direction = ListSortDirection.Ascending;
                        break;
                }

                // Remove the sorting glyph from the old column, if any:
                if (oldColumn != null)
                {
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }

            dgv.Sort(newColumn, direction);

            newColumn.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }


        public static void readSequenceList(Floor floor, List<RoomGroup> roomGroups, List<SequenceGroup> sequenceGroups)
        {
            string path = Info.fileInfo.Directory.FullName;
            string fileName = Info.fileInfo.Name.Replace(Info.fileInfo.Extension, "_sg.csv");
            string file = path + "\\" + fileName;
            FileInfo fi = new FileInfo(file);
            if (!fi.Exists) return;
            string[] lines = System.IO.File.ReadAllLines(file);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] rows = lines[i].Split(',');
                    string id = rows[0];
                    string sgName = rows[1];
                    string[] rgo = rows[2].Split(Protocol.Delimiter_Rooms); //room or group
                    List<RoomAndGroupObject> roomList = new List<RoomAndGroupObject>();
                    for (int j = 0; j < rgo.Length; j++)
                    {
                        RoomAndGroupObject room = FindUtil.findRoom(floor, rgo[j]);
                        if (room != null) roomList.Add(room);
                        room = FindUtil.findGroup(roomGroups, rgo[j]);
                        if (room != null) roomList.Add(room);
                    }
                    SequenceGroup sg = new SequenceGroup();
                    sg.id = Convert.ToDouble(id);
                    sg.definedSequence.name = sgName;
                    sg.definedSequence.roomList = roomList;
                    sg.definedSequence.frequency = int.Parse(rows[3]);
                    sequenceGroups.Add(sg);
                }
                catch
                {
                    return;
                }
            }
        }

        public static void readRoomGroupList(Floor floor, List<RoomGroup> roomGroups)
        {
            string path = Info.fileInfo.Directory.FullName;
            string fileName = Info.fileInfo.Name.Replace(Info.fileInfo.Extension, "_rg.csv");
            string file = path + "\\" + fileName;
            FileInfo fi = new FileInfo(file);
            if (!fi.Exists) return;
            string[] lines = System.IO.File.ReadAllLines(file);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                string rgName = rows[0];
                bool isOrder = false;
                if (rows[1] == RoomGroup.Order) isOrder = true;
                else isOrder = false;
 
                string rooms = rows[2];
                string[] rooom = rooms.Split(Protocol.Delimiter_Rooms);   

                List<Room> roomList = new List<Room>();
                for (int j = 0; j < rooom.Length; j++)
                {
                    Room room = FindUtil.findRoom(floor, rooom[j]);
                    roomList.Add(room);
                }
                RoomGroup rg = new RoomGroup(rgName, roomList, isOrder);
                roomGroups.Add(rg);
            }
        }

        public static void readMainRouteList(Floor floor, List<MainRoute> mainRoutes)
        {
            string path = Info.fileInfo.Directory.FullName;
            string fileName = Info.fileInfo.Name.Replace(Info.fileInfo.Extension, "_mr.csv");
            string file = path + "\\" + fileName;
            FileInfo fi = new FileInfo(file);
            if (!fi.Exists) return;
            string[] lines = System.IO.File.ReadAllLines(file);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                string rgName = rows[0];
                string rooms = rows[1];
                string[] rooom = rooms.Split(Protocol.Delimiter_Rooms);

                List<Room> roomList = new List<Room>();
                for (int j = 0; j < rooom.Length; j++)
                {
                    Room room = FindUtil.findRoom(floor, rooom[j]);
                    roomList.Add(room);
                }
                MainRoute rg = new MainRoute(rgName, roomList);
                mainRoutes.Add(rg);
            }
        }
    }
}
