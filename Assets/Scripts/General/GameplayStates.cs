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
    
}
