using Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        public float Speed;
        public float Smoothing = 1f;
        
        #region UnityFunctions

        private void FixedUpdate()
        {
            var playerPosition = transform.position;
            var movementVector = InputManager.Instance.Movement * (Speed * Time.deltaTime);
            var desiredPosition = new Vector3(playerPosition.x + movementVector.x, playerPosition.y, playerPosition.z + movementVector.y);
            transform.position = Vector3.Lerp(playerPosition, desiredPosition, Smoothing * Time.deltaTime);
        }

        #endregion
    }
}
