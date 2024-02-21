using System.Collections.Generic;

namespace SaveSystem
{
    [System.Serializable]
    public class PlayerData
    {
        public int playerHealth;
        public int maxPlayerHealth;
        public int[] dicesIDs;
        
        public PlayerData(int playerHP,int maxPlayerHP, int[] currentDicesIDs)
        {
            playerHealth = playerHP;
            maxPlayerHealth = maxPlayerHP;
            dicesIDs = currentDicesIDs;
        }
    }
}
