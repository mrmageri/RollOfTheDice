using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instanceMMM;
        
    public bool playerCanMove = false;
    public int currentLevel = 1;
        
    private string _fightingStr = "FightingScene";
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
        currentLevel = SaveSystem.SaveSystem.LoadLevelData() == null ? 1 : SaveSystem.SaveSystem.LoadLevelData().currentLevel;
    }

    private void Start()
    {
        if(SaveSystem.SaveSystem.LoadLevelData() != null)
        //if (currentLevel != 0)
        {
            loadButton.interactable = true;
        }
    }

    public void ClearGameFiles()
    {
        SaveSystem.SaveSystem.DeleteAllData();
        currentLevel = 1;
    }


        
    private void ActivateTransition()
    {
        transitionObj.SetActive(true);
        transitionAnimator.SetBool("DownScene",true);
    }

    public void LoadFightingScene()
    {
        SaveSystem.SaveSystem.SaveLevelData(currentLevel,new int[0]);
        ActivateTransition();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}