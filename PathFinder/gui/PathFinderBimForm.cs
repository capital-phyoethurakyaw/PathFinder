using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vdIFC;
using VectorDraw.Actions;
using VectorDraw.Geometry;
using VectorDraw.Professional.Control;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Render;

// This small viewer application is provided as it is in order to help you comprehend the structure of the IFC Document.
// In this sample we use an external tree component (objectListView) which we found on the web and we liked it's implementation.We do not support this component , we just liked it's features and we think it is suitable for this viewer.
// Please read our help file and also the description we have in our website (https://www.vdraw.com/products/vectordraw-ifc-library/) for more information

namespace PathFinder
{
    public partial class PathFinderBimForm : Form
    {
        FileInfo fi;

        public PathFinderBimForm()
        {
            //Console.WriteLine("InitializeComponent Starts");
            InitializeComponent();
        }
        /// <summary>
        /// A document reference for easy access.
        /// </summary>
        private vdDocument doc { get { return this.vdFramedControl1.ActiveDocument; } }
        /// <summary>
        /// Load of the form and some basic initialization.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //Console.WriteLine("override_OnLoad Starts");
            base.OnLoad(e);
            AddTreeComponent();
            //Console.WriteLine("AddTreeComponent completed");
            string ver = typeof(VectorDraw.Professional.vdObjects.vdDocument).Assembly.GetName().Version.ToString();
            this.Text += "";

            vdIFCComponent1.MeterProgress = doc.MeterProgress;
            vdIFCComponent1.GenericError += VdIFCComponent1_GenericError;

            vdFramedControl1.Focus();
            doc.OnLoadUnknownFileName += Doc_OnLoadUnknownFileName;
            doc.OnGetOpenFileFilterFormat += Doc_OnGetOpenFileFilterFormat;
            doc.OnGetSaveFileFilterFormat += new vdDocument.GetSaveFileFilterFormatEventHandler(ActiveDocument_OnGetSaveFileFilterFormat);
            doc.OnSaveUnknownFileName += new VectorDraw.Professional.vdObjects.vdDocument.SaveUnknownFileName(ActiveDocument_OnSaveUnknownFileName);
            doc.OnIsValidOpenFormat += Doc_OnIsValidOpenFormat;
            doc.OnAfterOpenDocument += Doc_OnAfterOpenDocument;
            doc.OnAfterNewDocument += new vdDocument.AfterNewDocument(doc_OnAfterNewDocument);
            vdFramedControl1.vdDragDrop += BaseControl_vdDragDrop;
            vdFramedControl1.vdMouseDown += BaseControl_vdMouseDown;
           // vdFramedControl1.CommandLine.CommandExecute +=new VectorDraw.Professional.vdCommandLine.CommandExecuteEventHandler(CommandLine_CommandExecute);
            vdFramedControl1.ActiveDocument.EnableAutoGripOn = false;
            doc.OnProgress += new VectorDraw.Professional.Utilities.ProgressEventHandler(doc_OnProgress);
            string[] arguments = System.Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
            {
                //Console.WriteLine("AddTreeComponent completed - Line 66");
                Application.DoEvents();
                string file = arguments[1];
                if (doc.Open(file)) doc.Redraw(true);
            }
           
        }

       

