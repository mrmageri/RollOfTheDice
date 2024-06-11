
namespace SaveSystem
{
    [System.Serializable]
    public partial class LevelData
    {
        public int currentLevel = 0;
        public int[] usedBosses;
        public LevelData(int newCurrentLevel, int[] newUsedBosses)
        {
            currentLevel = newCurrentLevel;
            usedBosses = new int[newUsedBosses.Length];
            
            for (int i = 0; i < newUsedBosses.Length; i++)
            {
                usedBosses[i] = newUsedBosses[i];
            }
        }
    }
}
