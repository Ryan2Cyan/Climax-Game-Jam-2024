using Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed;
        
        #region UnityFunctions

        private void FixedUpdate()
        {
            var Transform = transform;
            var currentPosition = Transform.position;
            var movementVector = InputManager.Instance.Movement * (Speed * Time.deltaTime);
            currentPosition = new Vector3(currentPosition.x + movementVector.x, currentPosition.y, currentPosition.z + movementVector.y);
            Transform.position = currentPosition;
        }

        #endregion
    }
}
