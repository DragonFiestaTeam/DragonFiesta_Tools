using QuestDataSQLConverter.IO;
using QuestDataSQLConverter.Object;
using QuestDataSQLConverter.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace QuestDataForm
{
    public partial class FrmMain : Form
    {
        private QuestFile pFile;

        public static FrmMain Instance { get; set; }
        public Dictionary<int, Quest> QuestsByIndex = new Dictionary<int, Quest>();

        public FrmMain()
        {
            InitializeComponent();
        }

        public void SetConnectLabel(bool State)
        {
            lDBConnect.Text = State.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Instance = this;
            OpenQuestData.Filter = "SHN Files (*.SHN)|*.SHN";
            if (OpenQuestData.ShowDialog() == DialogResult.OK)
            {
                pFile = new QuestFile(OpenQuestData.FileName);
                pFile.ReadQuestsFromFile();

                QuestCount.Text = pFile.QuestList.Count.ToString();

                if (File.Exists(@"DatabaseSettings.xml"))
                {
                    if (!DatabaseSetting.Load("DatabaseSettings.xml", out DatabaseSetting.Instance))
                    {
                        MessageBox.Show("Failed to Load DatabaseSetting");
                        return;
                    }
                    if (!DatabaseManager.Initialize(DatabaseSetting.Instance.SQLHost,
                                    DatabaseSetting.Instance.SQLUser,
                                    DatabaseSetting.Instance.SQLPassword,
                                    DatabaseSetting.Instance.SQLDB))
                    {
                        MessageBox.Show("Failed To Connect SQL Server");
                        return;
                    }
                    lDBConnect.Text = true.ToString();
                }

                for (int i = 0; i < pFile.QuestList.Count; i++)
                {
                    QuestsByIndex.Add(i, pFile.QuestList[i]);
                    lbQuestList.Items.Add("Index " + i + "  ID " + pFile.QuestList[i].PInfo.ID);
                }

                TabControl.TabPages[0].Text = "Basic Info";
                TabControl.TabPages[1].Text = "Accept-Script";
                TabControl.TabPages[2].Text = "Active-Script";
                TabControl.TabPages[3].Text = "Finish-Script";
                TabControl.TabPages[4].Text = "MobInfo";
                TabControl.TabPages[5].Text = "ItemInfo";
                TabControl.TabPages[6].Text = "ItemDrop";
                TabControl.TabPages[7].Text = "Reward Info";
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] im = lbQuestList.SelectedItem.ToString().Split(' ');

            if (!QuestsByIndex.TryGetValue(int.Parse(im[1]), out Quest pQuest))
            {
                return;
            }

            // BasicInfo
            TabControlQuestID.Text = pQuest.PInfo.ID.ToString();
            TabControlTitleID.Text = pQuest.PInfo.TitleID.ToString();
            TabControlDescriptionID.Text = pQuest.PInfo.DescriptionID.ToString();
            TabControlGrade.Text = pQuest.PInfo.QuestGrade.ToString();
            TabControlRepeat.Text = pQuest.PInfo.MultiQuest.ToString();
            TabControlDaily.Text = pQuest.PInfo.DailyQuest.ToString();
            TabControlEnabled.Text = pQuest.PInfo.Enable.ToString();
            TabControlInstAccept.Text = pQuest.PInfo.InstAcc.ToString();
            TabControlNeedLevel.Text = pQuest.PInfo.NeedLevel.ToString();
            TabControlMinLevel.Text = pQuest.PInfo.MinLevel.ToString();
            TabControlMaxLevel.Text = pQuest.PInfo.MaxLevel.ToString();
            TabControlNeedNPC.Text = pQuest.PInfo.NeedNPC.ToString();
            TabControlStartingNPC.Text = pQuest.PInfo.StartingNPC.ToString();
            TabControlNeedItem.Text = pQuest.PInfo.NeedItem.ToString();
            TabControlItemID.Text = pQuest.PInfo.ItemID.ToString();
            TabControlItemVanish.Text = pQuest.PInfo.ItemVanish.ToString();
            TabControlNeedPred.Text = pQuest.PInfo.NeedPreed.ToString();
            TabControlPredesessor.Text = pQuest.PInfo.Predesessor.ToString();
            TabControlNeedClass.Text = pQuest.PInfo.NeedClass.ToString();
            TabControlClassType.Text = pQuest.PInfo.ClassType.ToString();
            TabControlInstHandin.Text = pQuest.PInfo.InstHand.ToString();
            TabControlRequireLevel.Text = pQuest.PInfo.RequireLevel.ToString();
            TabControlFinishLevel.Text = pQuest.PInfo.FinishLevel.ToString();

            MobInfoView.Items.Clear();
            ItemInfoView.Items.Clear();
            ItemDropView.Items.Clear();
            RewardView.Items.Clear();

            // MobInfo
            for (int inxMob = 0; inxMob < 5; inxMob++)
            {
                MobInfoView.Items.Add("ID " + inxMob + " MobID " + pQuest.Mobs[inxMob].MobID);
            }

            // ItemInfo
            for (int inxItem = 0; inxItem < 10; inxItem++)
            {
                ItemInfoView.Items.Add("ID " + inxItem + " ItemID " + pQuest.Items[inxItem].ItemID);
            }
            
            // ItemDrop
            for (int inxDrop = 0; inxDrop < 10; inxDrop++)
            {
                ItemDropView.Items.Add("ID " + inxDrop + " ItemID " + pQuest.ItemDrops[inxDrop].ItemID);
            }
            
            // Reward
            for (int inxReward = 0; inxReward < 12; inxReward++)
            {
                QuestReward rewrad = pQuest.Rewards[inxReward];
                if (rewrad.Type != QuestDataSQLConverter.Enum.RewardType.Item)
                {
                    RewardView.Items.Add("ID " + inxReward + " Fame/EXP " + pQuest.Rewards[inxReward].Amount);
                }
                else
                {
                    RewardView.Items.Add("ID " + inxReward + " ItemID " + pQuest.Rewards[inxReward].ItemID);
                }
            }
            
            // Script
            TabControlAcceptScript.Text = pQuest.Script.StartScript;
            TabControlActiveScript.Text = pQuest.Script.ActionScript;
            TabControlFinishScript.Text = pQuest.Script.FinishScript;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (lbQuestList.SelectedItem == null) return;
            string[] im = lbQuestList.SelectedItem.ToString().Split(':');
            if (!DatabaseManager.IsInitialed)
            {
                DatabaseForm DBForm = new DatabaseForm();
                DBForm.Show();
                return;
            }
            Quest.CheckTables();

            if (!QuestsByIndex.TryGetValue(int.Parse(im[1]), out Quest pQuest))
            {
                MessageBox.Show("Failed To Select Quest for Save");
                return;
            }

            pQuest.SaveSQL();
            MessageBox.Show("Saving Singel Quest with ID: " + pQuest.PInfo.ID + " Success!");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!DatabaseManager.IsInitialed)
            {
                DatabaseForm DBForm = new DatabaseForm();
                DBForm.Show();
                return;
            }
            Quest.CheckTables();

            foreach (var pQ in QuestsByIndex.Values)
            {
                pQ.SaveSQL();
            }
            MessageBox.Show("Save All Success");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DatabaseForm DBForm = new DatabaseForm();
            DBForm.Show();
        }

        private void MobInfoView_SelectedValueChanged(object sender, EventArgs e)
        {
            string[] im = MobInfoView.SelectedItem.ToString().Split(' ');

            if (int.TryParse(im[1], out int Index))
            {
                if (lbQuestList.SelectedItem != null)
                {
                    string[] im2 = lbQuestList.SelectedItem.ToString().Split(' ');

                    if (!QuestsByIndex.TryGetValue(int.Parse(im2[1]), out Quest pQuest))
                    {
                        return;
                    }

                    QuestMob pMobs = pQuest.Mobs[Index];

                    TabControlMobInfoIsActive.Text = pMobs.IsEnabled.ToString();
                    TabControlMobInfoMobID.Text = pMobs.MobID.ToString();
                    TabControlMobInfoIsKillable.Text = pMobs.IsKillable.ToString();
                    TabControlMobInfoAmount.Text = pMobs.Amount.ToString();
                }
            }
        }

        private void ItemInfoView_SelectedValueChanged(object sender, EventArgs e)
        {
            string[] im = ItemInfoView.SelectedItem.ToString().Split(' ');

            if (int.TryParse(im[1], out int Index))
            {
                if (lbQuestList.SelectedItem != null)
                {
                    string[] im2 = lbQuestList.SelectedItem.ToString().Split(' ');

                    if (!QuestsByIndex.TryGetValue(int.Parse(im2[1]), out Quest pQuest))
                    {
                        return;
                    }

                    QuestItem pItem = pQuest.Items[Index];
                    TabControlItemInfoViewIsActive.Text = pItem.IsEnabled.ToString();
                    TabControlItemInfoViewItemType.Text = pItem.Type.ToString();
                    TabControlItemInfoViewItemID.Text = pItem.ItemID.ToString();
                    TabControlItemInfoViewAmount.Text = pItem.Amount.ToString();
                }
            }
        }

        private void ItemDropView_SelectedValueChanged(object sender, EventArgs e)
        {
            
            string[] im = ItemDropView.SelectedItem.ToString().Split(' ');

            if (int.TryParse(im[1], out int Index))
            {
                if (lbQuestList.SelectedItem != null)
                {
                    string[] im2 = lbQuestList.SelectedItem.ToString().Split(' ');

                    if (!QuestsByIndex.TryGetValue(int.Parse(im2[1]), out Quest pQuest))
                    {
                        return;
                    }
                    
                    QuestItemDrop pItemDrop = pQuest.ItemDrops[Index];
                    TabControlItemDropAmount.Text = pItemDrop.Amount.ToString();
                    TabControlItemDropMobID.Text = pItemDrop.MobID.ToString();
                    TabControlItemDropIsEnabled.Text = pItemDrop.IsEnabled.ToString();
                    TabControlItemDropItemID.Text = pItemDrop.ItemID.ToString();
                    TabControlItemDropRate.Text = pItemDrop.Rate.ToString();
                    TabControlItemDropMinDrop.Text = pItemDrop.MinDrop.ToString();
                    TabControlItemDropMaxDrop.Text = pItemDrop.MaxDrop.ToString();
                    TabControlItemDropUnk.Text = pItemDrop.Unk.ToString();
                    
                }
            }
            
        }

        private void RewardView_SelectedValueChanged(object sender, EventArgs e)
        {
            string[] im = RewardView.SelectedItem.ToString().Split(' ');

            if (int.TryParse(im[1], out int Index))
            {
                if (lbQuestList.SelectedItem != null)
                {
                    string[] im2 = lbQuestList.SelectedItem.ToString().Split(' ');

                    if (!QuestsByIndex.TryGetValue(int.Parse(im2[1]), out Quest pQuest))
                    {
                        return;
                    }

                    QuestReward pReward = pQuest.Rewards[Index];
                    TabControlRewardMethod.Text = pReward.Method.ToString();
                    TabControlRewardType.Text = pReward.Type.ToString();
                    TabControlRewardItemID.Text = pReward.ItemID.ToString();
                    TabControlRewardAmount.Text = pReward.Amount.ToString();

                }
            }
        }
    }
}