        private void VdIFCComponent1_GenericError([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object sender, string Membername, string errormessage)
        {
            doc.Prompt("\r\nError " + Membername + " " + errormessage);
            doc.Prompt(null);
        }

        void doc_OnProgress(object sender, long percent, string jobDescription)
        {
            //Console.WriteLine("doc_OnProgress starts");
            int val = (int)System.Math.Max((int)percent, 0);
            val = (int)System.Math.Min(val, 100);
            progressBar1.Value = (int)val;
        }

        /// <summary>
        /// Initialize the tree component that shows the IFC object tree.
        /// </summary>
        private void AddTreeComponent()
        {
            //Console.WriteLine("Inside AddTreeComponent");
            int dist = splitContainer1.SplitterDistance;
            int wid = splitContainer1.Width;
            int p2 = wid - dist - 8;

            this.mTree.CanExpandGetter = delegate (object x) { return ((mTreeNode)x).Children.Count > 0; };
            this.mTree.ChildrenGetter = delegate (object x) { return ((mTreeNode)x).Children; };

            BrightIdeasSoftware.OLVColumn TypeCol = new BrightIdeasSoftware.OLVColumn("Type", "Type");
            TypeCol.AspectGetter = delegate (object x) { return ((mTreeNode)x).Type; };
            TypeCol.Width = 250;

            BrightIdeasSoftware.OLVColumn VisCol = new BrightIdeasSoftware.OLVColumn("Visibility", "Visibility");
            VisCol.AspectGetter = delegate (object x) { return ((mTreeNode)x).Visibility; };
            VisCol.CheckBoxes = true;
            VisCol.Width = 50;

            BrightIdeasSoftware.OLVColumn NameCol = new BrightIdeasSoftware.OLVColumn("Name", "Name");
            NameCol.AspectGetter = delegate (object x) { return ((mTreeNode)x).Name; };
            NameCol.Width = 100;

            BrightIdeasSoftware.OLVColumn DescriCol = new BrightIdeasSoftware.OLVColumn("Description", "Description");
            DescriCol.AspectGetter = delegate (object x) { return ((mTreeNode)x).Description; };
            DescriCol.Width = p2 - 400;

            this.mTree.Columns.Add(TypeCol);
            this.mTree.Columns.Add(VisCol);
            this.mTree.Columns.Add(NameCol);
            this.mTree.Columns.Add(DescriCol);

            mTree.EmptyListMsg = "";

            mTree.SelectionChanged += MTree_SelectionChanged;
            mTree.UseCellFormatEvents = true;
            mTree.FormatRow += MTree_FormatRow;
        }
        /// <summary>
        /// This event of the tree is being used in order to highlight the row of the product that is selected by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTree_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            //Console.WriteLine("MTree_FormatRow starts");
            mTreeNode node = e.Model as mTreeNode;
            if (node == null) return;
            vdFigure fig = node.mObject as vdFigure;
            if (fig == null) return;

            if (fig.ShowGrips)
            {
                e.Item.BackColor = Color.FromName ("Highlight");
            }
        }


        #region Prepare Document
        
        /// <summary>
        /// This method is being used in order to clear the tree component.
        /// </summary>
        /// <param name="node"></param>
        private void ClearNode(mTreeNode node)
        {
            node.mObject = null;
            node.OnNeedRedraw -= Node_OnNeedRedraw;
            foreach (mTreeNode item in node.Children)
            {
                ClearNode(item);
            }
        }
        /// <summary>
        /// Clear the tree while opening a new ifc file.
        /// </summary>
        private void ClearTree()
        {
            if (mTree.Items.Count == 0) return;
            if (AllNodes != null)
            {
                foreach (mTreeNode  item in AllNodes)
                {
                    ClearNode(item);
                }
            }
            mTree.Items.Clear();
            mTree.Roots = null;
        }
        List<mTreeNode> AllNodes = null;
        /// <summary>
        /// iterate through the IFCDocument to load the tree.
        /// </summary>
        /// <param name="doc"></param>
        private void LoadTreeView(vdDocument doc)
        {
            ClearTree();
            if (doc == null || doc.Model.Entities.Count == 0) return;
            if (vdIFCComponent1 == null || vdIFCComponent1.Site == null) return;
            vdIFCDocument vdifcdoc = doc.Model.Entities[0] as vdIFCDocument;
            if (vdifcdoc == null)
            {
                return;
            }
            string fname =  VectorDraw.Professional.Utilities.vdGlobals.GetFileName(vdifcdoc.IFCFileName);

            vdIFCProject vdproject = vdifcdoc.Project;

            AllNodes = new List<mTreeNode>();
            mTreeNode ProjectNode = new mTreeNode("Project:" + fname, true, vdproject.Name, vdproject.Description , vdproject);
            ProjectNode.OnNeedRedraw += Node_OnNeedRedraw;
            AllNodes.Add(ProjectNode);

            foreach (vdIFCSite vdSite in vdproject.Sites)
            {
                mTreeNode SiteNode = new mTreeNode("Site", vdSite.Visible, vdSite.Name, vdSite.Description, vdSite);
                SiteNode.OnNeedRedraw += Node_OnNeedRedraw;

                foreach (vdIFCBuilding vdbuilding in vdSite.Buildings)
                {
                    mTreeNode BuildingNode = new mTreeNode("Building", vdbuilding.Visible, vdbuilding.Name, vdbuilding.Description, vdbuilding);
                    BuildingNode.OnNeedRedraw += Node_OnNeedRedraw;
                    foreach (IvdIFCBuildingStorey vdStorey in vdbuilding.BuildingStoreys)
                    {
                        mTreeNode StoreyNode = new mTreeNode("Building Storey", vdStorey.Visible, vdStorey.Name, vdStorey.Description, vdStorey);
                        StoreyNode.OnNeedRedraw += Node_OnNeedRedraw;
                        toolStripComboBox2.Items.Add(StoreyNode);
                        foreach (vdIFCProduct vdProduct in vdStorey.Products)
                        {
                            
                            mTreeNode ProductNode = new mTreeNode(vdProduct.IFCType, vdProduct.Visible, vdProduct.Name, vdProduct.Description, vdProduct);
                            ProductNode.OnNeedRedraw += Node_OnNeedRedraw;
                            StoreyNode.Children.Add(ProductNode);
                          
                        }
                        
                        BuildingNode.Children.Add(StoreyNode);
                    }
                    SiteNode.Children.Add(BuildingNode);
                }

                foreach (vdIFCProduct vdProduct1 in vdSite.Products)
                {
                    mTreeNode ProductNode = new mTreeNode(vdProduct1.IFCType, vdProduct1.Visible, vdProduct1.Name, vdProduct1.Description, vdProduct1);
                    ProductNode.OnNeedRedraw += Node_OnNeedRedraw;
                    SiteNode.Children.Add(ProductNode);
                }
                ProjectNode.Children.Add(SiteNode);
            }
            
            mTree.Roots = AllNodes;
          //  mTree.ExpandAll();
            mTree.Refresh();

          if(toolStripComboBox2.Items.Count > 0)  toolStripComboBox2.SelectedIndex = 0;
        }
        /// <summary>
        /// This event is being raised from the treenode in order to redraw the screen after a change in a visibility of an object (checkbox).
        /// </summary>
        private void Node_OnNeedRedraw()
        {
            doc.Redraw(true);
        }
        #endregion

