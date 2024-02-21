using System;
using System.Collections;
using System.Collections.Generic;
using Dices;
using Enemies;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float turnDuration;
    public EnemiesSpawner enemiesSpawner;
    public int turnNumber = 0;

    private DiceManager _diceManager;
    
    public static TurnManager instanceTM;

    TurnManager()
    {
        instanceTM = this;
    }

    public bool highlightIsActive = false;

    private void Awake()
    {
        _diceManager = DiceManager.instanceDm;
    }

    public void Start()
    {
        StartCoroutine(TurnSwitching());
    }

    IEnumerator TurnSwitching()
    {
        yield return new WaitForSeconds(turnDuration);
        NewTurn();
        StartCoroutine(TurnSwitching());
    }

    private void NewTurn()
    {
        ++turnNumber;
        _diceManager.UpdateOnTimeReloadingPoints();
        enemiesSpawner.SpawningEnemies(turnNumber);
        if (enemiesSpawner.enemiesOnScene.Count <= 0) return;
        for (int i = 0; i < enemiesSpawner.enemiesOnScene.Count; i++)
        {
            enemiesSpawner.enemiesOnScene[i].StatusesUpdate();
        }
        //TODO Шкала времени
    }
}
