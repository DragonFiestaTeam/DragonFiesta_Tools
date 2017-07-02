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
using System.Windows.Forms;

namespace Alsing.Windows.Forms.SyntaxBox
{
    /// <summary>
    /// Summary description for FindReplace.
    /// </summary>
    public class FindReplaceForm : Form
    {
        private WeakReference _Control;
        private string _Last = "";
        private Button btnClose;
        private Button btnDoReplace;
        private Button btnFind;
        private Button btnMarkAll;
        private Button btnRegex1;
        private Button btnReplace;
        private Button btnReplaceAll;
        private Button button1;
        private ComboBox cboFind;
        private ComboBox cboReplace;
        private CheckBox chkMatchCase;
        private CheckBox chkRegEx;
        private CheckBox chkWholeWord;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel3;
        private Panel pnlButtons;
        private Panel pnlFind;
        private Panel pnlReplace;
        private Panel pnlReplaceButtons;
        private Panel pnlSettings;

        /// <summary>
        /// Default constructor for the FindReplaceForm.
        /// </summary>
        public FindReplaceForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Creates a FindReplaceForm that will be assigned to a specific Owner control.
        /// </summary>
        /// <param name="Owner">The SyntaxBox that will use the FindReplaceForm</param>
        public FindReplaceForm(EditViewControl Owner)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            mOwner = Owner;
        }

        private EditViewControl mOwner
        {
            get
            {
                if (_Control != null)
                    return (EditViewControl)_Control.Target;
                else
                    return null;
            }
            set { _Control = new WeakReference(value); }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //				unchecked
                //				{
                //					int i= (int)0x80000000;
                //					cp.Style |=i;
                //				}
                return cp;
            }
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

        /// <summary>
        /// Displays the FindReplaceForm and sets it in "Find" mode.
        /// </summary>
        public void ShowFind()
        {
            pnlReplace.Visible = false;
            pnlReplaceButtons.Visible = false;
            Text = "Find";
            Show();
            Height = 160;
            btnDoReplace.Visible = false;
            btnReplace.Visible = true;
            _Last = "";
            cboFind.Focus();
        }

        /// <summary>
        /// Displays the FindReplaceForm and sets it in "Replace" mode.
        /// </summary>
        public void ShowReplace()
        {
            pnlReplace.Visible = true;
            pnlReplaceButtons.Visible = true;
            Text = "Replace";
            Show();
            Height = 200;
            btnDoReplace.Visible = true;
            btnReplace.Visible = false;
            _Last = "";
            cboFind.Focus();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            ShowReplace();
        }

        private void FindReplace_Closing(object sender,
                                         CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindNext();
            cboFind.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mOwner.Focus();
            Hide();
        }

