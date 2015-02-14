using Microsoft.Win32;
using ScriptNET.Runtime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace MapleShark
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] pArgs)
        {
           
            RuntimeHost.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
           _MainForm = new QuestAnalyserForm();
            Application.Run(_MainForm);
        }

		internal static QuestAnalyserForm MainForm { get { return _MainForm; } }


		private static QuestAnalyserForm _MainForm;
	}
}
