using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public abstract class EnemyStatus
    {
        protected int duration = 4;
        protected int startTurn = 0;
        protected int endTurn = 0;
        protected int maxDiceRes = 0;
        public bool isOnStatus = false;

        public void SetStatus(bool newSt, int newDiceMax)
        {
            isOnStatus = newSt;
            maxDiceRes = newDiceMax;
        }

        public abstract void Effect(Enemy enemy);
    }
}
