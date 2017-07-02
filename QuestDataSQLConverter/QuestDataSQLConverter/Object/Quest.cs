using QuestDataSQLConverter.Database;
using QuestDataSQLConverter.IO;
using System.Collections.Generic;

namespace QuestDataSQLConverter.Object
{
    public class Quest
    {
        public QuestInfo PInfo { get; set; }
        public QuestScript Script { get; private set; }
        public List<QuestMob> Mobs { get; private set; }
        public List<QuestItem> Items { get; private set; }
        public List<QuestItemDrop> ItemDrops { get; private set; }
        public List<QuestReward> Rewards { get; private set; }
        public int QuestDataLenght { get; set; }

        public static bool Read(QuestStream pStream, out Quest pQuest)
        {
            pQuest = new Quest();

            if (!QuestInfo.Read(pStream, out QuestInfo pInf))
            {
                return false;
            }

            pQuest.PInfo = pInf;

            if (!pQuest.LoadAllMobs(pStream))
            {
                return false;
            }

            if (!pQuest.LoadAllItems(pStream))
            {
                return false;
            }

            if (!pQuest.LoadAllItemDrops(pStream))
            {
                return false;
            }

            if (!pQuest.LoadAllRewards(pStream))
            {
                return false;
            }

            if (!pQuest.LoadScript(pStream))
            {
                return false;
            }

            return true;
        }

        public bool LoadScript(QuestStream pStream)
        {
            if (!QuestScript.Read(pStream, out QuestScript pScript))
                return false;

            Script = pScript;

            return true;
        }

        public bool LoadAllMobs(QuestStream pStream)
        {
            Mobs = new List<QuestMob>();
            for (int inxMob = 0; inxMob < 5; inxMob++)
            {
                if (!QuestMob.Read(pStream, out QuestMob pMob))
                    return false;

                Mobs.Add(pMob);
            }
            return true;
        }

        public bool LoadAllItems(QuestStream pStream)
        {
            Items = new List<QuestItem>();
            for (int inxItem = 0; inxItem < 10; inxItem++)
            {
                if (!QuestItem.Read(pStream, out QuestItem pItem))
                    return false;

                Items.Add(pItem);
            }

            return true;
        }

        public bool LoadAllItemDrops(QuestStream pStream)
        {
            ItemDrops = new List<QuestItemDrop>();
            for (int inxDrop = 0; inxDrop < 10; inxDrop++)
            {
                if (!QuestItemDrop.Read(pStream, out QuestItemDrop pItemDrops))
                    return false;

                ItemDrops.Add(pItemDrops);
            }

            if (!pStream.TryReadInt32(out int DropCount))
            {
                return false;
            }

            return true;
        }

        public bool LoadAllRewards(QuestStream pStream)
        {
            Rewards = new List<QuestReward>();
            for (int inxReward = 0; inxReward < 12; inxReward++)
            {
                if (!QuestReward.Read(pStream, out QuestReward reward))
                {
                    return false;
                }
                Rewards.Add(reward);
            }
            if (!pStream.ReadSkip(14))  //itemData
            {
                return false;
            }
            return true;
        }

        public static void CheckTables()
        {
            //CheckTable exis when not then create
            DatabaseManager.RunSQL(TableDefines.QuestInfo);
            DatabaseManager.RunSQL(TableDefines.QuestItem);
            DatabaseManager.RunSQL(TableDefines.QuestItemDrop);
            DatabaseManager.RunSQL(TableDefines.QuestMobs);
            DatabaseManager.RunSQL(TableDefines.QuestRewards);
            DatabaseManager.RunSQL(TableDefines.QuestScript);
        }

        public void SaveSQL()
        {

            PInfo.SaveSQL();
            foreach (var mobs in Mobs)
            {
                mobs.SaveSQL(PInfo.ID);
            }

            foreach (var items in Items)
            {
                items.SaveSQL(PInfo.ID);
            }

            foreach (var itemDrops in ItemDrops)
            {
                itemDrops.SaveSQL(PInfo.ID);
            }

            foreach (var reward in Rewards)
            {
                reward.SaveSQL(PInfo.ID);
            }
          
            Script.SaveSQL(PInfo.ID);
        }
    }
}