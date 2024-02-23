using UnityEngine;

namespace Enemies.EnemyAreas
{
    public class BombFlyExplosion : MonoBehaviour
    {
        [SerializeField] private int damage = 3;
        private void OnTriggerEnter(Collider other)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            newEnemy.TakingDamage(damage);
        }
    }
}