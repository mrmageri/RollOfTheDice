using System;
using Enemies;
using UnityEngine;

namespace Dices
{
    public class OldBatteryDiceExplosion : DiceArea
    {
        public int damage;
        public CircleCollider2D collider2D;
        protected override void OnEnterEvent(Enemy enemy)
        {
            enemy.TakingDamage(damage);
        }
    }
}
