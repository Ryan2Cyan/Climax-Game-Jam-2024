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
        private float _cooldownTimer;

        private static readonly int ArcaneWeapon = Animator.StringToHash("ArcaneWeapon");
        
        public void OnStart(PlayerManager player)
        {
            if(player.DebugActive) Debug.Log("New Spell: <b>[Arcane Weapon]</b>");
            _cooldownTimer = player.ArcaneWeaponCooldown;
        }

        public void OnUpdate(PlayerManager player)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        public void OnAttack(PlayerManager player)
        {
            if(_cooldownTimer > 0f) return;
            player.Animator.SetTrigger(ArcaneWeapon);
            player.audioManager.PlayOnce("ArcaneWeapon");
            foreach (var enemy in EnemyManager.Instance.AllEnemies)
            {
                var distance = Vector3.Distance(enemy.transform.position, player.MeleeCentre.position);
                if (distance > player.MeleeRadius) continue;
                enemy.OnDamage(player.ArcaneWeaponDamage);
            }
            _cooldownTimer = player.ArcaneWeaponCooldown;
        }

        public void OnDamaged(PlayerManager player, int damage)
        {
            player.CurrentHealth -= damage;
            if (player.CurrentHealth <= 0) player.OnDeath();
            else
            {
                // player.StartCoroutine(player.DamageShaderSwap(player.DamagedCooldown));
                player.StartCoroutine(player.IFrames());
            }
        }

        public void OnEnd(PlayerManager player)
        {
      
        }
    }
    
    public class FireWallPlayerState : IPlayerSpellState
    {
        public void OnStart(PlayerManager player)
        {
            if(player.DebugActive) Debug.Log("New Spell: <b>[Fire Wall]</b>");
            player.FireWall.SetActive(true);
        }
        public void OnUpdate(PlayerManager player) { }
        public void OnAttack(PlayerManager player) { }
        public void OnDamaged(PlayerManager player, int damage)
        {
            player.CurrentHealth -= damage;
            if (player.CurrentHealth <= 0) player.OnDeath();
            else
            {
                // player.StartCoroutine(player.DamageShaderSwap(player.DamagedCooldown));
                player.StartCoroutine(player.IFrames());
            }
        }

        public void OnEnd(PlayerManager player)
        {
            player.FireWall.SetActive(false);
        }
    }
    
}
