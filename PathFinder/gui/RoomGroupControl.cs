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
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Windows.Forms;
    using PathFinder.util;
    using static System.Net.Mime.MediaTypeNames;
    using static System.Net.WebRequestMethods;




    public delegate void GroupChangedEventHandler(object sender, EventArgs e);
    public delegate void RoomSelectionChangedEventHandler(object sender, List<Room> e);
    public delegate void RoomHighlightChangedEventHandler(object sender, List<Room> e);


    public partial class RoomGroupControl : UserControl
    {

        List<RoomGroup> roomGroups;
        Info info;
        public event GroupChangedEventHandler groupChangedReceiveMsg;
        public event RoomSelectionChangedEventHandler roomSelectionChangedEventMsg;
        public event RoomHighlightChangedEventHandler roomHighlightChangedEventMsg;

        private int _previousIndex;
        private bool _sortDirection;





        public RoomGroupControl()
        {
            InitializeComponent();
            groupOrderComboBox.SelectedIndex = 0;
        }

        public void setRoomGroups(Info info)
        {
            this.info = info;
            this.roomGroups = info.roomGroups;
            addRoomGroup(roomGroups);

        }
        public void setRoomList(List<Room> roomList)
        {
            foreach (Room r in roomList)
            {
                roomListDataGridView.Rows.Add(new object[] { r });
            }

            // roomListDataGridView.Columns["Room"].SortMode = DataGridViewColumnSortMode.Automatic;
            /*            roomListDataGridView.Sort(roomListDataGridView.Columns["Room"],
   System.ComponentModel.ListSortDirection.Descending);*/
            /*            var bs = new BindingSource();
                        bs.DataSource = roomListTable;
                        roomListDataGridView.DataSource=bs;
                        roomListDataGridView.Sort = "Room ASC";*/

            /*            SortableBindingList<RoomTable> myBindingSource = new SortedBindingList<RoomTable>();
                        myBindingSource.ApplySort(propertyName, ListSortDirection.Ascending);
                        roomListDataGridView.DataSource = myBindingSource;*/
            //roomListDataGridView.Columns["Room"].SortMode = DataGridViewColumnSortMode.Automatic;

        }

        public void addRoomGroup(List<RoomGroup> roomGroups)
        {
            roomGroupDataGridView.Rows.Clear();
            foreach (RoomGroup rg in roomGroups)
            {
                object[] obs = new object[] { rg, rg.getOrder(), rg.getRooms() };
              roomGroupDataGridView.Rows.Add(obs);
            }
        }



        private void addRoomGroup(object sender, EventArgs e)
        {
            


        }

        private void moveRoomUpInGroup(object sender, EventArgs e)
        {
            int selectedIndex = this.roomListBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                this.roomListBox.Items.Insert(selectedIndex - 1, this.roomListBox.Items[selectedIndex]);
                this.roomListBox.Items.RemoveAt(selectedIndex + 1);
                this.roomListBox.SelectedIndex = selectedIndex - 1;
            }
        }

        private void moveRoomDownInGroup(object sender, EventArgs e)
        {
            int selectedIndex = this.roomListBox.SelectedIndex;
            if (selectedIndex < this.roomListBox.Items.Count - 1 & selectedIndex != -1)
            {
                this.roomListBox.Items.Insert(selectedIndex + 2, this.roomListBox.Items[selectedIndex]);
                this.roomListBox.Items.RemoveAt(selectedIndex);
                this.roomListBox.SelectedIndex = selectedIndex + 1;

            }
        }
        private void addRoomsToGroup(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection str = this.roomListDataGridView.SelectedRows;
            foreach (DataGridViewRow s in str)
            {
                object nm = s.Cells[0].Value;
                bool roomListContain = this.roomListBox.Items.Contains(nm);
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
                        MessageBox.Show("취소", "중복된 값 취소 메시지",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        break;


                    }

                }
                this.roomListBox.Items.Add(nm);
            }
            this.roomListDataGridView.ClearSelection();

        }

        private void removeRoomsFromGroup(object sender, EventArgs e)
        {

            //if(this.roomListBox.SelectedItems.Count > 0) this.roomListBox.Items.Remove(this.roomListBox.SelectedItems[0]);
            for (int x = this.roomListBox.SelectedItems.Count - 1; x >= 0; x--)
            {
                Room room = (Room)this.roomListBox.SelectedItems[x];

                roomListBox.Items.Remove(room);

            }
        }

        private void removeRoomGroup(object sender, EventArgs e)
        {




           

        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {

            //  show rows of roomListDataGridView like name


        }


        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            //this.roomGroupDataGridView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Automatic;
            DataGridViewColumn newColumn = this.roomListDataGridView.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = this.roomListDataGridView.SortedColumn;
            ListSortDirection direction;

            if (oldColumn == newColumn)
            {
                // Sorting by the same column: toggle between ASC and DESC:
                direction = this.roomListDataGridView.SortOrder == SortOrder.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }
            else
            {
                // Sorting by a new column.

                // Choose the default direction based on the column name:
                switch (this.roomListDataGridView.Columns[e.ColumnIndex].Name)
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

            this.roomListDataGridView.Sort(newColumn, direction);

            newColumn.HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending
                ? SortOrder.Ascending
                : SortOrder.Descending;

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupOrderComboBox_Click(object sender, EventArgs e)
        {

        }

        private void roomListDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            List<Room> rooms = new List<Room>();
            foreach (DataGridViewRow row in roomListDataGridView.SelectedRows)
            {
                Room room = (Room)row.Cells[0].Value;
                rooms.Add(room);
            }
            try
            {
                roomSelectionChangedEventMsg(sender, rooms);
            }
            catch (Exception ex) { }
        }


        private void RoomGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            info.selectRooms.Clear();
            for (int i = 0; i < roomListDataGridView.SelectedRows.Count; i++)
            {
                Room r = (Room)roomListDataGridView.SelectedRows[i].Cells[0].Value;

                info.selectRooms.Add(r);
            }
            roomHighlightChangedEventMsg(sender, info.selectRooms);
        }

        private void roomGroupDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void roomListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            info.selectRooms.Clear();
            for (int i = 0; i < roomListDataGridView.SelectedRows.Count; i++)
            {
                Room r = (Room)roomListDataGridView.SelectedRows[i].Cells[0].Value;

                info.selectRooms.Add(r);


            }
            roomHighlightChangedEventMsg(sender, info.selectRooms);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void roomGroupDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            

            if (e.RowIndex == -1) return; 
            Console.WriteLine(e.RowIndex + " " + e.ColumnIndex);


            //RoomGroup rg = (RoomGroup)this.roomGroupDataGridView.Rows[e.RowIndex].Cells[0].Value;
            string name = (string)this.roomGroupDataGridView.Rows[e.RowIndex].Cells[0].Value;
             RoomGroup rg = info.roomGroups[e.RowIndex];
             rg.name = name;
            
            Console.WriteLine("rg1 " + rg.isOrder);

            string isOrder = (string)this.roomGroupDataGridView.Rows[e.RowIndex].Cells[1].Value;
            if (isOrder == RoomGroup.Order) rg.isOrder = true;
            else rg.isOrder = false;

            Console.WriteLine("rg2 " + rg.isOrder);

            for (int i = 0; i < rg.roomList.Count - 1; i++)
            {
                Room r1 = rg.roomList[i];

                for (int j = i + 1; j < rg.roomList.Count; j++)
                {
                    Room r2 = rg.roomList[j];
                    if (r1 == r2)
                    {
                        MessageBox.Show("중복된 값이 포함되어 있어 Non-Order 옵션으로 변경할 수 없습니다.");
                       break;
                    }

                }

            }
       

        }

        private void roomGroupDataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            Console.WriteLine("roomGroupDataGridView_CellStateChanged");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.roomListBox.Items.Count == 0)
            {
                MessageBox.Show("선택된 룸이 없습니다.");
                return;
            }
            List<Room> rooms = new List<Room>();

            foreach (Room r in this.roomListBox.Items) rooms.Add(r);
            bool isOrder = false;
            if (this.groupOrderComboBox.SelectedIndex == 0) isOrder = true;
            else isOrder = false;
            RoomGroup rg = new RoomGroup("Group" + roomGroups.Count, rooms, isOrder);
            this.roomGroups.Add(rg);
            object[] obs = new object[] { rg.name, rg.getOrder(), rg.getRooms() };
            roomGroupDataGridView.Rows.Add(obs);
            this.roomListBox.Items.Clear();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //기존 시퀀스에 룸 그룹이 있는지를 체크
            DataGridViewSelectedRowCollection rows = roomGroupDataGridView.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                string rgName = (row.Cells[0].Value.ToString());
                RoomGroup rg = info.roomGroups[row.Index];
                bool isExist = false;
                foreach (SequenceGroup sg in info.sequenceGroups)
                {

                    foreach (RoomAndGroupObject ro in sg.definedSequence.roomList)
                    {
                        if (ro == rg)
                        {

                            isExist = true;
                            MessageBox.Show(rg.name + "은 시퀀스에 정의되어 있습니다.");
                            break;
                        }

                    }
                    if (isExist) break;
                }
                if (!isExist)
                {

                    this.roomGroups.Remove(rg);
                    roomGroupDataGridView.Rows.Remove(row);
                }
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            ReadWriteUtil.saveRoomGroupList(this.roomGroups);
            groupChangedReceiveMsg(this, e);
        }

        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }
    }
}

            /*
            private void SaveRoomGroupList(object sender, EventArgs e)
    {
       //Display in Sequence settings
       this.dataGridView5.DataSource = Globalval.RGList;
       this.listBox3.Items.Clear();
       //   MessageBox.Show("Successfully saved", " Room Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    */
    
    
  
