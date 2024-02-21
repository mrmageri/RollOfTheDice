using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
    public class LevelScrObject : ScriptableObject
    {
        public int backgroundID;
        public int thisLevel;
        public int[] enemiesIDs;
        public int levelEnemiesPoints;
    }
}

