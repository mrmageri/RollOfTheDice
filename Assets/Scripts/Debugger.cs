using System;
using System.Collections;
using System.Collections.Generic;
using Dices;
using Enemies;
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
    private EnemiesSpawner _enemiesSpawner;

    private void Awake()
    {
        _diceManager = DiceManager.instanceDm;
        _enemiesSpawner = EnemiesSpawner.instanceES;
    }

    public void GetText()
    {
        if (inputField.text.Contains("give ")){ GiveDice(FindNumber()); inputField.text = ""; return;}
        if (inputField.text.Contains("clear")){ ClearData(); inputField.text = ""; return;}
        if(inputField.text.Contains("del")){if(FindNumber() - 1 < _diceManager.dices.Count)_diceManager.DeleteDice(FindNumber() - 1); inputField.text = ""; BackpackMenuManager.instanceBMM.UpdateDice(); return;}
        if(inputField.text.Contains("open")){_diceManager.OpenDice(FindNumber()); inputField.text = ""; return;}
        if(inputField.text.Contains("enemy")){if(FindNumber() - 1 < _enemiesSpawner.enemiesPool.Count && FindNumber() >= 0)SpawnEnemy(FindNumber()); inputField.text = "";}
        if(inputField.text.Contains("boss")){if(FindNumber() - 1 < _enemiesSpawner.bossesPool.Count && FindNumber() >= 0)SpawnBoss(FindNumber()); inputField.text = "";}
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

    private void SpawnEnemy(int num)
    {
        _enemiesSpawner.comingEnemies.Add(_enemiesSpawner.enemiesPool[num]);
    }
    
    private void SpawnBoss(int num)
    {
        _enemiesSpawner.comingBoss = _enemiesSpawner.bossesPool[num];
        _enemiesSpawner.bossIsComing = true;
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
