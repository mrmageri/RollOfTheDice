using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class CutletDice : DiceEffect
    {

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
            _player.HealingPlayer(Healing(enemy.damageToTake));
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }

        private int Healing(int res)
        {
            if (res <= 2)
            {
                return 1;
            } else if (res <= 4)
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }
    }
}
