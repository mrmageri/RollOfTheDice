using UnityEngine;

namespace Enemies.EnemiesVariants
{
    public class TiredFly : Enemy
    {
        private Vector3 _target;
        [SerializeField] private float _hight = 2f;
        private void Start()
        {
            var position = targetTransform.position;
            position = new Vector3(position.x,position.y + _hight, position.z);
            _target = position;
        }

        protected override void Moving()
        {
            if (transform.position != _target)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
                if(animator.GetBool("isFighting")) animator.SetBool("isFighting", false);
                return;
            }
            if (isMovingBack)
            {
                MovingBack();
                return;
            }
            if(!animator.GetBool("isFighting")) animator.SetBool("isFighting", true);
        }
        protected override void EventOnDeathExtra()
        {
            
        }
    }
}
