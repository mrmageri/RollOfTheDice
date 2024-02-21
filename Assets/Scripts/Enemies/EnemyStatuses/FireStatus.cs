using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class FireStatus : EnemyStatus
    {
        private int damage = 2;

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
                enemy.fireEffect.SetActive(false);
                enemy.SetColor(enemy.defaultColor);
                isOnStatus = false;
            }
        }
    }
}