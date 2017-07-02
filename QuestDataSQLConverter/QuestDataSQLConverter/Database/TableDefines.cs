namespace QuestDataSQLConverter.Database
{
    public static class TableDefines
    {
        public const string QuestInfo = "IF OBJECT_ID('[QuestInfo]', 'U') IS NULL CREATE TABLE [dbo].[QuestInfo] ("
                                          + "[ID] int NOT NULL ,"
                                          + "[TitleID] int NOT NULL ,"
                                          + "[DescriptionID] int NOT NULL ,"
                                          + "[QuestGrade] bit NOT NULL ,"
                                          + "[MultiQuest] bit NOT NULL ,"
                                          + "[DailyQuest] bit NOT NULL ,"
                                          + "[Enable] bit NOT NULL ,"
                                          + "[InstAcc] bit NOT NULL ,"
                                          + "[NeedLevel] bit NOT NULL ,"
                                          + "[MinLevel] smallint NOT NULL ,"
                                          + "[MaxLevel] smallint NOT NULL ,"
                                          + "[NeedNPC] bit NOT NULL ,"
                                          + "[StartingNPC] int NOT NULL ,"
                                          + "[NeedItem] bit NOT NULL ,"
                                          + "[ItemID] int NOT NULL ,"
                                          + "[ItemVanish] bit NOT NULL ,"
                                          + "[NeedPreed] smallint NOT NULL ,"
                                          + "[Predesessor] smallint NOT NULL ,"
                                          + "[NeedClass] bit NOT NULL ,"
                                          + "[ClassType] smallint NOT NULL ,"
                                          + "[InstHand] smallint NOT NULL ,"
                                          + "[RequireLevel] smallint NOT NULL ,"
                                          + "[FinishLevel] smallint NOT NULL ) ON[PRIMARY]";

        public const string QuestMobs = "IF OBJECT_ID('[QuestMob]', 'U') IS NULL CREATE TABLE [dbo].[QuestMob] ("
                                         + "[QuestID] int NOT NULL ,"
                                         + "[IsEnabled] bit NOT NULL ,"
                                         + "[MobID] int NOT NULL ,"
                                         + "[IsKillable] bit NOT NULL ,"
                                         + "[Amount] smallint NOT NULL ) ON[PRIMARY]";

        public const string QuestItem = "IF OBJECT_ID('[QuestItem]', 'U') IS NULL CREATE TABLE [dbo].[QuestItem] ("
                                          + "[QuestID] int NOT NULL ,"
                                          + "[IsEnabled] bit NOT NULL ,"
                                          + "[Type] smallint NOT NULL ,"
                                          + "[ItemID] int NOT NULL ,"
                                          + "[Amount] int NOT NULL ) ON[PRIMARY] ";

        public const string QuestItemDrop = "IF OBJECT_ID('[QuestItemDrop]', 'U') IS NULL CREATE TABLE [dbo].[QuestItemDrop] ("
                                            + "[QuestID] int NOT NULL ,"
                                            + "[Amount] int NOT NULL ,"
                                            + "[MobID] int NOT NULL ,"
                                            + "[IsEnabled] int NOT NULL ,"
                                            + "[ItemID] int NOT NULL ,"
                                            + "[Rate] int NOT NULL ,"
                                            + "[MinDrop] int NOT NULL ,"
                                            + "[MaxDrop] int NOT NULL ,"
                                            + "[Unk] int NOT NULL ) ON[PRIMARY] ";

        public const string QuestRewards = "IF OBJECT_ID('[QuestReward]', 'U') IS NULL CREATE TABLE [dbo].[QuestReward] ("
                                            + "[QuestID] int NOT NULL ,"
                                            + "[Method] smallint NOT NULL ,"
                                            + "[Type] smallint NOT NULL ,"
                                            + "[ItemID]  int NOT NULL ,"
                                            + "[Amount] int NOT NULL ) ON[PRIMARY]";

        public const string QuestScript = "IF OBJECT_ID('[QuestScript]', 'U') IS NULL CREATE TABLE[dbo].[QuestScript] ("
                                            + "[StartScript] varchar(1) COLLATE Latin1_General_CI_AS NOT NULL,"
                                            + "[ActionScript] varchar(1) COLLATE Latin1_General_CI_AS NOT NULL,"
                                            + "[FinishScript] varchar(1) COLLATE Latin1_General_CI_AS NOT NULL,"
                                            + "[QuestID] int NOT NULL ) ON[PRIMARY]";
    }
}