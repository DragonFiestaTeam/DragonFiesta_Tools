using System;
using System.IO;
using System.Windows.Forms;

namespace FiestaVersionsGrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult res = BinDialog.ShowDialog();
            
            if (res == DialogResult.OK)
            {
                string file = BinDialog.FileName;

                HashBox.Text = Md5Crypto.GetMD5HashFromFile(file).ToLower();
                ClientDateBox.Text = File.GetCreationTime(file).ToString("yyyy-MM-dd:HH:mm.ss");
            }
        }
    }
}
