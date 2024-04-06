using System.Collections;
using System.Collections.Generic;
using General;
using Player;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float speed = 5;
    public int damage = 1;
    public float bulletLifespan = 2;
    private float lifespan;
    private Enemys.Enemy bulletOwner;
  
    Vector3 direction;
    public void Instantiate()
    {
        lifespan = bulletLifespan;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
    public void InitBullet(int newDamage, Enemys.Enemy newBulletOwner)
    {
        bulletOwner = newBulletOwner;
        damage = newDamage;
        var target = PlayerManager.Instance.transform.position;
        direction = (target - transform.position).normalized;
    }


    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (lifespan < 0)
        {
            bulletOwner.DespawnBullet(this);

        }
        else
        {
            lifespan -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "Player":
                {
                    print("Hit!");
                    PlayerManager.Instance.OnDamaged(damage);
                }
                break;

        }
    }
    public void Release()
    {
        gameObject.SetActive(false);
    }

}
