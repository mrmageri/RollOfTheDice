using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class DorbluDice : DiceEffect
    {
        [SerializeField] private GameObject poisonEffect;
        [SerializeField] private DorbluArea dorbluAreaObject;
        [SerializeField] private Color poisonColor;


        public new void Effect()
        {
            poisonEffect.SetActive(true);
            _enemiesSpawner.HighlightEnemies(true);
            _enemiesSpawner.EnemiesClickable(true);
        }
        
        public new void EffectDeactivate()
        {
            poisonEffect.SetActive(false);
            Time.timeScale = 1f;
            _enemiesSpawner.HighlightEnemies(false);
            _enemiesSpawner.EnemiesClickable(false);
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            if (!enemy.poisonImmune)
            {
                enemy.SetColor(poisonColor);
                enemy.SetPoison(upToDamage);
            }
            AreaSpawning(enemy.damageToTake, enemy);
            enemy.TakingDamage(enemy.damageToTake);
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
            DorbluArea newArea = dorbluAreaObject;
            newArea.targetEnemyObj = targetEnemy.gameObject;
            newArea.upDamage = upToDamage;
            if (damage == upToDamage) newArea.collider2D.radius = 1.25f;
            Instantiate(newArea.thisArea, targetEnemy.transform.position, Quaternion.identity);
        }
    }
}
