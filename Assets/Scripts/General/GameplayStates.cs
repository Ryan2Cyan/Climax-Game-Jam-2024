using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public interface IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager);
        public void OnUpdate(GameplayManager gameplayManager);
        public void OnEnd(GameplayManager gameplayManager);
        void OnSceneLoaded(Scene scene, LoadSceneMode mode);
        void OnPause(GameplayManager gameplayManager);
    }
    
    public class BootingUpGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>BootUp</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager) { }
        public void OnEnd(GameplayManager gameplayManager) { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class MainMenuGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Main Menu</b>");
            SceneManager.ChangeScene(SceneManager.Scene.MainMenu);
        }

        public void OnUpdate(GameplayManager gameplayManager) { }
        public void OnEnd(GameplayManager gameplayManager) { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class SettingsGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Settings</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager) { }
        public void OnEnd(GameplayManager gameplayManager) { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class StartGameplayState : IGameplayState
    {
        private static readonly int Hide = Animator.StringToHash("Hide");

        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Start</b>");
            SceneManager.ChangeScene(SceneManager.Scene.Game);
            gameplayManager.SpellChangeTimer = gameplayManager.SpellChangeInterval;
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = true; 
            PlayerManager.Instance.Animator.SetBool(Hide, false);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = false;
            PlayerManager.Instance.Animator.SetBool(Hide, true);
        }

        public void OnPause(GameplayManager gameplayManager)
        {
            gameplayManager.SetState(gameplayManager.MainMenuState);
        }
    }
    
    public class PlayingGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Playing</b>");
            WaveManager.Instance.ToggleActive(true);
            UIManager.Instance.Open();
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            if (gameplayManager.SpellChangeTimer <= 0)
            {
                // Change Spell:
                PlayerManager.Instance.ChangeSpell();
                gameplayManager.SpellChangeTimer = gameplayManager.SpellChangeInterval;
            }
            else gameplayManager.SpellChangeTimer -= Time.deltaTime;
            UIManager.Instance.SpellCountDown.text = ((int)gameplayManager.SpellChangeTimer).ToString();
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            PlayerManager.Instance.PlayerCameraScript.enabled = true;
            WaveManager.Instance.ToggleActive(false);
            UIManager.Instance.Close();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

        public void OnPause(GameplayManager gameplayManager)
        {
            gameplayManager.SetState(gameplayManager.PauseState);
        }
    }
    
    public class PauseGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Pause</b>");
            gameplayManager.Paused = true;
            UIManager.Instance.PauseScreen.SetActive(true);
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager)
        {
            gameplayManager.Paused = false;
            UIManager.Instance.PauseScreen.SetActive(false);
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

        public void OnPause(GameplayManager gameplayManager)
        {
            gameplayManager.SetState(gameplayManager.PlayState);
        }
    }
    
    public class GameOverGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>GameOver</b>");
        }

        public void OnUpdate(GameplayManager gameplayManager) { }

        public void OnEnd(GameplayManager gameplayManager) { }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode) { }

        public void OnPause(GameplayManager gameplayManager) { }
    }
}
