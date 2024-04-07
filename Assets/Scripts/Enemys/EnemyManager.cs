using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace Enemys
{
    ///  The enemy manager spawns in the enemies as told by the wave manager and keeps track of all enemies spawned,
    /// along with initializing the enemies as they are spawned. 
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        [Header("Settings")] 
        public bool DebugActive;
        
        [Header("References")]
        public ObjectPool EnemyPool;
        public ObjectPool BulletPool;
        public List<Enemy> AllEnemies = new();

        [Header("Enemy Types")]
        public EnemyType[] enemyTypes;

        public enum EnemyTypeEnum
        {
            Small = 0,
            Large = 1,
            Ranged = 2,
            Bomb = 3
        }
        
        [Header("Settings")]
        public Vector2 MapBounds;
        private Vector2 _spawnExtent;
        private int _spawnedEnemies;
        private IEnumerator _spawnCoroutine;
        
        #region UnityFunctions

        private void Awake()
        {
            Instance = this;
            _spawnExtent = MapBounds / 2;
        }

        #endregion

        #region PublicFunctions
        
        public void DespawnEnemy(Enemy enemy)
        {
            EnemyPool.ReleasePooledObject(enemy);    
        }

        #endregion

        #region PrivateFunctions

       
        public void SpawnEnemies(int spawnPoints, float waveDuration)
        {
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
            StartCoroutine(_spawnCoroutine = SpawnEnemiesCoroutine(spawnPoints, waveDuration));
        }

        /// <summary>Spawns the wave of enemies.</summary>
        private IEnumerator SpawnEnemiesCoroutine(int spawnPoints, float waveDuration)
        {
            var timePerSpawn = waveDuration / 10f;
            var elapsedTime = timePerSpawn;
            var spawnCurrency = spawnPoints;
            
            while (spawnCurrency > 0)
            {
                if (!GameplayManager.Instance.Paused)
                {
                    elapsedTime -= Time.deltaTime;
                    if (elapsedTime < 0f)
                    {
                        var type = GetRandomType();
                        if (DebugActive) Debug.Log("Spawn Enemy: <b>[" + (EnemyTypeEnum)type + "]</b>");
                        var enemyValue = enemyTypes[type].SpawnValue;
                        var newTotal = spawnCurrency - enemyValue;
                        if (newTotal > 0)
                        {
                            spawnCurrency -= enemyValue;
                            Spawn(type);
                        }
                        else spawnCurrency--;

                        elapsedTime = timePerSpawn;
                    }
                }

                yield return null;
            }
            yield return null;
        }
        
        /// <summary>Spawn the enemy, name it, set the player up as its target, then add to the enemies list.</summary>
        private void Spawn(int enemyTypeIndex) //spawns one enemy
        {
            
            var enemy = (Enemy)EnemyPool.GetPooledObject();
            if (EnemyPool.NewObjectAdded) AllEnemies.Add(enemy);
            enemy.transform.position = DetermineSpawnLocation();
            enemy.SetEnemyType(enemyTypes[enemyTypeIndex]);

        }
        
        /// <summary>Spawn enemies either north, south, east, or west, then random the other coordinate within the
        /// spawn extent.</summary>
        private Vector3 DetermineSpawnLocation()
        {
            var spawnPosition = Vector3.zero;
            var spawnLocationRoll = Random.Range(0, 4);
            switch (spawnLocationRoll)
            {
                case 0:
                    spawnPosition.x = _spawnExtent.x; // north
                    spawnPosition.z = Random.Range(-_spawnExtent.y, _spawnExtent.y);
                    break;
                case 1:
                    spawnPosition.z = _spawnExtent.y; // east
                    spawnPosition.x = Random.Range(-_spawnExtent.x, _spawnExtent.x);
                    break;
                case 2:
                    spawnPosition.x = -_spawnExtent.x; // south
                    spawnPosition.z = Random.Range(-_spawnExtent.y, _spawnExtent.y);
                    break;
                case 3:
                    spawnPosition.z = -_spawnExtent.y; // west
                    spawnPosition.x = Random.Range(-_spawnExtent.x, _spawnExtent.x);
                    break;
            }
            return spawnPosition;
        }
        
        private static int GetRandomType()
        {
            var randomValue = Random.value;
            // return (int)EnemyTypeEnum.Large;
            return randomValue switch
            {
                > 0f and < 0.6f => (int)EnemyTypeEnum.Small,        // 60%
                > 0.6f and < 0.8f => (int)EnemyTypeEnum.Large,      // 20%
                > 0.8f and < 1f => (int)EnemyTypeEnum.Ranged,     // 20%
                // > 0.9f and < 1f => (int)EnemyTypeEnum.Bomb,         // 10%
                _ => 0
            };
        }
       
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube( Vector3.zero, new Vector3(MapBounds.x, 0f, MapBounds.y));
        }
        
        #endregion
    }
}
