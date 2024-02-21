using UnityEngine;

namespace Levels
{
    public class LevelMenuPlayer : MonoBehaviour
    {
        public GameObject transitionObj;
        public Animator transitionAnimator;

        public void ActivateTransition()
        {
            transitionObj.SetActive(true);
            transitionAnimator.SetBool("DownScene",true);
        }
    }
}
