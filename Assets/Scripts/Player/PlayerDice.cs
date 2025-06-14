using System.Collections;
using Dices;
using Enemies;
using UnityEngine;

namespace Player
{
    public class PlayerDice : MonoBehaviour
    {
        public AnimationCurve curve;

        public Enemy targetEnemy;

        [SerializeField] private float duration = 1.0f;

        [SerializeField] private float heightY = 3.0f;

        public IEnumerator Curve(Vector3 start, Vector2 target, Enemy enemy,Dice dice)
        {
            targetEnemy = enemy;
            float timePassed = 0f;

            Vector2 end = target;

            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;

                float linearT = timePassed / duration;
                float heightT = curve.Evaluate(linearT);
                float height = Mathf.Lerp(0f, heightY, heightT);

                transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);
            
                yield return null;
            }

            if (targetEnemy)
            {
                dice.diceEffect.OnEnemyEffect(enemy);
                dice.ShowingDiceResult(transform,targetEnemy.damageToTake);
            }
            Destroy(gameObject);
        }
    }
}

