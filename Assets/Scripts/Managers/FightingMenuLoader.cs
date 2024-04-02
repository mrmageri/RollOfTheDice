using Dices;
using Enemies;
using UnityEngine;



public class FightingMenuLoader : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private int[] defaultDiceIDs = new int[] {0};
    [SerializeField] private int[] startDiceIDs;
    private Player.Player _player;
    private EnemiesSpawner _enemiesSpawner;
    private DiceManager _diceManager;
    private GameManager _gameManager;
    private ScoreManager _scoreManager;


    private void Awake()
    {
            Time.timeScale = 1;
            transitionAnimator.SetBool("Up",true);
            _player = Player.Player.instancePlayer;
            _diceManager = DiceManager.instanceDm;
            _enemiesSpawner = EnemiesSpawner.instanceES;
            _gameManager = GameManager.instanceGm;
            _scoreManager = ScoreManager.instanceSm;
            
            //Loading data...
            
            if (SaveSystem.SaveSystem.LoadPlayerData() == null)
            {
                _player.maxHealth = _player.defaultMaxHealth;
                _player.health = _player.maxHealth;
                _diceManager.LoadDices(defaultDiceIDs);
                _scoreManager.score = 0;
            } 
            else 
            {
                _player.maxHealth = SaveSystem.SaveSystem.LoadPlayerData().maxPlayerHealth;
                _player.health = SaveSystem.SaveSystem.LoadPlayerData().playerHealth;
                _diceManager.LoadDices(SaveSystem.SaveSystem.LoadPlayerData().dicesIDs);
                _scoreManager.score = SaveSystem.SaveSystem.LoadPlayerData().score;
            }
            if (SaveSystem.SaveSystem.LoadLevelData() != null)
            {
                _gameManager.currentLevelNumber = SaveSystem.SaveSystem.LoadLevelData().currentLevel;
                var cLevel = _gameManager.currentLevelNumber;
                if (_gameManager.currentLevelNumber % 5 == 0)
                {
                    _enemiesSpawner.AddBoss(_enemiesSpawner.bossesPool.Count);
                    _enemiesSpawner.levelEnemiesPoints = (GameManager.instanceGm.currentLevelNumber / 2 + 2) * 2;
                    _enemiesSpawner.maxEnemiesOnScene /= 2;
                    _enemiesSpawner.bossIsComing = true;
                    GenerateLevel(cLevel);
                }
                else
                {
                    _enemiesSpawner.levelEnemiesPoints = (GameManager.instanceGm.currentLevelNumber / 2 + 2) * 4;
                    GenerateLevel(cLevel);
                }
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
    private void GenerateLevel(int cLevel)
    {
        int enemiesMaxId = 0;
        enemiesMaxId = cLevel <= 3 ?  3 : (cLevel / 6 + 1) * 3;
        _enemiesSpawner.AddEnemies(enemiesMaxId);
    }
    
     
}
