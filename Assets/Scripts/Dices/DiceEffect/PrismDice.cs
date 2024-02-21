using UnityEngine;
using Enemies;

namespace Dices.DiceEffect
{
    public class PrismDice : DiceEffect
    {
        [SerializeField] private PrismArea prismArea;

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
            PrismArea newArea = prismArea;
            newArea.damage = damage == 1 ? 1 : damage / 2;
            newArea.targetEnemyObj = targetEnemy.gameObject;
            newArea.starY = targetEnemy.transform.position.y;
            Instantiate(newArea.thisArea, targetEnemy.transform.position, Quaternion.identity);
        }
    }
}