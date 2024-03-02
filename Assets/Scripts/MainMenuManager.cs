using Levels;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instanceMMM;
        
    public bool playerCanMove = false;
    public int currentLevel = 0;
        
    [SerializeField] private LevelScrObject[] levelScrObjects;
    private string fightingStr = "FightingScene";
    public GameObject transitionObj;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private Button loadButton;
        
    MainMenuManager()
    {
        instanceMMM = this;
    }

    private void Awake()
    {
        Time.timeScale = 1;
        currentLevel = SaveSystem.SaveSystem.LoadLevelData() == null ? 0 : SaveSystem.SaveSystem.LoadLevelData().currentLevel;
    }

    private void Start()
    {
        if (currentLevel != 0)
        {
            loadButton.interactable = true;
        }
    }

    /*private void Start()
        {

            SetCompletedLevels();
        }*/

    /*private void Update()
        {
            if(playerCanMove) MovingSmallPlayer();
        }*/
        
    public void ClearGameFiles()
    {
        SaveSystem.SaveSystem.DeleteAllData();
        currentLevel = 0;
    }

    /*public void SetPlayerMove()
        {
            if (currentLevel == 0)
            {
                ActivateTransition();
            }
            else
            {
                playerCanMove = true;
            }
        }*/
        
    private void ActivateTransition()
    {
        transitionObj.SetActive(true);
        transitionAnimator.SetBool("DownScene",true);
    }

    public void LoadFightingScene()
    {
        LevelScrObject level = levelScrObjects[currentLevel];
        SaveSystem.SaveSystem.SaveLevelData(level.enemiesIDs,level.thisLevel,level.levelEnemiesPoints);
        ActivateTransition();
    }

    /*private void SetCompletedLevels()
        {
            foreach (var elem in levelButtonLoaders)
            {
                if (elem._levelInfo.thisLevel < currentLevel)
                { 
                    elem.levelButtonSpriteRenderer.color = completeLevelColor;
                } 
                else if (elem._levelInfo.thisLevel > currentLevel)
                { 
                    elem.levelButtonSpriteRenderer.color = nextLevelColor;
                }
            }
            if (currentLevel == levelButtonLoaders.Length)
            {
                onWin.Invoke();
            }
        }*/

    /*private void MovingSmallPlayer()
        {
            if (player.transform.position != levelButtonLoaders[currentLevel].transform.position)
            {
                playerAnimator.SetBool("running", true);
                player.transform.position = Vector3.MoveTowards(player.transform.position,levelButtonLoaders[currentLevel].transform.position, playerSpeed * Time.deltaTime);
            }
            else
            {
                playerCanMove = false;
                playerAnimator.SetBool("running", false);
            }
        }*/
}