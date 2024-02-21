using Dices;
using UnityEngine;

namespace SaveSystem
{
    [System.Serializable]
    public class DicesData
    {
        public bool[] diceInfo;
        
        public DicesData(bool[] newDiceInfo)
        {
            diceInfo = newDiceInfo;
        }
    }
}