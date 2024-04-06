using Player;
using System.Collections;
using General;
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
            enemy.SetState(enemy.MoveEnemyState);
        }

        public void OnUpdate(Enemy enemy)
        {
    
        }

        public void OnEnd(Enemy enemy)
        {
        }
    } 
    
   public class MoveEnemyState : IEnemyState
   {
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): Move");

         
        }

       public void OnUpdate(Enemy enemy)
       {
            if (enemy.isRanged)
            {
                if (enemy.rangeRadius.CanSeePlayer())
                {
                    enemy.SetState(enemy.AttackEnemyState);
                }

            }


            // Move towards the target (the player) by step each frame:
            var step = enemy.Speed * Time.deltaTime;
            enemy.MoveVector = Vector3.MoveTowards(enemy.transform.position, enemy.CurrentTarget.position, step);
            enemy.transform.position = enemy.MoveVector;
          
          
       }

       public void OnEnd(Enemy enemy)
       {
        
       }
   }
   
   public class AttackEnemyState : IEnemyState
   {
       private float _attackTimer = 0;
       
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): Attack");
            _attackTimer = 0;
        }

       public void OnUpdate(Enemy enemy)
       {
            if (enemy.isRanged)
            {
                if (_attackTimer > enemy.AttackCooldown)
                {
                    //do bullet stuff here
                    //var bullet = Object.Instantiate(enemy.bullet, enemy.transform.position, Quaternion.identity, enemy.transform);
                    var bullet = (Bullet)enemy.bulletPool.GetPooledObject();
                    if (enemy.bulletPool.NewObjectAdded) enemy.bullets.Add(bullet);
                    bullet.transform.position = enemy.transform.position;
                    bullet.GetComponent<Bullet>().InitBullet(enemy.Damage, enemy);
                    
                    _attackTimer = 0f;
                }
                else _attackTimer += Time.deltaTime;

                if (!enemy.rangeRadius.CanSeePlayer())
                {
                    _attackTimer = 0f;
                    enemy.SetState(enemy.MoveEnemyState);
                }
               
            }
            else
            {
                if (_attackTimer > enemy.AttackCooldown)
                {
                    PlayerManager.Instance.OnDamaged(enemy.Damage);
                    _attackTimer = 0f;
                }
                else _attackTimer += Time.deltaTime;
            }
            
          
       }

       public void OnEnd(Enemy enemy)
       {
        
       }
   }
   
   public class DeathEnemyState : IEnemyState
   {
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): Death");
           enemy.IsAlive = false;
           EnemyManager.Instance.DespawnEnemy(enemy);
       }

       public void OnUpdate(Enemy enemy)
       {
           
       }

       public void OnEnd(Enemy enemy)
       {
        
       }
   }
}
