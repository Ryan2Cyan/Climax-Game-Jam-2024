using UnityEngine;

namespace General
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Components")]
        public GameObject PauseScreen;

        private void Awake()
        {
            Instance = this;
        }
    }
}
