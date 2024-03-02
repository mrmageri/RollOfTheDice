using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Levels
{
    public class LevelButtonLoader : MonoBehaviour
    {
        public LevelScrObject _levelInfo;
        private bool _interactable = true;
        public SpriteRenderer levelButtonSpriteRenderer;
        [SerializeField] private string loadSceneName;
        private MainMenuManager _mainMenuManager;

        private void Awake()
        {
            _mainMenuManager = MainMenuManager.instanceMMM;
        }

        private void Start()
        {
            if (_levelInfo.thisLevel != _mainMenuManager.currentLevel)
            {
                _interactable = false;
            }
        }
        
        private void OnMouseDown()
        {
            if(!_interactable) return;
            
            //_mainMenuManager.SetPlayerMove();
            SetData();
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        private void SetData()
        {
            SaveSystem.SaveSystem.SaveLevelData(_levelInfo.enemiesIDs,_levelInfo.thisLevel,_levelInfo.levelEnemiesPoints);
        }
    
    }
}
