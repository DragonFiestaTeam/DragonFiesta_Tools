using System.Windows.Forms;

namespace QuestDataForm
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenQuestData = new System.Windows.Forms.OpenFileDialog();
            this.lbQuestList = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lDBConnect = new System.Windows.Forms.Label();
            this.lFullCount = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.grpbQuestList = new System.Windows.Forms.GroupBox();
            this.grpbTabControl = new System.Windows.Forms.GroupBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.TabControlFinishLevel = new System.Windows.Forms.TextBox();
            this.TabControlRequireLevel = new System.Windows.Forms.TextBox();
            this.TabControlInstHandin = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.TabControlClassType = new System.Windows.Forms.TextBox();
            this.TabControlNeedClass = new System.Windows.Forms.TextBox();
            this.TabControlPredesessor = new System.Windows.Forms.TextBox();
            this.TabControlNeedPred = new System.Windows.Forms.TextBox();
            this.TabControlItemVanish = new System.Windows.Forms.TextBox();
            this.TabControlItemID = new System.Windows.Forms.TextBox();
            this.TabControlNeedItem = new System.Windows.Forms.TextBox();
            this.TabControlStartingNPC = new System.Windows.Forms.TextBox();
            this.TabControlNeedNPC = new System.Windows.Forms.TextBox();
            this.TabControlMaxLevel = new System.Windows.Forms.TextBox();
            this.TabControlMinLevel = new System.Windows.Forms.TextBox();
            this.TabControlNeedLevel = new System.Windows.Forms.TextBox();
            this.TabControlInstAccept = new System.Windows.Forms.TextBox();
            this.TabControlEnabled = new System.Windows.Forms.TextBox();
            this.TabControlDaily = new System.Windows.Forms.TextBox();
            this.TabControlRepeat = new System.Windows.Forms.TextBox();
            this.TabControlGrade = new System.Windows.Forms.TextBox();
            this.TabControlDescriptionID = new System.Windows.Forms.TextBox();
            this.TabControlTitleID = new System.Windows.Forms.TextBox();
            this.TabControlQuestID = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.QuestCount = new System.Windows.Forms.Label();
            this.QuestGrade = new System.Windows.Forms.Label();
            this.DescriptionID = new System.Windows.Forms.Label();
            this.TitleID = new System.Windows.Forms.Label();
            this.QuestID = new System.Windows.Forms.Label();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.TabControlAcceptScript = new System.Windows.Forms.TextBox();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.TabControlActiveScript = new System.Windows.Forms.TextBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.TabControlFinishScript = new System.Windows.Forms.TextBox();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.TabControlMobInfoAmount = new System.Windows.Forms.TextBox();
            this.TabControlMobInfoIsKillable = new System.Windows.Forms.TextBox();
            this.TabControlMobInfoMobID = new System.Windows.Forms.TextBox();
            this.TabControlMobInfoIsActive = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.MobInfoView = new System.Windows.Forms.ListBox();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.TabControlItemInfoViewAmount = new System.Windows.Forms.TextBox();
            this.TabControlItemInfoViewItemID = new System.Windows.Forms.TextBox();
            this.TabControlItemInfoViewItemType = new System.Windows.Forms.TextBox();
            this.TabControlItemInfoViewIsActive = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.ItemInfoView = new System.Windows.Forms.ListBox();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.TabControlItemDropUnk = new System.Windows.Forms.TextBox();
            this.TabControlItemDropMaxDrop = new System.Windows.Forms.TextBox();
            this.TabControlItemDropMinDrop = new System.Windows.Forms.TextBox();
            this.TabControlItemDropRate = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.TabControlItemDropItemID = new System.Windows.Forms.TextBox();
            this.TabControlItemDropIsEnabled = new System.Windows.Forms.TextBox();
            this.TabControlItemDropMobID = new System.Windows.Forms.TextBox();
            this.TabControlItemDropAmount = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.ItemDropView = new System.Windows.Forms.ListBox();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.RewardView = new System.Windows.Forms.ListBox();
            this.DataBaseInfo = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.TabControlRewardMethod = new System.Windows.Forms.TextBox();
            this.TabControlRewardType = new System.Windows.Forms.TextBox();
            this.TabControlRewardItemID = new System.Windows.Forms.TextBox();
            this.TabControlRewardAmount = new System.Windows.Forms.TextBox();
            this.grpbQuestList.SuspendLayout();
            this.grpbTabControl.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.DataBaseInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbQuestList
            // 
            this.lbQuestList.FormattingEnabled = true;
            this.lbQuestList.Location = new System.Drawing.Point(6, 19);
            this.lbQuestList.Name = "lbQuestList";
            this.lbQuestList.Size = new System.Drawing.Size(208, 576);
            this.lbQuestList.TabIndex = 0;
            this.lbQuestList.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "SaveQuest";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 23);
            this.button2.TabIndex = 27;
            this.button2.Text = "SaveAllQuest";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "DatabaseConnect :";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(486, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(43, 13);
            this.label18.TabIndex = 29;
            this.label18.Text = "Quests:";
            // 
            // lDBConnect
            // 
            this.lDBConnect.AutoSize = true;
            this.lDBConnect.Location = new System.Drawing.Point(105, 20);
            this.lDBConnect.Name = "lDBConnect";
            this.lDBConnect.Size = new System.Drawing.Size(0, 13);
            this.lDBConnect.TabIndex = 30;
            // 
            // lFullCount
            // 
            this.lFullCount.AutoSize = true;
            this.lFullCount.Location = new System.Drawing.Point(554, 60);
            this.lFullCount.Name = "lFullCount";
            this.lFullCount.Size = new System.Drawing.Size(0, 13);
            this.lFullCount.TabIndex = 31;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 32;
            this.button3.Text = "EditDatabaseSettings";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // grpbQuestList
            // 
            this.grpbQuestList.Controls.Add(this.lbQuestList);
            this.grpbQuestList.Location = new System.Drawing.Point(12, 9);
            this.grpbQuestList.Name = "grpbQuestList";
            this.grpbQuestList.Size = new System.Drawing.Size(220, 601);
            this.grpbQuestList.TabIndex = 35;
            this.grpbQuestList.TabStop = false;
            this.grpbQuestList.Text = "Quest List:";
            // 
            // grpbTabControl
            // 
            this.grpbTabControl.Controls.Add(this.TabControl);
            this.grpbTabControl.Controls.Add(this.lFullCount);
            this.grpbTabControl.Location = new System.Drawing.Point(238, 9);
            this.grpbTabControl.Name = "grpbTabControl";
            this.grpbTabControl.Size = new System.Drawing.Size(600, 601);
            this.grpbTabControl.TabIndex = 36;
            this.grpbTabControl.TabStop = false;
            this.grpbTabControl.Text = "Tab-Control";
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage8);
            this.TabControl.Controls.Add(this.tabPage9);
            this.TabControl.Controls.Add(this.tabPage10);
            this.TabControl.Controls.Add(this.tabPage11);
            this.TabControl.Controls.Add(this.tabPage12);
            this.TabControl.Controls.Add(this.tabPage13);
            this.TabControl.Controls.Add(this.tabPage14);
            this.TabControl.Controls.Add(this.tabPage15);
            this.TabControl.Location = new System.Drawing.Point(1, 20);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(593, 575);
            this.TabControl.TabIndex = 0;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.TabControlFinishLevel);
            this.tabPage8.Controls.Add(this.TabControlRequireLevel);
            this.tabPage8.Controls.Add(this.TabControlInstHandin);
            this.tabPage8.Controls.Add(this.label19);
            this.tabPage8.Controls.Add(this.label20);
            this.tabPage8.Controls.Add(this.label21);
            this.tabPage8.Controls.Add(this.TabControlClassType);
            this.tabPage8.Controls.Add(this.TabControlNeedClass);
            this.tabPage8.Controls.Add(this.TabControlPredesessor);
            this.tabPage8.Controls.Add(this.TabControlNeedPred);
            this.tabPage8.Controls.Add(this.TabControlItemVanish);
            this.tabPage8.Controls.Add(this.TabControlItemID);
            this.tabPage8.Controls.Add(this.TabControlNeedItem);
            this.tabPage8.Controls.Add(this.TabControlStartingNPC);
            this.tabPage8.Controls.Add(this.TabControlNeedNPC);
            this.tabPage8.Controls.Add(this.TabControlMaxLevel);
            this.tabPage8.Controls.Add(this.TabControlMinLevel);
            this.tabPage8.Controls.Add(this.TabControlNeedLevel);
            this.tabPage8.Controls.Add(this.TabControlInstAccept);
            this.tabPage8.Controls.Add(this.TabControlEnabled);
            this.tabPage8.Controls.Add(this.TabControlDaily);
            this.tabPage8.Controls.Add(this.TabControlRepeat);
            this.tabPage8.Controls.Add(this.TabControlGrade);
            this.tabPage8.Controls.Add(this.TabControlDescriptionID);
            this.tabPage8.Controls.Add(this.TabControlTitleID);
            this.tabPage8.Controls.Add(this.TabControlQuestID);
            this.tabPage8.Controls.Add(this.label13);
            this.tabPage8.Controls.Add(this.label14);
            this.tabPage8.Controls.Add(this.label15);
            this.tabPage8.Controls.Add(this.label16);
            this.tabPage8.Controls.Add(this.label9);
            this.tabPage8.Controls.Add(this.label10);
            this.tabPage8.Controls.Add(this.label11);
            this.tabPage8.Controls.Add(this.label12);
            this.tabPage8.Controls.Add(this.label5);
            this.tabPage8.Controls.Add(this.label6);
            this.tabPage8.Controls.Add(this.label7);
            this.tabPage8.Controls.Add(this.label8);
            this.tabPage8.Controls.Add(this.label1);
            this.tabPage8.Controls.Add(this.label2);
            this.tabPage8.Controls.Add(this.label3);
            this.tabPage8.Controls.Add(this.label4);
            this.tabPage8.Controls.Add(this.QuestCount);
            this.tabPage8.Controls.Add(this.label18);
            this.tabPage8.Controls.Add(this.QuestGrade);
            this.tabPage8.Controls.Add(this.DescriptionID);
            this.tabPage8.Controls.Add(this.TitleID);
            this.tabPage8.Controls.Add(this.QuestID);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(585, 549);
            this.tabPage8.TabIndex = 0;
            this.tabPage8.Text = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // TabControlFinishLevel
            // 
            this.TabControlFinishLevel.Location = new System.Drawing.Point(84, 497);
            this.TabControlFinishLevel.Name = "TabControlFinishLevel";
            this.TabControlFinishLevel.Size = new System.Drawing.Size(50, 20);
            this.TabControlFinishLevel.TabIndex = 72;
            // 
            // TabControlRequireLevel
            // 
            this.TabControlRequireLevel.Location = new System.Drawing.Point(84, 475);
            this.TabControlRequireLevel.Name = "TabControlRequireLevel";
            this.TabControlRequireLevel.Size = new System.Drawing.Size(50, 20);
            this.TabControlRequireLevel.TabIndex = 71;
            // 
            // TabControlInstHandin
            // 
            this.TabControlInstHandin.Location = new System.Drawing.Point(84, 453);
            this.TabControlInstHandin.Name = "TabControlInstHandin";
            this.TabControlInstHandin.Size = new System.Drawing.Size(50, 20);
            this.TabControlInstHandin.TabIndex = 70;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 500);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 13);
            this.label19.TabIndex = 69;
            this.label19.Text = "FinishLevel:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 478);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 13);
            this.label20.TabIndex = 68;
            this.label20.Text = "RequireLevel:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 456);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 67;
            this.label21.Text = "InstHandin:";
            // 
            // TabControlClassType
            // 
            this.TabControlClassType.Location = new System.Drawing.Point(84, 430);
            this.TabControlClassType.Name = "TabControlClassType";
            this.TabControlClassType.Size = new System.Drawing.Size(50, 20);
            this.TabControlClassType.TabIndex = 66;
            // 
            // TabControlNeedClass
            // 
            this.TabControlNeedClass.Location = new System.Drawing.Point(84, 408);
            this.TabControlNeedClass.Name = "TabControlNeedClass";
            this.TabControlNeedClass.Size = new System.Drawing.Size(50, 20);
            this.TabControlNeedClass.TabIndex = 65;
            // 
            // TabControlPredesessor
            // 
            this.TabControlPredesessor.Location = new System.Drawing.Point(84, 386);
            this.TabControlPredesessor.Name = "TabControlPredesessor";
            this.TabControlPredesessor.Size = new System.Drawing.Size(50, 20);
            this.TabControlPredesessor.TabIndex = 64;
            // 
            // TabControlNeedPred
            // 
            this.TabControlNeedPred.Location = new System.Drawing.Point(84, 364);
            this.TabControlNeedPred.Name = "TabControlNeedPred";
            this.TabControlNeedPred.Size = new System.Drawing.Size(50, 20);
            this.TabControlNeedPred.TabIndex = 63;
            // 
            // TabControlItemVanish
            // 
            this.TabControlItemVanish.Location = new System.Drawing.Point(84, 342);
            this.TabControlItemVanish.Name = "TabControlItemVanish";
            this.TabControlItemVanish.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemVanish.TabIndex = 62;
            // 
            // TabControlItemID
            // 
            this.TabControlItemID.Location = new System.Drawing.Point(84, 320);
            this.TabControlItemID.Name = "TabControlItemID";
            this.TabControlItemID.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemID.TabIndex = 61;
            // 
            // TabControlNeedItem
            // 
            this.TabControlNeedItem.Location = new System.Drawing.Point(84, 298);
            this.TabControlNeedItem.Name = "TabControlNeedItem";
            this.TabControlNeedItem.Size = new System.Drawing.Size(50, 20);
            this.TabControlNeedItem.TabIndex = 60;
            // 
            // TabControlStartingNPC
            // 
            this.TabControlStartingNPC.Location = new System.Drawing.Point(84, 276);
            this.TabControlStartingNPC.Name = "TabControlStartingNPC";
            this.TabControlStartingNPC.Size = new System.Drawing.Size(50, 20);
            this.TabControlStartingNPC.TabIndex = 59;
            // 
            // TabControlNeedNPC
            // 
            this.TabControlNeedNPC.Location = new System.Drawing.Point(84, 254);
            this.TabControlNeedNPC.Name = "TabControlNeedNPC";
            this.TabControlNeedNPC.Size = new System.Drawing.Size(50, 20);
            this.TabControlNeedNPC.TabIndex = 58;
            // 
            // TabControlMaxLevel
            // 
            this.TabControlMaxLevel.Location = new System.Drawing.Point(84, 232);
            this.TabControlMaxLevel.Name = "TabControlMaxLevel";
            this.TabControlMaxLevel.Size = new System.Drawing.Size(50, 20);
            this.TabControlMaxLevel.TabIndex = 57;
            // 
            // TabControlMinLevel
            // 
            this.TabControlMinLevel.Location = new System.Drawing.Point(84, 210);
            this.TabControlMinLevel.Name = "TabControlMinLevel";
            this.TabControlMinLevel.Size = new System.Drawing.Size(50, 20);
            this.TabControlMinLevel.TabIndex = 56;
            // 
            // TabControlNeedLevel
            // 
            this.TabControlNeedLevel.Location = new System.Drawing.Point(84, 188);
            this.TabControlNeedLevel.Name = "TabControlNeedLevel";
            this.TabControlNeedLevel.Size = new System.Drawing.Size(50, 20);
            this.TabControlNeedLevel.TabIndex = 55;
            // 
            // TabControlInstAccept
            // 
            this.TabControlInstAccept.Location = new System.Drawing.Point(84, 166);
            this.TabControlInstAccept.Name = "TabControlInstAccept";
            this.TabControlInstAccept.Size = new System.Drawing.Size(50, 20);
            this.TabControlInstAccept.TabIndex = 54;
            // 
            // TabControlEnabled
            // 
            this.TabControlEnabled.Location = new System.Drawing.Point(84, 144);
            this.TabControlEnabled.Name = "TabControlEnabled";
            this.TabControlEnabled.Size = new System.Drawing.Size(50, 20);
            this.TabControlEnabled.TabIndex = 53;
            // 
            // TabControlDaily
            // 
            this.TabControlDaily.Location = new System.Drawing.Point(84, 122);
            this.TabControlDaily.Name = "TabControlDaily";
            this.TabControlDaily.Size = new System.Drawing.Size(50, 20);
            this.TabControlDaily.TabIndex = 52;
            // 
            // TabControlRepeat
            // 
            this.TabControlRepeat.Location = new System.Drawing.Point(84, 100);
            this.TabControlRepeat.Name = "TabControlRepeat";
            this.TabControlRepeat.Size = new System.Drawing.Size(50, 20);
            this.TabControlRepeat.TabIndex = 51;
            // 
            // TabControlGrade
            // 
            this.TabControlGrade.Location = new System.Drawing.Point(84, 78);
            this.TabControlGrade.Name = "TabControlGrade";
            this.TabControlGrade.Size = new System.Drawing.Size(50, 20);
            this.TabControlGrade.TabIndex = 50;
            // 
            // TabControlDescriptionID
            // 
            this.TabControlDescriptionID.Location = new System.Drawing.Point(84, 56);
            this.TabControlDescriptionID.Name = "TabControlDescriptionID";
            this.TabControlDescriptionID.Size = new System.Drawing.Size(50, 20);
            this.TabControlDescriptionID.TabIndex = 49;
            // 
            // TabControlTitleID
            // 
            this.TabControlTitleID.Location = new System.Drawing.Point(84, 34);
            this.TabControlTitleID.Name = "TabControlTitleID";
            this.TabControlTitleID.Size = new System.Drawing.Size(50, 20);
            this.TabControlTitleID.TabIndex = 48;
            // 
            // TabControlQuestID
            // 
            this.TabControlQuestID.Location = new System.Drawing.Point(84, 12);
            this.TabControlQuestID.Name = "TabControlQuestID";
            this.TabControlQuestID.Size = new System.Drawing.Size(50, 20);
            this.TabControlQuestID.TabIndex = 47;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 433);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 46;
            this.label13.Text = "ClassType:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 411);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 13);
            this.label14.TabIndex = 45;
            this.label14.Text = "NeedClass:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 389);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "Predesessor:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 367);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 43;
            this.label16.Text = "NeedPred:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 345);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "ItemVanish:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 323);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "ItemID:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "NeedItem:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 279);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 13);
            this.label12.TabIndex = 39;
            this.label12.Text = "StartingNPC:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "NeedNPC:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "MaxLevel:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "MinLevel:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "NeedLevel:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "InstAccept:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Enabled:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Daily:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Repeat:";
            // 
            // QuestCount
            // 
            this.QuestCount.AutoSize = true;
            this.QuestCount.Location = new System.Drawing.Point(535, 16);
            this.QuestCount.Name = "QuestCount";
            this.QuestCount.Size = new System.Drawing.Size(0, 13);
            this.QuestCount.TabIndex = 30;
            // 
            // QuestGrade
            // 
            this.QuestGrade.AutoSize = true;
            this.QuestGrade.Location = new System.Drawing.Point(6, 81);
            this.QuestGrade.Name = "QuestGrade";
            this.QuestGrade.Size = new System.Drawing.Size(39, 13);
            this.QuestGrade.TabIndex = 3;
            this.QuestGrade.Text = "Grade:";
            // 
            // DescriptionID
            // 
            this.DescriptionID.AutoSize = true;
            this.DescriptionID.Location = new System.Drawing.Point(6, 59);
            this.DescriptionID.Name = "DescriptionID";
            this.DescriptionID.Size = new System.Drawing.Size(74, 13);
            this.DescriptionID.TabIndex = 2;
            this.DescriptionID.Text = "DescriptionID:";
            // 
            // TitleID
            // 
            this.TitleID.AutoSize = true;
            this.TitleID.Location = new System.Drawing.Point(6, 37);
            this.TitleID.Name = "TitleID";
            this.TitleID.Size = new System.Drawing.Size(41, 13);
            this.TitleID.TabIndex = 1;
            this.TitleID.Text = "TitleID:";
            // 
            // QuestID
            // 
            this.QuestID.AutoSize = true;
            this.QuestID.Location = new System.Drawing.Point(6, 15);
            this.QuestID.Name = "QuestID";
            this.QuestID.Size = new System.Drawing.Size(49, 13);
            this.QuestID.TabIndex = 0;
            this.QuestID.Text = "QuestID:";
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.TabControlAcceptScript);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage9.Size = new System.Drawing.Size(585, 549);
            this.tabPage9.TabIndex = 1;
            this.tabPage9.Text = "tabPage9";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // TabControlAcceptScript
            // 
            this.TabControlAcceptScript.Location = new System.Drawing.Point(6, 6);
            this.TabControlAcceptScript.Multiline = true;
            this.TabControlAcceptScript.Name = "TabControlAcceptScript";
            this.TabControlAcceptScript.Size = new System.Drawing.Size(573, 537);
            this.TabControlAcceptScript.TabIndex = 0;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.TabControlActiveScript);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(585, 549);
            this.tabPage10.TabIndex = 2;
            this.tabPage10.Text = "tabPage10";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // TabControlActiveScript
            // 
            this.TabControlActiveScript.Location = new System.Drawing.Point(6, 6);
            this.TabControlActiveScript.Multiline = true;
            this.TabControlActiveScript.Name = "TabControlActiveScript";
            this.TabControlActiveScript.Size = new System.Drawing.Size(573, 537);
            this.TabControlActiveScript.TabIndex = 1;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.TabControlFinishScript);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage11.Size = new System.Drawing.Size(585, 549);
            this.tabPage11.TabIndex = 3;
            this.tabPage11.Text = "tabPage11";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // TabControlFinishScript
            // 
            this.TabControlFinishScript.Location = new System.Drawing.Point(6, 6);
            this.TabControlFinishScript.Multiline = true;
            this.TabControlFinishScript.Name = "TabControlFinishScript";
            this.TabControlFinishScript.Size = new System.Drawing.Size(573, 537);
            this.TabControlFinishScript.TabIndex = 1;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.TabControlMobInfoAmount);
            this.tabPage12.Controls.Add(this.TabControlMobInfoIsKillable);
            this.tabPage12.Controls.Add(this.TabControlMobInfoMobID);
            this.tabPage12.Controls.Add(this.TabControlMobInfoIsActive);
            this.tabPage12.Controls.Add(this.label25);
            this.tabPage12.Controls.Add(this.label24);
            this.tabPage12.Controls.Add(this.label23);
            this.tabPage12.Controls.Add(this.label22);
            this.tabPage12.Controls.Add(this.MobInfoView);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(585, 549);
            this.tabPage12.TabIndex = 4;
            this.tabPage12.Text = "tabPage12";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // TabControlMobInfoAmount
            // 
            this.TabControlMobInfoAmount.Location = new System.Drawing.Point(60, 162);
            this.TabControlMobInfoAmount.Name = "TabControlMobInfoAmount";
            this.TabControlMobInfoAmount.Size = new System.Drawing.Size(50, 20);
            this.TabControlMobInfoAmount.TabIndex = 10;
            // 
            // TabControlMobInfoIsKillable
            // 
            this.TabControlMobInfoIsKillable.Location = new System.Drawing.Point(60, 140);
            this.TabControlMobInfoIsKillable.Name = "TabControlMobInfoIsKillable";
            this.TabControlMobInfoIsKillable.Size = new System.Drawing.Size(50, 20);
            this.TabControlMobInfoIsKillable.TabIndex = 9;
            // 
            // TabControlMobInfoMobID
            // 
            this.TabControlMobInfoMobID.Location = new System.Drawing.Point(60, 118);
            this.TabControlMobInfoMobID.Name = "TabControlMobInfoMobID";
            this.TabControlMobInfoMobID.Size = new System.Drawing.Size(50, 20);
            this.TabControlMobInfoMobID.TabIndex = 8;
            // 
            // TabControlMobInfoIsActive
            // 
            this.TabControlMobInfoIsActive.Location = new System.Drawing.Point(60, 96);
            this.TabControlMobInfoIsActive.Name = "TabControlMobInfoIsActive";
            this.TabControlMobInfoIsActive.Size = new System.Drawing.Size(50, 20);
            this.TabControlMobInfoIsActive.TabIndex = 7;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 165);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(46, 13);
            this.label25.TabIndex = 6;
            this.label25.Text = "Amount:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 143);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "IsKillable:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 121);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(42, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "MobID:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 99);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "IsActive:";
            // 
            // MobInfoView
            // 
            this.MobInfoView.FormattingEnabled = true;
            this.MobInfoView.Location = new System.Drawing.Point(3, 240);
            this.MobInfoView.Name = "MobInfoView";
            this.MobInfoView.Size = new System.Drawing.Size(573, 303);
            this.MobInfoView.TabIndex = 2;
            this.MobInfoView.SelectedValueChanged += new System.EventHandler(this.MobInfoView_SelectedValueChanged);
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.TabControlItemInfoViewAmount);
            this.tabPage13.Controls.Add(this.TabControlItemInfoViewItemID);
            this.tabPage13.Controls.Add(this.TabControlItemInfoViewItemType);
            this.tabPage13.Controls.Add(this.TabControlItemInfoViewIsActive);
            this.tabPage13.Controls.Add(this.label26);
            this.tabPage13.Controls.Add(this.label27);
            this.tabPage13.Controls.Add(this.label28);
            this.tabPage13.Controls.Add(this.label29);
            this.tabPage13.Controls.Add(this.ItemInfoView);
            this.tabPage13.Location = new System.Drawing.Point(4, 22);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(585, 549);
            this.tabPage13.TabIndex = 5;
            this.tabPage13.Text = "tabPage13";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // TabControlItemInfoViewAmount
            // 
            this.TabControlItemInfoViewAmount.Location = new System.Drawing.Point(60, 162);
            this.TabControlItemInfoViewAmount.Name = "TabControlItemInfoViewAmount";
            this.TabControlItemInfoViewAmount.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemInfoViewAmount.TabIndex = 18;
            // 
            // TabControlItemInfoViewItemID
            // 
            this.TabControlItemInfoViewItemID.Location = new System.Drawing.Point(60, 140);
            this.TabControlItemInfoViewItemID.Name = "TabControlItemInfoViewItemID";
            this.TabControlItemInfoViewItemID.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemInfoViewItemID.TabIndex = 17;
            // 
            // TabControlItemInfoViewItemType
            // 
            this.TabControlItemInfoViewItemType.Location = new System.Drawing.Point(60, 118);
            this.TabControlItemInfoViewItemType.Name = "TabControlItemInfoViewItemType";
            this.TabControlItemInfoViewItemType.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemInfoViewItemType.TabIndex = 16;
            // 
            // TabControlItemInfoViewIsActive
            // 
            this.TabControlItemInfoViewIsActive.Location = new System.Drawing.Point(60, 96);
            this.TabControlItemInfoViewIsActive.Name = "TabControlItemInfoViewIsActive";
            this.TabControlItemInfoViewIsActive.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemInfoViewIsActive.TabIndex = 15;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 165);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(46, 13);
            this.label26.TabIndex = 14;
            this.label26.Text = "Amount:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 143);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(41, 13);
            this.label27.TabIndex = 13;
            this.label27.Text = "ItemID:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 121);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 13);
            this.label28.TabIndex = 12;
            this.label28.Text = "ItemType:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 99);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(48, 13);
            this.label29.TabIndex = 11;
            this.label29.Text = "IsActive:";
            // 
            // ItemInfoView
            // 
            this.ItemInfoView.FormattingEnabled = true;
            this.ItemInfoView.Location = new System.Drawing.Point(6, 240);
            this.ItemInfoView.Name = "ItemInfoView";
            this.ItemInfoView.Size = new System.Drawing.Size(573, 303);
            this.ItemInfoView.TabIndex = 3;
            this.ItemInfoView.SelectedValueChanged += new System.EventHandler(this.ItemInfoView_SelectedValueChanged);
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.TabControlItemDropUnk);
            this.tabPage14.Controls.Add(this.TabControlItemDropMaxDrop);
            this.tabPage14.Controls.Add(this.TabControlItemDropMinDrop);
            this.tabPage14.Controls.Add(this.TabControlItemDropRate);
            this.tabPage14.Controls.Add(this.label34);
            this.tabPage14.Controls.Add(this.label35);
            this.tabPage14.Controls.Add(this.label36);
            this.tabPage14.Controls.Add(this.label37);
            this.tabPage14.Controls.Add(this.TabControlItemDropItemID);
            this.tabPage14.Controls.Add(this.TabControlItemDropIsEnabled);
            this.tabPage14.Controls.Add(this.TabControlItemDropMobID);
            this.tabPage14.Controls.Add(this.TabControlItemDropAmount);
            this.tabPage14.Controls.Add(this.label30);
            this.tabPage14.Controls.Add(this.label31);
            this.tabPage14.Controls.Add(this.label32);
            this.tabPage14.Controls.Add(this.label33);
            this.tabPage14.Controls.Add(this.ItemDropView);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(585, 549);
            this.tabPage14.TabIndex = 6;
            this.tabPage14.Text = "tabPage14";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // TabControlItemDropUnk
            // 
            this.TabControlItemDropUnk.Location = new System.Drawing.Point(60, 170);
            this.TabControlItemDropUnk.Name = "TabControlItemDropUnk";
            this.TabControlItemDropUnk.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropUnk.TabIndex = 34;
            // 
            // TabControlItemDropMaxDrop
            // 
            this.TabControlItemDropMaxDrop.Location = new System.Drawing.Point(60, 148);
            this.TabControlItemDropMaxDrop.Name = "TabControlItemDropMaxDrop";
            this.TabControlItemDropMaxDrop.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropMaxDrop.TabIndex = 33;
            // 
            // TabControlItemDropMinDrop
            // 
            this.TabControlItemDropMinDrop.Location = new System.Drawing.Point(60, 126);
            this.TabControlItemDropMinDrop.Name = "TabControlItemDropMinDrop";
            this.TabControlItemDropMinDrop.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropMinDrop.TabIndex = 32;
            // 
            // TabControlItemDropRate
            // 
            this.TabControlItemDropRate.Location = new System.Drawing.Point(60, 104);
            this.TabControlItemDropRate.Name = "TabControlItemDropRate";
            this.TabControlItemDropRate.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropRate.TabIndex = 31;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 173);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(30, 13);
            this.label34.TabIndex = 30;
            this.label34.Text = "Unk:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 151);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(50, 13);
            this.label35.TabIndex = 29;
            this.label35.Text = "MaxDrop";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 129);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(50, 13);
            this.label36.TabIndex = 28;
            this.label36.Text = "MinDrop:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 107);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(33, 13);
            this.label37.TabIndex = 27;
            this.label37.Text = "Rate:";
            // 
            // TabControlItemDropItemID
            // 
            this.TabControlItemDropItemID.Location = new System.Drawing.Point(60, 78);
            this.TabControlItemDropItemID.Name = "TabControlItemDropItemID";
            this.TabControlItemDropItemID.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropItemID.TabIndex = 26;
            // 
            // TabControlItemDropIsEnabled
            // 
            this.TabControlItemDropIsEnabled.Location = new System.Drawing.Point(60, 56);
            this.TabControlItemDropIsEnabled.Name = "TabControlItemDropIsEnabled";
            this.TabControlItemDropIsEnabled.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropIsEnabled.TabIndex = 25;
            // 
            // TabControlItemDropMobID
            // 
            this.TabControlItemDropMobID.Location = new System.Drawing.Point(60, 34);
            this.TabControlItemDropMobID.Name = "TabControlItemDropMobID";
            this.TabControlItemDropMobID.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropMobID.TabIndex = 24;
            // 
            // TabControlItemDropAmount
            // 
            this.TabControlItemDropAmount.Location = new System.Drawing.Point(60, 12);
            this.TabControlItemDropAmount.Name = "TabControlItemDropAmount";
            this.TabControlItemDropAmount.Size = new System.Drawing.Size(50, 20);
            this.TabControlItemDropAmount.TabIndex = 23;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 81);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(41, 13);
            this.label30.TabIndex = 22;
            this.label30.Text = "ItemID:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 59);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(57, 13);
            this.label31.TabIndex = 21;
            this.label31.Text = "IsEnabled:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 37);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(42, 13);
            this.label32.TabIndex = 20;
            this.label32.Text = "MobID:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 15);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(46, 13);
            this.label33.TabIndex = 19;
            this.label33.Text = "Amount:";
            // 
            // ItemDropView
            // 
            this.ItemDropView.FormattingEnabled = true;
            this.ItemDropView.Location = new System.Drawing.Point(6, 240);
            this.ItemDropView.Name = "ItemDropView";
            this.ItemDropView.Size = new System.Drawing.Size(573, 303);
            this.ItemDropView.TabIndex = 3;
            this.ItemDropView.SelectedValueChanged += new System.EventHandler(this.ItemDropView_SelectedValueChanged);
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.TabControlRewardAmount);
            this.tabPage15.Controls.Add(this.TabControlRewardItemID);
            this.tabPage15.Controls.Add(this.TabControlRewardType);
            this.tabPage15.Controls.Add(this.TabControlRewardMethod);
            this.tabPage15.Controls.Add(this.label50);
            this.tabPage15.Controls.Add(this.label51);
            this.tabPage15.Controls.Add(this.label52);
            this.tabPage15.Controls.Add(this.label38);
            this.tabPage15.Controls.Add(this.RewardView);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(585, 549);
            this.tabPage15.TabIndex = 7;
            this.tabPage15.Text = "tabPage15";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // RewardView
            // 
            this.RewardView.FormattingEnabled = true;
            this.RewardView.Location = new System.Drawing.Point(6, 240);
            this.RewardView.Name = "RewardView";
            this.RewardView.Size = new System.Drawing.Size(573, 303);
            this.RewardView.TabIndex = 3;
            this.RewardView.SelectedValueChanged += new System.EventHandler(this.RewardView_SelectedValueChanged);
            // 
            // DataBaseInfo
            // 
            this.DataBaseInfo.Controls.Add(this.label17);
            this.DataBaseInfo.Controls.Add(this.lDBConnect);
            this.DataBaseInfo.Controls.Add(this.button3);
            this.DataBaseInfo.Location = new System.Drawing.Point(844, 9);
            this.DataBaseInfo.Name = "DataBaseInfo";
            this.DataBaseInfo.Size = new System.Drawing.Size(145, 70);
            this.DataBaseInfo.TabIndex = 37;
            this.DataBaseInfo.TabStop = false;
            this.DataBaseInfo.Text = "DataBase Info:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(844, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 100);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quest Editing:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 89);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(83, 13);
            this.label38.TabIndex = 4;
            this.label38.Text = "RewardMethod:";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(6, 115);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(71, 13);
            this.label52.TabIndex = 5;
            this.label52.Text = "RewardType:";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 137);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(41, 13);
            this.label51.TabIndex = 6;
            this.label51.Text = "ItemID:";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 160);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(46, 13);
            this.label50.TabIndex = 7;
            this.label50.Text = "Amount:";
            // 
            // TabControlRewardMethod
            // 
            this.TabControlRewardMethod.Location = new System.Drawing.Point(95, 86);
            this.TabControlRewardMethod.Name = "TabControlRewardMethod";
            this.TabControlRewardMethod.Size = new System.Drawing.Size(50, 20);
            this.TabControlRewardMethod.TabIndex = 8;
            // 
            // TabControlRewardType
            // 
            this.TabControlRewardType.Location = new System.Drawing.Point(95, 112);
            this.TabControlRewardType.Name = "TabControlRewardType";
            this.TabControlRewardType.Size = new System.Drawing.Size(50, 20);
            this.TabControlRewardType.TabIndex = 9;
            // 
            // TabControlRewardItemID
            // 
            this.TabControlRewardItemID.Location = new System.Drawing.Point(95, 134);
            this.TabControlRewardItemID.Name = "TabControlRewardItemID";
            this.TabControlRewardItemID.Size = new System.Drawing.Size(50, 20);
            this.TabControlRewardItemID.TabIndex = 10;
            // 
            // TabControlRewardAmount
            // 
            this.TabControlRewardAmount.Location = new System.Drawing.Point(95, 157);
            this.TabControlRewardAmount.Name = "TabControlRewardAmount";
            this.TabControlRewardAmount.Size = new System.Drawing.Size(50, 20);
            this.TabControlRewardAmount.TabIndex = 11;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 621);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataBaseInfo);
            this.Controls.Add(this.grpbTabControl);
            this.Controls.Add(this.grpbQuestList);
            this.Name = "FrmMain";
            this.Text = "QuestData";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpbQuestList.ResumeLayout(false);
            this.grpbTabControl.ResumeLayout(false);
            this.grpbTabControl.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.tabPage13.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            this.tabPage14.PerformLayout();
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this.DataBaseInfo.ResumeLayout(false);
            this.DataBaseInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenQuestData;
        private System.Windows.Forms.ListBox lbQuestList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lDBConnect;
        private System.Windows.Forms.Label lFullCount;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox grpbQuestList;
        private System.Windows.Forms.GroupBox grpbTabControl;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label TitleID;
        private System.Windows.Forms.Label QuestID;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.TextBox TabControlAcceptScript;
        private System.Windows.Forms.TextBox TabControlActiveScript;
        private System.Windows.Forms.TextBox TabControlFinishScript;
        private System.Windows.Forms.Label QuestGrade;
        private System.Windows.Forms.Label DescriptionID;
        private System.Windows.Forms.GroupBox DataBaseInfo;
        private System.Windows.Forms.Label QuestCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TabControlDescriptionID;
        private System.Windows.Forms.TextBox TabControlTitleID;
        private System.Windows.Forms.TextBox TabControlQuestID;
        private System.Windows.Forms.TextBox TabControlDaily;
        private System.Windows.Forms.TextBox TabControlRepeat;
        private System.Windows.Forms.TextBox TabControlGrade;
        private System.Windows.Forms.TextBox TabControlClassType;
        private System.Windows.Forms.TextBox TabControlNeedClass;
        private System.Windows.Forms.TextBox TabControlPredesessor;
        private System.Windows.Forms.TextBox TabControlNeedPred;
        private System.Windows.Forms.TextBox TabControlItemVanish;
        private System.Windows.Forms.TextBox TabControlItemID;
        private System.Windows.Forms.TextBox TabControlNeedItem;
        private System.Windows.Forms.TextBox TabControlStartingNPC;
        private System.Windows.Forms.TextBox TabControlNeedNPC;
        private System.Windows.Forms.TextBox TabControlMaxLevel;
        private System.Windows.Forms.TextBox TabControlMinLevel;
        private System.Windows.Forms.TextBox TabControlNeedLevel;
        private System.Windows.Forms.TextBox TabControlInstAccept;
        private System.Windows.Forms.TextBox TabControlEnabled;
        private System.Windows.Forms.TextBox TabControlFinishLevel;
        private System.Windows.Forms.TextBox TabControlRequireLevel;
        private System.Windows.Forms.TextBox TabControlInstHandin;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListBox MobInfoView;
        private System.Windows.Forms.ListBox ItemInfoView;
        private System.Windows.Forms.ListBox ItemDropView;
        private System.Windows.Forms.ListBox RewardView;
        private System.Windows.Forms.TextBox TabControlMobInfoAmount;
        private System.Windows.Forms.TextBox TabControlMobInfoIsKillable;
        private System.Windows.Forms.TextBox TabControlMobInfoMobID;
        private System.Windows.Forms.TextBox TabControlMobInfoIsActive;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private TextBox TabControlItemInfoViewAmount;
        private TextBox TabControlItemInfoViewItemID;
        private TextBox TabControlItemInfoViewItemType;
        private TextBox TabControlItemInfoViewIsActive;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private TextBox TabControlItemDropUnk;
        private TextBox TabControlItemDropMaxDrop;
        private TextBox TabControlItemDropMinDrop;
        private TextBox TabControlItemDropRate;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private TextBox TabControlItemDropItemID;
        private TextBox TabControlItemDropIsEnabled;
        private TextBox TabControlItemDropMobID;
        private TextBox TabControlItemDropAmount;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private TextBox TabControlRewardAmount;
        private TextBox TabControlRewardItemID;
        private TextBox TabControlRewardType;
        private TextBox TabControlRewardMethod;
        private Label label50;
        private Label label51;
        private Label label52;
        private Label label38;
    }
}

