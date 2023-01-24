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

    public partial class RoomGroupForm : Form
    {
        RoomGroup rg;
       public bool isOk = false;
        public RoomGroupForm()
        {
           
            InitializeComponent();
        }

        public void setGroup(RoomGroup rg) {
            this.rg = rg;

            this.nameTextBox.Text = rg.name;
            this.checkBox.Checked = rg.isOrder;
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.rg.name = this.nameTextBox.Text;
            this.rg.isOrder = this.checkBox.Checked;
            isOk  =     true;   
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isOk = false;
            this.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
