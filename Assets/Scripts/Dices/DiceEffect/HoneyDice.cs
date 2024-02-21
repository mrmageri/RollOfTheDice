using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class HoneyDice : DiceEffect
    {

        public new void EffectDeactivate()
        {
            Time.timeScale = 1f;
            _enemiesSpawner.HighlightEnemies(false);
            _enemiesSpawner.EnemiesClickable(false);
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.SetSticky(upToDamage);
            enemy.isAttackTarget = false;
            enemy.StatusesUpdate();
            enemy.TakingDamage(enemy.damageToTake);
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position, enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }
    }
}
