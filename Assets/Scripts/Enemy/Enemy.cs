using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health;
    [SerializeField]float speed;
    int damage;
    [SerializeField] float attackCooldown;
    float timer;
    GameObject target;
    Vector3 position;
    bool isAlive = true;

    public enum State
    {
        Idle,
        Move,
        Attack,
        Die,
    }
    private State currentState;
    void Start()
    {
        currentState = State.Idle;
    }
    void Update()
    {
        position = transform.position;

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Die:
                Die();
                break;
        }
        
    }
    private void Idle()
    {
        currentState = State.Move;
    }
    private void Move()
    {
        //move towards the target (the player) by step each frame
        var step = speed * Time.deltaTime;
        var move = Vector3.MoveTowards(position, target.transform.position, step);
        transform.position = move;
    }
    private void Attack()
    {
        if (timer > attackCooldown)
        {
            print(gameObject.name + " attacks!");
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    void Die()
    {
        //sets a death flag, for the enemy manager to delete this enemy
        isAlive = false;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            currentState = State.Die;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            currentState = State.Attack;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            currentState = State.Move;
        }
    }


    //setters
    public void setTarget(GameObject newTarget){ target = newTarget;}
    //getters
    public bool IsAlive() { return isAlive;}
}

