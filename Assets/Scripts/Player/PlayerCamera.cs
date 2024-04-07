using General;
using UnityEngine;

namespace Player
{
    /// <remarks>Source: https://www.youtube.com/watch?v=MFQhpwc6cKE</remarks>
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Components")]
        public Transform Target;

        [Header("Settings")] 
        public float SmoothSpeed = 10f;
        public Vector3 Offset;

        private void FixedUpdate()
        {
            if (GameplayManager.Instance.Paused) return;
            
            var desiredPosition = Target.position + Offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            transform.LookAt(Target);
        }
    }
}
