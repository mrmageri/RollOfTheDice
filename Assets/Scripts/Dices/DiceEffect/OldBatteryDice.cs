using Dices.DiceAreas;
using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class OldBatteryDice : DiceEffect
    {
        [SerializeField] private OldBatteryDiceExplosion batteryDiceExplosion;

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
            Explosion(enemy.damageToTake, enemy);
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }

        private void Explosion(int damage, Enemy targetEnemy)
        {
            OldBatteryDiceExplosion newExplosion = batteryDiceExplosion;
            newExplosion.damage = damage;
            newExplosion.targetEnemyObj = targetEnemy.gameObject;
            if (damage == upToDamage) newExplosion.collider.radius = 6f;
            Instantiate(newExplosion.thisArea, targetEnemy.transform.position, Quaternion.identity);
        }
    }
}
