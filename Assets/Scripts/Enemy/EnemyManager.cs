using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("Refrences")]
    public GameObject enemyPrefab;
    public GameObject player;

    private List<Enemy> enemies = new List<Enemy>();

    [Header("Enemy Types")]
  // EnemyType[] enemyTypes;


    [Header("Settings")]
    private Vector3 spawnPos;
    public Vector3 mapBounds;
    private Vector3 spawnExtent;
    private int spawnedEnemies;

    // The enemy manager spawns in the enemies as told by the wave manager
    // and keeps track of all enemies spawned, along with initializing 
    // the enemies as they are spawned

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        spawnExtent = mapBounds / 2; //to get the radius and not the diameter
    }
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
        DetermineSpawnLocation();
       

        //spawn the enemy, name it, set the player up as its target, then add to the enemies list
       var spawnedPrefab = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
       Enemy spawnedEnemy = spawnedPrefab.GetComponent<Enemy>();
       spawnedEnemy.name = "Enemy" + (enemies.Count + 1);
       spawnedEnemy.setTarget(player);
       enemies.Add(spawnedEnemy);
    }
    void DetermineSpawnLocation()
    {
       //roll a d4 to spawn enemies either north south east or west, then random the other coordinate within the spawnextent
        var spawnLocationRoll = Random.Range(0, 4);
        switch (spawnLocationRoll)
        {
            case 0:
                spawnPos.x = spawnExtent.x; // north
                spawnPos.z = Random.Range(-spawnExtent.z, spawnExtent.z); ;
                break;
            case 1:
                spawnPos.z = spawnExtent.z; // east
                spawnPos.x = Random.Range(-spawnExtent.x, spawnExtent.x); ;
                break;
            case 2:
                spawnPos.x = -spawnExtent.x; // south
                spawnPos.z = Random.Range(-spawnExtent.z, spawnExtent.z); ;
                break;
            case 3:
                spawnPos.z = -spawnExtent.z; // west
                spawnPos.x = Random.Range(-spawnExtent.x, spawnExtent.x); ;
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube( Vector3.zero, mapBounds);
    }
}
