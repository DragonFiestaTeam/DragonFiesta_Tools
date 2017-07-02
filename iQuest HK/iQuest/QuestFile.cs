using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace iQuest
{
    public sealed class QuestFile
    {
        private FileStream _File;
        private BinaryReader Reader;
        byte[] QuestHeader = new byte[2];
        public static List<Quest> Quests = new List<Quest>();
        private byte RewardMax = 12;
        public short QuestCount = 0;

        public QuestFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("File does not exists");
            }
            _File = File.Open(path, FileMode.Open);
            Reader = new BinaryReader(_File);
            QuestHeader = Reader.ReadBytes(2);
            short questCount = Reader.ReadInt16();
            for (short i = 0; i < questCount; ++i)
            {
                LoadQuest();
            }
            _File.Dispose();
        }

        public Quest CO_get_Item(ushort ID)
        {
            Quest result;
            foreach (Quest current in Quests)
            {
                if (current.ID == ID)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }

        private void LoadQuest()
        {
            Quest quest = new Quest();
            ParseDefault(quest);
            ParseMobs(quest);
            ParseItems(quest);
            ParseRewards(quest);
            quest.StartScript = ReadNulledString();
            quest.ActionScript = ReadNulledString();
            quest.FinishScript = ReadNulledString();
            Quests.Add(quest);
        }

        void ParseDefault(Quest quest)
        {
            Reader.ReadUInt32();
            quest.ID = Reader.ReadUInt16();
            if(quest.ID == 15)
            {
                quest.ID = quest.ID;
            }
            quest.Title = Reader.ReadUInt16();
            quest.Description = Reader.ReadUInt16();
            quest.UnkBytes.Add(1, Reader.ReadBytes(1));
            quest.QuestGrade = Reader.ReadByte();
            quest.MultiQuest = Reader.ReadByte();
            quest.DailyQuest = Reader.ReadByte();
            quest.UnkBytes.Add(2, Reader.ReadBytes(2));
            quest.EnableQuest = Reader.ReadByte();
            quest.InstAcc = Reader.ReadByte();
            quest.NeedLevel = Reader.ReadByte();
            quest.MinLevel = Reader.ReadByte();
            quest.MaxLevel = Reader.ReadByte();
            quest.NeedNPC = Reader.ReadByte();
            quest.StartingNPC = Reader.ReadUInt16();
            quest.NeedItem = Reader.ReadByte();
            quest.UnkBytes.Add(3, Reader.ReadBytes(1));
            quest.ItemID = Reader.ReadUInt16();
            quest.ItemVanish = Reader.ReadByte();
            quest.UnkBytes.Add(4, Reader.ReadBytes(19));
            quest.NeedPred = Reader.ReadByte();
            quest.UnkBytes.Add(5, Reader.ReadBytes(1));
            quest.Predesessor = Reader.ReadUInt16();
            quest.UnkBytes.Add(6, Reader.ReadBytes(2));
            quest.NeedClass = Reader.ReadByte();
            quest.ClassType = Reader.ReadByte();
            quest.UnkBytes.Add(7, Reader.ReadBytes(24));
            quest.InstHand = Reader.ReadByte();
            quest.UnkBytes.Add(8, Reader.ReadBytes(3));
        }

        void RestoreDefault(BinaryWriter writer, Quest quest)
        {
            writer.Write(quest.DataLen);
            writer.Write(quest.ID);
            writer.Write(quest.Title);
            writer.Write(quest.Description);
            writer.Write(quest.UnkBytes[1]);
            writer.Write(quest.QuestGrade);
            writer.Write(quest.MultiQuest);
            writer.Write(quest.DailyQuest);
            writer.Write(quest.UnkBytes[2]);
            writer.Write(quest.EnableQuest);
            writer.Write(quest.InstAcc);
            writer.Write(quest.NeedLevel);
            writer.Write(quest.MinLevel);
            writer.Write(quest.MaxLevel);
            writer.Write(quest.NeedNPC);
            writer.Write(quest.StartingNPC);
            writer.Write(quest.NeedItem);
            writer.Write(quest.UnkBytes[3]);
            writer.Write(quest.ItemID);
            writer.Write(quest.ItemVanish);
            writer.Write(quest.UnkBytes[4]);
            writer.Write(quest.NeedPred);
            writer.Write(quest.UnkBytes[5]);
            writer.Write(quest.Predesessor);
            writer.Write(quest.UnkBytes[6]);
            writer.Write(quest.NeedClass);
            writer.Write(quest.ClassType);
            writer.Write(quest.UnkBytes[7]);
            writer.Write(quest.InstAcc);
            writer.Write(quest.UnkBytes[8]);
        }

        void ParseRewards(Quest quest)
        {
            for (byte i = 0; i < RewardMax; i +=1)
            {
                QuestReward questReward = new QuestReward();
                questReward.isGiven = Reader.ReadByte();
                questReward.Type = (QuestReward.RewardType)Reader.ReadByte();
                questReward.unkShort = Reader.ReadUInt16();
                if (questReward.Type == QuestReward.RewardType.Item)
                {
                    questReward.Value = Reader.ReadUInt16();
                    questReward.ItemCount = Reader.ReadUInt16();
                    questReward.unkDword = Reader.ReadUInt32();
                }
                else
                {
                    questReward.Amount = Reader.ReadUInt64();
                }
                quest.Rewards.Add(questReward);
            }
            Reader.BaseStream.Position += 6;
            quest.RewardData = Reader.ReadBytes(14);
        }

        public void SaveFile(string path)
        {
            FileStream stream = File.Create(path);
            BinaryWriter writor = new BinaryWriter(stream);
            writor.Write(QuestHeader);
            writor.Write((short)Quests.Count);
            for (int i = 0; i < Quests.Count; ++i)
            {
                long pos = writor.BaseStream.Position;
                RestoreDefault(writor, Quests[i]);

                foreach (Mob monster in Quests[i].Mobs)
                {
                    writor.Write(Convert.ToByte(monster.isActive));
                    writor.Write(Convert.ToByte(monster.isMob));
                    writor.Write(monster.ID);
                    writor.Write(Convert.ToByte(monster.HasToBeKilled));
                    writor.Write(monster.Amount);
                    writor.Write(monster.UnkDataMob);
                    writor.Write(monster.UnkDataMob2);
                }

                foreach (Item item in Quests[i].Items)
                {
                    writor.Write(item.isActive);
                    writor.Write(item.Type);
                    writor.Write(item.ID);
                    writor.Write(item.Amount);
                }

                writor.Write(Quests[i].DropCount);
                foreach (ItemDrop itemDrop in Quests[i].ItemDrop)
                {
                    writor.Write(itemDrop.isActive);
                    writor.Write(itemDrop.MobID);
                    writor.Write(itemDrop.Amount);
                    writor.Write(itemDrop.ItemID);
                    writor.Write(itemDrop.Rate);
                    writor.Write(itemDrop.UnkByte1);
                    writor.Write(itemDrop.UnkByte2);
                }

                foreach (ItemDropPadding itemDropPadding in Quests[i].ItemPadding)
                {
                    writor.Write(itemDropPadding.Padding);
                }

                long startpos = writor.BaseStream.Position;
                foreach (QuestReward reward in Quests[i].Rewards)
                {
                    writor.Write(reward.isGiven);
                    switch (reward.Type)
                    {
                        case QuestReward.RewardType.Exp:
                            writor.Write(0);
                            break;

                        case QuestReward.RewardType.Money:
                            writor.Write(1);
                            break;

                        case QuestReward.RewardType.Item:
                            writor.Write(2);
                            break;

                        case QuestReward.RewardType.Fame:
                            writor.Write(4);
                            break;
                    }
                    writor.Write(reward.unkShort);
                    if (reward.Type == QuestReward.RewardType.Item)
                    {
                        writor.Write(reward.Value);
                        writor.Write(reward.ItemCount);
                        writor.Write(reward.unkDword);
                    }
                    else
                    {
                        writor.Write(reward.Amount);
                    }
                }

                long totalf = writor.BaseStream.Position - startpos;
                writor.Write((ushort)(Quests[i].StartScript.Length + 1));
                writor.Write((ushort)(Quests[i].FinishScript.Length + 1));
                writor.Write((ushort)(Quests[i].ActionScript.Length + 1));
                writor.Write(Quests[i].RewardData);
                long byteswritten = writor.BaseStream.Position - pos; ;
                long byteswritten2 = 672L;
                if (byteswritten != 672L)
                {
                    throw new Exception("Required Length: " + byteswritten2.ToString() + " - Current Length: " + byteswritten.ToString());
                }
                WriteNulledString(writor, Quests[i].StartScript);
                WriteNulledString(writor, Quests[i].ActionScript);
                WriteNulledString(writor, Quests[i].FinishScript);

            }
            stream.Close();
            stream.Dispose();
        }

        void ParseItems(Quest quest)
        {
            for (byte i = 0; i < 10; ++i)
            {
                Item item = new Item();
                item.isActive = Reader.ReadByte();
                item.Type = Reader.ReadByte();
                item.ID = Reader.ReadUInt16();
                item.Amount = Reader.ReadUInt16();
                quest.Items.Add(item);
            }
            quest.DropCount = Reader.ReadUInt32();
            byte b2 = 0;
            while(b2 < quest.DropCount)
            {
                ItemDrop itemDrop = new ItemDrop();
                itemDrop.isActive = Reader.ReadUInt32();
                itemDrop.MobID = Reader.ReadUInt32();
                itemDrop.Amount = Reader.ReadUInt32();
                itemDrop.ItemID = Reader.ReadUInt32();
                itemDrop.Rate = Reader.ReadUInt32();
                itemDrop.UnkByte1 = Reader.ReadUInt32();
                itemDrop.UnkByte2 = Reader.ReadUInt32();
                quest.ItemDrop.Add(itemDrop);
                b2 += 1;
            }
            uint value;
            if (quest.DropCount > 0)
            {
                value = 28 * (11 - quest.DropCount) + 12;
            }
            else
            {
                value = 28 * (11 - quest.DropCount) + 12u;
            }
            ItemDropPadding ItemPadding = new ItemDropPadding();
            ItemPadding.Padding = Reader.ReadBytes(Convert.ToInt32(value));
            quest.ItemPadding.Add(ItemPadding);
        }

        void ParseMobs(Quest quest)
        {
            for (int i = 0; i < 5; ++i)
            {
                Mob mob = new Mob();
                mob.isActive = Convert.ToBoolean(Reader.ReadByte());
                mob.isMob = Convert.ToBoolean(Reader.ReadByte());
                mob.ID = Reader.ReadUInt16();
                mob.HasToBeKilled = Convert.ToBoolean(Reader.ReadByte());
                mob.Amount = Reader.ReadByte();
                mob.UnkDataMob = Reader.ReadByte();
                mob.UnkDataMob2 = Reader.ReadByte();
                quest.Mobs.Add(mob);
            }
        }

        string ReadNulledString()
        {
            byte[] data = new byte[256];
            int index = 0;
            byte b;
            while ((b = Reader.ReadByte()) != 0)
            {
                if (data.Length == index) Array.Resize(ref data, data.Length * 2);
                data[index++] = b;
            }
            return Encoding.ASCII.GetString(data, 0, index);
        }

        void WriteZeros(BinaryWriter writer, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                writer.Write((byte)0);
            }
        }

        void WriteNulledString(BinaryWriter writer, string text)
        {
            writer.Write(Encoding.ASCII.GetBytes(text));
            writer.Write((byte)0);
        }
    }

    public class Quest 
    {
        public List<Mob> Mobs = new List<Mob>();
        public List<ItemDrop> ItemDrop = new List<ItemDrop>();
        public List<ItemDropPadding> ItemPadding = new List<ItemDropPadding>();
        public List<QuestReward> Rewards = new List<QuestReward>();
        public List<Item> Items = new List<Item>();
        public uint DropCount = 0;

        public ushort ID { get; set; }
        
        public ushort Title { get; set; }
        public ushort Description { get; set; }
        public byte QuestGrade { get; set; }
        public byte DailyQuest { get; set; }
        public byte EnableQuest { get; set; }
        public byte InstAcc { get; set; }
        public byte NeedLevel { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public byte NeedNPC { get; set; }
        public ushort StartingNPC { get; set; }
        public byte NeedItem { get; set; }
        public ushort ItemID { get; set; }
        public byte NeedClass { get; set; }
        public byte ItemVanish { get; set; }
        public byte NeedPred { get; set; }
        public ushort Predesessor { get; set; }
        public byte MultiQuest { get; set; }
        public byte ClassType { get; set; }
        public byte InstHand { get; set; }

        public Dictionary<byte, byte[]> UnkBytes = new Dictionary<byte, byte[]>();

        public byte[] RewardData { get; set; }
        public byte[] ItemData { get; set; }
        public string StartScript { get; set; }
        public string ActionScript { get; set; }
        public string FinishScript { get; set; }

        public string GetUnkString()
        {
            string tot = "";
            for (int i = 0; i < RewardData.Length - 1; ++i)
            {
                tot += RewardData[i].ToString("X2") + " ";
            }
            tot += RewardData[RewardData.Length - 1].ToString("X2");
            return tot;
        }

        public void SetUnkString(string hex)
        {
            string[] splitted = hex.Split(' ');
            if (splitted.Length != 18) { throw new Exception("Incorrect lenght!"); }
            for (int i = 0; i < splitted.Length; ++i)
            {
                RewardData[i] = byte.Parse(splitted[i], System.Globalization.NumberStyles.HexNumber);
            }
        }

        public uint DataLen {
            get
            {
                uint start = 0;
                start += 672;
                start += (uint)StartScript.Length + 1;
                start += (uint)ActionScript.Length + 1;
                start += (uint)FinishScript.Length + 1;
                return start;
            }
        }
    }

    public class Mob
    {
        public ushort ID { get; set; }
        public byte Amount { get; set; }
        public bool isMob { get; set; }
        public bool isActive { get; set; }
        public bool HasToBeKilled { get; set; }
        public byte UnkDataMob { get; set; }
        public byte UnkDataMob2 { get; set; }
    }


    public class QuestReward
    {
        public enum RewardType : byte
        {
            Exp,
            Money,
            Item,
            Fame = 4
        }
        public RewardType Type { get; set; }
        public uint unkDword { get; set; }
        public ushort unkShort { get; set; }
        public ushort Value { get; set; }
        public ushort ItemCount { get; set; }
        public ulong Amount { get; set; }
        public byte isGiven { get; set; }
    }

    public class Item
    {
        public byte Type { get; set; }
        public ushort ID { get; set; }
        public ushort Amount { get; set; }
        public byte isActive { get; set; }
    }

    public sealed class ItemDropPadding
    {
        public byte[] Padding { get; set; }
    }

    public sealed class ItemDrop
    {
        public uint isActive { get; set; }
        public uint MobID { get; set; }
        public uint Amount { get; set; }
        public uint ItemID { get; set; }
        public uint Rate { get; set; }
        public uint UnkByte1 { get; set; }
        public uint UnkByte2 { get; set; }
    }
}
