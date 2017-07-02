using ScriptNET.Runtime;
using System;
using System.Windows.Forms;

namespace QuestDataAnalyser
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] pArgs)
        {
            RuntimeHost.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _MainForm = new AnalyserForm();
            Application.Run(_MainForm);
        }

        internal static AnalyserForm MainForm { get { return _MainForm; } }

        private static AnalyserForm _MainForm;
    }
}