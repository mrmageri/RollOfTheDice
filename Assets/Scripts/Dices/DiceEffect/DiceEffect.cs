using System.Collections;
using Enemies;
using UnityEngine;

namespace Dices.DiceEffect
{
    public abstract class DiceEffect : MonoBehaviour
    {
        public int fromDamage;
        public int upToDamage;
        [HideInInspector]public Dice dice;
        protected EnemiesSpawner _enemiesSpawner;
        protected Player.Player _player;

        private void Start()
        {
            _player = Player.Player.instancePlayer;
            _enemiesSpawner = EnemiesSpawner.instanceES;
        }
        
        public void Effect()
        {
            _enemiesSpawner.HighlightEnemies(true);
            _enemiesSpawner.EnemiesClickable(true);
        }

        public void EffectDeactivate()
        {
            Time.timeScale = 1f;
            _enemiesSpawner.HighlightEnemies(false);
            _enemiesSpawner.EnemiesClickable(false);
        }
        public abstract int GetDiceDamage(Enemy enemy);
        public abstract void OnEnemyEffect(Enemy enemy);
    }
}
