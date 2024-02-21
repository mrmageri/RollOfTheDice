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
        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = LevelManager.instanceLM;
        }

        private void Start()
        {
            if (_levelInfo.thisLevel != _levelManager.currentLevel)
            {
                _interactable = false;
            }
        }
        
        private void OnMouseDown()
        {
            if(!_interactable) return;
            
            _levelManager.SetPlayerMove();
            SetData();
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        private void SetData()
        {
            SaveSystem.SaveSystem.SaveLevelData(_levelInfo.enemiesIDs,_levelInfo.thisLevel,_levelInfo.backgroundID,_levelInfo.levelEnemiesPoints);
        }
    
    }
}
