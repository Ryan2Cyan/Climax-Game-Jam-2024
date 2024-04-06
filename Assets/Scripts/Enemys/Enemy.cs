using System.Collections;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        [Header("Settings")]
        public int MaxHealth = 250;
        public float Speed;
        public int Damage;
        public float AttackCooldown;
        public float DamagedCooldown = 0.25f;
        public float GhostTargetShiftCooldown = 1f;
        public bool EnableDebug = true;

        public Material DefaultMaterial;
        public Material DamagedMaterial;

        [HideInInspector] public Vector3 MoveVector;
        [HideInInspector] public Transform CurrentTarget;
        [HideInInspector] public float CurrentHealth;
        [HideInInspector] public bool IsAlive = true;
        
        private float _targetUpdateTimer;
        private MeshRenderer _meshRenderer;
        private IEnumerator _currentCoroutine;

        // States:
        private IEnemyState _currentState;
        private readonly MoveEnemyState _moveEnemyState = new();
        private readonly AttackEnemyState _attackEnemyState = new();
        private readonly DeathEnemyState _deathEnemyState = new();
        
        #region UnityFunctions

        private void Start()
        {
            TargetUpdate();
            _currentState = _moveEnemyState;
            CurrentHealth = MaxHealth;
            CurrentTarget = PlayerManager.Instance.transform;
            
            // Create a new instance of mesh renderer's material:
            _meshRenderer = GetComponent<MeshRenderer>();
            var material = _meshRenderer.material;
            _meshRenderer.material = new Material(material);
        }

        private void Update()
        {
            TargetUpdate();
            _currentState.OnUpdate(this);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Player":
                {
                    SetState(_attackEnemyState);
                } break;
                case "Enemy":
                {
                    
                } break;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Player":
                {
                    SetState(_moveEnemyState);
                }break;
                case "Enemy":
                {
                    MoveVector *= -1;
                }break;
            }
        }

        #endregion

        #region PublicFunctions

        public void SetState(IEnemyState state)
        {
            _currentState.OnEnd(this);
            _currentState = state;
            _currentState.OnStart(this);
        }
        #endregion

        #region PrivateFunctions

        public void OnDamage(int damage)
        {
            if(EnableDebug) Debug.Log("Enemy (" + gameObject.name + "): Damaged");
            CurrentHealth -= damage;
            if (CurrentHealth <= 0) SetState(_deathEnemyState);
            else
            {
                if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
                StartCoroutine(_currentCoroutine = DamageShaderSwap(DamagedCooldown));
            }
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
            _meshRenderer.material = new Material(DamagedMaterial);
            while (elapsedTime > 0f)
            {
                elapsedTime -= Time.deltaTime;
                yield return null;
            }

            _meshRenderer.material = new Material(DefaultMaterial);
            yield return null;
        }
        
        #endregion
    }
}

