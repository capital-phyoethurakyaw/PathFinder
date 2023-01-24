
namespace PathFinder
{
    partial class PathFinderForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cADFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jPEGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.seletionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.vectorDrawBaseControl1 = new VectorDraw.Professional.Control.VectorDrawBaseControl();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.analysisRouteControl1 = new PathFinder.gui.AnalysisRouteControl();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.sequenceSettingControlcs1 = new PathFinder.gui.SequenceSettingControlcs();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.roomGroupControl1 = new PathFinder.gui.RoomGroupControl();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.mainRouteControl1 = new PathFinder.gui.MainRouteControl();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.obstacleSettingControl1 = new PathFinder.gui.ObstacleSettingControl();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.algorithComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabControl4.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2113, 60);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.exitAppToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(87, 50);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Enabled = false;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(298, 54);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Enabled = false;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(298, 54);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(298, 54);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(298, 54);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cADFormatToolStripMenuItem,
            this.jPEGToolStripMenuItem});
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(298, 54);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // cADFormatToolStripMenuItem
            // 
            this.cADFormatToolStripMenuItem.Name = "cADFormatToolStripMenuItem";
            this.cADFormatToolStripMenuItem.Size = new System.Drawing.Size(346, 54);
            this.cADFormatToolStripMenuItem.Text = "CAD format";
            this.cADFormatToolStripMenuItem.Click += new System.EventHandler(this.cADFormatToolStripMenuItem_Click);
            // 
            // jPEGToolStripMenuItem
            // 
            this.jPEGToolStripMenuItem.Name = "jPEGToolStripMenuItem";
            this.jPEGToolStripMenuItem.Size = new System.Drawing.Size(346, 54);
            this.jPEGToolStripMenuItem.Text = ".JPEG";
            // 
            // exitAppToolStripMenuItem
            // 
            this.exitAppToolStripMenuItem.Enabled = false;
            this.exitAppToolStripMenuItem.Name = "exitAppToolStripMenuItem";
            this.exitAppToolStripMenuItem.Size = new System.Drawing.Size(298, 54);
            this.exitAppToolStripMenuItem.Text = "Exit App";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(2113, 62);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::PathFinder.Properties.Resources.Main_Save;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(58, 55);
            this.toolStripButton3.Text = "Save";
            this.toolStripButton3.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::PathFinder.Properties.Resources.Clear_route;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(58, 55);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Remove Path";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seletionLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(2113, 54);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // seletionLabel
            // 
            this.seletionLabel.Name = "seletionLabel";
            this.seletionLabel.Size = new System.Drawing.Size(117, 41);
            this.seletionLabel.Text = "         ";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(0, 69);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl3);
            this.splitContainer3.Size = new System.Drawing.Size(2108, 1290);
            this.splitContainer3.SplitterDistance = 1331;
            this.splitContainer3.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(8320, 20158);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.vectorDrawBaseControl1);
            this.tabPage2.Location = new System.Drawing.Point(10, 48);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(8300, 20100);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "IFC file";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // vectorDrawBaseControl1
            // 
            this.vectorDrawBaseControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.vectorDrawBaseControl1.AllowDrop = true;
            this.vectorDrawBaseControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.vectorDrawBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vectorDrawBaseControl1.Location = new System.Drawing.Point(3, 3);
            this.vectorDrawBaseControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.vectorDrawBaseControl1.Name = "vectorDrawBaseControl1";
            this.vectorDrawBaseControl1.Size = new System.Drawing.Size(8294, 20094);
            this.vectorDrawBaseControl1.TabIndex = 1;
            this.vectorDrawBaseControl1.DrawAfter += new VectorDraw.Professional.Control.DrawAfterEventHandler(this.vectorDrawBaseControl1_DrawAfter);
            this.vectorDrawBaseControl1.GripSelectionModified += new VectorDraw.Professional.Control.GripSelectionModifiedEventHandler(this.vectorDrawBaseControl1_GripSelectionModified);
            this.vectorDrawBaseControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.vectorDrawBaseControl1_KeyDown);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage9);
            this.tabControl3.Controls.Add(this.tabPage10);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(0, 0);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(4833, 8063);
            this.tabControl3.TabIndex = 2;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.tabControl5);
            this.tabPage9.Location = new System.Drawing.Point(10, 48);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(4813, 8005);
            this.tabPage9.TabIndex = 0;
            this.tabPage9.Text = "Analysis";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage17);
            this.tabControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl5.Location = new System.Drawing.Point(3, 3);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(4807, 7999);
            this.tabControl5.TabIndex = 0;
            // 
            // tabPage17
            // 
            this.tabPage17.Controls.Add(this.analysisRouteControl1);
            this.tabPage17.Location = new System.Drawing.Point(10, 48);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage17.Size = new System.Drawing.Size(4787, 7941);
            this.tabPage17.TabIndex = 2;
            this.tabPage17.Text = "Sequence";
            this.tabPage17.UseVisualStyleBackColor = true;
            // 
            // analysisRouteControl1
            // 
            this.analysisRouteControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisRouteControl1.Location = new System.Drawing.Point(3, 3);
            this.analysisRouteControl1.Margin = new System.Windows.Forms.Padding(5);
            this.analysisRouteControl1.Name = "analysisRouteControl1";
            this.analysisRouteControl1.Size = new System.Drawing.Size(4781, 7935);
            this.analysisRouteControl1.TabIndex = 1;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.tabControl4);
            this.tabPage10.Location = new System.Drawing.Point(10, 48);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(753, 1232);
            this.tabPage10.TabIndex = 1;
            this.tabPage10.Text = "Setting";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage11);
            this.tabControl4.Controls.Add(this.tabPage12);
            this.tabControl4.Controls.Add(this.tabPage15);
            this.tabControl4.Controls.Add(this.tabPage16);
            this.tabControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl4.Location = new System.Drawing.Point(3, 3);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(747, 1226);
            this.tabControl4.TabIndex = 0;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.sequenceSettingControlcs1);
            this.tabPage11.Location = new System.Drawing.Point(10, 48);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(727, 1168);
            this.tabPage11.TabIndex = 0;
            this.tabPage11.Text = "Sequence";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // sequenceSettingControlcs1
            // 
            this.sequenceSettingControlcs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequenceSettingControlcs1.Location = new System.Drawing.Point(3, 3);
            this.sequenceSettingControlcs1.Margin = new System.Windows.Forms.Padding(5);
            this.sequenceSettingControlcs1.Name = "sequenceSettingControlcs1";
            this.sequenceSettingControlcs1.Size = new System.Drawing.Size(721, 1162);
            this.sequenceSettingControlcs1.TabIndex = 13;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.roomGroupControl1);
            this.tabPage12.Location = new System.Drawing.Point(10, 48);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(727, 1168);
            this.tabPage12.TabIndex = 1;
            this.tabPage12.Text = "Room Group";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // roomGroupControl1
            // 
            this.roomGroupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomGroupControl1.Location = new System.Drawing.Point(3, 3);
            this.roomGroupControl1.Margin = new System.Windows.Forms.Padding(5);
            this.roomGroupControl1.Name = "roomGroupControl1";
            this.roomGroupControl1.Size = new System.Drawing.Size(721, 1162);
            this.roomGroupControl1.TabIndex = 10;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.mainRouteControl1);
            this.tabPage15.Location = new System.Drawing.Point(10, 48);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(727, 1168);
            this.tabPage15.TabIndex = 2;
            this.tabPage15.Text = "Main Route";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // mainRouteControl1
            // 
            this.mainRouteControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainRouteControl1.Location = new System.Drawing.Point(3, 3);
            this.mainRouteControl1.Margin = new System.Windows.Forms.Padding(5);
            this.mainRouteControl1.Name = "mainRouteControl1";
            this.mainRouteControl1.Size = new System.Drawing.Size(721, 1162);
            this.mainRouteControl1.TabIndex = 11;
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.obstacleSettingControl1);
            this.tabPage16.Location = new System.Drawing.Point(10, 48);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(727, 1168);
            this.tabPage16.TabIndex = 3;
            this.tabPage16.Text = "Obstacle";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // obstacleSettingControl1
            // 
            this.obstacleSettingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.obstacleSettingControl1.Location = new System.Drawing.Point(3, 3);
            this.obstacleSettingControl1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.obstacleSettingControl1.Name = "obstacleSettingControl1";
            this.obstacleSettingControl1.Size = new System.Drawing.Size(721, 1162);
            this.obstacleSettingControl1.TabIndex = 0;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.toolStrip2);
            this.tabPage14.Controls.Add(this.dataGridView1);
            this.tabPage14.Location = new System.Drawing.Point(4, 28);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(398, 650);
            this.tabPage14.TabIndex = 1;
            this.tabPage14.Text = "Room To Room";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.algorithComboBox,
            this.toolStripComboBox1});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(392, 49);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // algorithComboBox
            // 
            this.algorithComboBox.DropDownWidth = 150;
            this.algorithComboBox.Items.AddRange(new object[] {
            "Human-Like",
            "Robot-Like(Test)"});
            this.algorithComboBox.Name = "algorithComboBox";
            this.algorithComboBox.Size = new System.Drawing.Size(150, 49);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Room to Room",
            "Door to Door"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 49);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(389, 1855);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // PathFinderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(2113, 1391);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PathFinderForm";
            this.Text = "병실 - 경로 분석 Hospital Room - Route Analysis(1.1)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabControl5.ResumeLayout(false);
            this.tabPage17.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.tabControl4.ResumeLayout(false);
            this.tabPage11.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage15.ResumeLayout(false);
            this.tabPage16.ResumeLayout(false);
            this.tabPage14.ResumeLayout(false);
            this.tabPage14.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private VectorDraw.Professional.Control.VectorDrawBaseControl vectorDrawBaseControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem exitAppToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel seletionLabel;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabControl tabControl5;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage17;
        private gui.AnalysisRouteControl analysisRouteControl1;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage11;
        private gui.SequenceSettingControlcs sequenceSettingControlcs1;
        private System.Windows.Forms.TabPage tabPage12;
        private gui.RoomGroupControl roomGroupControl1;
        private System.Windows.Forms.TabPage tabPage15;
        private gui.MainRouteControl mainRouteControl1;
        private System.Windows.Forms.TabPage tabPage16;
        private gui.ObstacleSettingControl obstacleSettingControl1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox algorithComboBox;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem cADFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jPEGToolStripMenuItem;
    }
}