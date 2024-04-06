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
    public bool isRanged = false;
    public EnemyType(int health, int damage, float speed, float attackCooldown, bool isRanged)
    {
        this.health = health;
        this.damage = health;
        this.speed = speed;
        this.attackCooldown = attackCooldown;
        this.isRanged = isRanged;
    }
}