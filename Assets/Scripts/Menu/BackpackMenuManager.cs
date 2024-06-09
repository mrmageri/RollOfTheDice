using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Dices;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

namespace Menu
{
    public class BackpackMenuManager : MonoBehaviour
    {
        [SerializeField] private Transform backpackMenuTransform;
        [SerializeField] private Transform fightMenuTransform;
        [SerializeField] private GameObject cameraGameObject;
        [SerializeField] private GameObject transitionCloser;
        public UnityEvent onEndOfTransitionToBackpack;
        public UnityEvent onEndOfTransitionToFight;
        
        [Header("Backpack inventory")]
        [SerializeField] private Transform backpackUITransform;
        public DiceBackpackItem diceItem;
        public TMP_Text diceNameText;
        public TMP_Text diceDescriptionText;
        public Dice newDice;
        
        public DiceBackpackItem firstDiceToSwitch;

        
        [Header("Backpack player")]
        [SerializeField] private Animator playerBackpackAnimator;
        [SerializeField] private SpriteRenderer playerBackpackDiceSprite;
        [SerializeField] private Button addDiceButton;
        [SerializeField] private GameObject trashButton;
        [SerializeField] private GameObject exitButton;
        [SerializeField] private UnityEvent toMenu;


        public float transitionTime;
        public bool gotNewDice = false;
        public bool switchingDices = false;
        private readonly List<DiceBackpackItem> _diceBackpackItems =new List<DiceBackpackItem>();

        private bool _isInBackpack = false;
        private float _previousTimeScale;
        private DiceManager _diceManager;
        private GameManager _gameManager;
        private bool _isOnTransition = false;
        private bool _loaded = false;


        [Header("ERROR")] 
        [SerializeField] private Sprite error;

        public static BackpackMenuManager instanceBMM;

        BackpackMenuManager()
        {
            instanceBMM = this;
        }
        
        public void Awake()
        {
            diceNameText.text = null;
            diceDescriptionText.text = null;
            _diceManager = DiceManager.instanceDm;
            _gameManager = GameManager.instanceGm;
            LoadingBackpackItems();
        }

        private void Start()
        {
            LoadingBackpackItems();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_isOnTransition && !_gameManager.fightEnded)
            {
                GoToBackpack();
            }
        }

        public void GoToBackpack()
        {
            StartCoroutine(Transition());
            if (!_loaded)
            {
                LoadingBackpackItems();
                _loaded = true;
            }
        }

        private IEnumerator Transition()
        {
            _isOnTransition = true;
            transitionCloser.SetActive(true);
            if (_gameManager.fightEnded && _isInBackpack)
            {
                _gameManager.ToFightTransition();
            }

            if (!_isInBackpack)
            {
                _diceManager.DeactivateDices();
                _previousTimeScale = Time.timeScale;
            }
            Time.timeScale = 0f;
            
            yield return new WaitForSecondsRealtime(transitionTime / 2);
            if (!_isInBackpack)
            {
                cameraGameObject.transform.position = backpackMenuTransform.position;
                onEndOfTransitionToBackpack.Invoke();
                _isInBackpack = true;
            }
            else
            {
                cameraGameObject.transform.position = fightMenuTransform.position;
                onEndOfTransitionToFight.Invoke();
                Time.timeScale = _previousTimeScale;
                _isInBackpack = false;
            }    
            
            yield return new WaitForSecondsRealtime(transitionTime / 2);
            _isOnTransition = false;
            transitionCloser.SetActive(false);
        }
        //Transition to menu

        public void UpdateDice()
        {
            LoadingBackpackItems();
        }

