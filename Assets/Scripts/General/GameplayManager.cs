using UnityEngine;

namespace General
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;
        public bool Paused;

        private IGameplayState _currentState;

        #region UnityFunctions

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Paused = false;
        }

        #endregion

        #region PublicFunctions

        public void SetState(IGameplayState state)
        {
            _currentState.OnEnd(this);
            _currentState = state;
            _currentState.OnStart(this);
        }

        #endregion
    }
}
