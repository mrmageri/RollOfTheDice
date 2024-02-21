using Enemies;
using Unity.Mathematics;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class DiceShoes : DiceEffect
    {
        public int damage;
        

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return damage;
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
        }

    }
}
