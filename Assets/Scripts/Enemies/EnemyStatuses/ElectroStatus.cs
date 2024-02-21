using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class ElectroStatus : EnemyStatus
    {
        private int damage = 1;

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
                enemy.TakingDamage(damage);
            }
            else
            {
                enemy.electroEffect.SetActive(false);
                isOnStatus = false;
            }
        }
    }
}