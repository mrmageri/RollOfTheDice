using System;
using Dices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Menu
{
    public class DiceBackpackItem : MonoBehaviour
    {
        
        [HideInInspector] public string diceName;
        [HideInInspector] public string diceDescription;
        [HideInInspector] public int thisDiceId;
        [SerializeField] public Image diceItemImage;
        private BackpackMenuManager _backpackMenuManager;
        [SerializeField] private Animator _animator;
        public int clicks = 0;

        public void Awake()
        {
            _backpackMenuManager = BackpackMenuManager.instanceBMM;
        }

        public void LoadDice(Sprite newImage, string newName, string newDescription, int newDiceId)
        {
            diceItemImage.sprite = newImage;
            diceName = newName;
            diceDescription = newDescription;
            thisDiceId = newDiceId;
        }

        public void Highlight(bool setActive = true)
        {
            _animator.SetBool("isScared",setActive);
        }

        public void OnClick()
        {
            if (_backpackMenuManager.gotNewDice && clicks == 1)
            {
                Highlight(false);
                Dice newDice = _backpackMenuManager.newDice;
                LoadDice(newDice.readyDiceSprite, newDice.diceName, newDice.description, newDice.id);
                _backpackMenuManager.ReplaceOldDice(this,thisDiceId);
                clicks = 0;
            } else if (!_backpackMenuManager.gotNewDice && clicks == 1 && !_backpackMenuManager.switchingDices)
            {
                Highlight();
                _backpackMenuManager.switchingDices = true;
                _backpackMenuManager.firstDiceToSwitch = this;
                clicks = 0;
            } else if (_backpackMenuManager.switchingDices)
            {
                Dice dice = _backpackMenuManager.SwitchDices(this);
                _backpackMenuManager.TurnOffHighlights();
                if(dice == null) return;
                LoadDice(dice.readyDiceSprite, dice.diceName, dice.description,dice.id);
            }
            else
            {
                _backpackMenuManager.UploadDiceItemText(diceName,diceDescription);
                clicks++;
                if (!_backpackMenuManager.gotNewDice) return;
                Highlight();
            }
        }
    }
}
