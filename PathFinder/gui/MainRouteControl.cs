namespace PathFinder.gui
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using PathFinder.util;
    using VectorDraw.Professional.vdObjects;

    public delegate void MainRouteSelectionChangedEventHandler(object sender, List<Room> e);
    public delegate void MainRouteHighlightChangedEventHanlder(object sender, List<Room> e); 

    public partial class MainRouteControl : UserControl
    {
      
        Info info;
       
        public event GroupChangedEventHandler groupChangedReceiveMsg;
        public event MainRouteSelectionChangedEventHandler mainRouteSelectionChangedEventMsg;
        public event MainRouteHighlightChangedEventHanlder mainRouteHighlightChangedEventMsg;        

        public MainRouteControl()
        {
            InitializeComponent();
        }

        private void DeleteMainRoute(object sender, EventArgs e)
        {
           
        }


        public void setRoomList(List<Room> roomList)
        {
            foreach (Room r in roomList)
            {
                roomListDataGridView.Rows.Add(new object[] { r });
            }
           
            //roomListDataGridView.Columns["Room"].SortMode = DataGridViewColumnSortMode.Automatic;
        }
       
        private void AddRoomsToMainRoute(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection str = this.roomListDataGridView.SelectedRows;
            foreach (DataGridViewRow s in str)
            {
                Room r = (Room)s.Cells[0].Value;
                this.roomListBox.Items.Add(r);
            }
        }

        private void RemoveRoomsFromMainRoute(object sender, EventArgs e)
        {
            //if(this.roomListBox.SelectedItems.Count>0) this.roomListBox.Items.Remove(this.roomListBox.SelectedItems[0]);
            for (int x = this.roomListBox.SelectedItems.Count - 1; x >= 0; x--)
            {
                Room room = (Room)this.roomListBox.SelectedItems[x];

                roomListBox.Items.Remove(room); 
            }
        }

        private void MoveRoomUpInMainRoute(object sender, EventArgs e)
        {
            int selectedIndex = this.roomListBox.SelectedIndex;
            if (selectedIndex > 0)
            {
                this.roomListBox.Items.Insert(selectedIndex - 1, this.roomListBox.Items[selectedIndex]);
                this.roomListBox.Items.RemoveAt(selectedIndex + 1);
                this.roomListBox.SelectedIndex = selectedIndex - 1;
            }
        }

        private void MoveRoomDownInMainRoute(object sender, EventArgs e)
        {
            int selectedIndex = this.roomListBox.SelectedIndex;
            if (selectedIndex < this.roomListBox.Items.Count - 1 & selectedIndex != -1)
            {
                this.roomListBox.Items.Insert(selectedIndex + 2, this.roomListBox.Items[selectedIndex]);
                this.roomListBox.Items.RemoveAt(selectedIndex);
                this.roomListBox.SelectedIndex = selectedIndex + 1;

            }
        }
        private void addMainRouteToGrid(object sender, EventArgs e)
        {
            
        }

        private void SaveMainRoute(object sender, EventArgs e)
        {
        }

        private void deleteMainRoute(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = mainRoouteDataGridView.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                info.mainRoutes.Remove((MainRoute)row.Cells[0].Value);
                mainRoouteDataGridView.Rows.Remove(row);
            }
        }

        private void addMainRoutes(List<MainRoute> mainRoutes)
        {

            foreach (MainRoute mainRoute in mainRoutes)
            {

              

                object[] ob = { mainRoute, mainRoute.getRooms() };
                mainRoouteDataGridView.Rows.Add(ob);
            }
            mainRoouteDataGridView.ClearSelection();
        }

       


        public void setMainRoutes(Info info)
        {
            this.info = info;
            addMainRoutes(info.mainRoutes);
        }

        private void roomListDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            info.selectRooms.Clear();
            for (int i = 0; i < roomListDataGridView.SelectedRows.Count; i++)
            {
                Room r = (Room)roomListDataGridView.SelectedRows[i].Cells[0].Value;

                info.selectRooms.Add(r);


            }
            mainRouteHighlightChangedEventMsg(sender, info.selectRooms);
        }

        private void roomListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

            ReadWriteUtil.saveMainRouteList(info.mainRoutes);
            //groupChangedReceiveMsg(this, e);
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
            MainRoute mr = new MainRoute("MainRoute" + info.mainRoutes.Count, rooms);
            info.mainRoutes.Add(mr);
            object[] obs = new object[] { mr, mr.getRooms() };
            mainRoouteDataGridView.Rows.Add(obs);
            this.roomListBox.Items.Clear();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = mainRoouteDataGridView.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                info.mainRoutes.Remove((MainRoute)row.Cells[0].Value);
                mainRoouteDataGridView.Rows.Remove(row);
            }
        }
    }




}
