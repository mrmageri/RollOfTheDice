using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class SceneLoader: MonoBehaviour
    {
        private MainMenuManager _mainMenuManager;
        [SerializeField] private UnityEvent _onDisable;
        [SerializeField] private UnityEvent _onCheck;

        private void Awake()
        {
            _mainMenuManager = MainMenuManager.instanceMMM;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            _onDisable.Invoke();
        }

        public void CheckOnStart()
        {
            _onCheck.Invoke();
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