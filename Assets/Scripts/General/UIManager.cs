using TMPro;
using UnityEngine;

namespace General
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Components")] 
        public Animator UIAnimator;
        public GameObject PauseScreen;
        public TextMeshProUGUI SpellCountDown;
        
        private static readonly int OpenParam = Animator.StringToHash("Open");

        private void Awake()
        {
            Instance = this;
        }

        public void Open()
        {
            UIAnimator.SetBool(OpenParam, true);
        }
        
        public void Close()
        {
            UIAnimator.SetBool(OpenParam, false);
        }
    }
}
