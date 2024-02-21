using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class PunchStatus : EnemyStatus
    {
        protected int duration = 1;
        private float _enemyStartSpeed = 0;
        public override void Effect(Enemy enemy)
        {
            if(!isOnStatus) return;
            if (startTurn == 0)
            {
                if (maxDiceRes < 6) duration /= 2;
                _enemyStartSpeed = enemy.speed;
                startTurn = TurnManager.instanceTM.turnNumber;
                endTurn = startTurn + duration;
                if (enemy.damageToTake == maxDiceRes)
                {                
                    enemy.speed = -enemy.speed * 8;
                    enemy.isMovingBack = true;
                }
                else
                {
                    enemy.speed = -enemy.speed * 4;
                    enemy.isMovingBack = true;
                }
                enemy.animator.speed = 0;
            }


            if (TurnManager.instanceTM.turnNumber != endTurn) return;
            enemy.isMovingBack = false;
            enemy.punchedEffect.SetActive(false);
            enemy.speed = _enemyStartSpeed;
            enemy.animator.speed = 1;
            startTurn = 0;
            isOnStatus = false;
        }
    }
}