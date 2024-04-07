using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        public Vector2 Movement;
        public Vector2 MousePosition;

        public delegate void InputActionDelegate();
        public static event InputActionDelegate OnMouseDown;
        public static event InputActionDelegate OnPause;

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

            _inputs.Player.MouseDown.performed += MouseDown;
            _inputs.Player.Pause.performed += Pause;
        }

        private void OnDisable()
        {
            _inputs.Player.Disable();
            
            _inputs.Player.MouseDown.performed -= MouseDown;
            _inputs.Player.Pause.performed -= Pause;
        }

        private void FixedUpdate()
        {
            Movement = _inputs.Player.Movement.ReadValue<Vector2>();
            MousePosition = _inputs.Player.MousePosition.ReadValue<Vector2>();
        }

        #endregion

        #region EventInvokers

        private static void MouseDown(InputAction.CallbackContext context) { OnMouseDown?.Invoke(); }
        private static void Pause(InputAction.CallbackContext context) { OnPause?.Invoke(); }

    #endregion
    }
}
