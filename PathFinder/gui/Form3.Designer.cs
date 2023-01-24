namespace PathFinder.gui
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.mainRoouteDataGridView = new System.Windows.Forms.DataGridView();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column50 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainRoouteDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.mainRoouteDataGridView);
            this.groupBox11.Location = new System.Drawing.Point(705, 578);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox11.Size = new System.Drawing.Size(430, 396);
            this.groupBox11.TabIndex = 16;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Main Route Sequence List";
            // 
            // mainRoouteDataGridView
            // 
            this.mainRoouteDataGridView.AllowUserToAddRows = false;
            this.mainRoouteDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mainRoouteDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainRoouteDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column27,
            this.Column50});
            this.mainRoouteDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mainRoouteDataGridView.Location = new System.Drawing.Point(5, 36);
            this.mainRoouteDataGridView.Margin = new System.Windows.Forms.Padding(5);
            this.mainRoouteDataGridView.Name = "mainRoouteDataGridView";
            this.mainRoouteDataGridView.RowHeadersWidth = 20;
            this.mainRoouteDataGridView.RowTemplate.Height = 28;
            this.mainRoouteDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainRoouteDataGridView.Size = new System.Drawing.Size(420, 355);
            this.mainRoouteDataGridView.TabIndex = 0;
            // 
            // Column27
            // 
            this.Column27.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column27.FillWeight = 113.6364F;
            this.Column27.HeaderText = "RouteName";
            this.Column27.MinimumWidth = 8;
            this.Column27.Name = "Column27";
            this.Column27.Width = 150;
            // 
            // Column50
            // 
            this.Column50.FillWeight = 86.36364F;
            this.Column50.HeaderText = "Rooms";
            this.Column50.MinimumWidth = 8;
            this.Column50.Name = "Column50";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1840, 1552);
            this.Controls.Add(this.groupBox11);
            this.Name = "Form3";
            this.Text = "Form3";
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainRoouteDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.DataGridView mainRoouteDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column50;
    }
}