using Enemys;
using UnityEngine;

namespace Player
{
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

        private static readonly int ArcaneWeapon = Animator.StringToHash("ArcaneWeapon");
        
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
            foreach (var enemy in EnemyManager.Instance.AllEnemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, player.MeleeCentre.position);
                if (distance > player.MeleeRadius) continue;
                enemy.OnDamage(_damageOnHit);
                _cooldownTimer = _hitCooldown;
            }
        }

        public void OnDamaged(PlayerManager player, int damage)
        {
            Debug.Log("Player Damaged");
            player.CurrentHealth -= damage;
            if (player.CurrentHealth <= 0) player.OnDeath();
            else
            {
                player.StartCoroutine(player.DamageShaderSwap(player.DamagedCooldown));
                player.StartCoroutine(player.IFrames());
            }
        }

        public void OnEnd(PlayerManager player)
        {
      
        }
    }
    
}
