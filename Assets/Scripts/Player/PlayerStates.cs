using UnityEngine;

namespace Player
{
    public abstract class PlayerStates
    {
        private static readonly int ArcaneWeapon = Animator.StringToHash("ArcaneWeapon");

        public interface IPlayerSpellState
        {
            public void OnStart(PlayerManager player);
            public void OnUpdate(PlayerManager player);
            public void OnAttack(PlayerManager player);
            public void OnDamaged(PlayerManager player, int damage);
            public void OnEnd(PlayerManager player);
        }

        public class ArcaneWeaponPlayerState : IPlayerSpellState
        {
            private const int _damageOnHit = 10;
            private const float _hitCooldown = 0.5f;
            private float _cooldownTimer;

            public void OnStart(PlayerManager player)
            {
          
            }

            public void OnUpdate(PlayerManager player)
            {
                _cooldownTimer -= Time.deltaTime;
            }

            public void OnAttack(PlayerManager player)
            {
                if(_cooldownTimer > 0f) return;
                player.Animator.SetTrigger(ArcaneWeapon);
                // foreach (var enemy in EnemyManager.Instance.enemies)
                // {
                //     var distance = Vector3.Distance(enemy.transform.position, player.MeleeCentre.position);
                //     if (distance > player.MeleeRadius) return;
                //     enemy.Damage(_damageOnHit);
                //     _cooldownTimer = _hitCooldown;
                // }
            }

            public void OnDamaged(PlayerManager player, int damage)
            {
                
            }

            public void OnEnd(PlayerManager player)
            {
          
            }
        }
    }
}
