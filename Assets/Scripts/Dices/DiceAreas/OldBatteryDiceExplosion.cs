using Enemies;
using UnityEngine;

namespace Dices.DiceAreas
{
    public class OldBatteryDiceExplosion : DiceArea
    {
        public int damage;
        public SphereCollider collider;
        protected override void OnEnterEvent(Enemy enemy)
        {
            enemy.TakingDamage(damage);
        }
    }
}
