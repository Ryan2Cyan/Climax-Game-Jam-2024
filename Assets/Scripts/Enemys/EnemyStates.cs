using System;
using Player;
using UnityEngine;

namespace Enemys
{
    public interface IEnemyState
   {
       public void OnStart(Enemy enemy);
       public void OnUpdate(Enemy enemy);
       public void OnEnd(Enemy enemy);
   }

    public class SpawnEnemyState : IEnemyState
    {
        public void OnStart(Enemy enemy)
        {
        }

        public void OnUpdate(Enemy enemy) { }

        public void OnEnd(Enemy enemy) { }
    } 
    
   public class MoveEnemyState : IEnemyState
   {
        private const float _attackRadius = 1.5f;
        private static readonly int Running = Animator.StringToHash("Running");
        
        public void OnStart(Enemy enemy)
        {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): <b>[Move]</b>");
           enemy.Animator.SetBool(Running, true);
        }

       public void OnUpdate(Enemy enemy)
       {
           // Move towards the target (the player) by step each frame:
           var step = enemy.Speed * Time.deltaTime;
           enemy.MoveVector = Vector3.MoveTowards(enemy.transform.position, enemy.CurrentTarget.position, step);
           enemy.transform.position = enemy.MoveVector;
           
           // Face player:
           enemy.transform.LookAt(PlayerManager.Instance.transform.position);

           var distanceToPlayer = Vector3.Distance(enemy.transform.position, PlayerManager.Instance.transform.position);
           switch (enemy.Type)
           {
               case EnemyManager.EnemyTypeEnum.Small:
               {
                   if (distanceToPlayer < _attackRadius) enemy.SetState(enemy.AttackEnemyState);    
               } break;
               case EnemyManager.EnemyTypeEnum.Large:
               {
                   if (distanceToPlayer < _attackRadius) enemy.SetState(enemy.AttackEnemyState);
               } break;
               case EnemyManager.EnemyTypeEnum.Ranged:
               {
                   if (distanceToPlayer < enemy.ShootingRadius) enemy.SetState(enemy.ShootEnemyState);  
               } break;
               case EnemyManager.EnemyTypeEnum.Bomb:
               {
                    
               } break;
               default:
                   throw new ArgumentOutOfRangeException();
           }
       }

       public void OnEnd(Enemy enemy)
       {
           enemy.Animator.SetBool(Running, false);
       }
   }
   
   public class AttackEnemyState : IEnemyState
   {
       private float _attackTimer;
       private const float _cancelRadius = 1.5f;
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): <b>[Attack]</b>");
           _attackTimer = enemy.AttackCooldown;
        }

       public void OnUpdate(Enemy enemy)
       {
           var distanceToPlayer = Vector3.Distance(PlayerManager.Instance.transform.position, enemy.transform.position);
           if(distanceToPlayer > _cancelRadius) enemy.SetState(enemy.MoveEnemyState);
           if (_attackTimer < 0f)
           {
               if (distanceToPlayer < enemy.MeleeRange) PlayerManager.Instance.OnDamaged(enemy.Damage);
               _attackTimer = enemy.AttackCooldown;
           }
           else _attackTimer -= Time.deltaTime;
       }

       public void OnEnd(Enemy enemy) { }
   }
   
   public class DeathEnemyState : IEnemyState
   {
       private float _timer;
       private float _matSwapTime;
       private bool _matSwapped;
       
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): <b>[Death]</b>");
            enemy.IsAlive = false;
            _timer = enemy.DespawnTime;
            enemy.MeshRenderer.material = enemy.DamagedMaterial;
            _matSwapTime = enemy.DespawnTime - enemy.DamagedCooldown;
            _matSwapped = false;
            enemy.Collider.enabled = false;
       }

       public void OnUpdate(Enemy enemy)
       {
           _timer -= Time.deltaTime;
           
           if (!_matSwapped)
           {
               if (_timer < _matSwapTime)
               {
                   enemy.MeshRenderer.material = enemy.DeadMaterial;
                   _matSwapped = true;
               }
           }

           if(_timer < 0f) EnemyManager.Instance.DespawnEnemy(enemy);
       }
       public void OnEnd(Enemy enemy) { }
   }
   
   public class ShootEnemyState : IEnemyState
   {
       private float _attackTimer;

       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): <b>[Shoot]</b>");
           _attackTimer = 0f;
       }

       public void OnUpdate(Enemy enemy)
       {
           // Switch back to movement if player goes out of range:
           if (Vector3.Distance(enemy.transform.position, PlayerManager.Instance.transform.position) >= enemy.ShootingRadius)
           {
               enemy.SetState(enemy.MoveEnemyState);  
           }
           
           // Shoot bullet when timer depletes:
           if (_attackTimer > enemy.AttackCooldown)
           {
               var bullet = (Bullet) EnemyManager.Instance.BulletPool.GetPooledObject();
               bullet.transform.position = enemy.transform.position;
               bullet.InitBullet(enemy.Damage);
               _attackTimer = 0f;
           }
           else _attackTimer += Time.deltaTime;
       }
       public void OnEnd(Enemy enemy) { }
   }
}
