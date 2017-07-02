using QuestDataSQLConverter.IO;
using System;

namespace QuestDataSQLConverter.Object
{
    public class QuestMob
    {
        public byte IsEnabled { get; private set; }
        public ushort MobID { get; private set; }
        public byte IsKillable { get; private set; }
        public byte Amount { get; private set; }


        public void SaveSQL(int qID)
        {

            string SQL = "INSERT INTO QuestMob (QuestID,IsEnabled,MobID,IsKillable,Amount) VALUES"
                + "('" + qID + "','" + (Convert.ToByte(IsEnabled) + "','" + MobID + "','" + Convert.ToByte(IsKillable) + "','" + Amount + "')");
            DatabaseManager.RunSQL(SQL);
        }

        public static bool Read(QuestStream pStream, out QuestMob pMob)
        {
            pMob = null;
            if (!pStream.TryReadByte(out byte pIsEnabled)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }

            if (!pStream.TryReadUInt16(out ushort MobID)) { return false; }

            if (!pStream.TryReadByte(out byte pIsKillable)) { return false; }

            if (!pStream.TryReadByte(out byte pAmount)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }

            if (!pStream.ReadSkip(1)) { return false; }


            pMob = new QuestMob
            {
                IsEnabled = pIsEnabled,
                MobID = MobID,
                IsKillable = pIsKillable,
                Amount = pAmount
            };

            return true;
        }
    }
}