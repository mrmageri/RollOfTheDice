using UnityEngine;
using Enemies;

namespace Dices
{
    public class DorbluArea : DiceArea
    {
        public int upDamage = 0;
        [SerializeField] private Color poisonColor;
        public CircleCollider2D collider2D;
        protected override void OnEnterEvent(Enemy enemy)
        {
            if (enemy.poisonImmune) return;
            enemy.SetColor(poisonColor);
            enemy.SetPoison(upDamage);
        }
    }
}