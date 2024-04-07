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
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
        public void OnPause(GameplayManager gameplayManager) { }
    }
    
    public class MainMenuGameplayState : IGameplayState
    {
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Main Menu</b>");
            SceneManager.ChangeScene(SceneManager.Scene.MainMenu);

            gameplayManager.audioManager.Play("MainMenuMusic");
            gameplayManager.audioManager.StopPlaying("GameplayMusic");

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
        private bool _sceneLoaded;
        public void OnStart(GameplayManager gameplayManager)
        {
            if(gameplayManager.DebugActive) Debug.Log("Gameplay State: <b>Playing</b>");
            SceneManager.ChangeScene(SceneManager.Scene.Game);


            gameplayManager.audioManager.Play("GameplayMusic");
            gameplayManager.audioManager.StopPlaying("MainMenuMusic");

            gameplayManager.SpellChangeTimer = gameplayManager.SpellChangeInterval;
            _sceneLoaded = false;
        }

        public void OnUpdate(GameplayManager gameplayManager)
        {
            if (!_sceneLoaded) return;
            if (gameplayManager.SpellChangeTimer <= 0)
            {
                if (PlayerManager.Instance)
                {
                    // Change Spell:
                    PlayerManager.Instance.ChangeSpell();
                }
                
                gameplayManager.SpellChangeTimer = gameplayManager.SpellChangeInterval;
            }
            else gameplayManager.SpellChangeTimer -= Time.deltaTime;
            if (UIManager.Instance) UIManager.Instance.SpellCountDown.text = ((int)gameplayManager.SpellChangeTimer).ToString();
        }

        public void OnEnd(GameplayManager gameplayManager)
        {
            if (PlayerManager.Instance)
                PlayerManager.Instance.PlayerCameraScript.enabled = true;
            if (WaveManager.Instance)
                WaveManager.Instance.ToggleActive(false);
            if (UIManager.Instance)
                UIManager.Instance.Close();
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            WaveManager.Instance.ToggleActive(true);
            UIManager.Instance.Open();
            _sceneLoaded = true;
        }

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
