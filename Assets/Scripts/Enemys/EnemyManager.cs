using System.Collections.Generic;
using UnityEngine;

namespace Enemys
{
    /// <summary> The enemy manager spawns in the enemies as told by the wave manager and keeps track of all enemies spawned,
    /// along with initializing the enemies as they are spawned. </summary>
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        [Header("References")]
        public GameObject EnemyPrefab;
        public List<Enemy> Enemies = new();

        [Header("Enemy Types")]
        // EnemyType[] enemyTypes;


        [Header("Settings")]
        public Vector2 MapBounds;
        private Vector2 _spawnExtent;
        private int _spawnedEnemies;

        private void Awake()
        {
            Instance = this;
            // To get the radius and not the diameter:
            _spawnExtent = MapBounds / 2;
        }

        [ContextMenu("SpawnWave")]
        public void Spawn10()
        {
            SpawnWave(3);
        }
        
        public void SpawnWave(int spawnAmount)
        {
       
            while (_spawnedEnemies < spawnAmount)
            {
                // ToDo: Replace with pooling:
                Spawn();
                _spawnedEnemies++;
            }
            _spawnedEnemies = 0;
        }

        /// <summary> Spawn the enemy, name it, set the player up as its target, then add to the enemies list. </summary>
        private void Spawn()
        {
            // ToDo: Replace with pooling:
            var spawnedPrefab = Instantiate(EnemyPrefab, DetermineSpawnLocation(), Quaternion.identity);
            var spawnedEnemy = spawnedPrefab.GetComponent<Enemy>();
            spawnedEnemy.name = "Enemy" + (Enemies.Count + 1);
            Enemies.Add(spawnedEnemy);
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
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube( Vector3.zero, new Vector3(MapBounds.x, 0f, MapBounds.y));
        }
    }
}
