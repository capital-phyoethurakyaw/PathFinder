namespace PathFinder.gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using PathFinder.util;
    using static System.Net.Mime.MediaTypeNames;

    public delegate void SequenceGroupChangedEventHandler(object sender, EventArgs e);
    public delegate void SequenceNameChangedEventHandler(object sender, EventArgs e);   
    public delegate void SequenceSelectionChangedEventHanlder(object sender, EventArgs e);
    public delegate void SequenceHighlightChangedEventHandler(object sender, List<Room> rooms);  
    
    public partial class SequenceSettingControlcs : UserControl
    {
       
        Info info;
//        SequenceSettingForm ssf = new SequenceSettingForm();
        public event SequenceGroupChangedEventHandler sequenceGroupChangedReceiveMsg;
        public event SequenceNameChangedEventHandler sequenceNameChangedReceiveMsg;
        public event SequenceSelectionChangedEventHanlder sequenceSelectionChangedEventHanlder;
        public event SequenceHighlightChangedEventHandler sequenceHighlightChangedReceiveMsg;   
        public SequenceSettingControlcs()
        {
            InitializeComponent();
        }

        public void setRoomList(List<Room> roomList)
        {

            foreach (Room r in roomList)
            {
                roomListDataGridView.Rows.Add(new object[] { r });
            }



            // roomListDataGridView.Columns["Room"].SortMode = DataGridViewColumnSortMode.Automatic;
        }

        public void setRoomGroups(Info info)
        {
            this.info = info;
            addRoomGroup(info);

        }
        public void addRoomGroup(Info info)
        {
            this.info = info;
            roomGroupDataGridView1.Rows.Clear();

            foreach (RoomGroup rg in info.roomGroups)
            {
                object[] obs = new object[] { rg.name, rg.getOrder(), rg.getRooms() };
                roomGroupDataGridView1.Rows.Add(obs);
            }
        }
        private void deleteSequnce(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.sequenceSettingdataGridView.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                this.info.sequenceGroups.Remove((SequenceGroup)row.Cells[0].Value);
                this.sequenceSettingdataGridView.Rows.Remove(row);
            }
            sequenceGroupChangedReceiveMsg(this, e);
        }

        private void AddRoomsToSequence_OnClick(object sender, EventArgs e)
        {

            if (roomListDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection str = this.roomListDataGridView.SelectedRows;
                foreach (DataGridViewRow s in str)
                {
                    Room nm = (Room)s.Cells[0].Value;
                    bool roomListContain = this.listBox1.Items.Contains(nm);
                    if (roomListContain)
                    {
                        DialogResult dialogResult = MessageBox.Show("중복된 룸이 선택됐습니다. 중복된 값을 추가하시겠습니까?", "중복된 값 메시지",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.OK)
                        {
                            MessageBox.Show("확인", "중복된 값 확인 메시지",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        }

                        else if (dialogResult == DialogResult.Cancel)
                        {
                            MessageBox.Show("확인", "중복된 값 취소 메시지",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                            break;

                        }


                    }
                    this.listBox1.Items.Add(nm);
                }
            }

            //room group
            if (this.roomGroupDataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection str = this.roomGroupDataGridView1.SelectedRows;
                foreach (DataGridViewRow s in str)
                {
                    RoomGroup nm = (RoomGroup)s.Cells[0].Value;
                    this.listBox1.Items.Add(nm);
                }
            }

            roomListDataGridView.ClearSelection();
            roomGroupDataGridView1.ClearSelection();

        }


        private void MoveRoomUpInSeq(object sender, EventArgs e)
        {

            int selectedIndex = this.listBox1.SelectedIndex;
            if (selectedIndex > 0)
            {
                this.listBox1.Items.Insert(selectedIndex - 1, this.listBox1.Items[selectedIndex]);
                this.listBox1.Items.RemoveAt(selectedIndex + 1);
                this.listBox1.SelectedIndex = selectedIndex - 1;
            }
        }

        private void MoveRoomDownInSeq(object sender, EventArgs e)
        {
            int selectedIndex = this.listBox1.SelectedIndex;
            if (selectedIndex < this.listBox1.Items.Count - 1 & selectedIndex != -1)
            {
                this.listBox1.Items.Insert(selectedIndex + 2, this.listBox1.Items[selectedIndex]);
                this.listBox1.Items.RemoveAt(selectedIndex);
                this.listBox1.SelectedIndex = selectedIndex + 1;

            }
        }

        private void RemoveRoomsFromSequence_OnClick(object sender, EventArgs e)
        {
            //if (this.listBox1.SelectedItems.Count > 0) this.listBox1.Items.Remove(this.listBox1.SelectedItems[0]);
            for (int x = this.listBox1.SelectedItems.Count - 1; x >= 0; x--)
            {
                RoomAndGroupObject ob = (RoomAndGroupObject)this.listBox1.SelectedItems[x];
                listBox1.Items.Remove(ob);
            }

        }


        

        

        private void roomGroupDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            roomListDataGridView.ClearSelection();
        }

        private void roomListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            roomGroupDataGridView1.ClearSelection();
        }

        public void setSequenceGroups(List<SequenceGroup> sequenceGroups) {
            this.sequenceSettingdataGridView.Rows.Clear();
            this.info.sequenceGroups = sequenceGroups;
            foreach (SequenceGroup sg in this.info.sequenceGroups) {
                this.sequenceSettingdataGridView.Rows.Add(new object[] {sg.id, sg.definedSequence.name, sg.definedSequence.frequency, sg.definedSequence.getNames() });
                string text = "";
                foreach (RoomAndGroupObject rgo in sg.definedSequence.roomList)
                {
                    text += rgo.name + Protocol.Delimiter_Rooms;
                }
            }
        }


        private void SequenceSettingList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int iCellNum = this.sequenceSettingdataGridView.CurrentCell.ColumnIndex;
            int iRetCellNum = this.sequenceSettingdataGridView.Columns["Frequency"].Index;

            if(iCellNum==iRetCellNum)
            {
                e.Control.KeyPress += new KeyPressEventHandler(IsNumericCheck);
            }
        }

        private void IsNumericCheck(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void roomListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           info.selectRooms.Clear();
            for (int i = 0; i < this.roomListDataGridView.SelectedRows.Count; i++)
            {
               
                    Room r = (Room)roomListDataGridView.SelectedRows[i].Cells[0].Value;

                    info.selectRooms.Add(r);
              

            }
            sequenceHighlightChangedReceiveMsg(sender, info.selectRooms); 
        }

        private void sequenceSettingdataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0) return;
            SequenceGroup sg = (SequenceGroup)this.sequenceSettingdataGridView.Rows[index].Cells[0].Value;
          
         
        }

        private void roomListDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sequenceSettingdataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sequenceSettingdataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            Console.WriteLine(e.RowIndex + " " + e.ColumnIndex); 

            string name = (string)this.sequenceSettingdataGridView.Rows[e.RowIndex].Cells[0].Value;
            SequenceGroup sg = info.sequenceGroups[e.RowIndex];
            sg.definedSequence.name = name;
            try
            {
                int fr = int.Parse(this.sequenceSettingdataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

                sg.definedSequence.frequency = fr;

            }
            catch (FormatException ex) {

                MessageBox.Show("");
            
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in roomListDataGridView.Rows)
            {
                int n = roomGroupDataGridView1.Rows.Add();
                foreach (DataGridViewColumn col in roomGroupDataGridView1.Columns)
                {
                    roomGroupDataGridView1.Rows[n].Cells[col.Index].Value = roomGroupDataGridView1.Rows[row.Index].Cells[col.Index].Value.ToString();
                }
            }
        }

        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       

        

        

       

      

        

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = this.sequenceSettingdataGridView.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                string SequenceGroup = row.Cells[0].Value.ToString();
                this.sequenceSettingdataGridView.Rows.Remove(row);
            }
            sequenceGroupChangedReceiveMsg(this, e);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SequenceGroup sg = new SequenceGroup();
            sg.definedSequence = new DefinedSequence();
            sg.definedSequence.name = "DefinedSequence" + this.info.sequenceGroups.Count;
            if (this.listBox1.Items.Count > 0)
            {
                foreach (var item in this.listBox1.Items)
                    sg.definedSequence.roomList.Add((RoomAndGroupObject)item); // definded seq
                this.sequenceSettingdataGridView.Rows.Add(new object[] { sg.id, sg, sg.definedSequence.frequency, sg.definedSequence.getNames() });
                info.sequenceGroups.Add(sg);


                sequenceGroupChangedReceiveMsg(this, e);
            }
        }

        

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            sequenceSettingdataGridView.EndEdit();
            foreach (DataGridViewRow dv in sequenceSettingdataGridView.Rows)
            {
                info.sequenceGroups.Where(w => w.id == Convert.ToDouble(dv.Cells["id"].Value)).ToList().ForEach(i => i.definedSequence.name = dv.Cells["SequenceName"].Value.ToString()
                );
                info.sequenceGroups.Where(w => w.id == Convert.ToDouble(dv.Cells["id"].Value)).ToList().ForEach(i => i.definedSequence.frequency = Convert.ToInt32((dv.Cells["Frequency"].Value.ToString())));
            }

            ReadWriteUtil.saveSequence(this.info.sequenceGroups);
          sequenceGroupChangedReceiveMsg(this, e);
           
        }
    }

}
