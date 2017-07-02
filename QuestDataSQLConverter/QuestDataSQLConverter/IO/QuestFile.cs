using QuestDataSQLConverter.Object;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuestDataSQLConverter.IO
{
    public class QuestFile
    {
        public QuestStream PFileStream { get; private set; }

        public List<Quest> QuestList { get; private set; }

        public QuestFile(string FileName)
        {
            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException();
            }
            PFileStream = new QuestStream(FileName);
            if (!PFileStream.TryReadInt16(out short QuestFileVersion))
            {
                throw new InvalidCastException("Can not Read FileVersion");
            }
            Console.WriteLine("Version " + QuestFileVersion + " found");
        }

        public void ReadQuestsFromFile()
        {
            QuestList = new List<Quest>();

            if (!PFileStream.TryReadInt16(out short QuestCount))
            {
                return;
            }

            for (short i = 0; i < QuestCount; i++)
            {
                if (!PFileStream.TryReadInt32(out int QuestDataLenght))
                {
                    throw new InsufficientMemoryException();
                }
                if (!ReadQuest(QuestDataLenght, PFileStream, out Quest pQuest))
                {
                    throw new InsufficientMemoryException();
                }

                QuestList.Add(pQuest);
            }
        }/*

        public bool GetQuestByID(ushort ID,out Quest pQuest)
        {
            if (QuestList.TryGetValue(ID, out pQuest))
                return true;

            return false;
        }
        public void SaveAllQuests()
        {
            foreach(var value in QuestList.Values)
            {
                value.SaveSQL();
            }
        }*/

        private bool ReadQuest(int Datalenght, QuestStream pStream, out Quest pQuest)
        {
            pQuest = null;

            if (!Quest.Read(pStream, out pQuest))
            {
                return false;
            }

            pQuest.QuestDataLenght = Datalenght;

            return true;
        }
    }
}