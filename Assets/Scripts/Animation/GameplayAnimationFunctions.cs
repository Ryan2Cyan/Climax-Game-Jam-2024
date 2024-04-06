using General;
using UnityEngine;

namespace Animation
{
    public class GameplayAnimationFunctions : MonoBehaviour
    {
        public void ChangeGameplayState(int state)
        {
            GameplayManager.Instance.SetState_Event((GameplayManager.GameplayState)state);
        }
    }
}
