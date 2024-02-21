using System;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dices.DiceEffect
{
    public class GlassDice : DiceEffect
    {

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
        }
    }
}