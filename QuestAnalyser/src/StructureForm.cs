using ScriptNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace QuestDataAnalyser
{
    public partial class StructureForm : DockContent
    {
        private QuestStream mParsing;
        private Stack<StructureNode> mSubNodes = new Stack<StructureNode>();

        public StructureForm()
        {
            InitializeComponent();
        }

        public AnalyserForm MainForm { get { return ParentForm as AnalyserForm; } }
        public TreeView Tree { get { return mTree; } }

        public void ParseQuestStream(QuestStream qr)
        {
            mTree.Nodes.Clear();
            mSubNodes.Clear();
            mParsing = qr;
            try
            {
                APIAddScript("QuestScript.txt");
            }
            catch (Exception ex)
            {
                OutputForm output = new OutputForm("Script Error");
                output.Append(ex.ToString());
            }
            if (qr.Remaining > 0) mTree.Nodes.Add(new StructureNode("Undefined", qr.InnerBuffer, qr.Cursor, qr.Remaining));
        }

        internal void APIAddScript(string name)
        {
            try
            {
                StringBuilder scriptCode = new StringBuilder();
                if (File.Exists(@"Scripts/" + name))
                {
                    scriptCode.Append(File.ReadAllText(@"Scripts/" + name));
                    Script script = Script.Compile(scriptCode.ToString());
                    script.Context.SetItem("ScriptAPI", new ScriptAPI(this));
                    script.Execute();
                }
            }
            catch (Exception ex)
            {
                OutputForm output = new OutputForm("Script Error");
                output.Append(ex.ToString());
                output.Show(DockPanel, new Rectangle(MainForm.Location, new Size(400, 400)));
            }
        }

        private TreeNodeCollection CurrentNodes { get { return mSubNodes.Count > 0 ? mSubNodes.Peek().Nodes : mTree.Nodes; } }

        internal byte APIAddByte(string pName)
        {
            if (!mParsing.ReadByte(out byte value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 1, 1));
            return value;
        }

        internal sbyte APIAddSByte(string pName)
        {
            if (!mParsing.ReadSByte(out sbyte value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 1, 1));
            return value;
        }

        internal ushort APIAddUShort(string pName)
        {
            if (!mParsing.ReadUShort(out ushort value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 2, 2));
            return value;
        }

        internal short APIAddShort(string pName)
        {
            if (!mParsing.ReadShort(out short value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 2, 2));

            return value;
        }

        internal void APIDropMessage(string pMSG)
        {
            mParsing.DropMessage(pMSG);
        }

        internal uint APIAddUInt(string pName)
        {
            if (!mParsing.ReadUInt(out uint value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 4, 4));
            return value;
        }

        internal int APIAddInt(string pName)
        {
            if (!mParsing.ReadInt(out int value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 4, 4));
            return value;
        }

        internal float APIAddFloat(string pName)
        {
            if (!mParsing.ReadFloat(out float value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 4, 4));
            return value;
        }

        internal long APIAddLong(string pName)
        {
            if (!mParsing.ReadLong(out long value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 8, 8));
            return value;
        }

        internal double APIAddDouble(string pName)
        {
            if (!mParsing.ReadDouble(out double value)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - 8, 8));
            return value;
        }

        internal string APIAddString(string pName)
        {
            APIStartNode(pName);
            short size = APIAddShort("Size");
            string value = APIAddPaddedString("String", size);
            APIEndNode(false);
            return value;
        }

        internal string APIAddPaddedString(string pName, int pLength)
        {
            if (!mParsing.ReadPaddedString(out string value, pLength)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - pLength, pLength));
            return "";
        }

        internal void APIAddField(string pName, int pLength)
        {
            byte[] buffer = new byte[pLength];
            if (!mParsing.ReadBytes(buffer)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - pLength, pLength));
        }

        internal void APINulledString(string pName)
        {
            if (!mParsing.ReadNulledString(out string value, out int index)) throw new Exception("Insufficient packet data");
            CurrentNodes.Add(new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor - index - 1, index));
        }

        internal void APIAddComment(string pComment)
        {
            CurrentNodes.Add(new StructureNode(pComment, mParsing.InnerBuffer, mParsing.Cursor, 0));
        }

        internal void APIStartNode(string pName)
        {
            StructureNode node = new StructureNode(pName, mParsing.InnerBuffer, mParsing.Cursor, 0);
            if (mSubNodes.Count > 0) mSubNodes.Peek().Nodes.Add(node);
            else mTree.Nodes.Add(node);
            mSubNodes.Push(node);
        }

        internal void APIEndNode(bool pExpand)
        {
            if (mSubNodes.Count > 0)
            {
                StructureNode node = mSubNodes.Pop();
                node.Length = mParsing.Cursor - node.Cursor;
                if (pExpand) node.Expand();
            }
        }

        internal int APIRemaining()
        {
            return mParsing.Remaining;
        }

        private void MTree_AfterSelect(object pSender, TreeViewEventArgs pArgs)
        {
            StructureNode node = pArgs.Node as StructureNode;
            if (node == null) { MainForm.pDataForm.HexBox.SelectionLength = 0; MainForm.PropertyForm.Properties.SelectedObject = null; return; }
            MainForm.pDataForm.HexBox.SelectionStart = node.Cursor;
            MainForm.pDataForm.HexBox.SelectionLength = node.Length;
            MainForm.PropertyForm.Properties.SelectedObject = new StructureSegment(node.Buffer, node.Cursor, node.Length);
        }

        private void MTree_KeyDown(object pSender, KeyEventArgs pArgs)
        {
            MainForm.CopyPacketHex(pArgs);
        }
    }
}