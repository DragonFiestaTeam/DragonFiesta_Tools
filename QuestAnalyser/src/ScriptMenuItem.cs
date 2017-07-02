using System;
using System.Windows.Forms;

namespace QuestDataAnalyser
{
    public class ScriptMenuItem : ToolStripMenuItem
    {
        private string Key;
        public AnalyserForm mMain;

        public ScriptMenuItem(string Name, AnalyserForm pForm) : base()
        {
            mMain = pForm;
            Key = Name;
            Visible = true;
            this.Name = Name;
            Text = Name;
            Visible = true;
            Click += new System.EventHandler(MenuItem_Click);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        public override string ToString()
        {
            return Key;
        }

        public void MenuItem_Click(object sender, EventArgs e)
        {
            mMain.mScriptForm = new ScriptForm(Text);
            mMain.mScriptForm.Show();
        }
    }
}