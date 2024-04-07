using System.Collections;
using Enemys;
using UnityEngine;

namespace General
{
    /// <summary>
    /// The wave manager tells the enemy manager to spawn enemies each wave.
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;

        [Header("Settings")] 
        public AnimationCurve SpawnPointIncrement;
        public AnimationCurve DurationIncrement;
        public float MaxWave = 100f;
        public float MaximumWaveDuration = 100f;
        public float MaximumWaveSpawnPoints = 100f;
        public int StartingWave = 1;
        public bool DebugActive;
        [HideInInspector] public int CurrentWave;

        private IEnumerator _waveCounterCoroutine;
        private float _waveTimer;
        private float _currentWaveDuration;
        private int _currentSpawnPoints;
        private bool _active;
        
        // Events:
        public delegate void WaveManagerDelegate();
        public static event WaveManagerDelegate OnWaveStart;

        #region UnityFunctions

        [ContextMenu("Activate")]
        public void Activate()
        {
            ToggleActive(true);
        }
        
        private void Awake()
        {
            Instance = this;
        }
        
        #endregion

        #region PublicFunctions


        public void ToggleActive(bool active)
        {
            _active = active;
            if (active) StartCoroutine(_waveCounterCoroutine = WaveCountTimer());
        }

        public void StartWave()
        {
            OnWaveStart?.Invoke();
            EnemyManager.Instance.SpawnEnemies(_currentSpawnPoints, _currentWaveDuration);
        }

        
        public void Reset()
        {
            CurrentWave = 0;
            if(_waveCounterCoroutine != null) StopCoroutine(_waveCounterCoroutine);
            
        }

        #endregion

        #region PrivateFunctions
        /// <summary> After each wave, we can adjust the number of enemies each wave will spawn. </summary>
        private void UpdateWaveParamaters()
        {
            CurrentWave++;
            var time = CurrentWave / MaxWave;
            _currentWaveDuration = DurationIncrement.Evaluate(time) * MaximumWaveDuration;
            _currentSpawnPoints = (int)(SpawnPointIncrement.Evaluate(time) * MaximumWaveSpawnPoints);
            if(DebugActive) Debug.Log("New Duration: " + _currentWaveDuration + " New SpawnPoints: " +
                                      _currentSpawnPoints);
        }

        private IEnumerator WaveCountTimer()
        {
            if(DebugActive) Debug.Log("Start WaveTimer");
            CurrentWave = StartingWave;
            _waveTimer = _currentWaveDuration;
            
            while (_active)
            {
                if (!GameplayManager.Instance.Paused)
                {
                    if (_waveTimer < 0f)
                    {
                        if(DebugActive) Debug.Log("Wave " + CurrentWave + ": New Wave");
                        UpdateWaveParamaters();
                        StartWave();
                        _waveTimer = _currentWaveDuration;
                    }
                    else _waveTimer -= Time.deltaTime;
                }

                yield return null;
            }
            if(DebugActive) Debug.Log("Stop WaveTimer");
            yield return null;
        }

        #endregion
    }
}
