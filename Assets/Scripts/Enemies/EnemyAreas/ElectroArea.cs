using UnityEngine;

namespace Enemies.EnemyAreas
{
    public class ElectroArea : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier = 2f;
        private void OnTriggerEnter(Collider other)
        {
           if(other.TryGetComponent(out Enemy enemy))
               enemy.speed *= speedMultiplier;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out Enemy enemy)) 
                enemy.speed /= speedMultiplier;
        }
    }
}