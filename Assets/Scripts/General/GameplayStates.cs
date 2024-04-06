using UnityEngine;

namespace General
{
    public interface IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager);
        public void OnUpdate(GameplayManager gameplayManager);
        public void OnEnd(GameplayManager gameplayManager);
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
    }
    
    public class StartGameplayState : IGameplayState
    {
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
    }
}
