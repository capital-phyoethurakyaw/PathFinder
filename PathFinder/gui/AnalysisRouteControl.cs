namespace PathFinder.gui
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using PathFinder.analysis;
    using PathFinder.util;
    using VectorDraw.Geometry;
    using VectorDraw.Professional.Components;
    using VectorDraw.Professional.Constants;
    using VectorDraw.Professional.vdFigures;
    using VectorDraw.Professional.vdObjects;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

    

    public partial class AnalysisRouteControl : UserControl
    {
        Info info;
        vdDocument doc;
        Thread analysisThread = null;
        private int i;

        public AnalysisRouteControl()
        {
            InitializeComponent();
            this.typeComboBox.SelectedIndex = 0;
            this.centerTypeComboBox.SelectedIndex = 0;
            mainRouteComboBox.SelectedIndex = 0;
        }

        public void setSequence(Info info, vdDocument doc) {
            this.info = info;
            this.doc = doc;
            sequenceGroupDataGridView.Rows.Clear();
            int count = 1;

            foreach (SequenceGroup sequenceGroup in info.sequenceGroups)
            {
                object[] ob = { count++, false,  sequenceGroup, sequenceGroup.definedSequence, 0, sequenceGroup.definedSequence.frequency,0 };
                sequenceGroupDataGridView.Rows.Add(ob);
            }
            sequenceGroupDataGridView.ClearSelection();
        }

        public void setSequence( )
        {
            /*
            sequenceGroupDataGridView.Rows.Clear();
            
            sequenceDataGridView.Rows.Clear();
            subSequenceDataGridView.Rows.Clear();
            foreach (SequenceGroup sequenceGroup in info.sequenceGroups)
            {
                int count = 1;
                if (sequenceGroup.getShortestCombinedSequence() != null)
                {
                    double len = 0;
                    double frequency = sequenceGroup.definedSequence.frequency;
                    double lenFrequency = 0;        
                    if (sequenceGroup.getShortestCombinedSequence() != null)
                    {
                        len = sequenceGroup.getShortestCombinedSequence().getPathLength();
                        frequency = sequenceGroup.definedSequence.frequency;
                        lenFrequency = len * frequency; 

                    }
                        
                        len = sequenceGroup.getShortestCombinedSequence().getPathLength();
                    object[] ob = { count++, true, sequenceGroup, sequenceGroup.definedSequence, len, frequency, lenFrequency };
                    sequenceGroupDataGridView.Rows.Add(ob);
                }
            }
            sequenceGroupDataGridView.ClearSelection();
        */
            }

        /*
         public   void analyze()
         {
             //  if (this.toolStripComboBox1.SelectedIndex != 0) {
             //       MessageBox.Show("테스트 중입니다.");
             //      return;
             //  }

             sequenceDataGridView.Rows.Clear();
             subSequenceDataGridView.Rows.Clear();

             this.label1.Text = "분석을 시작합니다.";

             double sum = 0;
             Stopwatch stopwatch = new Stopwatch();
             stopwatch.Start();
             //for check process, count number seq.
             for (int i = 0; i < info.sequenceGroups.Count; i++) 
             {
                SequenceGroup sequenceGroup =  (SequenceGroup)info.sequenceGroups[i];
                  bool isSelected = (bool)sequenceGroupDataGridView.Rows[i].Cells[1].Value;

                 foreach (CombinedSequence combinedSequence in sequenceGroup.combinedSequenceList)
                 {
                     foreach (Sequence sequence in combinedSequence.sequenceList)
                     {
                         sequence.shortestSubSequence = null;
                     }
                 }

                 if (!isSelected)
                 {
                     continue;
                 }
                 foreach (CombinedSequence combinedSequence in sequenceGroup.combinedSequenceList)
                 {
                     foreach (Sequence sequence in combinedSequence.sequenceList)
                     {

                         sum++;
                     }
                 }
             }

             int count = 0;
             int sameCount = 0;
             for (int i = 0; i < info.sequenceGroups.Count; i++)
             {
                 int count2 = 1;
                 SequenceGroup sequenceGroup = (SequenceGroup)info.sequenceGroups[i];
                 bool isSelected = (bool)sequenceGroupDataGridView.Rows[i].Cells[1].Value;
                 if (!isSelected) continue;

                 bool isRoomCenter = false;
                if( this.centerTypeComboBox.SelectedIndex == 0) isRoomCenter = true;
                 foreach (CombinedSequence combinedSequence in sequenceGroup.combinedSequenceList)
                 {
                     foreach (Sequence sequence in combinedSequence.sequenceList)
                     {
                           bool isFound   = FindUtil.fineSequence(info,sequence);
                         if (!isFound)
                         {
                             foreach (SubSequence subSequence in sequence.subSequences)
                             {
                                 gPoints ps = null;
                                 if (this.typeComboBox.SelectedIndex == 0) ps = AnalysisShortDistance.getShortDistanceDD(subSequence.roomList, info, doc, isRoomCenter);
                                 else if (this.typeComboBox.SelectedIndex == 1) ps = AnalysisShortDistance.getShortDistanceLikeHuman(subSequence.roomList, info, doc, isRoomCenter);
                                 else   ps = AnalysisShortDistance.getShortDistanceLikeMachine(subSequence.roomList, info, doc); 
                                 subSequence.routeLine = ps;
                                 subSequence.distance2= ps.Length();
                             }
                         }
                         else sameCount++;



                         sequence.setShortestPath(mainRoute, minimumRoute);
                         count++;
                         this.label1.Text =  sequenceGroup.definedSequence.name +"("+ count2 +")" + ":Sequnce(" + sequence.sRoom+","+ sequence.eRoom+")";
                         progressBar2.Value = (int)(count / sum * 100);
                     }
                     count2++;
                 }
                 sequenceGroup.setShortestCombinedSequence();
             }

             info.routes = new List<vdPolyline>();
             doc.Model.Entities.Update();
             doc.Update();
             doc.Redraw(true);
             stopwatch.Stop();
             TimeSpan ti =  stopwatch.Elapsed ;

             this.label1.Text = "분석이 완료되었습니다.("+ Math.Round( ti.TotalSeconds)+"Sec)";
             MessageBox.Show("분석이 완료되었습니다.");

             setResult();

             for (int i = 0; i < info.sequenceGroups.Count; i++)
             {
                 SequenceGroup sequenceGroup = (SequenceGroup)info.sequenceGroups[i];
                 if (sequenceGroup.getShortestCombinedSequence() != null)
                 {
                     double len = sequenceGroup.getShortestCombinedSequence().getPathLength(); ;
                     double frequency = sequenceGroup.definedSequence.frequency;
                     sequenceGroupDataGridView.Rows[i].Cells[4].Value = sequenceGroup.getShortestCombinedSequence().getPathLength();
                     sequenceGroupDataGridView.Rows[i].Cells[5].Value = frequency;
                     sequenceGroupDataGridView.Rows[i].Cells[6].Value = len * frequency;

                 }
             }
         }

         public void setResult()
         {
             if (info == null) return;
             bool mainRoute = false;
             bool minimumRoute = false;
             if (mainRouteComboBox.SelectedIndex == 0) {
                   mainRoute = true;
                   minimumRoute = true;
             } else if (mainRouteComboBox.SelectedIndex ==1) mainRoute = true;
             else if (mainRouteComboBox.SelectedIndex == 2) minimumRoute = true;



             foreach (SequenceGroup sequenceGroup in info.sequenceGroups)
             {

                 foreach (CombinedSequence combinedSequence in sequenceGroup.combinedSequenceList)
                 {
                     foreach (Sequence sequence in combinedSequence.sequenceList)
                     {
                         int count = 0;
                         List<SubSequence> subSequences = new List<SubSequence>();
                         foreach (SubSequence subSequence in sequence.subSequences)
                         {
                             bool isIn = false;
                             foreach (Room room in subSequence.roomList)
                             {
                                 foreach (MainRoute mr in info.mainRoutes)
                                 {
                                     isIn = mr.isInMainRoute(room);
                                     if (isIn)
                                     {
                                         subSequences.Add(subSequence);
                                         break;
                                     }
                                 }
                             }
                             if (isIn) count++;
                         }

                         if (mainRoute)
                         {
                             if (sequence.subSequences.Count > 2)
                             {
                                 foreach (SubSequence subSequence in sequence.subSequences)
                                 {
                                     subSequence.isRoute = false;

                                 }
                                 foreach (SubSequence subSequence in subSequences)
                                 {
                                     subSequence.isRoute = true;
                                 }

                                 if (minimumRoute)
                                 {
                                     int min = int.MaxValue;
                                     foreach (SubSequence subSequence in subSequences)
                                     {
                                         if (min > subSequence.roomList.Count) min = subSequence.roomList.Count;
                                     }
                                     foreach (SubSequence subSequence in subSequences)
                                     {
                                         if (min != subSequence.roomList.Count) subSequence.isRoute = false;
                                     }
                                 }
                             }
                         }
                         else {
                             foreach (SubSequence subSequence in sequence.subSequences)
                             {
                                 subSequence.isRoute = true;

                             }
                         }

                         if (minimumRoute)
                         {
                             foreach (SubSequence subSequence in sequence.subSequences) subSequence.isRoute = false;
                             int min = int.MaxValue;
                             foreach (SubSequence subSequence in subSequences)
                             {
                                 if (min > subSequence.roomList.Count) min = subSequence.roomList.Count;
                             }
                             foreach (SubSequence subSequence in subSequences)
                             {
                                 if (min == subSequence.roomList.Count) subSequence.isRoute = true;
                             }
                         }

                         sequence.setShortestPath(mainRoute, minimumRoute, info.mainRoutes);
                     }
                 }
                 sequenceGroup.setShortestCombinedSequence();
             }
         }

         */


        public void setText(string s){
            this.label1.Text = s;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (analysisThread != null && analysisThread.IsAlive) {

                MessageBox.Show("현재 분석 중 입니다.");
                return;
            }
             analysisThread = new Thread(delegate ()
            {
                analysis();
            });
            analysisThread.Start();
        }

    

        private void sequenceDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            subSequenceDataGridView.Rows.Clear();
            Sequence sequence = (Sequence)sequenceDataGridView.Rows[e.RowIndex].Cells[1].Value;
            foreach (SubSequence subSequence in sequence.subSequences)
            {
                object[] ob = { sequence.sRoom, sequence.eRoom, subSequence, subSequence.getDistanceValue() };
                subSequenceDataGridView.Rows.Add(ob);
            }
            List<vdPolyline> vdPolylines = new List<vdPolyline>();
            if (sequence.shortestSubSequence != null)
            {
                vdPolyline poly = new vdPolyline(doc, sequence.shortestSubSequence.getRouteLine());
                CadUtil.setColorPath(poly, Color.Magenta, VdConstLineWeight.LW_50);
                vdPolylines.Add(poly);
              
            }
            info.routes = vdPolylines;
            doc.Update();
            doc.Redraw(true);

        }

     
        private void sequenceGroupDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex < 0) return;

            sequenceGroupDataGridView.UpdateCellValue( 1, e.RowIndex);

            sequenceDataGridView.Rows.Clear();  
            subSequenceDataGridView.Rows.Clear();
            int count = 1;

            SequenceGroup sequenceGroup = (SequenceGroup)sequenceGroupDataGridView.Rows[e.RowIndex].Cells[2].Value;

            foreach (Sequence sequence in sequenceGroup.sequences)
            {
                double dis = 0;
                if (sequence.shortestSubSequence != null) dis = sequence.shortestSubSequence.getDistance();
                else dis = 0;
                object[] ob = { count++, sequence, sequence.shortestSubSequence, dis };
                sequenceDataGridView.Rows.Add(ob);

            }
            List<vdPolyline> vdPolylines = sequenceGroup.getShortestPaths(doc);
            info.routes = vdPolylines;
            info.route = new vdPolyline( doc, sequenceGroup.getShortestPath());
            doc.Update();
            doc.Redraw(true);
        }

 

        private void subSequenceDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SubSequence sequence = (SubSequence)subSequenceDataGridView.Rows[e.RowIndex].Cells[2].Value;
            List<vdPolyline> vdPolylines = new List<vdPolyline>();
            vdPolyline poly = new vdPolyline(doc, sequence.getRouteLine());
            CadUtil.setColorPath(poly,Color.Yellow, VdConstLineWeight.LW_50);
            
           
            vdPolylines.Add(poly);
            info.routes = vdPolylines;
            doc.Update();
            doc.Redraw(true);
        }

        private void saveRouteButton_Click(object sender, EventArgs e)
        {
            ReadWriteUtil.saveResult(info);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (this.analysisThread != null) this.analysisThread.Abort();
            MessageBox.Show("분석을 중지하였습니다");
            this.setText("분석을 중지하였습니다");
            this.progressBar2.Value = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        

      

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (analysisThread != null && analysisThread.IsAlive)
            {

                MessageBox.Show("현재 분석 중 입니다.");
            }
            analysisThread = new Thread(delegate ()
            {
                analysis();
            });
            analysisThread.Start();
        }

        public void analysis() {

            ///1218 추가
            sequenceGroupDataGridView.EndEdit();
            ///


            sequenceDataGridView.Rows.Clear();
            subSequenceDataGridView.Rows.Clear();

            this.label1.Text = "분석을 시작합니다.";

            double sum = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            int type = typeComboBox.SelectedIndex;
            bool isRoomCenter = false;
            if (this.centerTypeComboBox.SelectedIndex == 0) isRoomCenter = true;
            bool mainRoute = false;
            bool minimumRoute = false;
            if (mainRouteComboBox.SelectedIndex == 0)
            {
                mainRoute = true;
                minimumRoute = true;
            }
            else if (mainRouteComboBox.SelectedIndex == 1) mainRoute = true;
            else if (mainRouteComboBox.SelectedIndex == 2) minimumRoute = true;

            ///1218 추가
            int workSum = 0;
            int workCount = 0;
            this.progressBar2.Value = 0;

            for (int i = 0; i < info.sequenceGroups.Count; i++)
            {
                bool isSelected = (bool)sequenceGroupDataGridView.Rows[i].Cells[1].Value;
                if (!isSelected) continue;
                SequenceGroup sequenceGroup = (SequenceGroup)info.sequenceGroups[i];
                for (int j = 0; j < sequenceGroup.definedSequence.roomList.Count; j++)
                {
                    workSum++;
                }
            }

            for (int i = 0; i < info.sequenceGroups.Count; i++)
            {
              
                bool isSelected = (bool)sequenceGroupDataGridView.Rows[i].Cells[1].Value;
                if (!isSelected) continue;
                Room preRoom = null;
                Room nextRoom = null;
             
                SequenceGroup sequenceGroup = (SequenceGroup)info.sequenceGroups[i];
                sequenceGroup.sequences.Clear();

                RoomAndGroupObject firstRoomAndGroupObject = sequenceGroup.definedSequence.roomList[0];
                Room lastRoom = null;

                if (firstRoomAndGroupObject is Room) lastRoom = (Room)firstRoomAndGroupObject;
                else
                {
                    if (((RoomGroup)firstRoomAndGroupObject).isOrder)
                    {
                        /////////////////////////////////////////////////////////////////////////
                        RoomGroup rg = (RoomGroup)firstRoomAndGroupObject;
                        lastRoom = rg.calOrderRooms(null, null, info, type, isRoomCenter,doc, mainRoute, minimumRoute);
                        sequenceGroup.sequences.AddRange(rg.shortestSequences);
                    }
                    else
                    {
                        MessageBox.Show("non-order의 그룹으로 시작할 수 없습니다.");
                        return;
                    }
                }

                Console.WriteLine(" sequenceGroup.definedSequence.roomList.Count  " + sequenceGroup.definedSequence.roomList.Count);

                for (int j = 1; j < sequenceGroup.definedSequence.roomList.Count; j++)
                {

                    ///1218 추가
                    workCount++;
                    this.progressBar2.Value = (int)( workCount/(double)workSum * 100);

                    RoomAndGroupObject ro0 = sequenceGroup.definedSequence.roomList[j - 1];
                    RoomAndGroupObject ro = sequenceGroup.definedSequence.roomList[j];
                    if (ro is Room)
                    {
                        if (lastRoom == null) MessageBox.Show("error");
                        if (lastRoom != ro)
                        {
                            Sequence sequence = new Sequence(lastRoom, (Room)ro);
                           lastRoom =  AnalysisShortDistance.setShortDistance(sequence, info, doc, isRoomCenter, type, mainRoute, minimumRoute);
                            sequenceGroup.sequences.Add(sequence);
                        }
                        //앞이 룸 그룹 non order의 경우 먼저 포함해서 계산을 하였음 추가
                    }

                    else // roomgroup
                    {
                        RoomGroup rg = (RoomGroup)ro;
                        if (((RoomGroup)ro).isOrder)
                        {
                                lastRoom = rg.calOrderRooms(lastRoom, null, info, type, isRoomCenter, doc, mainRoute, minimumRoute);
                                sequenceGroup.sequences.AddRange(rg.shortestSequences);
                        }
                        else
                        { // non-order
                            if (j == sequenceGroup.definedSequence.roomList.Count - 1)
                            {
                                //error 뒤에 없음
                                MessageBox.Show("non-order 그룹의 최종 목적지 공간이 없음. 작업을 중지함");
                                return;
                            }
                            RoomAndGroupObject ro2 = sequenceGroup.definedSequence.roomList[j + 1];
                           
                            if (ro2 is RoomGroup)
                            {
                                if (((RoomGroup)ro2).isOrder)
                                {
                                    nextRoom = ((RoomGroup)ro2).roomList.First();
                                }
                                else // non-order + non-order // 계산 못함
                                {
                                    MessageBox.Show("non-order and non-order  작업을 중지함");
                                    return;
                                }
                            }
                            else nextRoom = ((Room)ro2);

                            lastRoom = rg.calNonOrderRooms(lastRoom, nextRoom, info, type, isRoomCenter, doc, mainRoute, minimumRoute);
                            sequenceGroup.sequences.AddRange(rg.shortestSequences);
                        }
                    }

                    
                }
                

            }

            this.progressBar2.Value = 100;
            stopwatch.Stop();
            TimeSpan ti = stopwatch.Elapsed;

            this.label1.Text = "분석이 완료되었습니다.(" + Math.Round(ti.TotalSeconds) + "Sec)";
            MessageBox.Show("분석이 완료되었습니다.");
            for (int i = 0; i < info.sequenceGroups.Count; i++)
            {
                    SequenceGroup sequenceGroup = (SequenceGroup)info.sequenceGroups[i];
                    double len = sequenceGroup.getPathLength(); ;
                    double frequency = sequenceGroup.definedSequence.frequency;
                    sequenceGroupDataGridView.Rows[i].Cells[4].Value = len;
                    sequenceGroupDataGridView.Rows[i].Cells[5].Value = frequency;
                    sequenceGroupDataGridView.Rows[i].Cells[6].Value = len * frequency;
            }
        }

        private void sequenceGroupDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
 
            foreach (DataGridViewRow row in sequenceGroupDataGridView.Rows)
            {
                row.Cells[1].Value = checkBox1.Checked;


            }
           
        }

        
    }


}
