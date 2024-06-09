using UnityEngine;

namespace Enemies.EnemyStatuses
{
    public class StickyStatus : EnemyStatus
    {
        private int _duration = 6;
        private float _divider = 2;
        private float _enemyStartSpeed = 0;
        public override void Effect(Enemy enemy)
        {
            if(!isOnStatus) return;
            if (startTurn == 0)
            {
                if (maxDiceRes < 6) _duration /= 2;
                _enemyStartSpeed = enemy.speed;
                startTurn = TurnManager.instanceTM.turnNumber;
                endTurn = startTurn + _duration;
                if (enemy.damageToTake == maxDiceRes)
                {
                    enemy.speed /= (_divider * 2);
                }
                else
                {
                    enemy.speed /= _divider;
                }
            }


            if (TurnManager.instanceTM.turnNumber != endTurn) return;
            enemy.stickyEffect.SetActive(false);
            enemy.speed = _enemyStartSpeed;
            startTurn = 0;
            isOnStatus = false;
        }
    }
}