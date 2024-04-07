using System;
using Inputs;
using UnityEngine;

namespace General
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;
        public GameplayState StartingState;
        public float SpellChangeInterval;
        public float SpellChangeTimer;
        public bool Paused;
        public bool DebugActive;

        // States:
        private IGameplayState _currentState;
        public readonly BootingUpGameplayState BootingUpState = new();
        public readonly MainMenuGameplayState MainMenuState = new();
        public readonly SettingsGameplayState SettingsState = new();
        public readonly StartGameplayState StartState = new();
        public readonly PlayingGameplayState PlayState = new();
        public readonly PauseGameplayState PauseState = new();
        public readonly GameOverGameplayState GameOverState = new();

        public enum GameplayState
        {
            BootingUp = 0,
            MainMenu = 1,
            Settings = 2,
            Start = 3,
            Playing = 4,
            Pause = 5,
            GameOver = 6
        }
        
        #region UnityFunctions

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Paused = false;

            _currentState = StartingState switch
            {
                GameplayState.BootingUp => BootingUpState,
                GameplayState.MainMenu => MainMenuState,
                GameplayState.Settings => SettingsState,
                GameplayState.Start => StartState,
                GameplayState.Playing => PlayState,
                GameplayState.Pause => PauseState,
                GameplayState.GameOver => GameOverState,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnEnable()
        {
            InputManager.OnPause += OnPause;
        }

        private void OnDisable()
        {
            InputManager.OnPause -= OnPause;
        }

        private void Update()
        {
            if (Paused) return;
            _currentState.OnUpdate(this);
        }

        #endregion

        #region PublicFunctions

        public void SetState_Integer(int state)
        {
            SetState_Event((GameplayState)state);
        }
        
        public void SetState_Event(GameplayState state)
        {
            switch (state)
            {
                case GameplayState.BootingUp:
                {
                    SetState(BootingUpState); 
                } break;
                case GameplayState.MainMenu:
                {
                    SetState(MainMenuState); 
                } break;
                case GameplayState.Settings:
                {
                    SetState(SettingsState);
                } break;
                case GameplayState.Start:
                {
                    SetState(StartState);
                } break;
                case GameplayState.Playing:
                {
                    SetState(PlayState);   
                } break;
                case GameplayState.Pause:
                {
                    SetState(PauseState);
                } break;
                case GameplayState.GameOver:
                {
                    SetState(GameOverState);
                } break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        public void SetState(IGameplayState state)
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= _currentState.OnSceneLoaded;
            _currentState.OnEnd(this);
            _currentState = state;
            _currentState.OnStart(this);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += _currentState.OnSceneLoaded;
        }

        #endregion

        #region PrivateFunctions

        private void OnPause()
        {
            _currentState.OnPause(this);
        }

        #endregion
    }
}
