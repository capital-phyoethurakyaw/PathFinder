namespace PathFinder
{
    using VectorDraw.Professional.Control;

    partial class PathFinderBimForm
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
            VectorDraw.Professional.Utilities.ProgressMeter progressMeter1 = new VectorDraw.Professional.Utilities.ProgressMeter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathFinderBimForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.vdFramedControl1 = new VectorDraw.Professional.Control.VectorDrawBaseControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mTree = new BrightIdeasSoftware.TreeListView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.mProps = new System.Windows.Forms.PropertyGrid();
            this.vdIFCComponent1 = new vdIFC.vdIFCComponent(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.Open = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTree)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 138);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.vdFramedControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(2928, 1764);
            this.splitContainer1.SplitterDistance = 2008;
            this.splitContainer1.SplitterWidth = 11;
            this.splitContainer1.TabIndex = 2;
            // 
            // vdFramedControl1
            // 
            this.vdFramedControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.vdFramedControl1.AllowDrop = true;
            this.vdFramedControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.vdFramedControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdFramedControl1.Location = new System.Drawing.Point(0, 0);
            this.vdFramedControl1.Margin = new System.Windows.Forms.Padding(27, 17, 27, 17);
            this.vdFramedControl1.Name = "vdFramedControl1";
            this.vdFramedControl1.Size = new System.Drawing.Size(2008, 1764);
            this.vdFramedControl1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mTree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer2.Panel2.Controls.Add(this.mProps);
            this.splitContainer2.Size = new System.Drawing.Size(909, 1764);
            this.splitContainer2.SplitterDistance = 704;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 0;
            // 
            // mTree
            // 
            this.mTree.CellEditUseWholeCell = false;
            this.mTree.Cursor = System.Windows.Forms.Cursors.Default;
            this.mTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTree.FullRowSelect = true;
            this.mTree.HideSelection = false;
            this.mTree.Location = new System.Drawing.Point(0, 0);
            this.mTree.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mTree.Name = "mTree";
            this.mTree.ShowGroups = false;
            this.mTree.Size = new System.Drawing.Size(909, 704);
            this.mTree.TabIndex = 0;
            this.mTree.UseCompatibleStateImageBehavior = false;
            this.mTree.View = System.Windows.Forms.View.Details;
            this.mTree.VirtualMode = true;
            this.mTree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mTree_MouseClick);
            this.mTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mTree_MouseDoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 993);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(909, 57);
            this.progressBar1.TabIndex = 1;
            // 
            // mProps
            // 
            this.mProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mProps.Location = new System.Drawing.Point(0, 0);
            this.mProps.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mProps.Name = "mProps";
            this.mProps.Size = new System.Drawing.Size(909, 885);
            this.mProps.TabIndex = 0;
            // 
            // vdIFCComponent1
            // 
            progressMeter1.MeterLimit = ((long)(0));
            progressMeter1.RaiseOnPaint = false;
            this.vdIFCComponent1.MeterProgress = progressMeter1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(2563, 60);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(87, 54);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(258, 54);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.but_Open_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.toolStripButton6,
            this.toolStripComboBox2,
            this.toolStripButton7});
            this.toolStrip2.Location = new System.Drawing.Point(0, 60);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip2.Size = new System.Drawing.Size(2563, 62);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // Open
            // 
            this.Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Open.Image = global::PathFinder.Properties.Resources.open;
            this.Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(58, 55);
            this.Open.Text = "Open";
            this.Open.Click += new System.EventHandler(this.but_Open_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::PathFinder.Properties.Resources.zoom_in;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(58, 55);
            this.toolStripButton6.Text = "Zoom Extent";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(191, 62);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::PathFinder.Properties.Resources.ifc_import;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(58, 55);
            this.toolStripButton7.Text = "Export Rooms";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // PathFinderBimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2563, 1410);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "PathFinderBimForm";
            this.Text = "병실 - 경로 분석 Hospital Room - Route Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTree)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private vdIFC.vdIFCComponent vdIFCComponent1;
        private BrightIdeasSoftware.TreeListView mTree;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid mProps;
        private System.Windows.Forms.ProgressBar progressBar1;
        private VectorDrawBaseControl vdFramedControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton Open;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
    }
}

