namespace SaveSystem
{
    [System.Serializable]
    public partial class LevelData
    {
        public int[] enemiesIDs;
        public int currentLevel = 0;
        public int levelEnemiesPoints;
        public LevelData( int[] newEnemiesIds, int newCurrentLevel, int newLevelEnemiesPoints)
        {
            currentLevel = newCurrentLevel;
            enemiesIDs = newEnemiesIds;
            levelEnemiesPoints = newLevelEnemiesPoints;
        }
    }
}
