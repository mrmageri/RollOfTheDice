using Enemies;
using UnityEditor;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class CerealDice : DiceEffect
    {
        public GameObject cerealParticle;
        public new void EffectDeactivate()
        {
            Time.timeScale = 1f;
            _enemiesSpawner.HighlightEnemies(false);
            _enemiesSpawner.EnemiesClickable(false);
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            switch (Random.Range(0,5))
            {
                case 0:
                    enemy.SetFire(upToDamage);
                    break;
                case 1:
                    enemy.SetFreezed(upToDamage);
                    break;
                case 2:
                    enemy.SetPoison(upToDamage);
                    break;
                case 3:
                    enemy.SetSticky(upToDamage);
                    break;
                case 4:
                    enemy.SetWater(upToDamage);
                    break;
            }
            enemy.isAttackTarget = false;
            enemy.StatusesUpdate();
            SpawnParticle(enemy);
            enemy.TakingDamage(enemy.damageToTake);
        }

        public override int GetDiceDamage(Enemy enemy)
        {
            _player.ThrowingDice(enemy.transform.position, enemy, dice);
            EffectDeactivate();
            dice.EndDiceEffect();
            return (Random.Range(fromDamage, upToDamage + 1));
        }
        
        private void SpawnParticle(Enemy targetEnemy)
        {
            Instantiate(cerealParticle, targetEnemy.transform.position, Quaternion.identity);
        }
    }
}