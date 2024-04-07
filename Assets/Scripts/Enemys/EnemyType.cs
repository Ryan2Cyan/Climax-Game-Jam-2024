using UnityEngine;

namespace Enemys
{
    [CreateAssetMenu]
    [System.Serializable]
    public class EnemyType : ScriptableObject
    {
        public EnemyManager.EnemyTypeEnum Type;
        public Material Material;
        public Vector3 Scale;
        public string Name;
        public float Speed;
        public float AttackCooldown;
        public float MeleeRange;
        public int MaxHealth;
        public int Damage;
        public int SpawnValue;
        
        public EnemyType(EnemyManager.EnemyTypeEnum type, Material material, Vector3 scale, string name, int maxHealth, 
            int damage, float speed, float meleeRange, float attackCooldown, int spawnValue)
        {
            Type = type;
            Material = material;
            Scale = scale;
            Name = name;
            Speed = speed;
            AttackCooldown = attackCooldown;
            MeleeRange = meleeRange;
            MaxHealth = maxHealth;
            Damage = damage;
            SpawnValue = spawnValue;
        }
    }
}