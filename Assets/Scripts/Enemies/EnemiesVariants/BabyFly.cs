using UnityEngine;

namespace Enemies.EnemiesVariants
{
    public class BabyFly : Enemy
    {
        protected override void Moving()
        {
            if (transform.position != targetTransform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
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

        
        

        protected override void EventOnDeath()
        {
            //throw new System.NotImplementedException();
        }
    }
}
