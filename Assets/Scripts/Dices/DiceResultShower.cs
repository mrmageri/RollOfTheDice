using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Dices
{
    public class DiceResultShower : MonoBehaviour
    {
        public TMP_Text diceResultText;
        public SpriteRenderer spriteRenderer;
        public float timeTillDestroy;
        public Canvas canvas;

        public void Awake()
        {
            canvas.worldCamera = Camera.main;
        }

        public void UpdateDiceLook(int number, Sprite newSprite)
        {
            diceResultText.text = number <= 20 ? number.ToString() : "∞"; // if(number <= 20) else "∞" 
            if (number < 0) diceResultText.text = " ";
            spriteRenderer.sprite = newSprite;
            StartCoroutine(WaitingToDestroy());
        }

        private IEnumerator WaitingToDestroy()
        {
            yield return new WaitForSeconds(timeTillDestroy);
            Destroy(gameObject);
        }
    }
}
