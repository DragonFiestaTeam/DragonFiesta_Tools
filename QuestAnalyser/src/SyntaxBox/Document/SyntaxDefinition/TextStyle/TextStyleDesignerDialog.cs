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
using System.Drawing;
using System.Windows.Forms;

namespace Alsing.SourceCode
{
    /// <summary>
    /// Summary description for TextStyleDesignerDialog.
    /// </summary>
    public class TextStyleDesignerDialog : Form
    {
        private readonly TextStyle _Style;
        private readonly TextStyle _TmpStyle;
        private Button btnCancel;
        private Button btnOK;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private Label lblCaption;
        private Label lblPreview;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private PropertyGrid pgStyles;

        public TextStyleDesignerDialog(TextStyle Style)
        {
            _Style = Style;
            _TmpStyle = (TextStyle)Style.Clone();

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            pgStyles.SelectedObject = _TmpStyle;
            lblCaption.Text = _Style.ToString();
            PreviewStyle();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public static event EventHandler Change;

        protected static void OnChange()
        {
            if (Change != null)
                Change(null, EventArgs.Empty);
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

        private void pgStyles_PropertyValueChanged(object s,
                                                   PropertyValueChangedEventArgs e)
        {
            PreviewStyle();
        }

        private void PreviewStyle()
        {
            TextStyle s = _TmpStyle;

            lblPreview.ForeColor = s.ForeColor;
            if (s.BackColor != Color.Transparent)
                lblPreview.BackColor = s.BackColor;
            else
                lblPreview.BackColor = Color.White;

            FontStyle fs = FontStyle.Regular;
            if (s.Bold)
                fs |= FontStyle.Bold;
            if (s.Italic)
                fs |= FontStyle.Italic;
            if (s.Underline)
                fs |= FontStyle.Underline;

            lblPreview.Font = new Font("Courier New", 11f, fs);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _Style.BackColor = _TmpStyle.BackColor;
            _Style.ForeColor = _TmpStyle.ForeColor;
            _Style.Bold = _TmpStyle.Bold;
            _Style.Italic = _TmpStyle.Italic;
            _Style.Underline = _TmpStyle.Underline;
            OnChange();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TextStyleDesignerDialog_Load(object sender, EventArgs e)
        {
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel2 = new System.Windows.Forms.Panel();
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            pgStyles = new System.Windows.Forms.PropertyGrid();
            panel1 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            lblCaption = new System.Windows.Forms.Label();
            lblPreview = new System.Windows.Forms.Label();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            //
            // panel2
            //
            panel2.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {
                                              lblPreview, btnCancel, btnOK
                                          }
                );
            panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel2.Location = new System.Drawing.Point(4, 255);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(354, 80);
            panel2.TabIndex = 8;
            //
            // btnCancel
            //
            btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Top |
                                     System.Windows.Forms.AnchorStyles.Right);
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(279, 48);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            //
            // btnOK
            //
            btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top |
                                 System.Windows.Forms.AnchorStyles.Right);
            btnOK.Location = new System.Drawing.Point(200, 48);
            btnOK.Name = "btnOK";
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.Click += new System.EventHandler(btnOK_Click);
            //
            // pgStyles
            //
            pgStyles.CommandsVisibleIfAvailable = true;
            pgStyles.Dock = System.Windows.Forms.DockStyle.Fill;
            pgStyles.LargeButtons = false;
            pgStyles.LineColor = System.Drawing.SystemColors.ScrollBar;
            pgStyles.Location = new System.Drawing.Point(4, 26);
            pgStyles.Name = "pgStyles";
            pgStyles.Size = new System.Drawing.Size(354, 221);
            pgStyles.TabIndex = 6;
            pgStyles.Text = "propertyGrid1";
            pgStyles.ToolbarVisible = false;
            pgStyles.ViewBackColor = System.Drawing.SystemColors.Window;
            pgStyles.ViewForeColor = System.Drawing.SystemColors.WindowText;
            pgStyles.PropertyValueChanged += new
                System.Windows.Forms.PropertyValueChangedEventHandler
                (pgStyles_PropertyValueChanged);
            //
            // panel1
            //
            panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(4, 247);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(354, 8);
            panel1.TabIndex = 9;
            //
            // panel3
            //
            panel3.BackColor = System.Drawing.SystemColors.ControlDark;
            panel3.Controls.AddRange(new System.Windows.Forms.Control[]
                                          {
                                              lblCaption
                                          }
                );
            panel3.Dock = System.Windows.Forms.DockStyle.Top;
            panel3.Location = new System.Drawing.Point(4, 2);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(354, 24);
            panel3.TabIndex = 10;
            //
            // lblCaption
            //
            lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            lblCaption.Font = new System.Drawing.Font("Microsoft Sans Serif",
                                                           11F, System.Drawing.FontStyle.Bold,
                                                           System.Drawing.GraphicsUnit.Point,
                                                           ((System.Byte)(0)));
            lblCaption.ForeColor = System.Drawing.SystemColors.Window;
            lblCaption.Name = "lblCaption";
            lblCaption.Size = new System.Drawing.Size(354, 24);
            lblCaption.TabIndex = 0;
            lblCaption.Text = "-";
            lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblPreview
            //
            lblPreview.BackColor = System.Drawing.SystemColors.Window;
            lblPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            lblPreview.Dock = System.Windows.Forms.DockStyle.Top;
            lblPreview.Font = new System.Drawing.Font("Courier New", 10F,
                                                           System.Drawing.FontStyle.Regular,
                                                           System.Drawing.GraphicsUnit.Point, (
                                                                                                  (System.Byte)(0)));
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new System.Drawing.Size(354, 40);
            lblPreview.TabIndex = 8;
            lblPreview.Text =
                "The quick brown fox jumped over the lazy dog.        ";
            lblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // TextStyleDesignerDialog
            //
            AcceptButton = btnOK;
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(362, 335);
            ControlBox = false;
            Controls.AddRange(new System.Windows.Forms.Control[]
                                   {
                                       pgStyles, panel3, panel1, panel2
                                   }
                );
            DockPadding.Left = 4;
            DockPadding.Right = 4;
            DockPadding.Top = 2;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "TextStyleDesignerDialog";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Style Designer";
            Load += new System.EventHandler(TextStyleDesignerDialog_Load);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}