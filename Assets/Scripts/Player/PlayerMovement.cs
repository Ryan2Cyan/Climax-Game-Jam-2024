using General;
using Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        public float Speed;
        public float Smoothing = 1f;

        private Vector3 _velocity;
        private static readonly int Running = Animator.StringToHash("Running");

        #region UnityFunctions

        private void FixedUpdate()
        {
            if (GameplayManager.Instance.Paused) return;
            
            var playerPosition = transform.position;
            var movementVector = InputManager.Instance.Movement * (Speed * Time.deltaTime);
            var desiredPosition = new Vector3(playerPosition.x + movementVector.x, playerPosition.y, playerPosition.z + movementVector.y);
            transform.position = Vector3.SmoothDamp(playerPosition, desiredPosition, ref _velocity, Smoothing * Time.deltaTime);
            
            PlayerManager.Instance.BalthazarAnimator.SetBool(Running, movementVector != Vector2.zero);
        }

        #endregion
    }
}
