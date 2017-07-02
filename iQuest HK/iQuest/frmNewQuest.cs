using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iQuest
{
    public partial class frmNewQuest : Form
    {
        frmMain form;
        public frmNewQuest(frmMain main)
        {
            form = main;
            InitializeComponent();
        }

        private void frmNewQuest_Load(object sender, EventArgs e)
        {
            if (form.ViewingQuest == null)
            {
                Close();
            }
            lblMax.Text = "Values allowed: " + ushort.MinValue.ToString() + " - " + ushort.MaxValue.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte enabled = 0;
            ushort id = 0;
            ushort des = 0;
            ushort title = 0;
            byte grade = 0;
            try
            {
                enabled = Convert.ToByte(txtEnabled.Text);
                id = Convert.ToUInt16(txtQID.Text);
                des = Convert.ToUInt16(txtDesc.Text);
                title = Convert.ToUInt16(txtTitle.Text);
                grade = Convert.ToByte(txtGrade.Text);
                foreach (Quest q in QuestFile.Quests)
                {
                    if (q.ID == id)
                    {
                        MessageBox.Show(id.ToString() + " is already in use!");
                        return;
                    }
                }
                NewQuest(enabled, id, title, des, grade);
            }
            catch
            {
                MessageBox.Show("Error parsing numbers");
                return;
            }
        }

        void NewQuest(byte enabled, ushort ID, ushort title, ushort desc, byte grade)
        {
            Quest q = new Quest();
            q.ID = ID;
            q.Title = title;
            q.Description = desc;
            q.QuestGrade = grade;
            q.MultiQuest = ViewingQuest.MultiQuest;
            q.DailyQuest = ViewingQuest.DailyQuest;
            q.EnableQuest = enabled;
            q.InstAcc = ViewingQuest.InstAcc;
            q.NeedLevel = ViewingQuest.NeedLevel;
            q.MinLevel = ViewingQuest.MinLevel;
            q.MaxLevel = ViewingQuest.MaxLevel;
            q.NeedNPC = ViewingQuest.NeedNPC;
            q.StartingNPC = ViewingQuest.StartingNPC;
            q.NeedItem = ViewingQuest.NeedItem;
            q.ItemID = ViewingQuest.ItemID;
            q.ItemVanish = ViewingQuest.ItemVanish;
            q.NeedPred = ViewingQuest.NeedPred;
            q.Predesessor = ViewingQuest.Predesessor;
            q.NeedClass = ViewingQuest.NeedClass;
            q.ClassType = ViewingQuest.ClassType;
            q.InstHand = ViewingQuest.InstHand;
            q.StartScript = ViewingQuest.StartScript;
            q.ActionScript = ViewingQuest.ActionScript;
            q.FinishScript = ViewingQuest.FinishScript;
            q.Mobs = ViewingQuest.Mobs;
            q.Rewards = ViewingQuest.Rewards;
            q.Items = ViewingQuest.Items;
            q.ItemData = ViewingQuest.ItemData;
            q.RewardData = ViewingQuest.RewardData;
            foreach (KeyValuePair<byte, byte[]> fo in ViewingQuest.UnkBytes)
            {
                byte[] newArr = new byte[fo.Value.Length];
                fo.Value.CopyTo(newArr, 0);
                q.UnkBytes.Add(fo.Key, newArr);
            }
            QuestFile.Quests.Add(q);
            form.ShowQuestList("");
            Close();
            form.Log("Duplicated Quest!");
        }

        public Quest ViewingQuest
        {
            get
            {
                return form.ViewingQuest;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (form.Editor == null)
            {
                form.Editor = new frmResEditor(form);
                form.Show();
            }
        }
    }
}
