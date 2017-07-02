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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Alsing.Design
{
    /// <summary>
    /// Summary description for CollectionEditorGui.
    /// </summary>
    [ToolboxItem(false)]
    public class CollectionEditorGui : UserControl
    {
        public Button btnAdd;
        public Button btnCancel;
        public Button btnDown;
        public Button btnDropdown;
        public Button btnOK;
        public Button btnRemove;
        public Button btnUp;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components;

        private GroupBox groupBox1;

        public Label lblMembers;
        public Label lblProperties;
        public ListBox lstMembers;
        public Panel pnlMain;
        public Panel pnlMembers;
        public PropertyGrid pygProperties;

        public CollectionEditorGui()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitForm call
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

        public void Bind()
        {
            var e = EditValue as ICollection;
            if (e == null)
            {
                MessageBox.Show("EditValue is null");
            }
            else
            {
                lstMembers.Items.Clear();
                foreach (object o in e)
                {
                    lstMembers.Items.Add(o);
                }

                EnableRemove();
                SelectObject();
            }
        }

        private void lstMembers_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index == -1)
                    return;

                int c = lstMembers.Items.Count;
                SizeF s = e.Graphics.MeasureString(c.ToString(), lstMembers.Font);
                var maxwidth = (int)s.Width;
                if (maxwidth < 16 + 2)
                    maxwidth = 16 + 2;

                var r = new Rectangle(0, e.Bounds.Top, maxwidth, lstMembers.ItemHeight);
                bool SupportPaint = Editor.GetPaintValueSupported();
                int w = SupportPaint ? 20 : 0;

                var rcItem = new Rectangle(r.Right + w, r.Top, e.Bounds.Width - r.Right - w, lstMembers.ItemHeight);

                ControlPaint.DrawBorder3D(e.Graphics, r, Border3DStyle.Raised, Border3DSide.All);
                StringFormat sf = StringFormat.GenericDefault;
                sf.Alignment = StringAlignment.Far;
                r.Inflate(-1, -1);
                e.Graphics.DrawString(e.Index.ToString(), lstMembers.Font, Brushes.Black, r, sf);

                bool Selected = ((int)e.State & (int)DrawItemState.Selected) != 0;

                using (SolidBrush bg = GetBgBrush(Selected))
                using (SolidBrush fg = GetFgBrush(Selected))
                {
                    e.Graphics.FillRectangle(bg, rcItem);
                    if (Selected && e.Index != -1)
                    {
                        if (((int)e.State & (int)DrawItemState.Focus) != 0)
                        {
                            ControlPaint.DrawFocusRectangle(e.Graphics, rcItem);
                        }
                    }

                    if (e.Index >= 0)
                    {
                        object o = lstMembers.Items[e.Index];
                        string name = GetDisplayText(o);
                        e.Graphics.DrawString(name, lstMembers.Font, fg, rcItem);
                    }
                }
            }
            catch { }
        }

        private SolidBrush GetFgBrush(bool Selected)
        {
            SolidBrush fg = Selected ? new SolidBrush(SystemColors.HighlightText) : new SolidBrush(lstMembers.ForeColor);
            return fg;
        }

        private SolidBrush GetBgBrush(bool Selected)
        {
            SolidBrush bg = Selected ? new SolidBrush(SystemColors.Highlight) : new SolidBrush(lstMembers.BackColor);
            return bg;
        }

        private void lstMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableRemove();
            SelectObject();
        }

        private void SelectObject()
        {
            pygProperties.SelectedObject = lstMembers.SelectedIndex >= 0 ? lstMembers.SelectedItem : null;
        }

        private void EnableRemove()
        {
            btnRemove.Enabled = lstMembers.SelectedIndices.Count > 0;
        }

        private static string GetDisplayText(object Item)
        {
            string ObjectName = null;

            if (Item == null)
            {
                return string.Empty;
            }
            PropertyDescriptor descriptor1 = TypeDescriptor.GetProperties(Item)["Name"];
            if (descriptor1 != null)
            {
                ObjectName = ((string)descriptor1.GetValue(Item));
                if (!string.IsNullOrEmpty(ObjectName))
                {
                    return ObjectName;
                }
            }

            if (string.IsNullOrEmpty(ObjectName))
            {
                ObjectName = Item.GetType().Name;
            }
            return ObjectName;
        }

        public void AddObject(object o)
        {
            Editor.AddObject(o);
        }

        public void RemoveObject(object o)
        {
            Editor.RemoveObject(o);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.Resources.ResourceManager(typeof(CollectionEditorGui));
            btnCancel = new System.Windows.Forms.Button();
            btnOK = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            pygProperties = new System.Windows.Forms.PropertyGrid();
            lstMembers = new System.Windows.Forms.ListBox();
            btnUp = new System.Windows.Forms.Button();
            btnDown = new System.Windows.Forms.Button();
            pnlMembers = new System.Windows.Forms.Panel();
            btnRemove = new System.Windows.Forms.Button();
            lblMembers = new System.Windows.Forms.Label();
            btnDropdown = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            pnlMain = new System.Windows.Forms.Panel();
            lblProperties = new System.Windows.Forms.Label();
            pnlMembers.SuspendLayout();
            pnlMain.SuspendLayout();
            SuspendLayout();
            //
            // btnCancel
            //
            btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            btnCancel.Location = new System.Drawing.Point(456, 312);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            //
            // btnOK
            //
            btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOK.Location = new System.Drawing.Point(376, 312);
            btnOK.Name = "btnOK";
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            //
            // groupBox1
            //
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                                     | System.Windows.Forms.AnchorStyles.Right);
            groupBox1.Location = new System.Drawing.Point(8, 296);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(520, 8);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            //
            // pygProperties
            //
            pygProperties.CommandsVisibleIfAvailable = true;
            pygProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            pygProperties.HelpVisible = false;
            pygProperties.LargeButtons = false;
            pygProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
            pygProperties.Location = new System.Drawing.Point(240, 16);
            pygProperties.Name = "pygProperties";
            pygProperties.Size = new System.Drawing.Size(280, 280);
            pygProperties.TabIndex = 3;
            pygProperties.Text = "propertyGrid1";
            pygProperties.ToolbarVisible = false;
            pygProperties.ViewBackColor = System.Drawing.SystemColors.Window;
            pygProperties.ViewForeColor = System.Drawing.SystemColors.WindowText;
            //
            // lstMembers
            //
            lstMembers.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                       | System.Windows.Forms.AnchorStyles.Left)
                                      | System.Windows.Forms.AnchorStyles.Right);
            lstMembers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            lstMembers.IntegralHeight = false;
            lstMembers.ItemHeight = 16;
            lstMembers.Location = new System.Drawing.Point(0, 16);
            lstMembers.Name = "lstMembers";
            lstMembers.Size = new System.Drawing.Size(208, 240);
            lstMembers.TabIndex = 4;
            lstMembers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(lstMembers_DrawItem);
            lstMembers.SelectedIndexChanged += new System.EventHandler(lstMembers_SelectedIndexChanged);
            //
            // btnUp
            //
            btnUp.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            btnUp.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnUp.Image")));
            btnUp.Location = new System.Drawing.Point(212, 16);
            btnUp.Name = "btnUp";
            btnUp.Size = new System.Drawing.Size(22, 28);
            btnUp.TabIndex = 5;
            btnUp.Click += new System.EventHandler(btnUp_Click);
            //
            // btnDown
            //
            btnDown.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            btnDown.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDown.Image")));
            btnDown.Location = new System.Drawing.Point(212, 48);
            btnDown.Name = "btnDown";
            btnDown.Size = new System.Drawing.Size(22, 28);
            btnDown.TabIndex = 6;
            btnDown.Click += new System.EventHandler(btnDown_Click);
            //
            // pnlMembers
            //
            pnlMembers.Controls.AddRange(new System.Windows.Forms.Control[]
                                              {
                                                  btnRemove,
                                                  lstMembers,
                                                  lblMembers,
                                                  btnDown,
                                                  btnUp,
                                                  btnDropdown,
                                                  btnAdd
                                              });
            pnlMembers.Dock = System.Windows.Forms.DockStyle.Left;
            pnlMembers.Name = "pnlMembers";
            pnlMembers.Size = new System.Drawing.Size(240, 296);
            pnlMembers.TabIndex = 7;
            //
            // btnRemove
            //
            btnRemove.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            btnRemove.Location = new System.Drawing.Point(136, 264);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(72, 24);
            btnRemove.TabIndex = 11;
            btnRemove.Text = "Remove";
            //
            // lblMembers
            //
            lblMembers.Dock = System.Windows.Forms.DockStyle.Top;
            lblMembers.Name = "lblMembers";
            lblMembers.Size = new System.Drawing.Size(240, 16);
            lblMembers.TabIndex = 10;
            lblMembers.Text = "Members:";
            //
            // btnDropdown
            //
            btnDropdown.Anchor = (System.Windows.Forms.AnchorStyles.Bottom |
                                       System.Windows.Forms.AnchorStyles.Right);
            btnDropdown.Image = ((System.Drawing.Bitmap)(resources.GetObject("btnDropdown.Image")));
            btnDropdown.Location = new System.Drawing.Point(95, 264);
            btnDropdown.Name = "btnDropdown";
            btnDropdown.Size = new System.Drawing.Size(24, 24);
            btnDropdown.TabIndex = 9;
            btnDropdown.Visible = false;
            //
            // btnAdd
            //
            btnAdd.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            btnAdd.Location = new System.Drawing.Point(8, 264);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(88, 24);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Add";
            //
            // pnlMain
            //
            pnlMain.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                   | System.Windows.Forms.AnchorStyles.Right);
            pnlMain.Controls.AddRange(new System.Windows.Forms.Control[]
                                           {
                                               pygProperties,
                                               lblProperties,
                                               pnlMembers
                                           });
            pnlMain.Location = new System.Drawing.Point(8, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new System.Drawing.Size(520, 296);
            pnlMain.TabIndex = 8;
            //
            // lblProperties
            //
            lblProperties.Dock = System.Windows.Forms.DockStyle.Top;
            lblProperties.Location = new System.Drawing.Point(240, 0);
            lblProperties.Name = "lblProperties";
            lblProperties.Size = new System.Drawing.Size(280, 16);
            lblProperties.TabIndex = 9;
            lblProperties.Text = "Properties:";
            //
            // CollectionEditorGui
            //
            Controls.AddRange(new System.Windows.Forms.Control[]
                                   {
                                       groupBox1,
                                       btnOK,
                                       btnCancel,
                                       pnlMain
                                   });
            Name = "CollectionEditorGui";
            Size = new System.Drawing.Size(536, 352);
            pnlMembers.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion Component Designer generated code

        #region PUBLIC PROPERTY EDITOR

        public ComponaCollectionEditor Editor { get; set; }

        #endregion PUBLIC PROPERTY EDITOR

        #region PUBLIC PROPERTY EDITORSERVICE

        public IWindowsFormsEditorService EditorService { get; set; }

        #endregion PUBLIC PROPERTY EDITORSERVICE

        #region PUBLIC PROPERTY EDITVALUE

        public object EditValue { get; set; }

        #endregion PUBLIC PROPERTY EDITVALUE
    }
}