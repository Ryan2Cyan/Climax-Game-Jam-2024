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
            
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            
        }
    }
}
