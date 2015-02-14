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
	public partial class LabelForm : DockContent
	{
		public LabelForm()
		{
			InitializeComponent();
		}
		public QuestAnalyserForm MainForm { get { return ParentForm as QuestAnalyserForm; } }
		public void SetLenght(int lenght)
		{
			label2.Text = lenght.ToString();
		}

		public void SetVersion(int version)
		{
			label6.Text = version.ToString();
		}

		public void SetFileName(string name)
		{
			label4.Text = name;
		}
	}
}