        #region Drag Drop
        /// <summary>
        /// Drop event in order to open an ifc file that is being dropped to the application.
        /// </summary>
        /// <param name="drgevent"></param>
        /// <param name="cancel"></param>
        private void BaseControl_vdDragDrop(DragEventArgs drgevent, ref bool cancel)
        {
            System.Windows.Forms.DataObject dataobject = new DataObject(drgevent.Data);

            if (dataobject.GetDataPresent("FIleDrop"))
            {
                cancel = true;
                System.Collections.Specialized.StringCollection strings = dataobject.GetFileDropList();
                int count = strings.Count;
                int i = 0;
                foreach (string filename in strings)
                {
                    doc.Prompt(string.Format("\r\n Open {0} from {1} {2}", ++i, count, filename)); doc.Prompt(null);
                    if (!doc.IsvalidOpenFormat(filename))
                    {
                        doc.Prompt("\r\nInvalid Format"); doc.Prompt(null);
                        continue;
                    }
                    bool success = doc.Open(filename);
                    try
                    {
                        if (success)
                        {
                            doc.ZoomExtents(); doc.Redraw(false);
                            doc.Prompt("\r\nSuccessed"); doc.Prompt(null);
                        }
                    }
                    catch (Exception e)
                    {
                        doc.Prompt("\r\nException " + e.Message); doc.Prompt(null);
                        success = false;
                    }
                    if (!success)
                    {
                        doc.Prompt("\r\nFailed!!!!!"); doc.Prompt(null);
                    }
                }
            }
        }
        #endregion

