using QuestDataSQLConverter.IO;
using System.Data.SqlClient;

namespace QuestDataSQLConverter
{
    public class QuestScript
    {
        public short StartLength { get; private set; }
        public short FinishLength { get; private set; }
        public short ActionLength { get; private set; }

        public string ActionScript { get; private set; }
        public string StartScript { get; private set; }
        public string FinishScript { get; private set; }
        //    public QuestStream Start;
        //    public QuestStream Finish;
        //    public QuestStream Action;
        //    public byte[] ScriptData { get; private set; }

        public const byte Seperator = 00;

        public void SaveSQL(int QuestID)
        {
            string SQL = "INSERT INTO QuestScript (StartScript,ActionScript,FinishScript,QuestID) VALUES"
                + "('@StartScp','@Actionscp','@FinishScp','@QuestID')";
            DatabaseManager.RunSQL(SQL, new SqlParameter("StartScp", StartScript), new SqlParameter("Actionscp", ActionScript), new SqlParameter("FinishScp", FinishScript), new SqlParameter("QuestID", 1));
        }

        public static bool Read(QuestStream qStream, out QuestScript pScript)
        {
            pScript = null;
            if (!qStream.TryReadInt16(out short pStartScpLenght))
            {
                return false;
            }

            if (!qStream.TryReadInt16(out short pActionLenght))
            {
                return false;
            }

            if (!qStream.TryReadInt16(out short pfinishLenght))
            {
                return false;
            }

            if (!qStream.TryReadNulledString(out string pStartScript))
            {
                return false;
            }

            if (!qStream.TryReadNulledString(out string pActionScript))
            {
                return false;
            }

            if (!qStream.TryReadNulledString(out string pFinishScript))
            {
                return false;
            }


            pScript = new QuestScript
            {
                StartLength = pStartScpLenght,
                ActionLength = pActionLenght,
                FinishLength = pfinishLenght,
                StartScript = pStartScript,
                ActionScript = pActionScript,
                FinishScript = pFinishScript,
            };
            return true;
        }
    }
}