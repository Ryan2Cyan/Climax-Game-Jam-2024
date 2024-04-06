using UnityEngine;

namespace General
{
    public class GameplayManagerMediator : MonoBehaviour
    {
        public void SetState_Integer(int state)
        {
            GameplayManager.Instance.SetState_Integer(state);
        }
    }
}
