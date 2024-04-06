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
   
   public class MoveEnemyState : IEnemyState
   {
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): Move");
       }

       public void OnUpdate(Enemy enemy)
       {
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
       private float _attackTimer;
       
       public void OnStart(Enemy enemy)
       {
           if(enemy.EnableDebug) Debug.Log("Enemy (" + enemy.gameObject.name + "): Attack");
       }

       public void OnUpdate(Enemy enemy)
       {
           if (_attackTimer > enemy.AttackCooldown)
           {
               PlayerManager.Instance.OnDamaged(enemy.Damage);
               _attackTimer = 0f;
           }
           else _attackTimer += Time.deltaTime;
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
