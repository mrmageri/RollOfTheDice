using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class WaterStatus : EnemyStatus
    {
        protected int duration = 6;
        private int damage = 5;

        public override void Effect(Enemy enemy)
        {
            if(!isOnStatus) return;
            if (startTurn == 0)
            {
                if (enemy.damageToTake == maxDiceRes) damage *= 2;
                if (maxDiceRes < 6) duration /= 2;
                startTurn = TurnManager.instanceTM.turnNumber;
                endTurn = startTurn + duration;
            }
            
            if (TurnManager.instanceTM.turnNumber < endTurn)
            {
                if (enemy.fireStatus.isOnStatus)
                {
                    enemy.fireStatus.SetStatus(false,0);
                    enemy.fireEffect.SetActive(false);
                }
                if (enemy.stickyStatus.isOnStatus)
                {
                    enemy.stickyStatus.SetStatus(false,0);
                    enemy.stickyEffect.SetActive(false);
                }
                if (enemy.poisonStatus.isOnStatus)
                {
                    enemy.poisonStatus.SetStatus(false,0);
                    enemy.poisonEffect.SetActive(false);
                    enemy.TakingDamage(damage);
                    endTurn = TurnManager.instanceTM.turnNumber - 1;
                }
                if (enemy.electroStatus.isOnStatus)
                {
                    enemy.electroStatus.SetStatus(false,0);
                    enemy.electroEffect.SetActive(false);
                    enemy.TakingDamage(damage);
                    endTurn = TurnManager.instanceTM.turnNumber - 1;
                }
            }
            else
            {
                enemy.waterEffect.SetActive(false);
                isOnStatus = false;
            }
        }
    }
}