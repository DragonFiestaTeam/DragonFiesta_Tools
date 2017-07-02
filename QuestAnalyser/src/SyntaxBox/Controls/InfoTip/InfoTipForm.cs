// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using Alsing.Windows.Forms.CoreLib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Alsing.Windows.Forms
{
    /// <summary>
    /// Summary description for InfoTip.
    /// </summary>
    public class InfoTipForm : Form
    {
        private WeakReference _Control;
        private int _Count = 1;
        private int _SelectedIndex;

        private PictureBox btnNext;
        private PictureBox btnPrev;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private FormatLabelControl InfoText;

        private Label lblIndex;
        private Panel panel1;
        private Panel panel2;
        private PictureBox picIcon;
        private Panel pnlImage;
        private Panel pnlSelect;

        /// <summary>
        ///
        /// </summary>
        public InfoTipForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parent"></param>
        public InfoTipForm(Control parent)
        {
            ParentControl = parent;
            if (CreateParams != null) CreateParams.ClassName = "tooltips_class32";

            InitializeComponent();
        }

        private Control ParentControl
        {
            get
            { return _Control != null ? (Control)_Control.Target : null; }
            set { _Control = new WeakReference(value); }
        }

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                if (value > _Count)
                    value = 1;
                if (value < 1)
                    value = _Count;

                _SelectedIndex = value;
                OnSelectedIndexChanged();
                SetPos();
            }
        }

        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public Image Image
        {
            get { return picIcon.Image; }
            set
            {
                picIcon.Image = value;
                if (value == null)
                {
                    pnlImage.Visible = false;
                }
                else
                {
                    pnlImage.Visible = true;
                    pnlImage.Width = Image.Width + 6;
                    picIcon.Size = Image.Size;
                }
                DoResize();
            }
        }

        public string Data
        {
            get { return InfoText.Text; }
            set { InfoText.Text = value; }
        }

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern
            int SendMessage(IntPtr hWnd, int message, int _data, int _id);

        public event EventHandler SelectedIndexChanged = null;

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

        private void InfoText_Resize(object sender, EventArgs e)
        {
            DoResize();
        }

        private void DoResize()
        {
            int w = InfoText.Left + InfoText.Width + 8;
            if (Count > 1)
            {
                w += pnlSelect.Width;
            }
            if (picIcon.Image != null)
            {
                w += pnlImage.Width;
            }

            int h = InfoText.Top + InfoText.Height + 6;
            if (Image != null && Image.Height + picIcon.Top * 2 > h)
                h = Image.Height + picIcon.Top * 2;

            ClientSize = new Size(w, h);
        }

        /// <summary>
        ///
        /// </summary>
        public void Init()
        {
            SelectedIndex = 1;
            SetPos();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            SelectedIndex++;
            SetPos();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            SelectedIndex--;
            SetPos();
        }

        private void btnPrev_DoubleClick(object sender, EventArgs e)
        {
            SelectedIndex--;
            SetPos();
        }

        private void btnNext_DoubleClick(object sender, EventArgs e)
        {
            SelectedIndex++;
            SetPos();
        }

        private void SetPos()
        {
            if (Count == 1)
            {
                pnlSelect.Visible = false;
            }
            else
            {
                pnlSelect.Visible = true;
            }
            DoResize();

            lblIndex.Text = SelectedIndex.ToString((CultureInfo.InvariantCulture)) + " of " +
                            Count.ToString(CultureInfo.InvariantCulture);

            if (ParentControl != null)
                ParentControl.Focus();
        }

        private void InfoTipForm_Enter(object sender, EventArgs e)
        {
            ParentControl.Focus();
        }

        private void InfoText_Enter(object sender, EventArgs e)
        {
            ParentControl.Focus();
        }

        private void OnSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, null);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new
                System.Resources.ResourceManager(typeof(InfoTipForm));
            pnlSelect = new System.Windows.Forms.Panel();
            btnNext = new System.Windows.Forms.PictureBox();
            btnPrev = new System.Windows.Forms.PictureBox();
            lblIndex = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            InfoText = new Alsing.Windows.Forms.CoreLib.FormatLabelControl();
            pnlImage = new System.Windows.Forms.Panel();
            picIcon = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            pnlSelect.SuspendLayout();
            panel2.SuspendLayout();
            pnlImage.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            //
            // pnlSelect
            //
            pnlSelect.Controls.AddRange(new System.Windows.Forms.Control[]
                                             {
                                                 btnNext, btnPrev, lblIndex
                                             }
                );
            pnlSelect.Dock = System.Windows.Forms.DockStyle.Left;
            pnlSelect.DockPadding.All = 4;
            pnlSelect.Location = new System.Drawing.Point(32, 0);
            pnlSelect.Name = "pnlSelect";
            pnlSelect.Size = new System.Drawing.Size(80, 35);
            pnlSelect.TabIndex = 0;
            //
            // btnNext
            //
            btnNext.BackColor = System.Drawing.SystemColors.Control;
            btnNext.Image = ((System.Drawing.Bitmap)(resources.GetObject(
                                                              "btnNext.Image")));
            btnNext.Location = new System.Drawing.Point(68, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(9, 11);
            btnNext.TabIndex = 1;
            btnNext.TabStop = false;
            btnNext.Click += new System.EventHandler(btnNext_Click);
            btnNext.DoubleClick += new System.EventHandler
                (btnNext_DoubleClick);
            //
            // btnPrev
            //
            btnPrev.BackColor = System.Drawing.SystemColors.Control;
            btnPrev.Image = ((System.Drawing.Bitmap)(resources.GetObject(
                                                              "btnPrev.Image")));
            btnPrev.Location = new System.Drawing.Point(4, 6);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new System.Drawing.Size(9, 11);
            btnPrev.TabIndex = 0;
            btnPrev.TabStop = false;
            btnPrev.Click += new System.EventHandler(btnPrev_Click);
            btnPrev.DoubleClick += new System.EventHandler
                (btnPrev_DoubleClick);
            //
            // lblIndex
            //
            lblIndex.Dock = System.Windows.Forms.DockStyle.Top;
            lblIndex.Location = new System.Drawing.Point(4, 4);
            lblIndex.Name = "lblIndex";
            lblIndex.Size = new System.Drawing.Size(72, 23);
            lblIndex.TabIndex = 2;
            lblIndex.Text = "20 of 20";
            lblIndex.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // panel2
            //
            panel2.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {
                                              InfoText
                                          }
                );
            panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.DockPadding.All = 4;
            panel2.Location = new System.Drawing.Point(112, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(126, 35);
            panel2.TabIndex = 1;
            //
            // InfoText
            //
            InfoText.AutoSizeHorizontal = true;
            InfoText.AutoSizeVertical = true;
            InfoText.BackColor = System.Drawing.SystemColors.Info;
            InfoText.BorderColor = System.Drawing.Color.Black;
            InfoText.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
            InfoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F,
                                                         System.Drawing.FontStyle.Regular,
                                                         System.Drawing.GraphicsUnit.Point, (
                                                                                                (System.Byte)(0)));
            InfoText.ImageList = null;
            InfoText.Link_Color = System.Drawing.Color.Blue;
            InfoText.Link_Color_Hover = System.Drawing.Color.Blue;
            InfoText.Link_UnderLine = false;
            InfoText.Link_UnderLine_Hover = true;
            InfoText.Location = new System.Drawing.Point(2, 4);
            InfoText.Name = "InfoText";
            InfoText.ScrollBars = System.Windows.Forms.ScrollBars.None;
            InfoText.Size = new System.Drawing.Size(59, 13);
            InfoText.TabIndex = 0;
            InfoText.Text = "format <b>label</b>";
            InfoText.WordWrap = false;
            InfoText.Resize += new System.EventHandler(InfoText_Resize);
            InfoText.Enter += new System.EventHandler(InfoText_Enter);
            //
            // pnlImage
            //
            pnlImage.Controls.AddRange(new System.Windows.Forms.Control[]
                                            {
                                                picIcon
                                            }
                );
            pnlImage.Dock = System.Windows.Forms.DockStyle.Left;
            pnlImage.Name = "pnlImage";
            pnlImage.Size = new System.Drawing.Size(32, 35);
            pnlImage.TabIndex = 2;
            pnlImage.Visible = false;
            //
            // picIcon
            //
            picIcon.Location = new System.Drawing.Point(5, 3);
            picIcon.Name = "picIcon";
            picIcon.Size = new System.Drawing.Size(19, 20);
            picIcon.TabIndex = 1;
            picIcon.TabStop = false;
            //
            // panel1
            //
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {
                                              panel2, pnlSelect, pnlImage
                                          }
                );
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(240, 37);
            panel1.TabIndex = 3;
            //
            // InfoTipForm
            //
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            BackColor = System.Drawing.SystemColors.Info;
            ClientSize = new System.Drawing.Size(240, 37);
            ControlBox = false;
            Controls.AddRange(new System.Windows.Forms.Control[]
                                   {
                                       panel1
                                   }
                );
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "InfoTipForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Enter += new System.EventHandler(InfoTipForm_Enter);
            pnlSelect.ResumeLayout(false);
            panel2.ResumeLayout(false);
            pnlImage.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}