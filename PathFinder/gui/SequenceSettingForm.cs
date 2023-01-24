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

    public partial class SequenceSettingForm : Form
    {
        SequenceGroup sg; 
        public bool isOk = false;
       
       
        public SequenceSettingForm()
        {
            InitializeComponent();

            
        }

        public void setGroup(SequenceGroup sg)
        {
            this.sg = sg;


            this.nameTextBox.Text = sg.definedSequence.name;
            this.frequencyTextBox.Text = "" + sg.definedSequence.frequency; 
        } 


      

        private void Ok_Click(object sender, EventArgs e)
        {
            this.sg.definedSequence.name = this.nameTextBox.Text;
           
      
            try
            {
                sg.definedSequence.frequency = int.Parse(this.frequencyTextBox.Text);
            }
            catch (FormatException ex)
            {

                MessageBox.Show("숫자를 입력하세요");
            }
            isOk = true;
            this.Visible = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            isOk = false;
            this.Visible = false;
        }
    }
}
