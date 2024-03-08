namespace SaveSystem
{
    [System.Serializable]
    public partial class LevelData
    {
        public int currentLevel = 0;
        public LevelData(int newCurrentLevel)
        {
            currentLevel = newCurrentLevel;
        }
    }
}
