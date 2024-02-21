using System;
using UnityEngine;

namespace Enemies
{
    public class BombFlyExplosion : MonoBehaviour
    {
        [SerializeField] private int damage = 3;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            newEnemy.TakingDamage(damage);
        }
    }
}