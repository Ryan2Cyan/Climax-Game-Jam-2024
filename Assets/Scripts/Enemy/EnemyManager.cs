using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField]GameObject enemyPrefab;
    [SerializeField] GameObject player;

    List<Enemy> enemies = new List<Enemy>();

  
    [Header("Settings")]
    [SerializeField] Vector3 spawnPos;
    int spawnedEnemies;
   
    // The enemy manager spawns in the enemies as told by the wave manager
    // and keeps track of all enemies spawned, along with initializing 
    // the enemies as they are spawned

    void Update()
    {
        //check for enemies not beibg alive, and clean them up
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].IsAlive())
            {
                Destroy(enemies[i].gameObject);
                enemies.RemoveAt(i);
            }
        }
       
    }
    public void SpawnWave(int spawnAmount)
    {
        while (spawnedEnemies < spawnAmount)
        {
            Spawn();
            spawnedEnemies++;
        }
        spawnedEnemies = 0;
    }
    void Spawn()
    {
        //add a random offset to the spawnPos to stop all the enemies clumping together
       var rngSpawnPos = spawnPos;
       rngSpawnPos.x += Random.Range(-5, 5);
       rngSpawnPos.z += Random.Range(-5, 5);

       //spawn the enemy, name it, set the player up as its target, then add to the enemies list
       var spawnedPrefab = Instantiate(enemyPrefab, rngSpawnPos, Quaternion.identity);
       Enemy spawnedEnemy = spawnedPrefab.GetComponent<Enemy>();
       spawnedEnemy.name = "Enemy" + (enemies.Count + 1);
       spawnedEnemy.setTarget(player);
       enemies.Add(spawnedEnemy);
    }
}
