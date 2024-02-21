using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class DoubleDamageStatus : EnemyStatus
    {
        protected new int duration = 8;
        public override void Effect(Enemy enemy)
        {
            if(!isOnStatus) return;
            if (startTurn == 0)
            {
                if (maxDiceRes < 6) duration /= 2;
                startTurn = TurnManager.instanceTM.turnNumber;
                endTurn = startTurn + duration;
            }

            if (TurnManager.instanceTM.turnNumber < endTurn)
            {
                enemy.damageMultiplier = 2;
            }
            else
            {
                enemy.damageMultiplier = 1;
                enemy.doubleDamageEffect.SetActive(false);
                isOnStatus = false;
            }
        }
    }
}