        private void btnDoReplace_Click(object sender, EventArgs e)
        {
            mOwner.ReplaceSelection(cboReplace.Text);
            btnFind_Click(null, null);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            string text = cboFind.Text;
            if (text == "")
                return;

            bool found = false;
            foreach (string s in cboFind.Items)
            {
                if (s == text)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cboFind.Items.Add(text);

            int x = mOwner.Caret.Position.X;
            int y = mOwner.Caret.Position.Y;
            mOwner.Caret.Position.X = 0;
            mOwner.Caret.Position.Y = 0;
            while (mOwner.SelectNext(cboFind.Text, chkMatchCase.Checked,
                                     chkWholeWord.Checked, chkRegEx.Checked))
            {
                mOwner.ReplaceSelection(cboReplace.Text);
            }

            mOwner.Selection.ClearSelection();
            //	mOwner.Caret.Position.X=x;
            //	mOwner.Caret.Position.Y=y;
            //	mOwner.ScrollIntoView ();

            cboFind.Focus();
        }

        private void btnMarkAll_Click(object sender, EventArgs e)
        {
            string text = cboFind.Text;
            if (text == "")
                return;

            bool found = false;
            foreach (string s in cboFind.Items)
            {
                if (s == text)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cboFind.Items.Add(text);

            int x = mOwner.Caret.Position.X;
            int y = mOwner.Caret.Position.Y;
            mOwner.Caret.Position.X = 0;
            mOwner.Caret.Position.Y = 0;
            while (mOwner.SelectNext(cboFind.Text, chkMatchCase.Checked,
                                     chkWholeWord.Checked, chkRegEx.Checked))
            {
                mOwner.Caret.CurrentRow.Bookmarked = true;
            }

            mOwner.Selection.ClearSelection();
            //	mOwner.Caret.Position.X=x;
            //	mOwner.Caret.Position.Y=y;
            //	mOwner.ScrollIntoView ();

            cboFind.Focus();
        }

        public void FindNext()
        {
            string text = cboFind.Text;

            if (_Last != "" && _Last != text)
            {
                mOwner.Caret.Position.X = 0;
                mOwner.Caret.Position.Y = 0;
                mOwner.ScrollIntoView();
            }

            _Last = text;

            if (text == "")
                return;

            bool found = false;
            foreach (string s in cboFind.Items)
            {
                if (s == text)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                cboFind.Items.Add(text);

            mOwner.SelectNext(cboFind.Text, chkMatchCase.Checked,
                              chkWholeWord.Checked, chkRegEx.Checked);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new
                System.Resources.ResourceManager(typeof(FindReplaceForm));
            pnlButtons = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            btnClose = new System.Windows.Forms.Button();
            btnMarkAll = new System.Windows.Forms.Button();
            pnlReplaceButtons = new System.Windows.Forms.Panel();
            btnReplaceAll = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            btnDoReplace = new System.Windows.Forms.Button();
            btnReplace = new System.Windows.Forms.Button();
            btnFind = new System.Windows.Forms.Button();
            pnlFind = new System.Windows.Forms.Panel();
            cboFind = new System.Windows.Forms.ComboBox();
            label1 = new System.Windows.Forms.Label();
            btnRegex1 = new System.Windows.Forms.Button();
            pnlReplace = new System.Windows.Forms.Panel();
            cboReplace = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            pnlSettings = new System.Windows.Forms.Panel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            chkRegEx = new System.Windows.Forms.CheckBox();
            chkWholeWord = new System.Windows.Forms.CheckBox();
            chkMatchCase = new System.Windows.Forms.CheckBox();
            pnlButtons.SuspendLayout();
            panel3.SuspendLayout();
            pnlReplaceButtons.SuspendLayout();
            panel1.SuspendLayout();
            pnlFind.SuspendLayout();
            pnlReplace.SuspendLayout();
            pnlSettings.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            //
            // pnlButtons
            //
            pnlButtons.Controls.AddRange(new System.Windows.Forms.Control[]
                                              {
                                                  panel3, pnlReplaceButtons, panel1
                                              }
                );
            pnlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            pnlButtons.Location = new System.Drawing.Point(400, 0);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new System.Drawing.Size(96, 178);
            pnlButtons.TabIndex = 0;
            //
            // panel3
            //
            panel3.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {
                                              btnClose, btnMarkAll
                                          }
                );
            panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Location = new System.Drawing.Point(0, 96);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(96, 82);
            panel3.TabIndex = 4;
            //
            // btnClose
            //
            btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnClose.Location = new System.Drawing.Point(8, 40);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(80, 24);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.Click += new System.EventHandler(btnClose_Click);
            //
            // btnMarkAll
            //
            btnMarkAll.Location = new System.Drawing.Point(8, 8);
            btnMarkAll.Name = "btnMarkAll";
            btnMarkAll.Size = new System.Drawing.Size(80, 24);
            btnMarkAll.TabIndex = 2;
            btnMarkAll.Text = "Mark all";
            btnMarkAll.Click += new System.EventHandler(btnMarkAll_Click);
            //
            // pnlReplaceButtons
            //
            pnlReplaceButtons.Controls.AddRange(new
                                                         System.Windows.Forms.Control[]
                                                     {
                                                         btnReplaceAll
                                                     }
                );
            pnlReplaceButtons.Dock = System.Windows.Forms.DockStyle.Top;
            pnlReplaceButtons.Location = new System.Drawing.Point(0, 64);
            pnlReplaceButtons.Name = "pnlReplaceButtons";
            pnlReplaceButtons.Size = new System.Drawing.Size(96, 32);
            pnlReplaceButtons.TabIndex = 3;
            pnlReplaceButtons.Visible = false;
            //
            // btnReplaceAll
            //
            btnReplaceAll.Location = new System.Drawing.Point(8, 8);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new System.Drawing.Size(80, 24);
            btnReplaceAll.TabIndex = 2;
            btnReplaceAll.Text = "Replace All";
            btnReplaceAll.Click += new System.EventHandler
                (btnReplaceAll_Click);
            //
            // panel1
            //
            panel1.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {
                                              btnDoReplace, btnReplace, btnFind
                                          }
                );
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(96, 64);
            panel1.TabIndex = 2;
            //
            // btnDoReplace
            //
            btnDoReplace.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnDoReplace.Location = new System.Drawing.Point(8, 40);
            btnDoReplace.Name = "btnDoReplace";
            btnDoReplace.Size = new System.Drawing.Size(80, 24);
            btnDoReplace.TabIndex = 4;
            btnDoReplace.Text = "Replace";
            btnDoReplace.Click += new System.EventHandler
                (btnDoReplace_Click);
            //
            // btnReplace
            //
            btnReplace.Image = ((System.Drawing.Bitmap)(resources.GetObject(
                                                                 "btnReplace.Image")));
            btnReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnReplace.Location = new System.Drawing.Point(8, 40);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new System.Drawing.Size(80, 24);
            btnReplace.TabIndex = 3;
            btnReplace.Text = "Replace";
            btnReplace.Click += new System.EventHandler(btnReplace_Click);
            //
            // btnFind
            //
            btnFind.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnFind.Location = new System.Drawing.Point(8, 8);
            btnFind.Name = "btnFind";
            btnFind.Size = new System.Drawing.Size(80, 24);
            btnFind.TabIndex = 2;
            btnFind.Text = "&FindNext";
            btnFind.Click += new System.EventHandler(btnFind_Click);
            //
            // pnlFind
            //
            pnlFind.Controls.AddRange(new System.Windows.Forms.Control[]
                                           {
                                               cboFind, label1, btnRegex1
                                           }
                );
            pnlFind.Dock = System.Windows.Forms.DockStyle.Top;
            pnlFind.Name = "pnlFind";
            pnlFind.Size = new System.Drawing.Size(400, 40);
            pnlFind.TabIndex = 1;
            //
            // cboFind
            //
            cboFind.Location = new System.Drawing.Point(104, 8);
            cboFind.Name = "cboFind";
            cboFind.Size = new System.Drawing.Size(288, 21);
            cboFind.TabIndex = 1;
            //
            // label1
            //
            label1.Location = new System.Drawing.Point(8, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(96, 24);
            label1.TabIndex = 0;
            label1.Text = "Fi&nd what:";
            //
            // btnRegex1
            //
            btnRegex1.Image = ((System.Drawing.Bitmap)(resources.GetObject(
                                                                "btnRegex1.Image")));
            btnRegex1.Location = new System.Drawing.Point(368, 8);
            btnRegex1.Name = "btnRegex1";
            btnRegex1.Size = new System.Drawing.Size(21, 21);
            btnRegex1.TabIndex = 2;
            btnRegex1.Visible = false;
            //
            // pnlReplace
            //
            pnlReplace.Controls.AddRange(new System.Windows.Forms.Control[]
                                              {
                                                  cboReplace, label2, button1
                                              }
                );
            pnlReplace.Dock = System.Windows.Forms.DockStyle.Top;
            pnlReplace.Location = new System.Drawing.Point(0, 40);
            pnlReplace.Name = "pnlReplace";
            pnlReplace.Size = new System.Drawing.Size(400, 40);
            pnlReplace.TabIndex = 2;
            pnlReplace.Visible = false;
            //
            // cboReplace
            //
            cboReplace.Location = new System.Drawing.Point(106, 8);
            cboReplace.Name = "cboReplace";
            cboReplace.Size = new System.Drawing.Size(286, 21);
            cboReplace.TabIndex = 4;
            //
            // label2
            //
            label2.Location = new System.Drawing.Point(10, 8);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(96, 24);
            label2.TabIndex = 3;
            label2.Text = "Re&place with:";
            //
            // button1
            //
            button1.Image = ((System.Drawing.Bitmap)(resources.GetObject(
                                                              "button1.Image")));
            button1.Location = new System.Drawing.Point(368, 8);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(21, 21);
            button1.TabIndex = 5;
            button1.Visible = false;
            //
            // pnlSettings
            //
            pnlSettings.Controls.AddRange(new System.Windows.Forms.Control[]
                                               {
                                                   groupBox1
                                               }
                );
            pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlSettings.Location = new System.Drawing.Point(0, 80);
            pnlSettings.Name = "pnlSettings";
            pnlSettings.Size = new System.Drawing.Size(400, 98);
            pnlSettings.TabIndex = 3;
            //
            // groupBox1
            //
            groupBox1.Controls.AddRange(new System.Windows.Forms.Control[]
                                             {
                                                 chkRegEx, chkWholeWord, chkMatchCase
                                             }
                );
            groupBox1.Location = new System.Drawing.Point(8, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(384, 88);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search";
            //
            // chkRegEx
            //
            chkRegEx.Location = new System.Drawing.Point(8, 64);
            chkRegEx.Name = "chkRegEx";
            chkRegEx.Size = new System.Drawing.Size(144, 16);
            chkRegEx.TabIndex = 2;
            chkRegEx.Text = "Use Regular expressions";
            chkRegEx.Visible = false;
            //
            // chkWholeWord
            //
            chkWholeWord.Location = new System.Drawing.Point(8, 40);
            chkWholeWord.Name = "chkWholeWord";
            chkWholeWord.Size = new System.Drawing.Size(144, 16);
            chkWholeWord.TabIndex = 1;
            chkWholeWord.Text = "Match &whole word";
            //
            // chkMatchCase
            //
            chkMatchCase.Location = new System.Drawing.Point(8, 16);
            chkMatchCase.Name = "chkMatchCase";
            chkMatchCase.Size = new System.Drawing.Size(144, 16);
            chkMatchCase.TabIndex = 0;
            chkMatchCase.Text = "Match &case";
            //
            // FindReplaceForm
            //
            AcceptButton = btnFind;
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            CancelButton = btnClose;
            ClientSize = new System.Drawing.Size(496, 178);
            Controls.AddRange(new System.Windows.Forms.Control[]
                                   {
                                       pnlSettings, pnlReplace, pnlFind, pnlButtons
                                   }
                );
            FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "FindReplaceForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Find";
            Closing += new System.ComponentModel.CancelEventHandler
                (FindReplace_Closing);
            pnlButtons.ResumeLayout(false);
            panel3.ResumeLayout(false);
            pnlReplaceButtons.ResumeLayout(false);
            panel1.ResumeLayout(false);
            pnlFind.ResumeLayout(false);
            pnlReplace.ResumeLayout(false);
            pnlSettings.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}