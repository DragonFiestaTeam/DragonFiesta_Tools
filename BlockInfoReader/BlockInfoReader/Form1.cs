using System;
using System.Windows.Forms;
using System.IO;

namespace BlockInfoReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "BlockInfo (*.shbd) | *shbd";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                       var data = File.ReadAllBytes(openFileDialog1.FileName);

                       using (var stream = new MemoryStream(data))
                       {
                           using (var reader = new BinaryReader(stream))
                           {
                           label3.Text = reader.ReadUInt32().ToString();
                           label4.Text = reader.ReadUInt32().ToString();
                           }
                       }
            }
        }
    }
}
