using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vdIFC;

namespace PathFinder
{
    internal class mTreeNode
    {
        private string _Type ="";
        public string Type 
        { 
            get { return _Type; }
            set { _Type = value; }
        }

        private bool mVisibility = true;
        public bool Visibility
        {
            get
            {
                if (mObject == null) return true;
                return mVisibility;
            }
            set
            {
                _setVisibility(value);
                RaiseOnNeedRedraw();
            }
        }
        internal void _setVisibility(bool val)
        {
            mVisibility = val;
            mObject.Visible = val;
            foreach (mTreeNode item in Children)
            {
                item._setVisibility(val);
            }
        }
        private string _Name = "";
        public string Name { 
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Description = "";
        public string Description { 
            get { return _Description; }
            set { _Description = value; }
        }

        private List<mTreeNode> _Children = null;
        public List<mTreeNode> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }
        private IIFCGlobalId _mObject =null;
        public IIFCGlobalId mObject
        {
            get { return _mObject; }
            set { _mObject = value; }
        }

        public mTreeNode(string Type, bool Visibility, string Name, string Description, IIFCGlobalId obj)
        {
            this.mObject = obj;
            this.Type = Type;
            //this.CID = CID;
            this.mVisibility = Visibility;
            this.Name = Name;
            this.Description = Description;
            this.Children = new List<mTreeNode>();
        }
        public delegate void NeedRedrawEventHandler();
        internal event NeedRedrawEventHandler OnNeedRedraw;

        public string ID
        {
            get
            {
                if (mObject == null) return "";
                else return mObject.IFCGlobalId;
            }
        }

        public void RaiseOnNeedRedraw()
        {
            OnNeedRedraw();
        }

        public override string ToString()
        {
            return this.Name;
        }

    }
}
