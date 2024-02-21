using UnityEngine;

namespace Enemies.EnemiesVariants
{
    public class PoopFly : Enemy
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
            int rand = Random.Range(1, 4);
            for (int i = 0; i < rand; i++)
            {
                int randSpawnPoint = Random.Range(0, _enemiesSpawner.spawnPoint.Length);
                _enemiesSpawner.SpawnEnemy(transform, EnemiesID.PoopTinyFly.GetHashCode(), randSpawnPoint);
            }
        }
    }
}
