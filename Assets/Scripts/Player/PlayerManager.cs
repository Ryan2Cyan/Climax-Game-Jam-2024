using System.Collections;
using System.Collections.Generic;
using General;
using Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [Header("Components")] 
        public Rigidbody Rigidbody;
        public Transform MeleeCentre;
        public List<GameObject> Ghosts = new();
        public PlayerCamera PlayerCameraScript;
        public Animator BalthazarAnimator;
        public GameObject Balthazar;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public CursorWorldRaycast CursorWorldRaycastScript;
        
        [Header("Spell Components")]
        public GameObject FireWall;
        public GameObject FireEldritchBlast;
        
        [Header("Player Settings")]
        public Material DamagedMaterial;
        public float MeleeRadius;
        public float DamagedCooldown = 0.25f;
        public float IFrameDuration;
        public int MaxHealth = 200;
        public bool DebugActive;
        
        [Header("Arcane Weapon")]
        public int ArcaneWeaponDamage;
        public float ArcaneWeaponCooldown = 0.25f;

        [Header("Fire Wall")] 
        public int FireWallDamage;
        public float FireWallDamageCooldown;
        
        [HideInInspector] public float CurrentHealth;
        [HideInInspector] public MeshRenderer MeshRenderer;

        private Material _defaultMaterial;
        private IPlayerSpellState _currentState;
        private Vector3 _targetDirection;
        private Vector2 _previousMousePosition;
        private float _movementVelocity;
        private float _prevAngle;
        private float _rotateStartTime;
        private bool _playerIFrames;

        public AudioManager audioManager;
        
        // Player states:
        private readonly ArcaneWeaponPlayerState _arcaneWeapon = new();
        private readonly FireWallPlayerState _fireWall = new();
        private readonly EldritchPlayerState _eldritchBlast = new();
        private readonly InvisibilityPlayerState _invisibility = new();
        
        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
            Animator = GetComponent<Animator>();
            audioManager = FindObjectOfType<AudioManager>();
            
            CursorWorldRaycastScript = GetComponent<CursorWorldRaycast>();
            _currentState = _arcaneWeapon;
            CurrentHealth = MaxHealth;
            
            // MeshRenderer = GetComponent<MeshRenderer>();
            // _defaultMaterial = MeshRenderer.material;
            // MeshRenderer.material = new Material(_defaultMaterial);
        }

        private void Update()
        {
            Rigidbody.velocity = Vector3.zero;
            if (GameplayManager.Instance.Paused) return;
            _currentState.OnUpdate(this);
            CursorWorldRaycastScript.GetCursorDirection();
            
        }

        private void OnEnable()
        {
            InputManager.OnMouseDown += OnMouseDown;
        }

        private void OnDisable()
        {
            InputManager.OnMouseDown -= OnMouseDown;
        }

        #endregion

        #region PublicFunctions

        public void ChangeState(IPlayerSpellState state)
        {
            _currentState.OnEnd(this);
            _currentState = state;
            _currentState.OnStart(this);
        }

        public void ChangeSpell()
        {
            var gettingNewSpell = true;
            while (gettingNewSpell)
            {
                var random = Random.Range(0, 4);
                switch (random)
                {
                    case 0:
                    {
                        if(_currentState == _arcaneWeapon) continue;
                        ChangeState(_arcaneWeapon);
                        gettingNewSpell = false;
                    }
                        break;
                    case 1:
                    {
                        if(_currentState == _fireWall) continue;
                        ChangeState(_fireWall);
                        gettingNewSpell = false;
                    } break;
                    case 2:
                    {
                        if(_currentState == _eldritchBlast) continue;
                        ChangeState(_eldritchBlast);
                        gettingNewSpell = false;
                    } break;
                    case 3:
                    {
                        if(_currentState == _invisibility) continue;
                        ChangeState(_invisibility);
                        gettingNewSpell = false;
                    } break;
                }
            }
        }

        public void OnDamaged(int damage)
        {
            if(!_playerIFrames) _currentState.OnDamaged(this, damage);
        }

        public void OnDeath()
        {
            
        }
        
        // public IEnumerator DamageShaderSwap(float duration)
        // {
        //     var elapsedTime = duration;
        //     MeshRenderer.material = new Material(DamagedMaterial);
        //     while (elapsedTime > 0f)
        //     {
        //         elapsedTime -= Time.deltaTime;
        //         yield return null;
        //     }
        //
        //     MeshRenderer.material = new Material(_defaultMaterial);
        //     yield return null;
        // }

        public IEnumerator IFrames()
        {
            var elapsedTime = IFrameDuration;
            _playerIFrames = true;
            while (elapsedTime > 0f)
            {
                elapsedTime -= Time.deltaTime;
                yield return null;
            }

            _playerIFrames = false;
            yield return null;
        }
        

        #endregion

        #region PrivateFunctions

        private void OnMouseDown()
        {
            _currentState.OnAttack(this);
        }
        
        #endregion
    }
}
