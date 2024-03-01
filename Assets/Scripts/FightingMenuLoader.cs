using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dices;
using Enemies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightingMenuLoader : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private int[] defaultDiceIDs = new int[] {0,1};
    [SerializeField] private int[] startDiceIDs;
     private Player.Player _player;
     private EnemiesSpawner _enemiesSpawner;
     private DiceManager _diceManager;
     private GameManager _gameManager;


     private void Awake()
     {
         transitionAnimator.SetBool("Up",true);
          _player = Player.Player.instancePlayer;
          _diceManager = DiceManager.instanceDm;
          _enemiesSpawner = EnemiesSpawner.instanceES;
          _gameManager = GameManager.instanceGm;
     }
     
     public void Start()
     {
         if (SaveSystem.SaveSystem.LoadPlayerData() == null)
         {
             _player.maxPlayerHealth = _player.defaultMaxHealth;
             _player.playerHealth = _player.maxPlayerHealth;
             _diceManager.LoadDices(defaultDiceIDs);
         } 
         else 
         {
             _player.maxPlayerHealth = SaveSystem.SaveSystem.LoadPlayerData().maxPlayerHealth;
             _player.playerHealth = SaveSystem.SaveSystem.LoadPlayerData().playerHealth;
             _diceManager.LoadDices(SaveSystem.SaveSystem.LoadPlayerData().dicesIDs);
         }
         if (SaveSystem.SaveSystem.LoadLevelData() != null)
         {
             _enemiesSpawner.levelEnemiesPoints = SaveSystem.SaveSystem.LoadLevelData().levelEnemiesPoints;
             _enemiesSpawner.AddEnemies(SaveSystem.SaveSystem.LoadLevelData().enemiesIDs);
             _gameManager.currentLevelNumber = SaveSystem.SaveSystem.LoadLevelData().currentLevel;
         }

         if (SaveSystem.SaveSystem.LoadDiceData() == null)
         {
             foreach (var dice in _diceManager.dicesPool)
             {
                 dice.isOpened = false;
             }

             foreach (var elem in startDiceIDs)
             {
                 _diceManager.OpenDice(elem);
             }
         }
         else
         {
             int dicePoolLenght = _diceManager.dicesPool.Count;
             if (SaveSystem.SaveSystem.LoadDiceData().diceInfo.Length < dicePoolLenght) --dicePoolLenght;
             for (int i = 0; i < dicePoolLenght; i++)
             {
                 _diceManager.dicesPool[i].isOpened = SaveSystem.SaveSystem.LoadDiceData().diceInfo[i];
             }
         }
     }
    
     
}
