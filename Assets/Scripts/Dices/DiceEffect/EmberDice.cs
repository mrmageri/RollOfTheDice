using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public class EmberDice : DiceEffect
    {
        [SerializeField] private GameObject fireEffect;
        [SerializeField] private Color fireColor;


        public new void Effect()
        {
            fireEffect.SetActive(true);
            _enemiesSpawner.HighlightEnemies(true);
            _enemiesSpawner.EnemiesClickable(true);
        }
        
        public new void EffectDeactivate()
        {
            fireEffect.SetActive(false);
            Time.timeScale = 1f;
            _enemiesSpawner.HighlightEnemies(false);
            _enemiesSpawner.EnemiesClickable(false);
        }

        public override void OnEnemyEffect(Enemy enemy)
        {
            enemy.SetColor(fireColor);
            enemy.TakingDamage(enemy.damageToTake);
            enemy.isAttackTarget = false;
            enemy.SetFire(upToDamage);
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
