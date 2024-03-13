using System;
using Dices;
using Levels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanceGm;
    public int currentLevelNumber = 0;
    public bool fightEnded = false;
    
        
    GameManager()
    {
        instanceGm = this;
    }

    [Header("LevelInfo")] 
    [SerializeField] private int maxLevelNUmber = 20;
    
    [SerializeField] private UnityEvent onGettingNewDice;
    [SerializeField] private UnityEvent onDeath;
    [Header("Transition")] 
    [SerializeField] private Animator transitionAnimator;

    private DiceManager _diceManager;
    private Player.Player _player;

    private void Awake()
    {
        _diceManager = DiceManager.instanceDm;
        _player = Player.Player.instancePlayer;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void ToFightTransition()
    {
        transitionAnimator.gameObject.SetActive(true);
        transitionAnimator.SetBool("DownScene", true);
    }

    public void ToMainMenu()
    {
        transitionAnimator.gameObject.SetActive(true);
        transitionAnimator.SetBool("DownMain", true);
    }

    public void SavePlayerData()
    {
        SaveSystem.SaveSystem.SavePlayerData(_player.health,_player.maxHealth, _diceManager.dicesIDs, ScoreManager.instanceSm.score);
    }

    public void SaveLevelData()
    {
        SaveSystem.SaveSystem.SaveLevelData(currentLevelNumber);
    }

    public void Winning()
    {
        fightEnded = true;
        if (currentLevelNumber % 3 == 0)
        {
            if (currentLevelNumber + 1 < maxLevelNUmber)
            {
                currentLevelNumber++;
                SaveSystem.SaveSystem.SaveLevelData(currentLevelNumber);
            }
            else
            {
                ToMainMenu();
            }
            onGettingNewDice.Invoke();
            _diceManager.DeactivateDicesButtons();
            Time.timeScale = 0;
        }
        else
        {
            if (currentLevelNumber + 1 < maxLevelNUmber)
            {
                currentLevelNumber++;
                SaveSystem.SaveSystem.SaveLevelData(currentLevelNumber);
            }
            else
            {
                ToMainMenu();
            }
            SavePlayerData();
            ToFightTransition();
            _diceManager.DeactivateDicesButtons();
            Time.timeScale = 0;
        }
    }

    public void Death()
    {
        fightEnded = true;
        SaveSystem.SaveSystem.DeleteAllData();
        onDeath.Invoke();
        _diceManager.DeactivateDicesButtons();
        Time.timeScale = 0;
    }
}
