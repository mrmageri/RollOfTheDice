using System;
using Enemies;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Dices
{
    public class BoxArea : DiceArea
    {
        [SerializeField] private Dice[] dices;
        public GameObject throwableDicePrefab;
        public CircleCollider2D collider2D;
        protected override void OnEnterEvent(Enemy enemy)
        {
            int rand = Random.Range(0, dices.Length);
            
            GameObject currentDice = Instantiate(throwableDicePrefab, transform.position, Quaternion.identity);
            currentDice.GetComponent<SpriteRenderer>().sprite = dices[rand].readyDiceSprite;
            PlayerDice currentPDice = currentDice.GetComponent<PlayerDice>();
            StartCoroutine(currentPDice.Curve(currentPDice.transform.position, enemy.transform.position,enemy,dices[rand]));
            enemy.damageToTake = Random.Range(dices[rand].diceEffect.fromDamage, dices[rand].diceEffect.upToDamage + 1);
        }
    }
}