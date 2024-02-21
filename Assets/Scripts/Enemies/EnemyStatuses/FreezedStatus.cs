using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class FreezedStatus : EnemyStatus
    {
        protected int duration = 4;
        private float _enemyStartSpeed = 0;
        
        public override void Effect(Enemy enemy)
        {
            
            if(!isOnStatus) return;
            if (startTurn == 0)
            {
                if (maxDiceRes < 6) duration /= 2;
                _enemyStartSpeed = enemy.speed;
                startTurn = TurnManager.instanceTM.turnNumber;
                if (enemy.damageToTake == maxDiceRes)
                {
                    endTurn = startTurn + duration + duration / 2;
                }
                else
                {
                    endTurn = startTurn + duration;
                }

                enemy.animator.speed = 0;
                enemy.speed = 0;
            }


            if (TurnManager.instanceTM.turnNumber != endTurn) return;
            enemy.freezingEffect.SetActive(false);
            enemy.speed = _enemyStartSpeed;
            enemy.animator.speed = 1;
            startTurn = 0;
            isOnStatus = false;
            
        }
        
    }
}