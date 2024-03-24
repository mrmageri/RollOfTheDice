using System.Collections;
using Dices;
using Enemies.EnemyStatuses;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Health")] 
        public float health = 5;
    
        [Header("Damage")] 
        public int enemyDamage = 1;
        
        [Header("Cost")] 
        public int enemyCost;

        [Header("Moving")] 
        public float speed;
        public Transform targetTransform;
        public bool isMovingBack = false;

        [Header("Statuses")]
        public readonly FireStatus fireStatus = new FireStatus();
        public readonly PoisonStatus poisonStatus = new PoisonStatus();
        public readonly FreezedStatus freezedStatus = new FreezedStatus();
        public readonly StickyStatus stickyStatus = new StickyStatus();
        public readonly PunchStatus punchStatus = new PunchStatus();
        public readonly WaterStatus waterStatus = new WaterStatus();
        public readonly DoubleDamageStatus doubleDamageStatus = new DoubleDamageStatus();
        public readonly ElectroStatus electroStatus = new ElectroStatus();
        
        [Header("Color Render")] 
        public Renderer [] entityRenders;
        public Color defaultColor;
        public Color damageColor;
        public Animator animator;
        public GameObject fireEffect;
        public GameObject poisonEffect;
        public GameObject freezingEffect;
        public GameObject stickyEffect;
        public GameObject punchedEffect;
        public GameObject waterEffect;
        public GameObject doubleDamageEffect;
        public GameObject electroEffect;

        [Header("Particle")] [SerializeField] 
        private GameObject bloodParticle;

        [Header("Effect Immune")]
        public bool poisonImmune;

        public GameObject enemyHighlight;
        public GameObject enemyPrefab;
        [HideInInspector]public int damageToTake;
        public bool isAttackTarget = false;
        public bool isClickable = false;

        [HideInInspector] public int damageMultiplier = 1;

        private Player.Player _player;
    
    
        private DiceManager _diceManager;
        protected EnemiesSpawner _enemiesSpawner;

        private void Awake()
        {
            _player = Player.Player.instancePlayer;
            _enemiesSpawner = EnemiesSpawner.instanceES;
            _diceManager = DiceManager.instanceDm;
        }


        private void Update()
        {
            Moving();
        }
        

        public void TakingDamage(int damage)
        {
            health -= (damage * damageMultiplier);
            StartCoroutine(ChangingColor());
            if (health <= 0)
            {
                _diceManager.UpdateOnKillReloadingPoints();
                EventOnDeath();
                EventOnDeathExtra();
                _enemiesSpawner.RemoveEnemy(this);
                Destroy(gameObject);
            }
        }

        public void StatusesUpdate()
        {
            if (fireStatus.isOnStatus)
            {
                fireStatus.Effect(this);
            }
            if(poisonStatus.isOnStatus)
            {
                poisonStatus.Effect(this);
            }
            if(freezedStatus.isOnStatus)
            {
                freezedStatus.Effect(this);
            }
            if (stickyStatus.isOnStatus)
            {
                stickyStatus.Effect(this);
            }
            if (punchStatus.isOnStatus)
            {
                punchStatus.Effect(this);
            }
            if (waterStatus.isOnStatus)
            {
                waterStatus.Effect(this);
            }
            if (doubleDamageStatus.isOnStatus)
            {
                doubleDamageStatus.Effect(this);
            }
            if (electroStatus.isOnStatus)
            {
                electroStatus.Effect(this);
            }
        }

        public void SetFire(int upDamage)
        {
            fireStatus.SetStatus(true, upDamage);
            fireEffect.SetActive(true);
        }

        public void SetPoison(int upDamage)
        {
            poisonStatus.SetStatus(true, upDamage);
            poisonEffect.SetActive(true);
        }
        
        public void SetFreezed(int upDamage)
        {
            freezedStatus.SetStatus(true, upDamage);
            freezingEffect.SetActive(true);
        }
        public void SetSticky(int upDamage)
        {
            stickyStatus.SetStatus(true, upDamage);
            stickyEffect.SetActive(true);
        }

        public void SetPunched(int upDamage)
        {
            punchStatus.SetStatus(true, upDamage);
            punchedEffect.SetActive(true);
        }

        public void SetWater(int upDamage)
        {
            waterStatus.SetStatus(true,upDamage);
            waterEffect.SetActive(true);
        }

        public void SetDoubleDamage(int upDamage)
        {
            doubleDamageStatus.SetStatus(true,upDamage);
            doubleDamageEffect.SetActive(true);
        }

        public void SetElectro(int upDamage)
        {
            electroStatus.SetStatus(true,upDamage);
            electroEffect.SetActive(true);
        }

        //PunchedEffect
        protected void MovingBack()
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

        public void SetColor(Color color)
        {
            foreach (var elem in entityRenders)
            {
                elem.material.color = color;
            }
        }
        private void OnMouseDown()
        {
            if (!isClickable) return;
            if(_diceManager.currentDice == null) return;
            isAttackTarget = true;
            //We need "damageTOTake" to damage enemy then dice hits it, not on click
            damageToTake = _diceManager.currentDice.diceEffect.GetDiceDamage(this);
        }


        private IEnumerator ChangingColor()
        {
            Color previousColor = entityRenders[0].material.color != defaultColor ? entityRenders[0].material.color : defaultColor;
            SetColor(damageColor);
            yield return new WaitForSeconds(0.2f);
            SetColor(previousColor);
            }

        protected void DamagingPlayer()
        {
            _player.TakingDamage(enemyDamage);
        }
        
        protected abstract void Moving();
        
        //default moving
        /* if(isAttackTarget) return;
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
            if(!animator.GetBool("isFighting")) animator.SetBool("isFighting", true);*/

        
        private void EventOnDeath()
        {
            ScoreManager.instanceSm.AddScore(enemyCost);
            Instantiate(bloodParticle, transform.position, Quaternion.identity);
        }
        protected void EventOnDeathExtra()
        {
            //is realised in child code
        }
    }
}
