using System;
using System.Collections.Generic;


namespace IuvoUnity
{
    namespace DataStructs
    {
        [Serializable]
        public class GameData
        {
            public string Name;
            public string CurrentLevelName;
            public int CurrentLevelIndex;
            List<string> LevelNames;

            public int TotalPlayTimeInSeconds;
            public int TotalDeaths;
            public int TotalEnemiesDefeated;
            public int TotalItemsCollected;
            public int TotalQuestsCompleted;
            public int TotalSaveCount;
            // add other progression metrics as needed
        }

        public class LevelData
        {
            public string LevelName; // Name of the level
            public int LevelIndex; // Index of the level
            public int TotalPlayTimeInSeconds;

            // public List<Character> Enemies; // List of Characters in the level

            // public List<Item> Items; // List of items in the level

            // public List<Quest> Quests; // List of quests in the level
            public int TotalLives; // Total number of lives available in the level
            public StopwatchTimer LevelTimer; // Timer for the level

            public int EnemiesDefeated; // Number of enemies defeated in the level
            public int ItemsCollected; // Number of items collected in the level
            public int QuestsCompleted; // Number of quests completed in the level
            public int Deaths; // Number of deaths in the level

            public int TimesCompleted; // Number of times the level has been completed
            public int TimesFailed; // Number of times the level has been failed
            public float BestCompletionTime; // Best completion time for the level
            public float AverageCompletionTime; // Average completion time for the level
        }

            public class SaveDataBase
        {
            public string SaveName;
            public DateTime SaveDate;
            public GameData GameData;
        }
    }
}