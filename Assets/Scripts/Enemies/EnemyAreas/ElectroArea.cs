using UnityEngine;

namespace Enemies.EnemyAreas
{
    public class ElectroArea : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 2f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            newEnemy.speed *= speedMultiplier;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            newEnemy.speed /= speedMultiplier;
        }
    }
}