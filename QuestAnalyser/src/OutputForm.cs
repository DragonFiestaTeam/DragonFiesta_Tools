﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace QuestDataAnalyser
{
    public partial class OutputForm : DockContent
    {
        public OutputForm(string pTitle)
        {
            InitializeComponent();
            Text = pTitle;
        }

        public void Append(string pOutput) { mTextBox.AppendText(pOutput); }

        private void mTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
