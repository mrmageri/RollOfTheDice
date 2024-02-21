using Enemies;
using UnityEngine;
namespace Dices.DiceEffect
{
    public class UraniumDice : DiceEffect
    {
        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
            PoisonAllEnemies(enemy.damageToTake);
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }

        private void PoisonAllEnemies(int dmg)
        {
            foreach (var elem in _enemiesSpawner.enemiesOnScene)
            {
                if (!elem.poisonImmune)
                {
                    elem.SetPoison(dmg);
                }
            }
        }
    }
}