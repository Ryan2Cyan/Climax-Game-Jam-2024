using Inputs;
using UnityEngine;

namespace General
{
    /// <remarks>Source: https://www.youtube.com/watch?v=AOVCKEJE6A8</remarks>
    public class CursorWorldRaycast : MonoBehaviour
    {
        [Header("Settings")] 
        public float RotationTime;
        
        [Header("Components")]
        public LayerMask GroundLayerMask;
        public Camera MainCamera;
        
        private Vector3 _rotationVelocity;
        
        public void GetCursorDirection()
        {
            var (success, position) = GetMousePosition();
            if (!success) return;
            var transformCache = transform;
            transformCache.forward = Vector3.SmoothDamp(transformCache.forward, position - transformCache.position, ref _rotationVelocity, RotationTime * Time.deltaTime);
            transformCache.eulerAngles = new Vector3(0f, transformCache.eulerAngles.y, 0f);
        }
        
        private (bool success, Vector3 worldPosition) GetMousePosition()
        {
            var ray = MainCamera.ScreenPointToRay(InputManager.Instance.MousePosition);
            return Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, GroundLayerMask) ? (success: true, worldPosition: hitInfo.point) : (success: false, worldPosition: Vector3.zero);
        }
    }
}
