using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MapleShark
{
    public partial class PropertyForm : DockContent
    {
        public PropertyForm()
        {
            InitializeComponent();
        }

        public QuestAnalyserForm MainForm { get { return ParentForm as QuestAnalyserForm; } }
        public PropertyGrid Properties { get { return mProperties; } }

        private void mProperties_Click(object sender, EventArgs e)
        {

        }
    }
}
