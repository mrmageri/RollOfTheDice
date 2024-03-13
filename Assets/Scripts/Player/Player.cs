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
        
        public int health  = 20;
        public int maxHealth = 20;
        public TMP_Text healthText;

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
        public void ThrowingDice(Vector3 enemyPosition, Enemy enemy,Dice dice)
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
            if (health + heal > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += heal;
            }
            UpdateTextData();
        }

        public void SetMaxPlayerHealth(int newMaxHealth)
        {
            if (health == maxHealth)
            {
                health = newMaxHealth;
                
            }
            else
            {
                float mh = maxHealth;
                float h = health;
                float part = h / mh;
                health = (int) (part * newMaxHealth);
            }
            maxHealth = newMaxHealth;
            if(health > maxHealth) health = maxHealth;
            UpdateTextData();
        }
        
    
        public void TakingDamage(int damage)
        {
            _diceManager.UpdateOnGettingHitReloadingPoints(damage);
            if (health - damage <= 0)
            {
                health = 0;
                UpdateTextData();
                _gameManager.Death();
                isAlive = false;
            }
            else
            {
                health -= damage;
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
            healthText.text = health.ToString();
            float cur = health;
            float max = maxHealth;
            float fill = cur / max;
            mainBar.fillAmount = fill;
        }
    }
}
