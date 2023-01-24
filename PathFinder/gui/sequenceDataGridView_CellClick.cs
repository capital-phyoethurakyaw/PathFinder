namespace PathFinder.gui
{
    using System;
    using System.Windows.Forms;

    internal class sequenceDataGridView_CellClick
    {
        public bool Checked { get; internal set; }

        public static explicit operator sequenceDataGridView_CellClick(Control v)
        {
            throw new NotImplementedException();
        }
    }
}