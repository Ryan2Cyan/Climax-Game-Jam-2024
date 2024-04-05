using UnityEngine;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        public Vector2 Movement;
        
        private InputActions _inputs;
        
        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
            _inputs = new InputActions();
        }

        private void OnEnable()
        {
            _inputs.Player.Enable();
        }

        private void OnDisable()
        {
            _inputs.Player.Disable();
        }

        private void FixedUpdate()
        {
            Movement = _inputs.Player.Movement.ReadValue<Vector2>();
        }

        #endregion

        // #region EventInvokers
        //
        //
        // #endregion
    }
}
