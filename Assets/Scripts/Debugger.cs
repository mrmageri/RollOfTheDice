using System;
using System.Collections;
using System.Collections.Generic;
using Dices;
using Menu;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    private bool _isActive = true;

    [Header("DebuggerMenu")] 
    private bool _menuIsActive = false;
    [SerializeField] private GameObject debuggerMenu;
    [SerializeField] private TMP_InputField inputField;

    private DiceManager _diceManager;

    private void Awake()
    {
        _diceManager = DiceManager.instanceDm;
    }

    public void GetText()
    {
        if (inputField.text.Contains("give ")){ GiveDice(FindNumber()); inputField.text = ""; return;}
        if (inputField.text.Contains("cdt")){ ClearData(); inputField.text = ""; return;}
        if(inputField.text.Contains("delete")){if(FindNumber() - 1 < _diceManager.dices.Count)_diceManager.DeleteDice(FindNumber() - 1); inputField.text = ""; BackpackMenuManager.instanceBMM.UpdateDice(); return;}
        if(inputField.text.Contains("open")){_diceManager.OpenDice(FindNumber()); inputField.text = ""; return;}
        inputField.text = "";
    }
    
    private void Update()
    {
        if(!_isActive) return;
        if (!Input.GetKeyDown("`")) return;
        _menuIsActive = !_menuIsActive;
        debuggerMenu.SetActive(_menuIsActive);
        Time.timeScale = _menuIsActive == false ? 1 : 0;
    }

    private int FindNumber()
    {
        string number = "";
        for (int i = 0; i < inputField.text.Length;i++)
        {
            if (Char.IsDigit(inputField.text[i]) && (inputField.text[i-1] == ' ' || Char.IsDigit(inputField.text[i-1])))
            {
                number += inputField.text[i];
            }
        }

        return number == "" ? -1 : Convert.ToInt32(number);
    }

    private void ClearData()
    {
        SaveSystem.SaveSystem.DeleteAllData();
    }
    private void GiveDice(int id)
    {
        if(id == -1) return;
        _diceManager.AddDice(id);
        BackpackMenuManager.instanceBMM.UpdateDice();
    }
}
