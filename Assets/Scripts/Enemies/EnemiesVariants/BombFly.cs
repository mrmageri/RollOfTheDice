using UnityEngine;

namespace Enemies.EnemiesVariants
{
    public class BombFly : Enemy
    {
        [SerializeField] private GameObject explosionParticle;
        [SerializeField] private GameObject explosion;
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
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            DamagingPlayer();
            _enemiesSpawner.RemoveEnemy(this);
            Destroy(gameObject);
            //if(!animator.GetBool("isFighting")) animator.SetBool("isFighting", true);
        }
        

        protected override void EventOnDeathExtra()
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
        protected override void onHitEffect()
        {
        
        }
    }
}