        #region Document Events
        void doc_OnAfterNewDocument(object sender)
        {
            vdDocument mdoc = sender as vdDocument;
            LoadTreeView(mdoc);
         
        }
        /// <summary>
        /// Initialize the Document after opening an ifc file.
        /// </summary>
        /// <param name="sender"></param>
        private void Doc_OnAfterOpenDocument(object sender)
        {
            vdDocument mdoc = sender as vdDocument;
            LoadTreeView(mdoc);
            
            if (vdIFCComponent.IsIFCExtension(mdoc.FileName))
            {
                mdoc.RenderingQuality = VectorDraw.Render.vdRender.RenderingQualityMode.HighQuality;
                mdoc.RenderMode = VectorDraw.Render.vdRender.Mode.Render;
                mdoc.PerspectiveMod = vdRender.VdConstPerspectiveMod.PerspectON;
                mdoc.GlobalRenderProperties.EdgeColor = Color.DimGray;
                mdoc.LensAngle = 75;
                mdoc.Background = Color.White;
                mdoc.Palette.Forground = Color.LightGray;
                mdoc.CommandAction.View3D("VINE");

                //correct World2ViewMatrix and viewcenter z value in order to work properly with OpenGL render modes
                Matrix m = new Matrix(mdoc.World2ViewMatrix);
                gPoint vcenter = mdoc.ViewCenter;
                m.TranslateMatrix(0, 0, -vcenter.z);
                vcenter.z = 0;
                mdoc.World2ViewMatrix = m;
                mdoc.ViewCenter = vcenter;
                mdoc.Redraw(false);
            }
            string ver = typeof(VectorDraw.Professional.vdObjects.vdDocument).Assembly.GetName().Version.ToString();
            this.Text = mdoc.FileName;//"PathFinder" + (" (" + ver + "  " + (System.IntPtr.Size * 8).ToString() + " bit)") + "   " + 
        }
        /// <summary>
        /// Important event in order to support the ifc format on the open method of the vdDocument
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="extension"></param>
        /// <param name="success"></param>
        private void Doc_OnIsValidOpenFormat(object sender, string extension, ref bool success)
        {
            success = vdIFCComponent.IsIFCExtension(extension);
        }
        /// <summary>
        /// Important event in order to show the ifc format filter while selecting a file to open (open dialog).
        /// </summary>
        /// <param name="openFilter"></param>
        private void Doc_OnGetOpenFileFilterFormat(ref string openFilter)
        {
            openFilter = vdIFCComponent.DefaultOpenDialogFilter + openFilter;
        }
        void ActiveDocument_OnGetSaveFileFilterFormat(ref string saveFilter)
        {
            string[] strs = saveFilter.Split(new string[] { "||" }, StringSplitOptions.None);

            string ifcfilter, ifcfilterversions;
            ifcfilter = vdIFCComponent.DefaultSaveDialogFilter(out ifcfilterversions);
            saveFilter = ifcfilter + strs[0] + "||" + ifcfilterversions;
            if (strs.Length > 1) saveFilter += strs[1];
        }
        /// <summary>
        /// Important event in order to support the ifc format on the open method of the vdDocument
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fileName"></param>
        /// <param name="success"></param>
        private void Doc_OnLoadUnknownFileName(object sender, string fileName, out bool success)
        {
            success = false;
            vdDocument mdoc = sender as vdDocument;
            if (vdIFCComponent.IsIFCExtension(fileName))
            {
                vdIFCDocument vdifcdoc = vdIFCComponent1.Open(fileName);
                if (vdifcdoc != null)
                {
                    mdoc.EnsureDefaults();
                    mdoc.Model.Entities.AddItem(vdifcdoc);
                    success = true;
                }
            }
        }
        vdIFCExportProperties IFCExportProperties = new vdIFCExportProperties();
        void ActiveDocument_OnSaveUnknownFileName(object sender, string fileName, out bool success)
        {
            success = false;
            vdDocument mdoc = sender as vdDocument;
            if (vdIFCComponent.IsIFCExtension(fileName))
            {

                vdIFCExportProperties ep = IFCExportProperties.Clone() as vdIFCExportProperties;
                if (mdoc.FileProperties.ExternalFileVersion == "IFC3")
                    ep.IFCVersion = IFCVersionEnum.IFC3;
                else if (mdoc.FileProperties.ExternalFileVersion == "IFC4")
                    ep.IFCVersion = IFCVersionEnum.IFC4;
                if (doc.Model.Entities.Count == 1 && (doc.Model.Entities[0] is vdIFCDocument))
                {


                    success = vdIFCComponent1.Save((vdIFCDocument)doc.Model.Entities[0], fileName, ep);

                }
                else
                {
                    vdIFCDocument vdifcdoc = vdIFCComponent1.New();
                    vdIFCSite defsite = vdifcdoc.Project.Site;
                    vdEntities ents = new vdEntities();
                    foreach (vdFigure item in doc.Model.Entities)
                    {
                        if (item.Deleted) continue;
                        if (item is vdIFCDocument)
                        {
                            vdIFCDocument ifcdoc = item as vdIFCDocument;
                            foreach (vdIFCSite site in ifcdoc.Project.Sites)
                            {
                                vdifcdoc.Project.Sites.Add(site.Clone() as vdIFCSite);
                            }
                        }
                        else ents.AddItem(item);
                    }
                    if (ents.Count == 0) defsite.Deleted = true;
                    else
                    {
                        vdIFCProduct product = new vdIFCProduct(doc, "Test", "Test IFRC Save", vdIFCProduct.IfcElementTypeName.Default, ents);
                        defsite.Products.Add(product);
                    }


                    success = vdIFCComponent1.Save(vdifcdoc, fileName, ep);
                }

            }
        }
        /// <summary>
        /// Soma basic-help commands to the commandLine.
        /// </summary>
        /// <param name="commandname"></param>
        /// <param name="isDefaultImplemented"></param>
        /// <param name="success"></param>
        private void CommandLine_CommandExecute(string commandname, bool isDefaultImplemented, ref bool success)
        {
            if (isDefaultImplemented) return;
            
            else if (string.Compare(commandname, "time", true) == 0)
            {
                success = true;

                doc.FreezeActionsStack.Push(true);

                doc.ActiveRender.BreakOnMessage = VectorDraw.WinMessages.MessageManager.BreakMessageMethod.None;
                int ctime = Environment.TickCount;
                doc.Redraw(true);
                int dt = (Environment.TickCount - ctime);
                doc.FreezeActionsStack.Pop();

                vdglTypes.MemoryStatus status = vdgl.GetMemoryStatus();

                doc.Prompt(string.Format("\r\nglver:{6} Speed: {0} memory {1} (MBytes) glLists {2} glTetxtures {3} --->{4}-{5}", dt.ToString(), (status.AllocBytes / 1000000.0).ToString(".###"), status.GLNumLists.ToString(), status.GLNumTextures.ToString(), doc.GlobalRenderProperties.CustomRenderTypeName, doc.GlobalRenderProperties.OpenGLDoubleBuffered.ToString(), doc.GlobalRenderProperties.OpenGLVersion));
                doc.Prompt(null);
            }
            else if (string.Compare(commandname, "explo", true) == 0)
            {
                success = true;
                vdSelection set = new vdSelection(); set.SetUnRegisterDocument(doc);
                set.Select(RenderSelect.SelectingMode.All, null);

                for (int i = 0; i < set.Count; i++)
                {
                    vdFigure fig = set[i];
                    if (fig is vdIFCDocument)
                    {
                        fig.Deleted = true;
                        vdEntities ents = ((vdIFCDocument)fig).Explode();
                        for (int k = 0; k < ents.Count; k++)
                        {
                            vdIFCProduct ifcproduct = ents[k] as vdIFCProduct;
                            if (ifcproduct != null)
                            {
                                ifcproduct.Deleted = true;
                                vdEntities ents2 = ifcproduct.Explode(); 
                                for (int k2 = 0; k2 < ents2.Count; k2++)
                                {
                                    if (ents2[k2] is vdSectionPath || ents2[k2] is vdSectionRevolved)
                                    {
                                        vdEntities ents3 = ents2[k2].Explode();
                                        foreach (vdFigure item in ents3) doc.Model.Entities.AddItem(item);
                                    }
                                    else
                                    {
                                        doc.Model.Entities.AddItem(ents2[k2]);
                                    }
                                }
                            }
                            else
                            {
                                doc.Model.Entities.AddItem(ents[k]);
                            }
                        }
                    }
                }
                doc.Redraw(true);


            }
        }
        #endregion

