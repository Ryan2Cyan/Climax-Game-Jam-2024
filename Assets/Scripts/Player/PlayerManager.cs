using Inputs;
using UnityEngine;
using static Player.PlayerStates;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Player Settings")]
        public float HitPoints;
        
        private IPlayerSpellState _currentState;
        
        // Player states:
        private readonly ArcaneWeaponPlayerState _arcaneWeapon = new();
        
        #region UnityFunctions

        private void Awake()
        {
            _currentState = _arcaneWeapon;   
        }

        private void Update()
        {
            _currentState.OnUpdate(this);
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

        public void OnDamaged()
        {
            _currentState.OnDamaged(this);
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
