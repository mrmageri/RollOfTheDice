using UnityEngine;

namespace Enemies.EnemyAreas
{
    public class ElectroArea : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 2f;
        private void OnTriggerEnter(Collider other)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            newEnemy.speed *= speedMultiplier;
        }
        
        private void OnTriggerExit(Collider other)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            newEnemy.speed /= speedMultiplier;
        }
    }
}