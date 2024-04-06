using UnityEngine;

namespace General
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;
        public bool Paused;

        // States:
        private IGameplayState _currentState;
        public BootingUpGameplayState BootingUpState;
        public MainMenuGameplayState MainMenuState;
        public SettingsGameplayState SettingsState;
        public StartGameplayState StartState;
        public PlayingGameplayState PlayState;
        public PauseGameplayState PauseState;
        public GameOverGameplayState GameOverState;
        
        
        
        #region UnityFunctions

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Paused = false;
            _currentState = BootingUpState;
            _currentState.OnStart(this);
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
