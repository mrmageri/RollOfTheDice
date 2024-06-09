using Dices.DiceAreas;
using UnityEngine;
using Enemies;

namespace Dices.DiceEffect
{
    public class BoxDice : DiceEffect
    {
        [SerializeField] private BoxArea boxAreaObject;
        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.TakingDamage(enemy.damageToTake);
            AreaSpawning(enemy.damageToTake, enemy);
            enemy.isAttackTarget = false;
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position,enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }
        private void AreaSpawning(int damage, Enemy targetEnemy)
        {
            BoxArea newArea = boxAreaObject;
            newArea.targetEnemyObj = targetEnemy.gameObject;
            //newArea.upDamage = upToDamage;
            if (damage == upToDamage) newArea.collider.radius = 4f;
            Instantiate(newArea.thisArea, targetEnemy.transform.position, Quaternion.identity);
        }
    }
}