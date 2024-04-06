using UnityEngine;

namespace General
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;

        public bool Paused;

        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
            Paused = false;
        }

        #endregion
    }
}
