using System;
using UnityEngine;

namespace General
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;
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
            _currentState = BootingUpState;
            _currentState.OnStart(this);
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
            _currentState.OnEnd(this);
            _currentState = state;
            _currentState.OnStart(this);
        }

        #endregion
    }
}
