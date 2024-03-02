using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class SceneLoader: MonoBehaviour
    {
        private MainMenuManager _mainMenuManager;
        
        private void Awake()
        {
            _mainMenuManager = MainMenuManager.instanceMMM;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void LoadFightingScene()
        {
            SceneManager.LoadScene("FightingScene");
        }

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}