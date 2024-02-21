using System;
using Dices;
using Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public readonly int defaultMaxHealth = 20;
        
        public int playerHealth  = 20;
        public int maxPlayerHealth = 20;
        public TMP_Text playerHealthText;

        [Header("HealthBar")] 
        [SerializeField] private Image mainBar;
    
        [Header("PlayerDice")]
        public SpriteRenderer playerDiceRenderer;
        public GameObject playerDicePrefab;
        public Transform diceThrowingPoint;
        public bool isAlive = true;

        public Animator animator;
    
        public static Player instancePlayer;
        [SerializeField] public float _slowTimeScale;

        private DiceManager _diceManager;
        private GameManager _gameManager;
    
        Player()
        {
            instancePlayer = this;
        }

        private void Start()
        {
            UpdateTextData();
        }

        private void Awake()
        {
            _gameManager = GameManager.instanceGm;
            _diceManager = DiceManager.instanceDm;
        }

        //Function which is invoked by clicking on enemy, it set the target position for player dice and throws it and sets ene,y to damage
        public void ThrowingDice(Vector2 enemyPosition, Enemy enemy,Dice dice)
        {
            GameObject currentDice = Instantiate(playerDicePrefab, diceThrowingPoint.position, Quaternion.identity);
            currentDice.GetComponent<SpriteRenderer>().sprite = playerDiceRenderer.sprite;
            playerDiceRenderer.sprite = null;
            PlayerDice currentPlayerDice = currentDice.GetComponent<PlayerDice>();
            StartCoroutine(currentPlayerDice.Curve(currentPlayerDice.transform.position, enemyPosition,enemy,dice));
        }
    
        //Animation there player takes dice and puts it above head 
        public void TakingDice(Sprite diceSprite, bool isTookDice)
        {
            playerDiceRenderer.sprite = diceSprite;
            animator.SetBool("TookDice", isTookDice);
        }

        public void HealingPlayer(int heal)
        {
            if (playerHealth + heal > maxPlayerHealth)
            {
                playerHealth = maxPlayerHealth;
            }
            else
            {
                playerHealth += heal;
            }
            UpdateTextData();
        }

        public void SetMaxPlayerHealth(int newMaxHealth)
        {
            maxPlayerHealth = newMaxHealth;
            if(playerHealth > maxPlayerHealth) playerHealth = maxPlayerHealth;
            UpdateTextData();
        }
        
    
        public void TakingDamage(int damage)
        {
            _diceManager.UpdateOnGettingHitReloadingPoints(damage);
            if (playerHealth - damage <= 0)
            {
                playerHealth = 0;
                UpdateTextData();
                _gameManager.Death();
                isAlive = false;
            }
            else
            {
                playerHealth -= damage;
                UpdateTextData();
            } 
        
        }
    
        //Activating dice effect on end of "TookDice" animation
        private void ObserversEnding(string message)
        {
            if (message.Equals("TookDiceEnded"))
            {
                if(_diceManager.currentDice != null) _diceManager.currentDice.diceEffect.Effect();
            }
        }

        private void UpdateTextData()
        {
            playerHealthText.text = playerHealth.ToString();
            float cur = playerHealth;
            float max = maxPlayerHealth;
            float fill = cur / max;
            mainBar.fillAmount = fill;
        }
    }
}
