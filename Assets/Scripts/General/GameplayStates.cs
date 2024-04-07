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
    }
    
    public class BootingUpGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>BootUp</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
    
    public class MainMenuGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Main Menu</b>");
            SceneManager.ChangeScene(SceneManager.Scene.MainMenu);
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
    
    public class SettingsGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Settings</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
    
    public class StartGameplayState : IGameplayState
    {
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Start</b>");
            SceneManager.ChangeScene(SceneManager.Scene.Game);
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

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
    }
    
    public class PlayingGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Playing</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = true;
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
    
    public class PauseGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Pause</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
    
    public class GameOverGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>GameOver</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
    }
}
