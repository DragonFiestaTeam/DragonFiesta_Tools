using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace iQuest
{
    public partial class frmMain : Form
    {
        public QuestFile QuestFile;
        public Quest ViewingQuest;
        public frmResEditor Editor;
        public SHNFile QuestDialog;
        public Dictionary<ushort, string> QuestDialogDic = new Dictionary<ushort, string>();
        public Dictionary<ushort, string> MobDic = new Dictionary<ushort, string>();
        public Dictionary<ushort, string> ItemDic = new Dictionary<ushort, string>();
        public Dictionary<ushort, string> ClassDic = new Dictionary<ushort, string>();
        public SHNFile MobInfo;
        public SHNFile ClassInfo;
        public SHNFile ItemInfo;
        public bool QuestAltered = false;
        string FileFolder = "";
        string[] FileCheck = new string[] 
        {
            "QuestDialog.shn",
            "ItemInfo.shn",
            "QuestData.shn",
            "MobInfo.shn",
            "ClassName.shn"
        };
        public frmMain()
        {
            InitializeComponent();
            FileFolder = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            string missing = "";
            for (int i = 0; i < FileCheck.Length; i++)
            {
                string missing2 = FileCheck[i];
                if (!File.Exists(FileFolder + missing2))
                {
                    missing = missing + "\r\n" + missing2;
                }
            }
            if (missing.Length > 1)
            {
                MessageBox.Show("Can't find following files: " + missing);
            }
            else
            {
                LoadFiles();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            grpDefault.Enabled = false;
            lstMobs.Columns.Add("ID", 100);
            lstMobs.Columns.Add("canKill", 50);
            lstMobs.Columns.Add("Amount", 60);
            lstMobs.Columns.Add("Enabled", 80);
            //
            cmbIsNPC.Items.Clear();
            cmbIsNPC.Items.Add("False");
            cmbIsNPC.Items.Add("True");
            grpMobSettings.Enabled = false;
            //
            lstItems.Columns.Add("ID", 120);
            lstItems.Columns.Add("Amount", 50);
            lstItems.Columns.Add("Type", 50);
            lstItems.Columns.Add("Enabled", 80);
            grpItemInfo.Enabled = false;
            //
            lstRewards.Columns.Add("Type", 100);
            lstRewards.Columns.Add("ID", 130);
            lstRewards.Columns.Add("Amount", 90);
            lstRewards.Columns.Add("Selective", 90);
            grpRewardEdit.Enabled = false;
            //
            cmbRewardType.Items.Clear();
            cmbRewardType.Items.Add("0 - Exp");
            cmbRewardType.Items.Add("1 - Money");
            cmbRewardType.Items.Add("2 - Item");
            cmbRewardType.Items.Add("4 - Fame");
            cmbRewardType.SelectedIndex = 0;
            //
            cmbRewSelective.Items.Clear();
            cmbRewSelective.Items.Add("Disabled");
            cmbRewSelective.Items.Add("Fixed");
            cmbRewSelective.Items.Add("Chosen");
            cmbRewSelective.SelectedIndex = 0;
            //
            cmbItemsEnabled.Items.Clear();
            cmbItemsEnabled.Items.Add("True");
            cmbItemsEnabled.Items.Add("False");
            cmbItemsEnabled.Items.Add("Unknown");
            cmbItemsEnabled.SelectedIndex = 1;
            //
            cmbMobsEnabled.Items.Clear();
            cmbMobsEnabled.Items.Add("True");
            cmbMobsEnabled.Items.Add("False");
            cmbMobsEnabled.SelectedIndex = 1;
            //
            cmbItemDropActive.Items.Clear();
            cmbItemDropActive.Items.Add("False");
            cmbItemDropActive.Items.Add("True");
            cmbItemDropActive.SelectedIndex = 0;
            groupBox8.Enabled = false;
            //
            lstItemDrop.Columns.Add("ItemID", 50);
            lstItemDrop.Columns.Add("MobID", 50);
            lstItemDrop.Columns.Add("Amount", 50);
            lstItemDrop.Columns.Add("Drop Rate", 80);
        }

        public void Log(string text)
        {
            lblStatus.Text = text;
        }

        void LoadFiles()
        {
            DateTime start = DateTime.Now;
            QuestFile = new QuestFile(FileFolder + "QuestData.shn");
            QuestDialog = new SHNFile(FileFolder + "QuestDialog.shn", 1);
            ClassInfo = new SHNFile(FileFolder + "ClassName.shn", 1);
            ItemInfo = new SHNFile(FileFolder + "ItemInfo.shn", 1);
            MobInfo = new SHNFile(FileFolder + "MobInfo.shn", 1);
            TimeSpan loadtime = DateTime.Now - start;
            UpdateQuestNames();
            MobGrid.DataSource = MobInfo.table;
            ItemGrid.DataSource = ItemInfo.table;
            Text = string.Concat(new string[]
            {
                "iQuest v2.0 - loaded in " + Math.Round(loadtime.TotalMilliseconds, 0).ToString() + "ms : Found ", QuestFile.QuestCount.ToString(), " Quests"
            });
        }

        public void LoadDiagDic()
        {
            QuestDialogDic.Clear();
            foreach (DataRow row in QuestDialog.table.Rows)
            {
                if (!QuestDialogDic.ContainsKey((ushort)row[0]))
                {
                    QuestDialogDic.Add((ushort)row[0], (string)row[1]);
                }
            }

        }

        void LoadMobInfo()
        {
            MobDic.Clear();
            foreach (DataRow row in MobInfo.table.Rows)
                if (!MobDic.ContainsKey((ushort)row[0])) MobDic.Add((ushort)row[0], (string)row[2]);
        }

        void LoadItemDic()
        {
            ItemDic.Clear();
            foreach (DataRow row in ItemInfo.table.Rows)
            {
                if (!ItemDic.ContainsKey((ushort)row[0]))
                {
                    ItemDic.Add((ushort)row[0], (string)row[2]);
                }
            }
        }

        private void LoadClassDic()
        {
            ClassDic.Clear();
            foreach (DataRow dataRow in ClassInfo.table.Rows)
            {
                if (!ClassDic.ContainsKey(Convert.ToUInt16(dataRow[0])))
                {
                    ClassDic.Add(Convert.ToUInt16(dataRow[0]), (string)dataRow[3]);
                }
            }
        }

        void UpdateQuestNames()
        {
            LoadDiagDic();
            LoadItemDic();
            LoadMobInfo();
            LoadClassDic();
            ShowQuestList("");
        }

        public void ShowQuestList(string filter)
        {
            lstQuests.Items.Clear();
            if (filter == "")
            {
                using (List<Quest>.Enumerator enumerator = QuestFile.Quests.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Quest current = enumerator.Current;
                        string text = QuestDialogDic.ContainsKey(current.Title) ? QuestDialogDic[current.Title] : current.ID.ToString();
                        lstQuests.Items.Add(current.ID + " - " + text);
                    }
                    goto IL_B14;
                }
            }
            foreach (Quest current in QuestFile.Quests)
            {

                string text = QuestDialogDic.ContainsKey(current.Title) ? QuestDialogDic[current.Title] : current.ID.ToString();
                try
                {
                    if (filter.Contains("="))
                    {
                        string text2 = filter.Split(new char[]
                        {
                            '='
                        })[0];
                        string text3 = filter.Split(new char[]
                        {
                            '='
                        })[1];
                        if (text2.ToLower() == "id" && current.ID == ushort.Parse(text3))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "name" && text.ToLower().Contains(text3.ToLower()))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "npc" && MobDic.ContainsKey(current.StartingNPC) && MobDic[current.StartingNPC].ToLower().Contains(text3.ToLower()))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "level" && isBetween(current.MinLevel, current.MaxLevel, int.Parse(text3)))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "class" && ClassDic[current.ClassType].ToLower().Contains(text3.ToLower()))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "startlevel" && current.MinLevel == int.Parse(text3))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "endlevel" && current.MaxLevel == int.Parse(text3))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "enabled" && int.Parse(text3) < 2 && current.EnableQuest == int.Parse(text3))
                        {
                            lstQuests.Items.Add(current.ID + " - " + text);
                        }
                        if (text2.ToLower() == "grade")
                        {
                            string text4 = text3.ToLower();
                            if (text4 != null)
                            {
                                if (!(text4 == "normal"))
                                {
                                    if (!(text4 == "epic"))
                                    {
                                        if (!(text4 == "event"))
                                        {
                                            if (!(text4 == "class"))
                                            {
                                                if (!(text4 == "chaos"))
                                                {
                                                    goto IL_5EC;
                                                }
                                                if (current.QuestGrade == 4)
                                                {
                                                    lstQuests.Items.Add(current.ID + " - " + text);
                                                }
                                            }
                                            else if (current.QuestGrade == 2)
                                            {
                                                lstQuests.Items.Add(current.ID + " - " + text);
                                            }
                                        }
                                        else if (current.QuestGrade == 3)
                                        {
                                            lstQuests.Items.Add(current.ID + " - " + text);
                                        }
                                    }
                                    else if (current.QuestGrade == 1 || current.QuestGrade == 6 || current.QuestGrade == 7)
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                                else if (current.QuestGrade == 0 || current.QuestGrade == 5 || current.QuestGrade == 8)
                                {
                                    lstQuests.Items.Add(current.ID + " - " + text);
                                }
                            }
                            IL_5EC:
                            searchError();
                            return;
                        }
                        try
                        {
                            if (text2.ToLower() == "reward")
                            {
                                foreach (QuestReward current2 in current.Rewards)
                                {
                                    if (ItemDic.ContainsKey(current2.Value) && ItemDic[current2.Value].ToLower().Contains(text3.ToLower()) && !lstQuests.Items.Contains(current.ID + " - " + text))
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                            }
                            if (text2.ToLower() == "rewardid")
                            {
                                foreach (QuestReward current2 in current.Rewards)
                                {
                                    if (current2.Value == ushort.Parse(text3) && !lstQuests.Items.Contains(current.ID + " - " + text))
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                            }
                            if (text2.ToLower() == "item")
                            {
                                foreach (Item current3 in current.Items)
                                {
                                    if (ItemDic.ContainsKey(current3.ID) && ItemDic[current3.ID].ToLower().Contains(text3.ToLower()) && !lstQuests.Items.Contains(current.ID + " - " + text))
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                            }
                            if (text2.ToLower() == "itemid")
                            {
                                foreach (Item current3 in current.Items)
                                {
                                    if (current3.ID == ushort.Parse(text3) && !lstQuests.Items.Contains(current.ID + " - " + text))
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                            }
                            if (text2.ToLower() == "mob")
                            {
                                foreach (Mob current4 in current.Mobs)
                                {
                                    if (MobDic.ContainsKey(current4.ID) && MobDic[current4.ID].ToLower().Contains(text3.ToLower()) && !lstQuests.Items.Contains(current.ID + " - " + text))
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                            }
                            if (text2.ToLower() == "mobid")
                            {
                                foreach (Mob current4 in current.Mobs)
                                {
                                    if (current4.ID == ushort.Parse(text3) && !lstQuests.Items.Contains(current.ID + " - " + text))
                                    {
                                        lstQuests.Items.Add(current.ID + " - " + text);
                                    }
                                }
                            }
                            continue;
                        }
                        catch
                        {
                            searchError();
                            return;
                        }
                    }
                    if (text.ToLower().Contains(filter.ToLower()))
                    {
                        lstQuests.Items.Add(current.ID + " - " + text);
                    }
                }
                catch
                {
                    searchError();
                    return;
                }
            }
            IL_B14:
            lblQuestCount.Text = "Quests: " + lstQuests.Items.Count.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowQuestList(txtSearchQuest.Text);
        }

        private void lstQuests_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ViewingQuest = QuestFile.CO_get_Item(ushort.Parse(lstQuests.Text.Split(' ')[0]));
                if (ViewingQuest != null)
                {
                    RefreshQuest();
                }
            }
            catch
            { }
        }

        private void searchError()
        {
            MessageBox.Show("Malformed Search.\n\n<exp>=<value>\n\nExp = Name, NPC, Level, StartLevel, EndLevel, Enabled, Grade, ID, Class, Item, ItemID, Reward, RewardID, Mob, MobID\n\nSearch is NOT case sensitive");
        }

        public void RefreshQuest()
        {
            if (ViewingQuest != null)
            {
                txtRewUnk.Text = string.Empty;
                grpDefault.Enabled = true;
                grpQuestTree.Enabled = true;
                grpRewardEdit.Enabled = false;
                grpItemInfo.Enabled = false;
                grpDropInfo.Enabled = true;
                btnRemoveItem.Enabled = false;
                grpMobSettings.Enabled = false;
                txtStartScript.Text = ViewingQuest.StartScript;
                txtActionScript.Text = ViewingQuest.ActionScript;
                txtFinishScript.Text = ViewingQuest.FinishScript;
                txtQEnabled.Text = ViewingQuest.EnableQuest.ToString();
                lblQEnabledDesc.Text = checkBool(ViewingQuest.EnableQuest);
                lblQuestTitle.Text = ((QuestDialogDic.ContainsKey(ViewingQuest.Title)) ? QuestDialogDic[ViewingQuest.Title] : ViewingQuest.ID.ToString());
                txtQuestTitle.Text = ViewingQuest.Title.ToString();
                txtStartingNPC.Text = ViewingQuest.StartingNPC.ToString();
                txtDesc.Text = ViewingQuest.Description.ToString();
                if (QuestDialogDic.ContainsKey(ViewingQuest.Description))
                {
                    txtDescription.Text = QuestDialogDic[ViewingQuest.Description];
                    txtDescription.Tag = ViewingQuest.Description;
                }
                else
                {
                    txtDescription.Text = "-";
                    txtDescription.Tag = 0;
                }
                txtQGrade.Text = ViewingQuest.QuestGrade.ToString();
                lblQGrade.Text = getQGrade(ViewingQuest.QuestGrade);
                txtQMulti.Text = ViewingQuest.MultiQuest.ToString();
                lblQMultiDesc.Text = checkBool(ViewingQuest.MultiQuest);
                txtQNeedPred.Text = ViewingQuest.NeedPred.ToString();
                lblQNeedPred.Text = checkBool(ViewingQuest.NeedPred);
                txtPreQuest.Text = ViewingQuest.Predesessor.ToString();
                try
                {
                    if (txtQNeedPred.Text == "1")
                    {
                        for (int i = 0; i < QuestFile.Quests.Count; i++)
                        {
                            if (QuestFile.Quests[i].ID == ViewingQuest.Predesessor)
                            {
                                lblPreQuest.Text = QuestDialogDic[QuestFile.Quests[i].Title];
                            }
                        }
                    }
                    else
                    {
                        lblPreQuest.Text = "-";
                    }
                }
                catch
                {
                    lblPreQuest.Text = "-";
                }
                txtQNeedLvl.Text = ViewingQuest.NeedLevel.ToString();
                lblQNeedLvl.Text = checkBool(ViewingQuest.NeedLevel);
                nmrMinLevel.Value = ViewingQuest.MinLevel;
                nmrMaxLevel.Value = ViewingQuest.MaxLevel;
                txtQNeedNPC.Text = ViewingQuest.NeedNPC.ToString();
                lblQNeedNPC.Text = checkBool(ViewingQuest.NeedNPC);
                txtStartingNPC.Text = ViewingQuest.StartingNPC.ToString();
                if (txtQNeedNPC.Text == "1" && MobDic.ContainsKey(ViewingQuest.StartingNPC))
                {
                    lblStartNPC.Text = MobDic[ViewingQuest.StartingNPC];
                }
                else
                {
                    lblStartNPC.Text = "-";
                }
                txtQNeedItem.Text = ViewingQuest.NeedItem.ToString();
                lblQNeedItem.Text = checkBool(ViewingQuest.NeedItem);
                txtQItemID.Text = ViewingQuest.ItemID.ToString();
                if (txtQNeedItem.Text == "1" && ItemDic.ContainsKey(ViewingQuest.ItemID))
                {
                    lblQItemID.Text = ItemDic[ViewingQuest.ItemID];
                }
                else
                {
                    lblQItemID.Text = "-";
                }
                txtQItemVanish.Text = ViewingQuest.ItemVanish.ToString();
                lblQItemVanish.Text = checkBool(ViewingQuest.ItemVanish);
                txtQNeedClass.Text = ViewingQuest.NeedClass.ToString();
                lblQNeedClass.Text = checkBool(ViewingQuest.NeedClass);
                lblQNeedClass.Text = ViewingQuest.ClassType.ToString();
                if (txtQNeedClass.Text == "1" && ClassDic.ContainsKey(ViewingQuest.ClassType))
                {
                    lblQClassID.Text = ClassDic[ViewingQuest.ClassType];
                }
                else
                {
                    lblQClassID.Text = "-";
                }
                txtQInstAcc.Text = ViewingQuest.InstAcc.ToString();
                lblQInstAcc.Text = checkBool(ViewingQuest.InstAcc);
                txtQInstHand.Text = ViewingQuest.InstHand.ToString();
                lblQInstHandin.Text = checkBool(ViewingQuest.InstHand);
                LoadMobList(ViewingQuest);
                LoadItemList(ViewingQuest);
                LoadRewardList(ViewingQuest);
                txtRewUnk.Text = ViewingQuest.GetUnkString();
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 0).ToString() + " (static short)\r\n\r\n";
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 2).ToString() + " (rel short)\r\n";
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 4).ToString() + " (static short)\r\n\r\n";
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 6).ToString() + " (rel short)\r\n";
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 8).ToString() + " (static short)\r\n\r\n";
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 10).ToString() + " (rel short)\r\n";
                txtRewUnk.Text += BitConverter.ToUInt16(ViewingQuest.RewardData, 12).ToString() + " (static short)\r\n\r\n";
            }
        }

        public void LoadMobList(Quest quest)
        {
            lstMobs.Items.Clear();
            lstMobs.Tag = quest.ID;
            foreach (Mob mob in quest.Mobs)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = (MobDic.ContainsKey(mob.ID)) ? MobDic[mob.ID] : mob.ID.ToString(),
                    Tag = mob.ID
                };
                item.SubItems.Add(mob.HasToBeKilled.ToString());
                item.SubItems.Add(mob.Amount.ToString());
                item.SubItems.Add(mob.isActive.ToString());
                lstMobs.Items.Add(item);
            }
        }

        public void LoadRewardList(Quest quest)
        {
            lstRewards.Items.Clear();
            lstRewards.Tag = quest.ID;
            short ChosenCount = 0;
            foreach (QuestReward rew in quest.Rewards)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = rew.Type.ToString()
                };
                if (rew.Type == QuestReward.RewardType.Item)
                {
                    item.SubItems.Add((ItemDic.ContainsKey(rew.Value)) ? ItemDic[rew.Value] : rew.Value.ToString());
                    item.SubItems.Add(rew.ItemCount.ToString());
                }
                else
                {
                    item.SubItems.Add("/");
                    item.SubItems.Add(rew.Amount.ToString());
                }
                switch (rew.isGiven)
                {
                    case 0:
                        item.SubItems.Add("Disabled");
                        break;
                    case 1:
                        item.SubItems.Add("Fixed");
                        break;
                    case 2:
                        item.SubItems.Add("Chosen");
                        break;
                }
                if (rew.isGiven == 2)
                {
                    ChosenCount++;
                }
                item.Tag = rew;
                lstRewards.Items.Add(item);
            }
        }

        private string checkBool(byte x)
        {
            string result;
            if (x == 1)
            {
                result = "True";
            }
            else
            {
                result = "False";
            }
            return result;
        }

        public void LoadItemList(Quest quest)
        {
            lstItems.Items.Clear();
            lstItems.Tag = quest.ID;
            foreach (Item witem in quest.Items)
            {
                ListViewItem item = new ListViewItem
                {
                    Text = (ItemDic.ContainsKey(witem.ID)) ? ItemDic[witem.ID] : witem.ID.ToString(),
                    Tag = witem.ID
                };
                item.SubItems.Add(witem.Amount.ToString());
                item.SubItems.Add(witem.Type.ToString());
                byte isActive = witem.isActive;
                switch (isActive)
                {
                    case 0:
                        item.SubItems.Add("False");
                        break;

                    case 1:
                        item.SubItems.Add("True");
                        break;

                    default:
                        if (isActive == 55)
                        {
                            item.SubItems.Add("Unknown");
                        }
                        break;
                }
                lstItems.Items.Add(item);
            }
        }

        private void lstQuests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtStartScript_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveStartScript_Click(object sender, EventArgs e)
        {
            ViewingQuest.StartScript = txtStartScript.Text;
            Log("Changes saved.");
        }

        private void btnSaveAction_Click(object sender, EventArgs e)
        {
            ViewingQuest.ActionScript = txtActionScript.Text;
            Log("Changes saved.");
        }

        private void btnSaveFinishScript_Click(object sender, EventArgs e)
        {
            ViewingQuest.FinishScript = txtFinishScript.Text;
            Log("Changes saved.");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lstMobs_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstMobs.SelectedItems.Count < 0) return;
            Mob mob = GetMobByID((ushort)lstMobs.SelectedItems[0].Tag);
            lblMobName.Text = lstMobs.SelectedItems[0].Text;
            grpMobSettings.Enabled = true;
            if (mob == null) return;
            txtMobID.Text = mob.ID.ToString();
            cmbIsNPC.SelectedIndex = Convert.ToByte(mob.HasToBeKilled);
            nmrMobAmount.Value = mob.Amount;
            btnDeleteMob.Tag = mob;
            int rowID = MobInfo.GetRowByIndex(0, mob.ID.ToString());
            if (rowID > 0) MobGrid.FirstDisplayedCell = MobGrid[0, rowID];
        }

        Mob GetMobByID(ushort ID)
        {
            foreach (Mob fock in ViewingQuest.Mobs)
            {
                if (fock.ID == ID)
                {
                    return fock;
                }
            }
            return null;
        }

        Item GetItemByID(ushort ID)
        {
            foreach (Item fock in ViewingQuest.Items)
            {
                if (fock.ID == ID)
                {
                    return fock;
                }
            }
            return null;
        }

        private void lstMobs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog diag = new SaveFileDialog();
            diag.Title = "Save Quest File";
            diag.Filter = "Shn File(*.shn)|*.shn";
            if (!(diag.ShowDialog() == DialogResult.OK)) return;
            try
            {
                QuestFile.SaveFile(diag.FileName);
                Log("File saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveMobFile_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MobGrid.Columns.Count; ++i)
            {
                MobInfo.displayToReal.Add(MobGrid.Columns[i].DisplayIndex, i);
            }
            bool suc = MobInfo.Save(FileFolder + "MobInfo.shn");
            MobInfo.displayToReal.Clear();
            if (suc)
                Log("Mob file saved!");
            else
                Log("Mob file could not be saved!");
        }

        private void btnDeleteMob_Click_2(object sender, EventArgs e)
        {
            if (btnDeleteMob.Tag == null) return;
            ViewingQuest.Mobs.Remove((Mob)btnDeleteMob.Tag);
            LoadMobList(ViewingQuest);
            grpMobSettings.Enabled = false;
        }

        private void btnAddMob_Click_1(object sender, EventArgs e)
        {
            if (ViewingQuest == null) return;
            frmAddMob addder = new frmAddMob(this);
            addder.ShowDialog();
        }

        private void btnSaveItemGrid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ItemGrid.Columns.Count; ++i)
            {
                ItemInfo.displayToReal.Add(ItemGrid.Columns[i].DisplayIndex, i);
            }
            bool suc = ItemInfo.Save(FileFolder + "ItemInfo.shn");
            ItemInfo.displayToReal.Clear();
            if (suc)
                Log("Item file saved!");
            else
                Log("Item file could not be saved!");
        }

        private void lstItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstItems.SelectedItems.Count < 0) return;
            Item item  = GetItemByID(((ushort)lstItems.SelectedItems[0].Tag));
            grpItemInfo.Enabled = true;
            if (item == null) return;
            txtItemID.Text = item.ID.ToString();
            nmrItemAmount.Value = item.Amount;
            btnDeleteItem.Tag = item;
            txtItemType.Text = item.Type.ToString();
            int rowID = ItemInfo.GetRowByIndex(0, item.ID.ToString());
            if (rowID > 0) ItemGrid.FirstDisplayedCell = ItemGrid[0, rowID];
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (btnDeleteItem.Tag == null) return;
            ViewingQuest.Items.Remove((Item)btnDeleteItem.Tag);
            LoadItemList(ViewingQuest);
            grpItemInfo.Enabled = false;
        }

        private void BtnSaveMobSettings_Click(object sender, EventArgs e)
        {
            if (btnDeleteMob.Tag == null) return;
            Mob mob = (Mob)btnDeleteMob.Tag;
            try
            {
                mob.Amount = (byte)nmrMobAmount.Value;
                mob.HasToBeKilled = Convert.ToBoolean(cmbIsNPC.SelectedIndex);
                mob.ID = Convert.ToUInt16(txtMobID.Text);
                LoadMobList(ViewingQuest);
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnDeleteItem.Tag == null) return;
            Item item = (Item)btnDeleteItem.Tag;
            try
            {
                item.ID = Convert.ToUInt16(txtItemID.Text);
                item.Type = Convert.ToByte(txtItemType.Text);
                item.Amount = (ushort)nmrItemAmount.Value;
                LoadItemList(ViewingQuest);
            }
            catch
            {
                MessageBox.Show("Please enter valid numbers!");
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (ViewingQuest == null) return;
            frmAddItem adder = new frmAddItem(this);
            adder.ShowDialog();
        }

        private void btnSaveDefault_Click(object sender, EventArgs e)
        {
            try
            {
                ViewingQuest.ID = Convert.ToUInt16(txtQID.Text);
                ViewingQuest.Title = Convert.ToUInt16(txtQuestTitle.Text);
                ViewingQuest.Description = Convert.ToUInt16(txtDescription.Text);
                ViewingQuest.QuestGrade = Convert.ToByte(txtQGrade.Text);
                ViewingQuest.MultiQuest = Convert.ToByte(txtQMulti.Text);
                ViewingQuest.EnableQuest = Convert.ToByte(txtQEnabled.Text);
                ViewingQuest.InstAcc = Convert.ToByte(txtQInstAcc.Text);
                ViewingQuest.NeedLevel = Convert.ToByte(txtQNeedLvl.Text);
                ViewingQuest.MinLevel = (byte)nmrMinLevel.Value;
                ViewingQuest.MaxLevel = (byte)nmrMaxLevel.Value;
                ViewingQuest.NeedNPC = Convert.ToByte(txtQNeedNPC.Text);
                ViewingQuest.StartingNPC = Convert.ToUInt16(txtStartingNPC.Text);
                ViewingQuest.NeedItem = Convert.ToByte(txtQNeedItem.Text);
                ViewingQuest.ItemID = Convert.ToUInt16(txtQItemID.Text);
                ViewingQuest.ItemVanish = Convert.ToByte(txtQItemVanish.Text);
                ViewingQuest.NeedPred = Convert.ToByte(txtQNeedPred.Text);
                ViewingQuest.Predesessor = Convert.ToUInt16(txtPreQuest.Text);
                ViewingQuest.NeedClass = Convert.ToByte(txtQNeedClass.Text);
                ViewingQuest.ClassType = Convert.ToByte(txtQClassID.Text);
                ViewingQuest.InstHand = Convert.ToByte(txtQInstHand.Text);
                RefreshQuest();
                QuestAltered = true;
            }
            catch
            {
                MessageBox.Show("Please enter a valid value!");
            }
        }

        private void lstRewards_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if ((lstRewards.SelectedItems.Count >= 0) && (lstRewards.SelectedItems[0].Tag != null))
                {
                    grpRewardEdit.Enabled = true;
                    QuestReward tag = (QuestReward)lstRewards.SelectedItems[0].Tag;
                    switch (tag.Type)
                    {
                        case QuestReward.RewardType.Exp:
                            cmbRewardType.SelectedIndex = 0;
                            break;

                        case QuestReward.RewardType.Money:
                            cmbRewardType.SelectedIndex = 1;
                            break;

                        case QuestReward.RewardType.Item:
                            cmbRewardType.SelectedIndex = 2;
                            break;

                        case QuestReward.RewardType.Fame:
                            cmbRewardType.SelectedIndex = 3;
                            break;
                    }
                    cmbRewSelective.SelectedIndex = tag.isGiven;
                    txtRewID.Text = (tag.Type == QuestReward.RewardType.Item) ? tag.Value.ToString() : "";
                    txtRewAmount.Text = (tag.Type == QuestReward.RewardType.Item) ? tag.ItemCount.ToString() : tag.Amount.ToString();
                    btnDeleteReward.Tag = tag;
                }
            }
            catch
            {
            }
        }

        private void lstRewards_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveRew_Click(object sender, EventArgs e)
        {
            if (btnDeleteReward.Tag != null)
            {
                QuestReward rew = (QuestReward)btnDeleteReward.Tag;
                try
                {
                    QuestReward.RewardType rewardType = (QuestReward.RewardType)Convert.ToByte(cmbRewardType.Text.Substring(0, 1));
                    rew.Type = rewardType;
                    if (rewardType == QuestReward.RewardType.Item)
                    {
                        rew.ItemCount = Convert.ToUInt16(txtRewAmount.Text);
                        rew.Value = Convert.ToUInt16(txtRewID.Text);
                        rew.isGiven = (byte)(cmbRewSelective.SelectedIndex);
                    }
                    else
                    {
                        rew.Amount = Convert.ToUInt64(txtRewAmount.Text);
                        rew.isGiven = (byte)(cmbRewSelective.SelectedIndex);
                    }
                    LoadRewardList(ViewingQuest);
                    QuestAltered = true;
                }
                catch
                {
                    MessageBox.Show("Incorrect values!");
                }
            }
        }

        private void btnAddReward_Click(object sender, EventArgs e)
        {
            if (ViewingQuest == null)
            {
                frmAddReward adder = new frmAddReward(this);
                adder.ShowDialog();
            }
        }

        private void btnDeleteReward_Click(object sender, EventArgs e)
        {
            if (btnDeleteReward.Tag != null || ViewingQuest != null)
            {
                grpRewardEdit.Enabled = false;
                ViewingQuest.Rewards.Remove((QuestReward)btnDeleteReward.Tag);
                LoadRewardList(ViewingQuest);
                Log("Reward removed.");
            }
        }

        private void btnModUnk_Click(object sender, EventArgs e)
        {
            try
            {
                ViewingQuest.SetUnkString(txtRewUnk.Text);
                Log("UnkBytes applied.");
            }
            catch
            {
                MessageBox.Show("Ooops.. something went wrong!");
            }
            
        }

         public void UpdateQuestIDs()
       {
           ushort count = 1;
           foreach (Quest q in QuestFile.Quests)
           {
               q.ID = count;
               count++;
           }
       }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((ushort)txtDescription.Tag != 0)
            {
                int rowID = QuestDialog.GetRowByIndex(0, txtDescription.Tag.ToString());
                if (rowID > -1)
                {
                    QuestDialog.table.Rows[rowID][1] = txtDescription.Text;
                }
                LoadDiagDic();
                ShowQuestList("");
            }
        }

        private void btnDialogEditor_Click(object sender, EventArgs e)
        {
            if (Editor == null)
            {
                Editor = new frmResEditor(this);
            }
            Editor.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveRes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < QuestDialog.table.Columns.Count; ++i)
            {
               QuestDialog.displayToReal.Add(i, i);
            }
            bool suc = MobInfo.Save(FileFolder + "QuestDialog.shn");
            QuestDialog.displayToReal.Clear();
            if (suc)
            {
                Log("Dialog file saved!");
            }
            else
            {
                Log("Dialog file could not be saved!");
            }
        }

        private void lblQuestTitle_Click(object sender, EventArgs e)
        {
            if (Editor == null)
            {
                Editor = new frmResEditor(this);
            }
            Editor.Show();
            Editor.ShowID(Convert.ToUInt16(txtQuestTitle.Text));
        }

        public ushort GetFreeQKey()
        {
            for (ushort i = 100; i < ushort.MaxValue; ++i)
            {
                if(!QuestDialogDic.ContainsKey(i)) return i;
            }
            return 0;
        }

        private void btnDeleteQuest_Click(object sender, EventArgs e)
        {
         
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void btnduplicate_Click(object sender, EventArgs e)
        {
          

        }

        private void btnModDescID_Click(object sender, EventArgs e)
        {
            try
            {
                ViewingQuest.Description = Convert.ToUInt16(txtDesc.Text);
                RefreshQuest();
                Log("Desc. ID changed!");
            }
            catch
            {
                MessageBox.Show("Invalid ID");
            }
        }

        private void btnResDesc_Click(object sender, EventArgs e)
        {
            if (Editor == null)
            {
                Editor = new frmResEditor(this);
            }
            Editor.Show();
            Editor.ShowID(ViewingQuest.Description);
        }

        private void btnDeleteQuest_Click_1(object sender, EventArgs e)
        {
            QuestFile.Quests.Remove(ViewingQuest);
            ViewingQuest = null;
            UpdateQuestIDs();
            ShowQuestList(txtSearchQuest.Text);
            RefreshQuest();
            Log("Quest deleted!");
        }

        private void btnduplicate_Click_1(object sender, EventArgs e)
        {
            frmNewQuest newQ = new frmNewQuest(this);
            newQ.ShowDialog();
        }

        private string Serialize(object objectToSerialize)
        {
            string serialString = null;
            using (MemoryStream ms1 = new MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms1, objectToSerialize);
                byte[] arrayByte = ms1.ToArray();
                serialString = Convert.ToBase64String(arrayByte);
            }
            return serialString;
        }

        private object DeSerialize(string serializationString)
        {
            object deserialObject = null;
            byte[] arrayByte = Convert.FromBase64String(serializationString);
            using (MemoryStream ms1 = new MemoryStream(arrayByte))
            {
                BinaryFormatter b = new BinaryFormatter();
                deserialObject = b.Deserialize(ms1);
            }
            return deserialObject;
        }

        public static bool isBetween(int a, int b, int c)
        {
            return ((b > a) ? ((c > a) && (c < b)) : ((c > b) && (c < a)));
        }

        private string getQGrade(byte x)
        {
            string result;
            switch (x)
            {
                case 0:
                    result = "Normal Quest";
                    break;
                case 1:
                    result = "Elite Quest";
                    break;
                case 2:
                    result = "Class Change Quest";
                    break;
                case 3:
                    result = "Event Quest";
                    break;
                case 4:
                    result = "Chaos Quest";
                    break;
                case 5:
                    result = "Normal Quest[Team]";
                    break;
                case 6:
                    result = "Elite Quest[Story Line]";
                    break;
                case 7:
                    result = "Elite Quest[Story Line][Team]";
                    break;
                case 8:
                    result = "Normal Quest[Expedition]";
                    break;
                case 9:
                    result = "Unknown";
                    break;
                case 10:
                    result = "Daily Quest";
                    break;
                default:
                    result = "-";
                    break;
            }
            return result;
        }

        private void tbxQuestInfoQuestID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}