using General;
using Player;
using UnityEngine;

namespace Enemys
{
    public class Bullet : MonoBehaviour, IPooledObject
    {
        [Header("Settings")] 
        public float Radius = 3;
        public float Speed = 5;
        public float BulletLifespan = 2;
        public int Damage = 1;
        
        private float _lifespan;
        private Vector3 _direction;

        #region UnityFunctions

        private void Update()
        {
            transform.Translate(_direction * (Speed * Time.deltaTime));
            if (_lifespan < 0) EnemyManager.Instance.BulletPool.ReleasePooledObject(this);
            else _lifespan -= Time.deltaTime;

            if (!(Vector3.Distance(PlayerManager.Instance.transform.position, transform.position) < Radius)) return;
            PlayerManager.Instance.OnDamaged(Damage);
            EnemyManager.Instance.BulletPool.ReleasePooledObject(this); ;
        }

        #endregion

        #region PublicFunctions

        public void Instantiate()
        {
            _lifespan = BulletLifespan;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        
        public void InitBullet(int newDamage)
        {
            Damage = newDamage;
            var target = PlayerManager.Instance.transform.position;
            _direction = (target - transform.position).normalized;
        }
        
        public void Release()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
