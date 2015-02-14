using System;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using System.IO;

namespace MapleShark
{
	public partial class QuestAnalyserForm : Form
	{
		private DataForm mDataForm = new DataForm();
		private StructureForm mStructureForm = new StructureForm();
		private PropertyForm mPropertyForm = new PropertyForm();
		private ScriptForm mScriptForm = new ScriptForm();
		private LabelForm mLabelForm = new LabelForm();
		private bool mLoad;
		public DataForm pDataForm { get { return mDataForm; } }
		public StructureForm StructureForm { get { return mStructureForm; } }
		public PropertyForm PropertyForm { get { return mPropertyForm; } }

		public LabelForm mLabel { get { return mLabelForm; } }

		public ScriptForm pScript { get { return mScriptForm; } }

		public QuestAnalyserForm()
		{
			InitializeComponent();
		}

		public void CopyPacketHex(KeyEventArgs pArgs)
		{
			if (mDataForm.HexBox.SelectionLength > 0 && pArgs.Modifiers == Keys.Control && pArgs.KeyCode == Keys.C)
			{
				Clipboard.SetText(BitConverter.ToString((mDataForm.HexBox.ByteProvider as DynamicByteProvider).Bytes.ToArray(), (int)mDataForm.HexBox.SelectionStart, (int)mDataForm.HexBox.SelectionLength).Replace("-", " "));
				pArgs.SuppressKeyPress = true;
			}
			else if (mDataForm.HexBox.SelectionLength > 0 && pArgs.Control && pArgs.Shift && pArgs.KeyCode == Keys.C)
			{
				byte[] buffer = new byte[mDataForm.HexBox.SelectionLength];
				Buffer.BlockCopy((mDataForm.HexBox.ByteProvider as DynamicByteProvider).Bytes.ToArray(), (int)mDataForm.HexBox.SelectionStart, buffer, 0, (int)mDataForm.HexBox.SelectionLength);
				pArgs.SuppressKeyPress = true;
			}
		}

		private void dataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.FileName = "QuestData";
			openFileDialog1.Filter = "Fiesta QuestData |*.shn";
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				byte[] pData = File.ReadAllBytes(openFileDialog1.FileName);
				QuestStream qr = new QuestStream(pData);
				mDataForm.HexBox.ByteProvider = new DynamicByteProvider(pData);

				mStructureForm.ParseQuestStream(qr);
				mLabelForm.SetFileName(openFileDialog1.SafeFileName);
				mLabel.SetVersion(qr.pVersion);
				mLabel.SetLenght(pData.Length);

			}
		
			mDataForm.Show(mDockPanel);
			mLabelForm.Show(mDockPanel);
			mStructureForm.Show(mDockPanel);
			mPropertyForm.Show(mDockPanel);
			DockPane rightPane1 = new DockPane(mStructureForm, DockState.DockRight, true);
			DockPane rightPane2 = new DockPane(mPropertyForm, DockState.DockRight, true);
			rightPane1.Show();
			rightPane2.Show();
	
			
			dataToolStripMenuItem.Visible = false;
			scriptToolStripMenuItem.Visible = true;
			refreshToolStripMenuItem.Visible = true;
			closeToolStripMenuItem.Visible = true;
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		
		private void scriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
	
			pScript.Show();
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}

		private void QuestAnalyserForm_Load(object sender, EventArgs e)
		{

		}

		public long GetHexBoxSelectetLenght()
		{
			return mDataForm.HexBox.SelectionLength;
		}
		private void mDockPanel_ActiveDocumentChanged(object pSender, EventArgs pArgs)
		{
			/*if (!mClosed)
			{*/
				QuestAnalyserForm session = mDockPanel.ActiveDocument as QuestAnalyserForm;
		
					if (mDataForm.HexBox.ByteProvider != null) mDataForm.HexBox.ByteProvider.DeleteBytes(0, mDataForm.HexBox.ByteProvider.Length);
					mStructureForm.Tree.Nodes.Clear();
					mPropertyForm.Properties.SelectedObject = null;
				
			//}
		}
	}
}