        public void UploadDiceItemText(string name, string description)
        {
            foreach (var elem in _diceBackpackItems)
            {
                elem.clicks = 0;
                elem.Highlight(false);
            }
            diceNameText.text = name;
            diceDescriptionText.text = description;
        }
        //Updates text about dice
        public void SetNewDice()
        {
            GoToBackpack();
            newDice = _diceManager.GetRandomDice();
            if (newDice == null)
            {
                playerBackpackDiceSprite.sprite = error;
                playerBackpackAnimator.SetBool("TookDice", true);
                return;
            }
            gotNewDice = true;
            UploadDiceItemText(newDice.diceName, newDice.description);
            if(_diceManager.dices.Count < 4) addDiceButton.gameObject.SetActive(true);
            playerBackpackDiceSprite.sprite = newDice.readyDiceSprite;
            playerBackpackAnimator.SetBool("TookDice", true);
        }

        public void NewDiceWasSet(DiceBackpackItem diceBi)
        {
            gotNewDice = false;
            playerBackpackAnimator.SetBool("TookDice", false);
            playerBackpackDiceSprite.gameObject.SetActive(false);
            _diceManager.AddDice(diceBi.thisDiceId);
            UploadDiceItemText(" ", " ");
            trashButton.SetActive(false);
            addDiceButton.gameObject.SetActive(false);
            exitButton.SetActive(true);
        }
        //Sets new dice in dice manager and deactivates diceItems

        public void AddDiceButtonAction()
        {
            DiceBackpackItem objDiceItem = Instantiate(diceItem, backpackUITransform.position, Quaternion.identity, backpackUITransform);
            _diceBackpackItems.Add(objDiceItem);
            objDiceItem.LoadDice(newDice.readyDiceSprite,newDice.diceName,newDice.description, newDice.id);
            NewDiceWasSet(objDiceItem);
            addDiceButton.gameObject.SetActive(false);
        }
        //Add dice button

        public void DeleteNewDice()
        {
            gotNewDice = false;
            addDiceButton.gameObject.SetActive(false);
            playerBackpackAnimator.SetBool("TookDice", false);
            playerBackpackDiceSprite.gameObject.SetActive(false);
            UploadDiceItemText(" ", " ");
            newDice = null;
        }
        //Trash button

        public void ReplaceOldDice(DiceBackpackItem item, int id)
        {
            gotNewDice = false;
            playerBackpackAnimator.SetBool("TookDice", false);
            playerBackpackDiceSprite.gameObject.SetActive(false);
            _diceManager.ReplaceDice(_diceBackpackItems.IndexOf(item),id);
            UploadDiceItemText(" ", " ");
            trashButton.SetActive(false);
            addDiceButton.gameObject.SetActive(false);
            exitButton.SetActive(true);
        }

        public Dice SwitchDices(DiceBackpackItem item)
        {
            switchingDices = false;
            if(item == firstDiceToSwitch) return null;
            _diceManager.SwitchDices(_diceBackpackItems.IndexOf(firstDiceToSwitch),firstDiceToSwitch.thisDiceId,_diceBackpackItems.IndexOf(item),item.thisDiceId);
            _diceManager.ReloadDices();
            int tmpItemId = firstDiceToSwitch.thisDiceId;
            Dice dice = _diceManager.dicesPool[item.thisDiceId];
            firstDiceToSwitch.LoadDice(dice.readyDiceSprite, dice.diceName, dice.description,dice.id);
            firstDiceToSwitch = null;
            return _diceManager.dicesPool[tmpItemId];
        }

        public void TurnOffHighlights()
        {
            foreach (var elem in _diceBackpackItems)
            {
                elem.Highlight(false);
            }
        }

        private void LoadingBackpackItems()
        {
            if (_diceBackpackItems.Count > 0)
            {
                foreach (var elem in _diceBackpackItems)
                {
                    Destroy(elem.gameObject);
                    Destroy(elem);
                }
                _diceBackpackItems.Clear();
            }
            foreach (var elem in _diceManager.dices)
            {
                DiceBackpackItem objDiceItem = Instantiate(diceItem, backpackUITransform.position, Quaternion.identity, backpackUITransform);
                objDiceItem.LoadDice(elem.readyDiceSprite,elem.diceName,elem.description, elem.id);
                _diceBackpackItems.Add(objDiceItem);
            }
        }
        
    }
}
