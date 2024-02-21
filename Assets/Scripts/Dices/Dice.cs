using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dices
{
    public enum ReloadingType
    {
       OnKill = 0, 
       OnTime = 1, 
       OnGettingHit = 2, 
       SacrificeHealth = 3,
       OneUse = 4
    }
    
    public enum Rarity
    {
        Ugh = 5,
        Common = 4,
        Rare = 3,
        Epic = 2,
        Legendary = 1
    }

    public enum OnPlayerHealthEffect
    {
        None = 0,
        Glass = 1,
        HealthUp = 5,
        HealthDown = -5
    }
    public class Dice : MonoBehaviour
    {
        [Header("Information")]
        public int id = 0;
        public string diceName;
        public string description;
        public bool isOpened = false;
        public Rarity diceRarity;
        public OnPlayerHealthEffect onPlayerHealthEffect;
        
        [Header("Reloading")]
        public ReloadingType diceReloadingType;
        public int reloadingPoints = 0;
        public int maxReloadingPoints;
        public bool isActive;
        public bool isReady;
        
        [Header("Dice Effect")]
        public DiceEffect.DiceEffect diceEffect;
        
        [Header("Dice Shower")]
        public Sprite diceShowerSprite;
        public DiceResultShower diceShower;
        
        [Header("Dice Appearance")]
        public Sprite onReloadDiceSprite;
        public Sprite readyDiceSprite;
        public Image spriteRenderer;
        public Image reloadingIcon;
        public Button diceButton;
        
        [Header("Dice text")]
        public TMP_Text counterText;
        private DiceManager _diceManager;
        private Player.Player _player;


        private void Awake()
        {
            _diceManager = DiceManager.instanceDm;
            _player = Player.Player.instancePlayer;
            diceEffect.dice = this;
        }

        private void Start()
        {
            CounterTextUpdate();
            if(diceReloadingType == ReloadingType.OneUse) UpdateReloadingStats();
        }
        
        public void OnClick()
        {
            if (diceReloadingType == ReloadingType.SacrificeHealth && !isActive) _diceManager.UpdateSacrificeHealthReloadingPoints(maxReloadingPoints,this);
            if (diceReloadingType == ReloadingType.SacrificeHealth && isActive) Player.Player.instancePlayer.HealingPlayer(maxReloadingPoints);
            if(!_player.isAlive) return;
            if (!isReady) return;
            _diceManager.previousDice = _diceManager.currentDice;
            _diceManager.currentDice = this;
            _diceManager.DeactivatePreviousDice();
            //First time click
            if (!isActive)
            {
                isActive = true;
                Time.timeScale = _player._slowTimeScale;
                if( _diceManager.previousDice != _diceManager.currentDice && _diceManager.previousDice) _diceManager.currentDice.diceEffect.Effect();
                _player.TakingDice(readyDiceSprite, isActive);
                spriteRenderer.fillAmount = 0f;
                // diceEffect.Effect() activates in Player at the end of "TookDice" animation
            }
            else
            {
                isActive = false;
                Time.timeScale = default;
                _player.TakingDice(null, isActive);
                spriteRenderer.fillAmount = 1f;
                diceEffect.EffectDeactivate();
                _diceManager.currentDice = null;
            }
            //second time click
        }

        //Used in DiceManager to DeactivatePreviousDices
        public void DeactivateDice()
        {
            isActive = false;
            _player.TakingDice(null, isActive);
            if(isReady) spriteRenderer.fillAmount = 1f;
            diceEffect.EffectDeactivate();
        }
        //Used after the dice effect was fishied
        public void EndDiceEffect()
        {
            isActive = false;
            isReady = false;
            _player.TakingDice(null, isActive);
            reloadingPoints = 0;
            CounterTextUpdate();
            _diceManager.currentDice = null;
            spriteRenderer.fillAmount = 0f;
        }

        public void ShowingDiceResult(Transform spawnTransform, int result)
        {
            DiceResultShower diceResultShower = Instantiate(diceShower,spawnTransform.position,Quaternion.identity);
            //DiceResultShower diceResultShower = diceResult.GetComponent<DiceResultShower>();
            diceResultShower.UpdateDiceLook(result, diceShowerSprite);
        }

        public void SetReloadIcon(Sprite sprite)
        {
            reloadingIcon.sprite = sprite;
        }

        //Compares dice reloading type
        public bool IsValidType(ReloadingType currentReloading)
        {
            return(diceReloadingType == currentReloading);
        }

        public void AddingReloadingPoints(int newPoints = 1)
        {
            if (reloadingPoints < maxReloadingPoints)
            {
                reloadingPoints += newPoints;
                float currentR = reloadingPoints;
                float maxR = maxReloadingPoints;
                float radiant = currentR/maxR;
                spriteRenderer.fillAmount = radiant;
                CounterTextUpdate();
                if (reloadingPoints >= maxReloadingPoints)
                {
                    isReady = true;
                    spriteRenderer.fillAmount = 1f;
                }
            }
        }

        public void UpdateReloadingStats()
        {
            CounterTextUpdate();
            if (reloadingPoints >= maxReloadingPoints)
            {
                isReady = true;
                spriteRenderer.fillAmount = 1f;
            }
        }

        private void CounterTextUpdate()
        {

            {
                if (diceReloadingType == ReloadingType.OneUse)
                {
                    counterText.text = "";
                }
                else if (diceReloadingType == ReloadingType.SacrificeHealth)
                {
                    counterText.text = maxReloadingPoints.ToString();
                }
                else
                {
                    counterText.text = reloadingPoints + " / " + maxReloadingPoints;
                }
            }
        }
    }
}
