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
        public Transform MeleeCentre;
        public List<GameObject> Ghosts = new();
        [HideInInspector] public Animator Animator;
        [HideInInspector] public CursorWorldRaycast CursorWorldRaycastScript;
        
        [Header("Player Settings")]
        public int MaxHealth = 200;
        public float MeleeRadius;
        
        [HideInInspector] public float CurrentHealth;

        private Vector2 _previousMousePosition;
        private float _movementVelocity;
        private IPlayerSpellState _currentState;
        private float _prevAngle;
        
        private Vector3 _targetDirection;
        private float _rotateStartTime;
        
        // Player states:
        private readonly ArcaneWeaponPlayerState _arcaneWeapon = new();
        
        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
            Animator = GetComponent<Animator>();
            CursorWorldRaycastScript = GetComponent<CursorWorldRaycast>();
            _currentState = _arcaneWeapon;
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
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

        public void OnDamaged(int damage)
        {
            _currentState.OnDamaged(this, damage);
        }

        public void OnDeath()
        {
            
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
