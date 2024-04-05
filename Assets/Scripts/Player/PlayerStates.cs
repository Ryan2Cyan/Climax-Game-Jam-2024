namespace Player
{
    public abstract class PlayerStates
    {
        public interface IPlayerSpellState
        {
            public void OnStart(PlayerManager player);
            public void OnUpdate(PlayerManager player);
            public void OnAttack(PlayerManager player);
            public void OnDamaged(PlayerManager player);
            public void OnEnd(PlayerManager player);
        }

        public class ArcaneWeaponPlayerState : IPlayerSpellState
        {
            public void OnStart(PlayerManager player)
            {
          
            }

            public void OnUpdate(PlayerManager player)
            {
          
            }

            public void OnAttack(PlayerManager player)
            {
                
            }

            public void OnDamaged(PlayerManager player)
            {
                
            }

            public void OnEnd(PlayerManager player)
            {
          
            }
        }
    }
}
