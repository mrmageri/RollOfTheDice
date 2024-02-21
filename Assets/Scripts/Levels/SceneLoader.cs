using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class SceneLoader: MonoBehaviour
    {
        private LevelManager _levelManager;
        
        private void Awake()
        {
            _levelManager = LevelManager.instanceLM;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public void LoadScene()
        {
            _levelManager.LoadFightingScene();
        }

        public void LoadMapScene()
        {
            SceneManager.LoadScene("MapScene");
        }
    }
}