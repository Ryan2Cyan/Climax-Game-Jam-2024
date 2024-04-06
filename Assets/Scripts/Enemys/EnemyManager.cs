using System.Collections;
using System.Collections.Generic;
using Enemys;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [Header("References")]
    public GameObject EnemyPrefab;
    public GameObject Player;

    public List<Enemy> Enemies = new List<Enemy>();

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
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (!Enemies[i].IsAlive())
            {
                Destroy(Enemies[i].gameObject);
                Enemies.RemoveAt(i);
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
       var spawnedPrefab = Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
       Enemy spawnedEnemy = spawnedPrefab.GetComponent<Enemy>();
       spawnedEnemy.name = "Enemy" + (Enemies.Count + 1);
       Enemies.Add(spawnedEnemy);
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
