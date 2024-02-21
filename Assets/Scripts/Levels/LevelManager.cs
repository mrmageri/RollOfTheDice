using System;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instanceLM;
        
        public bool playerCanMove = false;
        public int currentLevel = 0;
        
        [SerializeField] private Sprite completedLevelSprite;
        [SerializeField] private LevelButtonLoader[] levelButtonLoaders;
        [SerializeField] private GameObject player;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private Color completeLevelColor;
        [SerializeField] private Color nextLevelColor;
        [SerializeField] private float playerSpeed = 3;
        private string fightingStr = "FightingScene";
        public GameObject transitionObj;
        public Animator transitionAnimator;
        
        LevelManager()
        {
            instanceLM = this;
        }

        private void Awake()
        {
            Time.timeScale = 1;
            if (SaveSystem.SaveSystem.LoadLevelData() == null)
            {
                currentLevel = 0;
            }
            else
            {
                currentLevel = SaveSystem.SaveSystem.LoadLevelData().currentLevel;
                if (currentLevel != 0)
                {
                    player.transform.position = levelButtonLoaders[currentLevel - 1].transform.position;
                }
            }
        }

        private void Start()
        {

            SetCompletedLevels();
        }

        private void Update()
        {
            if(playerCanMove) MovingSmallPlayer();
        }

        public void SetPlayerMove()
        {
            if (currentLevel == 0)
            {
                ActivateTransition();
                //levelButtonLoaders[currentLevel].LoadScene(fightingStr);
            }
            else
            {
                playerCanMove = true;
            }
        }
        
        public void ActivateTransition()
        {
            transitionObj.SetActive(true);
            transitionAnimator.SetBool("DownScene",true);
        }

        public void LoadFightingScene()
        {
            levelButtonLoaders[currentLevel].LoadScene(fightingStr);
        }

        private void SetCompletedLevels()
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
        }

        private void MovingSmallPlayer()
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
        }
    }
}
