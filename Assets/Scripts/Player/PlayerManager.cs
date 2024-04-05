using Inputs;
using UnityEngine;
using static Player.PlayerStates;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Components")] 
        public Camera PlayerCamera;
        public Transform MeleeCentre;
        [HideInInspector] public Animator Animator;
        
        [Header("Player Settings")]
        public float HitPoints;
        public float MeleeRadius;
        
        private IPlayerSpellState _currentState;
        private float _prevAngle;
        
        // Player states:
        private readonly ArcaneWeaponPlayerState _arcaneWeapon = new();
        
        #region UnityFunctions

        private void Awake()
        {
            _currentState = _arcaneWeapon;
            Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _currentState.OnUpdate(this);
            FaceCursorDirection();
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

        #endregion

        #region PrivateFunctions

        private void OnMouseDown()
        {
            _currentState.OnAttack(this);
        }

        private void FaceCursorDirection()
        {
            // Convert mouse position from screen to world space:
            var mousePosition = InputManager.Instance.MousePosition;
            var lookPosition = PlayerCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            
            // Get direction to the world-space mouse position:
            var direction = lookPosition - transform.position;
            direction = new Vector3(direction.x, 0f, direction.z).normalized;
            
            transform.rotation = Quaternion.LookRotation(direction);
        }

        #endregion
    }
}
