using QuestDataSQLConverter.IO;

namespace QuestDataSQLConverter.Object
{
    public class QuestItem
    {
        public byte IsEnabled { get; private set; }
        public byte Type { get; private set; }
        public ushort ItemID { get; private set; }
        public ushort Amount { get; private set; }

        public void SaveSQL(int questid)
        {
            string SQL = "INSERT INTO QuestItem (QuestID,IsEnabled,Type,ItemID,Amount) VALUES "
                + "('" + questid + "','" + IsEnabled + "','" + Type + "','" + ItemID + "','" + Amount + "')";
            DatabaseManager.RunSQL(SQL);
        }

        public static bool Read(QuestStream pStream, out QuestItem pItem)
        {
            pItem = null;

            if (!pStream.TryReadByte(out byte pIsEnabled)) { return false; }

            if (!pStream.TryReadByte(out byte pItemType)) { return false; }

            if (!pStream.TryReadUInt16(out ushort pItemID)) { return false; }

            if (!pStream.TryReadUInt16(out ushort pAmount)) { return false; }

            pItem = new QuestItem
            {
                IsEnabled = pIsEnabled,
                Type = pItemType,
                ItemID = pItemID,
                Amount = pAmount,
            };

            return true;
        }
    }
}