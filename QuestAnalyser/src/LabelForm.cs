using WeifenLuo.WinFormsUI.Docking;

namespace QuestDataAnalyser
{
    public partial class LabelForm : DockContent
    {
        public LabelForm()
        {
            InitializeComponent();
        }

        public AnalyserForm MainForm { get { return ParentForm as AnalyserForm; } }

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