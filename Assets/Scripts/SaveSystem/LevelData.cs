namespace SaveSystem
{
    [System.Serializable]
    public partial class LevelData
    {
        public int[] enemiesIDs;
        public int currentLevel = 0;
        public int backgroundID;
        public int levelEnemiesPoints;
        public LevelData( int[] newEnemiesIds, int newCurrentLevel, int newBackground, int newLevelEnemiesPoints)
        {
            currentLevel = newCurrentLevel;
            enemiesIDs = newEnemiesIds;
            backgroundID = newBackground;
            levelEnemiesPoints = newLevelEnemiesPoints;
        }
    }
}
