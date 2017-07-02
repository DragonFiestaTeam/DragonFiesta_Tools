using QuestDataSQLConverter.IO;
using System;

namespace QuestDataSQLConverter.Object
{
    public class QuestInfo
    {
        public int ID { get; set; }
        public int TitleID { get; private set; }
        public int DescriptionID { get; private set; }
        public byte QuestGrade { get; private set; }
        public byte MultiQuest { get; private set; }
        public byte DailyQuest { get; private set; }
        public byte Enable { get; private set; }
        public byte InstAcc { get; private set; }
        public byte NeedLevel { get; private set; }
        public byte MinLevel { get; private set; }
        public byte MaxLevel { get; private set; }
        public byte NeedNPC { get; private set; }
        public short StartingNPC { get; private set; }
        public byte NeedItem { get; private set; }
        public short ItemID { get; private set; }
        public byte ItemVanish { get; private set; }
        public byte NeedPreed { get; private set; }
        public short Predesessor { get; private set; }
        public byte NeedClass { get; private set; }
        public byte ClassType { get; private set; }
        public byte InstHand { get; private set; }
        public byte RequireLevel { get; private set; }
        public byte FinishLevel { get; private set; }

        public void SaveSQL()
        {
            // Fix this later
            string SQL = "INSERT INTO QuestInfo (ID,TitleID,DescriptionID,QuestGrade,MultiQuest,DailyQuest,Enable,InstAcc,NeedLevel,MinLevel,MaxLevel,NeedNPC,StartingNPC,NeedItem,ItemID,ItemVanish,NeedPreed,Predesessor,NeedClass,ClassType,InstHand,RequireLevel,FinishLevel) VALUES"
                + "('" + ID + "','" + TitleID + "','" + DescriptionID + "','" + Convert.ToByte(QuestGrade) + "','" + Convert.ToByte(MultiQuest) + "','" + Convert.ToByte(DailyQuest) + "','" + Convert.ToByte(Enable) + "','" + Convert.ToByte(InstAcc) + "')";

            DatabaseManager.RunSQL(SQL);
        }

        public static bool Read(QuestStream pStream, out QuestInfo pinfo)
        {
            pinfo = null;
            if (!pStream.TryReadInt32(out int pID)) { return false; }

            if (!pStream.TryReadInt32(out int pTitleID)) { return false; }

            if (!pStream.TryReadInt32(out int pDescription)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }

            if (!pStream.TryReadByte(out byte pQuestGrade)) { return false; }

            if (!pStream.TryReadByte(out byte pMultiQuest)) { return false; }

            if (!pStream.TryReadByte(out byte pDailyQuest)) { return false; }

            if (!pStream.ReadSkip(4)) { return false; }

            if (!pStream.TryReadByte(out byte pEnable)) { return false; }

            if (!pStream.TryReadByte(out byte pInstAcc)) { return false; }

            if (!pStream.TryReadByte(out byte pNeedLevel)) { return false; }

            if (!pStream.TryReadByte(out byte pMinLevel)) { return false; }

            if (!pStream.TryReadByte(out byte pMaxLevel)) { return false; }

            if (!pStream.TryReadByte(out byte pNeedNPC)) { return false; }

            if (!pStream.TryReadInt16(out short pStartingNPC)) { return false; }

            if (!pStream.TryReadByte(out byte pNeedItem)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }

            if (!pStream.TryReadInt16(out short pItemID)) { return false; }

            if (!pStream.TryReadByte(out byte pItemVanish)) { return false; }

            if (!pStream.ReadSkip(19)) { return false; }

            if (!pStream.TryReadByte(out byte pNeedPreed)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }

            if (!pStream.TryReadInt16(out short pPredesessor)) { return false; }

            if (!pStream.ReadSkip(2)) { return false; }

            if (!pStream.TryReadByte(out byte pNeedClass)) { return false; }

            if (!pStream.TryReadByte(out byte pClassType)) { return false; }

            if (!pStream.ReadSkip(24)) { return false; }

            if (!pStream.TryReadByte(out byte pInstHand)) { return false; }

            if (!pStream.TryReadByte(out byte pRequireLevel)) { return false; }

            if (!pStream.TryReadByte(out byte pFinishLevel)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }

            pinfo = new QuestInfo
            {
                ID = pID,
                TitleID = pTitleID,
                DescriptionID = pDescription,
                QuestGrade = pQuestGrade,
                MultiQuest = pMultiQuest,
                DailyQuest = pDailyQuest,
                Enable = pEnable,
                InstAcc = pInstAcc,
                NeedLevel = pNeedLevel,
                MinLevel = pMinLevel,
                MaxLevel = pMaxLevel,
                NeedNPC = pNeedNPC,
                StartingNPC = pStartingNPC,
                NeedItem = pNeedItem,
                ItemID = pItemID,
                ItemVanish = pItemVanish,
                NeedPreed = pNeedPreed,
                Predesessor = pPredesessor,
                NeedClass = pNeedClass,
                ClassType = pClassType,
                InstHand = pInstHand,
                RequireLevel = pRequireLevel,
                FinishLevel = pFinishLevel
            };
            return true; ;
        }
    }
}