        #region Buttons
        /// <summary>
        /// Just calling the open method and the Document's events will handle the rest.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_Open_Click(object sender, EventArgs e)
        {
           
            object ret = doc.GetOpenFileNameDlg(0, "", 0);
            if (ret == null) return;
            this.toolStripComboBox2.Items.Clear();


            string fname = (string)ret;
            bool success = doc.Open(fname);
          
            if (success) doc.Redraw(false);
             fi = new FileInfo(doc.FileName);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string ver = "";
            string fname = doc.GetSaveFileNameDlg(doc.FileName, out ver);
            if (fname != null)
            {
                doc.SaveAs(fname, null, ver);
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            doc.CommandAction.Zoom("E", 0, 0);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            System.Reflection.Assembly asm = typeof(VectorDraw.Professional.Control.VectorDrawBaseControl).Assembly;
            System.Reflection.AssemblyName asmname = new System.Reflection.AssemblyName(asm.FullName);
            
            string about = VectorDraw.Serialize.Properties.Resource.About.Replace("\\n", "\n");

            System.Windows.Forms.MessageBox.Show("Vectordraw Developer Framework CAD version : " + asmname.Version.ToString() + 
                                                "\nNot for commercial purposes.\nThis application's source code is provided to all our VDF subscribers."+
                                                about , "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Select entities code
        /// <summary>
        /// Returns the BoundingBox of an ifc entity(building , product , etc...).
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Box GetBBoxFromIFCGlobalIDobj(IIFCGlobalId obj)
        {
            if (obj is vdIFCSite)
            {
                vdIFCSite site = obj as vdIFCSite;
                return site.GetBoundingBox();
            }
            else if (obj is vdIFCBuilding)
            {
                vdIFCBuilding building = obj as vdIFCBuilding;
                return building.GetBoundingBox();
            }
            else if (obj is vdIFCBuildingStorey)
            {
                vdIFCBuildingStorey storey = obj as vdIFCBuildingStorey;
                return storey.GetBoundingBox();
            }
            else if (obj is vdIFCProduct)
            {
                vdIFCProduct product = obj as vdIFCProduct;
                return product.BoundingBox;
            }
            return null;
        }
        /// <summary>
        /// Event of the tree component to handle selecting a product to the tree and zoom to it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTree_SelectionChanged(object sender, EventArgs e)
        {
            if (disableSelectingTree) return;
            System.Collections.IList selected = mTree.SelectedObjects;
            if (selected.Count == 0) return;
            Box bbox = new Box();
            foreach (mTreeNode item in selected)
            {
                bbox.AddBox(GetBBoxFromIFCGlobalIDobj(item.mObject));
            }

            mTreeNode selectednode = (mTreeNode)selected[0];
            vdFigure fig = selectednode.mObject as vdFigure;
            
            disableSelectingTree = true;
            SelectFigure(fig);
            ShowProps(selectednode.mObject);
            disableSelectingTree = false;
            
            if (bbox.IsEmpty) return;

            //expand the Bounding Box 50%
            gPoint offset = new gPoint(bbox.Width, bbox.Height, bbox.Thickness) * 0.25;
            bbox.AddPoint(bbox.Min - offset);
            bbox.AddPoint(bbox.Max + offset);

            bbox.TransformBy(doc.World2ViewMatrix);
            gPoint CameraPosition;
            double viewsize = doc.ActiveRender.GetFittingViewSize(bbox, out CameraPosition);
            doc.ViewCenter = CameraPosition;
            doc.ViewSize = viewsize;

            doc.ActiveRender.Invalidate(false);
        }
        /// <summary>
        /// Find the node on the tree from an entity of the Document.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        private mTreeNode FindNode(vdIFCProduct product, System.Collections.IEnumerable collection)
        {
            if (product == null) return null;
            foreach (mTreeNode node in collection)
            {
                if (node.mObject.IFCGlobalId.Equals(product.IFCGlobalId))
                {
                    return node;
                }
                mTreeNode found = FindNode(product, node.Children);
                if (found != null) return found;
            }
            return null;
        }
        private bool disableSelectingTree = false;
        /// <summary>
        /// method to handle when the user changes the selection and highlight the propert tree node.
        /// </summary>
        private void GripSelectionModified()
        {
            List<mTreeNode> toSelect = new List<mTreeNode>();
            foreach (vdFigure item in GripEntities)
            {
                mTreeNode found = FindNode(item as vdIFCProduct, mTree.Objects);
                if (found != null)
                {
                    toSelect.Add(found);
                }
            }
            if (toSelect.Count > 0)
            {
                foreach (mTreeNode item in toSelect)
                {
                    mTree.Reveal(item, false);
                }
            }

        }
        /// <summary>
        /// Clear the selected entity of the Document.
        /// </summary>
        private void ClearAllGrips()
        {
            foreach (vdFigure fig in GripEntities)
            {
                fig.ShowGrips = false;
                fig.HighLight = false;
                fig.Update();
                fig.Invalidate();
            }
            GripEntities.RemoveAll();
        }
        /// <summary>
        /// Select/highlight a figure
        /// </summary>
        /// <param name="fig"></param>
        private void SelectFigure(vdFigure fig)
        {
            ClearAllGrips();
            if (fig == null) return;
            ShowProps(fig as IIFCGlobalId);
            GripEntities.AddItem(fig, true, vdSelection.AddItemCheck.Nochecking);
            fig.ShowGrips = true;
            fig.HighLight = true;
            fig.Update();
            fig.Invalidate();
            GripSelectionModified();
            vdFramedControl1.ActiveDocument.ActiveLayOut.RefreshGraphicsControl(vdFramedControl1.ActiveDocument.ActiveLayOut.ActiveActionRender.control);
        }
        /// <summary>
        /// A selection to keep the selected entity
        /// </summary>
        vdSelection GripEntities = new vdSelection();
        /// <summary>
        /// Loads the properties of the ifc entity to the bottom right properties list.
        /// </summary>
        /// <param name="fig"></param>
        private void ShowProps(IIFCGlobalId fig)
        {
            if (fig == null) return;
            mProps.SelectedObject = fig.PropertiesGroup;
            mProps.ExpandAllGridItems();
            
        }
        /// <summary>
        /// MouseDown event to handle a one entity selection mode.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancel"></param>
        private void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {
            if (e.Button == MouseButtons.Left)
            {
                vdFigure Fig = null;
                Point location = e.Location;
                Fig = vdFramedControl1.ActiveDocument.ActiveLayOut.GetEntityFromPoint(location, vdFramedControl1.ActiveDocument.ActiveLayOut.ActiveActionRender.GlobalProperties.PickSize, false, vdDocument.LockLayerMethodEnum.EnableGetObjectGrip);
                if (Fig != null)
                {
                    SelectFigure(Fig);
                }
            }
        }
        #endregion


        private void mTree_MouseClick(object sender, MouseEventArgs e)
        {
        }


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (doc == null || doc.Model.Entities.Count == 0) return;
            if (vdIFCComponent1 == null || vdIFCComponent1.Site == null) return;
            vdIFCDocument vdifcdoc = doc.Model.Entities[0] as vdIFCDocument;
            if (vdifcdoc == null)
            {
                return;
            }
            string fname = VectorDraw.Professional.Utilities.vdGlobals.GetFileName(vdifcdoc.IFCFileName);
        
            vdIFCDocument ifc = vdIFCComponent1.New();
            vdIFCProject vdproject = vdifcdoc.Project;
       
            ifc.Project.ApplicationName = vdproject.ApplicationName;
            vdIFCSite vdSite = vdproject.Sites[0];
            vdIFCSite site = ifc.Project.Sites[0];
           
            site.Name = vdSite.Name;
            site.Address = vdSite.Address;
            site.Description = vdSite.Description;
            vdIFCBuilding building = new vdIFCBuilding();
            vdIFCBuilding vdbuilding = vdSite.Buildings[0];
            building.Name = vdbuilding.Name;
            site.Buildings.Add(building);
                vdIFCBuildingStorey vdStorey = vdbuilding.BuildingStoreys[10];
                vdIFCBuildingStorey story = new vdIFCBuildingStorey();
                story = (vdIFCBuildingStorey)vdStorey.Clone();
                building.BuildingStoreys.Add(story);
                Console.WriteLine(story.Products.Count);

                vdIFCExportProperties ep = IFCExportProperties.Clone() as vdIFCExportProperties;
                ep.IFCVersion = IFCVersionEnum.IFC3;
                bool isSave = vdIFCComponent1.Save(ifc, "test0"+8+".ifc", ep);
                Console.WriteLine(isSave);
            
        }

        [Obsolete]
        private void mTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        
            mTreeNode node = (mTreeNode)mTree.GetSelectedObject();
            if (node == null) return;
            if (node.mObject == null) return;
            if (node.mObject is vdIFCBuildingStorey)
            {
                vdIFCBuildingStorey storey = (vdIFCBuildingStorey)node.mObject;
                PathFinderForm form3 = new PathFinderForm();
                Info.fileInfo = fi;
                form3.info.floor.name = node.ToString();
                form3.importIFC(storey, form3.getDoc());
            }
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            mTreeNode node = (mTreeNode)this.toolStripComboBox2.SelectedItem;
            if (node == null) return;
            if (node.mObject == null) return;
            if (node.mObject is vdIFCBuildingStorey)
            {
                vdIFCBuildingStorey storey = (vdIFCBuildingStorey)node.mObject;
                PathFinderForm form3 = new PathFinderForm();
                form3.info.floor.name = node.ToString();
                Info.fileInfo = fi;
                form3.importIFC(storey, form3.getDoc());
              
            }
        }
    }
}