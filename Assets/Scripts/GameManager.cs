using Dices;
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
    
    public void ToMapTransition()
    {
        transitionAnimator.gameObject.SetActive(true);
        transitionAnimator.SetBool("DownMap", true);
    }

    public void SavePlayerData()
    {
        SaveSystem.SaveSystem.SavePlayerData(_player.playerHealth,_player.maxPlayerHealth, _diceManager.dicesIDs);
    }

    public void SaveLevelData()
    {
        SaveSystem.SaveSystem.SaveLevelData(null, currentLevelNumber, 0,0);
    }

    public void Winning()
    {
        fightEnded = true;
        if (currentLevelNumber % 3 == 0)
        {
            SaveSystem.SaveSystem.SaveLevelData(null, ++currentLevelNumber, 0,0);
            onGettingNewDice.Invoke();
            _diceManager.DeactivateDicesButtons();
            Time.timeScale = 0;
        }
        else
        {
            SaveSystem.SaveSystem.SaveLevelData(null, ++currentLevelNumber, 0,0);
            SavePlayerData();
            ToMapTransition();
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
