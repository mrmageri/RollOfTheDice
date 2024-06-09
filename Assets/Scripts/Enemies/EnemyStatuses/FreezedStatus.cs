
namespace Enemies.EnemyStatuses
{
    public class FreezedStatus : EnemyStatus
    {
        private int _duration = 4;
        private float _enemyStartSpeed = 0;
        
        public override void Effect(Enemy enemy)
        {
            
            if(!isOnStatus) return;
            if (startTurn == 0)
            {
                if (maxDiceRes < 6) _duration /= 2;
                _enemyStartSpeed = enemy.speed;
                startTurn = TurnManager.instanceTM.turnNumber;
                if (enemy.damageToTake == maxDiceRes)
                {
                    endTurn = startTurn + _duration + _duration / 2;
                }
                else
                {
                    endTurn = startTurn + _duration;
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