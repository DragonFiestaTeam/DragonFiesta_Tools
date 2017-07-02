// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Alsing.Windows.Forms.SyntaxBox
{
    /// <summary>
    /// Summary description for GotoLine.
    /// </summary>
    public class GotoLineForm : Form
    {
        private readonly EditViewControl mOwner;
        private Button btnCancel;
        private Button btnOK;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private Label lblLines;
        private TextBox txtRow;

        /// <summary>
        /// Default constructor for the GotoLineForm.
        /// </summary>
        public GotoLineForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Creates a GotoLineForm that will be assigned to a specific Owner control.
        /// </summary>
        /// <param name="Owner">The SyntaxBox that will use the GotoLineForm</param>
        /// <param name="RowCount">The number of lines in the owner control</param>
        public GotoLineForm(EditViewControl Owner, int RowCount)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            lblLines.Text = "Line number (1-" + RowCount.ToString(CultureInfo.InvariantCulture) + "):";
            mOwner = Owner;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int row = int.Parse(txtRow.Text) - 1;
                mOwner.GotoLine(row);
            }
            catch { }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GotoLine_Closing(object sender,
                                      CancelEventArgs e)
        {
            //e.Cancel =true;
            //this.Hide ();
        }

        private void GotoLine_Activated(object sender, EventArgs e)
        {
            txtRow.Focus();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            txtRow = new System.Windows.Forms.TextBox();
            lblLines = new System.Windows.Forms.Label();
            SuspendLayout();
            //
            // btnCancel
            //
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(160, 48);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(72, 24);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            //
            // btnOK
            //
            btnOK.Location = new System.Drawing.Point(80, 48);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(72, 24);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.Click += new System.EventHandler(btnOK_Click);
            //
            // txtRow
            //
            txtRow.Location = new System.Drawing.Point(8, 24);
            txtRow.Name = "txtRow";
            txtRow.Size = new System.Drawing.Size(224, 20);
            txtRow.TabIndex = 2;
            txtRow.Text = "";
            //
            // lblLines
            //
            lblLines.Location = new System.Drawing.Point(8, 8);
            lblLines.Name = "lblLines";
            lblLines.Size = new System.Drawing.Size(128, 16);
            lblLines.TabIndex = 3;
            lblLines.Text = "-";
            //
            // GotoLineForm
            //
            AcceptButton = btnOK;
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(242, 82);
            Controls.AddRange(new System.Windows.Forms.Control[]
                                   {
                                       lblLines, txtRow, btnOK, btnCancel
                                   }
                );
            FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "GotoLineForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Go To Line";
            Closing += new System.ComponentModel.CancelEventHandler
                (GotoLine_Closing);
            Activated += new System.EventHandler(GotoLine_Activated);
            ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}