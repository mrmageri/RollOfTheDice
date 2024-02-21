using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class IVDice : DiceEffect
    {

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
            _player.HealingPlayer(enemy.damageToTake);
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

