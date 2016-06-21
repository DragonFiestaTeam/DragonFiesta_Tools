using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiestaVersionsGrapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = BinDialog.ShowDialog();
            
            if (res == DialogResult.OK)
            {
                string file = BinDialog.FileName;

                HashBox.Text = Md5Crypto.GetMD5HashFromFile(file).ToLower();
                ClientDateBox.Text = File.GetCreationTime(file).ToString("yyyyMMddHHmmss");
            }
        }



    }
}
