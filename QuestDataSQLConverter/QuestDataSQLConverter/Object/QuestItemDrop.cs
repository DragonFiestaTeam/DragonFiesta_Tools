using QuestDataSQLConverter.IO;

namespace QuestDataSQLConverter.Object
{
    public class QuestItemDrop
    {
        public int Amount { get; private set; }
        public int MobID { get; private set; }
        public int IsEnabled { get; private set; }
        public int ItemID { get; private set; }
        public int Rate { get; private set; }
        public int MinDrop { get; private set; }
        public int MaxDrop { get; private set; }
        public int Unk { get; private set; }

        public void SaveSQL(int questid)
        {
            string SQL = "INSERT INTO QuestItemDrop (QuestID,Amount,MobID,IsEnabled,ItemID,Rate,MinDrop,MaxDrop,Unk) VALUES "
                + "('" + questid + "','" + Amount + "','" + MobID + "','" + IsEnabled + "','" + ItemID + "','" + Rate + "','" + MinDrop + "','" + MaxDrop + "','" + Unk + "')";
            DatabaseManager.RunSQL(SQL);
        }

        public static bool Read(QuestStream pStream, out QuestItemDrop pItemDrop)
        {
            pItemDrop = null;

            if (!pStream.TryReadInt32(out int pAmount)) { return false; }

            if (!pStream.TryReadInt32(out int pMobID)) { return false; }

            if (!pStream.TryReadInt32(out int pIsEnabled)) { return false; }

            if (!pStream.TryReadInt32(out int pItemID)) { return false; }

            if (!pStream.TryReadInt32(out int pRate)) { return false; }

            if (!pStream.TryReadInt32(out int pMinDrop)) { return false; }

            if (!pStream.TryReadInt32(out int pMaxDrop)) { return false; }

            if (!pStream.TryReadInt32(out int pUnk)) { return false; }


            pItemDrop = new QuestItemDrop
            {
                Amount = pAmount,
                MobID = pMobID,
                IsEnabled = pIsEnabled,
                ItemID = pItemID,
                Rate = pRate,
                MinDrop = pMinDrop,
                MaxDrop = pMaxDrop,
                Unk = pUnk
            };

            return true;
        }
    }
}
