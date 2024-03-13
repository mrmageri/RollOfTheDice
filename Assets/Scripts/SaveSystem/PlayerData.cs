using System.Collections.Generic;

namespace SaveSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int playerHealth;
        public int maxPlayerHealth;
        public int[] dicesIDs;
        public ulong score;
        
        public PlayerData(int playerHP,int maxPlayerHP, int[] currentDicesIDs, ulong currentScore)
        {
            playerHealth = playerHP;
            maxPlayerHealth = maxPlayerHP;
            dicesIDs = currentDicesIDs;
            score = currentScore;
        }
    }
}
