using System;
using System.Collections;
using General;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemys
{
    public class Enemy : MonoBehaviour, IPooledObject
    {
        [Header("Enemy Stats")]
        public int MaxHealth;
        public float Speed;
        public int Damage;
        public float AttackCooldown;
        public EnemyManager.EnemyTypeEnum Type;

        [Header("Settings")]
        public Material DamagedMaterial;
        public Material DeadMaterial;
        public float GhostTargetShiftCooldown = 1f;
        public float DamagedCooldown = 0.25f;
        public float MeleeRange;
        public float ExplosionRadius;
        public float ShootingRadius;
        public float DespawnTime;
        public bool EnableDebug = true;

        [HideInInspector] public Vector3 MoveVector;
        [HideInInspector] public Transform CurrentTarget;
        [HideInInspector] public MeshRenderer MeshRenderer;
        [HideInInspector] public float CurrentHealth;
        public bool IsAlive = true;
        
        public readonly MoveEnemyState SpawnEnemyState = new();
        public readonly MoveEnemyState MoveEnemyState = new();
        public readonly AttackEnemyState AttackEnemyState = new();
        public readonly DeathEnemyState DeathEnemyState = new();
        public readonly ShootEnemyState ShootEnemyState = new();
        
        private Material _defaultMaterial;
        private IEnumerator _currentCoroutine;
        private Rigidbody _rigidbody;
        private BoxCollider _collider;
        private float _targetUpdateTimer;
        private bool _inFireWall;
        
        // States:
        private IEnemyState _currentState;
        
        #region UnityFunctions
        
        private void Update()
        {
            if (GameplayManager.Instance.Paused) return;
            TargetUpdate();
            _currentState.OnUpdate(this);
        }
        
        #endregion

        #region PublicFunctions

        public void SetState(IEnemyState state)
        {
            _currentState.OnEnd(this);
            _currentState = state;
            _currentState.OnStart(this);
        }
        
        public void Instantiate()
        {
            gameObject.SetActive(true);
            TargetUpdate();
            _currentState = SpawnEnemyState;
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
            CurrentTarget = PlayerManager.Instance.transform;
            
            // Create a new instance of mesh renderer's material:
            MeshRenderer = GetComponent<MeshRenderer>();
            var material = MeshRenderer.material;
            MeshRenderer.material = new Material(material);
            IsAlive = true;
        }
        
        public void SetEnemyType(EnemyType newEnemyType)
        {
            Type = newEnemyType.Type;
            _defaultMaterial = newEnemyType.Material;
            MeshRenderer.material = new Material(newEnemyType.Material);
            transform.localScale = newEnemyType.Scale;
            name = newEnemyType.Name;
            MeleeRange = newEnemyType.MeleeRange;
            MaxHealth = newEnemyType.MaxHealth;
            Damage = newEnemyType.Damage;
            Speed = newEnemyType.Speed;
            AttackCooldown = newEnemyType.AttackCooldown;
            Damage = newEnemyType.Damage;
            CurrentHealth = MaxHealth;

            _collider.enabled = false;
            _rigidbody.velocity = Vector3.zero;
        }
        
        public void Release()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("FireWall")) return;
            _inFireWall = true;
            StartCoroutine(InsideFireWall());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("FireWall")) _inFireWall = false;
        }

        #endregion

        #region PrivateFunctions

        public void OnDamage(int damage)
        {
            if (!IsAlive) return; 
            if(EnableDebug) Debug.Log("Enemy (" + gameObject.name + "): Damaged");
            CurrentHealth -= damage;
            if (CurrentHealth <= 0) SetState(DeathEnemyState);
            else
            {
                if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                StartCoroutine(_currentCoroutine = DamageShaderSwap(DamagedCooldown));
            }
        }

        private IEnumerator InsideFireWall()
        {
            var fireWallDamage = PlayerManager.Instance.FireWallDamage;
            var fireWallDamageTick = PlayerManager.Instance.FireWallDamageCooldown;
            var elapsedTime = fireWallDamageTick;
            OnDamage(fireWallDamage);
            
            while (_inFireWall && IsAlive)
            {
                if (elapsedTime < 0f)
                {
                    OnDamage(fireWallDamage);
                    elapsedTime = fireWallDamageTick;
                }
                else elapsedTime -= Time.deltaTime;
                yield return null;
            }
            
            yield return null;
        }
        
        private void TargetUpdate()
        {
            if (_targetUpdateTimer >= GhostTargetShiftCooldown)
            {
                // This randomly sets the enemies target to either be the player or one of 4 "ghost" objects that are children
                //of the player, this helps to stop all the clumping together of the large number of enemies somewhat:
                var randomIndex = Random.Range(0, PlayerManager.Instance.Ghosts.Count);
            
                // If it equals four, just follow the player as normal:
                if (randomIndex == 4) CurrentTarget = PlayerManager.Instance.transform;
                var ghost = PlayerManager.Instance.Ghosts[randomIndex].transform;
                CurrentTarget = ghost;
                _targetUpdateTimer = 0;
            }
            else _targetUpdateTimer += Time.deltaTime;
        }
        
        private IEnumerator DamageShaderSwap(float duration)
        {
            var elapsedTime = duration;
            MeshRenderer.material = new Material(DamagedMaterial);
            while (elapsedTime > 0f)
            {
                elapsedTime -= Time.deltaTime;
                yield return null;
            }

            MeshRenderer.material = new Material(_defaultMaterial);
            yield return null;
        }
        
        #endregion
    }
}

