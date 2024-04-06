using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

namespace Enemys
{
    /// <summary> The enemy manager spawns in the enemies as told by the wave manager and keeps track of all enemies spawned,
    /// along with initializing the enemies as they are spawned. </summary>
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        [Header("References")]
        public ObjectPool EnemyPool;
        public List<Enemy> AllEnemies = new();

        [Header("Enemy Types")]
        // EnemyType[] enemyTypes;
        
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

        [ContextMenu("SpawnWave")]
        public void Spawn10()
        {
            SpawnEnemies(3, 10f);
        }
        
        public void SpawnEnemies(int spawnAmount, float waveDuration)
        {
            if(_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
            StartCoroutine(_spawnCoroutine = SpawnEnemiesCoroutine(spawnAmount, waveDuration));
        }

        public void DespawnEnemy(Enemy enemy)
        {
            EnemyPool.ReleasePooledObject(enemy);    
        }
        
        #endregion

        #region PrivateFunctions
        
        /// <summary> Spawn the enemy, name it, set the player up as its target, then add to the enemies list. </summary>
        private void Spawn()
        {
            var enemy = (Enemy)EnemyPool.GetPooledObject();
            if(EnemyPool.NewObjectAdded) AllEnemies.Add(enemy);
            enemy.transform.position = DetermineSpawnLocation();
        }

        /// <summary> Spawn enemies either north, south, east, or west, then random the other coordinate within the
        /// spawn extent. </summary>
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

        private IEnumerator SpawnEnemiesCoroutine(int amountToSpawn, float waveDuration)
        {
            var timePerSpawn = waveDuration / amountToSpawn;
            var elapsedTime = timePerSpawn;
            var spawnedEnemies = amountToSpawn;
            while (spawnedEnemies > 0)
            {
                elapsedTime -= Time.deltaTime;
                if (elapsedTime < 0f)
                {
                    elapsedTime = timePerSpawn;
                    spawnedEnemies -= 1;
                    Spawn();
                }
                yield return null;
            }
            yield return null;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube( Vector3.zero, new Vector3(MapBounds.x, 0f, MapBounds.y));
        }
        
        #endregion
    }
}
