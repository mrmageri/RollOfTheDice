using System;
using Enemies;
using UnityEngine;

namespace Dices
{
    public class PrismArea : DiceArea
    {
        [HideInInspector]public int damage;
        public float starY = 0;

        protected override void OnEnterEvent(Enemy enemy)
        {
            if (Math.Abs(enemy.transform.position.y - starY) > -0.25 &&
                Math.Abs(enemy.transform.position.y - starY) < 0.25) enemy.TakingDamage(damage);
        }
    }
}
