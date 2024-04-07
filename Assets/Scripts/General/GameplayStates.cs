using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public interface IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager);
        public void OnUpdate(GameplayManager gameplayManager);
        public void OnEnd(GameplayManager gameplayManager);
        void OnSceneLoaded(Scene scene, LoadSceneMode mode);
        void OnPause(GameplayManager gameplayManager);
    }
    
    public class BootingUpGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>BootUp</b>");

            
        }

        public void OnUpdate(GameplayManager gameplayManager) { }
        public void OnEnd(GameplayManager gameplayManager) { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class MainMenuGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Main Menu</b>");
            SceneManager.ChangeScene(SceneManager.Scene.MainMenu);

            gameplayManager.audioManager.Play("MainMenuMusic");
            gameplayManager.audioManager.StopPlaying("GameplayMusic");

        }

        public void OnUpdate(GameplayManager gameplayManager) { }
        public void OnEnd(GameplayManager gameplayManager) { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class SettingsGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Settings</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager) { }
        public void OnEnd(GameplayManager gameplayManager) { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class StartGameplayState : IGameplayState
    {
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Start</b>");
            SceneManager.ChangeScene(SceneManager.Scene.Game);

            gameplayManager.audioManager.Play("GameplayMusic");
            gameplayManager.audioManager.StopPlaying("MainMenuMusic");
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = true; 
            PlayerManager.Instance.Animator.SetBool(Hide, false);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = false;
            PlayerManager.Instance.Animator.SetBool(Hide, true);
        }

        public void OnPause(GameplayManager gameplayManager)
        {
            gameplayManager.SetState(gameplayManager.MainMenuState);
        }
    }
    
    public class PlayingGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Playing</b>");
            WaveManager.Instance.ToggleActive(true);
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = true;
            WaveManager.Instance.ToggleActive(false);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

        public void OnPause(GameplayManager gameplayManager)
        {
            gameplayManager.SetState(gameplayManager.PauseState);
        }
    }
    
    public class PauseGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Pause</b>");
            gameplayManager.Paused = true;
            UIManager.Instance.PauseScreen.SetActive(true);
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager)
        {
            gameplayManager.Paused = false;
            UIManager.Instance.PauseScreen.SetActive(false);
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

        public void OnPause(GameplayManager gameplayManager)
        {
            gameplayManager.SetState(gameplayManager.PlayState);
        }
    }
    
    public class GameOverGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>GameOver</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

        public void OnPause(GameplayManager gameplayManager) { }
    }
}
