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

        [Header("References")]
        public ObjectPool EnemyPool;
        public List<Enemy> AllEnemies = new();

        [Header("Enemy Types")]
        public EnemyType[] enemyTypes;
     

        [Header("Settings")]
        public Vector2 MapBounds;
        private Vector2 _spawnExtent;
        private int _spawnedEnemies;
        private IEnumerator _spawnCoroutine;

        public int spawnCurrency; //the spawnValue the enemy manager has per wave to determine the enemy types it can spawn
        public List<int> enemyTypesToSpawn = new ();
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


        public void DespawnEnemy(Enemy enemy)
        {
            EnemyPool.ReleasePooledObject(enemy);    
        }

        #endregion

        #region PrivateFunctions

       
        public void SpawnEnemies(int spawnPoints, float waveDuration)
        {
            spawnCurrency = spawnPoints;
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
            StartCoroutine(_spawnCoroutine = SpawnEnemiesCoroutine(waveDuration));
        }

        private IEnumerator SpawnEnemiesCoroutine(float waveDuration) //spawns the wave of enemies
        {
            DetermineEnemyTypesToSpawn();
            var amountToSpawn = enemyTypesToSpawn.Count;
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
                    Spawn(enemyTypesToSpawn[0]);
                    enemyTypesToSpawn.RemoveAt(0);
                }
                yield return null;
            }
            yield return null;
        }
        ///Spawn the enemy, name it, set the player up as its target, then add to the enemies list. 
        private void Spawn(int enemyTypeIndex) //spawns one enemy
        {
            
            var enemy = (Enemy)EnemyPool.GetPooledObject();
            if (EnemyPool.NewObjectAdded) AllEnemies.Add(enemy);
            enemy.transform.position = DetermineSpawnLocation();
            enemy.enemyTypeIndex = enemyTypeIndex;
            enemy.SetEnemyType(enemyTypes[enemyTypeIndex]);

        }
        /// Spawn enemies either north, south, east, or west, then random the other coordinate within the
        /// spawn extent. 
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
        private void DetermineEnemyTypesToSpawn()
        {
            while (spawnCurrency > 0)
            {
                //int type = Random.Range(0, enemyTypes.Length);
                int type = GetRandomType();
                if (enemyTypes[type].spawnValue <= spawnCurrency)
                {
                    spawnCurrency -= enemyTypes[type].spawnValue;
                    enemyTypesToSpawn.Add(type);
                }
                
            }
           
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube( Vector3.zero, new Vector3(MapBounds.x, 0f, MapBounds.y));
        }
        
        #endregion
        private int GetRandomType()
        {
            float rnd = Random.value;
            float numForAdding = 0f;
            float total = 0f;

            for (int i = 0; i < enemyTypes.Length; i++)
            {
                total += enemyTypes[i].spawnChance;
            }

            for (int i = 0; i < enemyTypes.Length; i++)
            {
                if (enemyTypes[i].spawnChance / total + numForAdding >= rnd)
                {
                    return i;
                }
                else
                {
                    numForAdding += enemyTypes[i].spawnChance / total;
                }
            }
            return 0;
        }
    }
}
