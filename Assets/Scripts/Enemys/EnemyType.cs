using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class EnemyType : ScriptableObject
{
    public int health = 10;
    public int damage = 1;
    public float speed = 5;
    public float attackCooldown = 1;
    [Header("Ranged")]
    public bool isRanged = false;
    public float rangeRadius = 5;
    [Header("Explode On Death")]
    public bool explodeOnDeath = false;
    public float explosionRadius = 5;
    [Header("Spawn Parameters")]
    public int spawnValue;
    public float spawnChance;
    public EnemyType(int health, int damage, float speed, float attackCooldown, bool isRanged,float rangeRadius,bool explodeOnDeath,float explosionRadius, int spawnValue, float spawnChance)
    {
        this.health = health;
        this.damage = health;
        this.speed = speed;
        this.attackCooldown = attackCooldown;
        this.isRanged = isRanged;
        this.rangeRadius = rangeRadius;
        this.explodeOnDeath = explodeOnDeath;
        this.explosionRadius = explosionRadius;
        this.spawnValue = spawnValue;
        this.spawnChance = spawnChance;
    }
}