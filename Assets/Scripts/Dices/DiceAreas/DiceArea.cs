using System;
using Enemies;
using UnityEngine;

namespace Dices
{
    public abstract class DiceArea : MonoBehaviour
    {
        public GameObject thisArea;
        public GameObject targetEnemyObj;

        public void OnEnd()
        {
            Destroy(gameObject);
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != targetEnemyObj)
            {
                Enemy newEnemy = other.GetComponent<Enemy>();
                if (other.GetComponent<Enemy>() != null) OnEnterEvent(newEnemy);
            }
        }

        protected abstract void OnEnterEvent(Enemy enemy);

    }
}
