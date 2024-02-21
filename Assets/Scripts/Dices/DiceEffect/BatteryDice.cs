using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class BatteryDice : DiceEffect
    {
        public new void Effect()
        {
            _enemiesSpawner.HighlightEnemies(true);
            _enemiesSpawner.EnemiesClickable(true);
        }
        
        public new void EffectDeactivate()
        {
            Time.timeScale = 1f;
            _enemiesSpawner.HighlightEnemies(false);
            _enemiesSpawner.EnemiesClickable(false);
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
            enemy.SetElectro(upToDamage);
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }
    }
}