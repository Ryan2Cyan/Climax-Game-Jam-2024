using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //stats
    public int health;
    public float speed;
    public int damage;
    public float attackCooldown;

    private float attackTimer;
    private float targetUpdateTimer;

    private GameObject target;
    private GameObject player;
    private Vector3 position;
    private Vector3 move;

    private bool isAlive = true;

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
        player = target;
        TargetUpdate();
        currentState = State.Idle;
    }
    void Update()
    {
        position = transform.position;

        //every 5 seconds change to a new target between the ghosts and the player 
        if (targetUpdateTimer >= 1)
        {
            TargetUpdate();
            targetUpdateTimer = 0;
        }
        else
        {
            targetUpdateTimer += Time.deltaTime;
        }

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
        move = Vector3.MoveTowards(position, target.transform.position, step);
        transform.position = move;
       
        
    }
    private void Attack()
    {
        if (attackTimer > attackCooldown)
        {
            //print(gameObject.name + " attacks!");
            
            player.GetComponent<Player.PlayerManager>().OnDamaged();
        }
        else
        {
            attackTimer += Time.deltaTime;
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
        switch (other.gameObject.tag)
        {
            case "Player":
                currentState = State.Attack;
                break;
            case "Enemy":
                
                break;

        }

    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                currentState = State.Move;
                break;
            case "Enemy":
                move *= -1;
                break;
        }
    }
    void TargetUpdate()
    {
        //this randomly sets the enemies target to either be the player or one of 4 "ghost" objects that are children
        //of the player, this helps to stop all the clumping together of the large number of enemies somewhat

        int randomIndex = Random.Range(0, 5);
        if (randomIndex != 4) //if it equals four, just follow the player as normal
        {
            TargetGhost[] ghosts = player.GetComponentsInChildren<TargetGhost>();
            if (ghosts[randomIndex] != null)
            {
                GameObject ghost = ghosts[randomIndex].gameObject;
                target = ghost;
            }
        }
    }


    //setters
    public void setTarget(GameObject newTarget){ target = newTarget;}
    //getters
    public bool IsAlive() { return isAlive;}
}

