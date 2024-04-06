using System;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemys
{
    public class Enemy : MonoBehaviour
    {
        public int Health;
        public float Speed;
        public int Damage;
        public float AttackCooldown;

        private float _attackTimer;
        private float _targetUpdateTimer;

        private Transform _currentTarget;
        private Vector3 _position;
        private Vector3 _move;

        private bool _isAlive = true;

        private enum State
        {
            Idle,
            Move,
            Attack,
            Die
        }
        private State _currentState;

        #region UnityFunctions

        private void Start()
        {
            TargetUpdate();
            _currentState = State.Idle;
        }

        private void Update()
        {
            _position = transform.position;

            // Every 5 seconds change to a new target between the ghosts and the player: 
            if (_targetUpdateTimer >= 1)
            {
                TargetUpdate();
                _targetUpdateTimer = 0;
            }
            else _targetUpdateTimer += Time.deltaTime;

            switch (_currentState)
            {
                case State.Idle: Idle(); break;
                case State.Move: Move(); break;
                case State.Attack: Attack(); break;
                case State.Die: OnDeath(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        
        }
        
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Player":
                    _currentState = State.Attack;
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
                    _currentState = State.Move;
                    break;
                case "Enemy":
                    _move *= -1;
                    break;
            }
        }

        #endregion

        #region PublicFunctions

        public void SetTarget(Transform newTarget)
        {
            _currentTarget = newTarget;
        }

        public bool IsAlive()
        {
            return _isAlive;
        }

        #endregion

        #region PrivateFunctions

        private void Idle()
        {
            _currentState = State.Move;
        }
        private void Move()
        {
       
            //move towards the target (the player) by step each frame
            var step = Speed * Time.deltaTime;
            _move = Vector3.MoveTowards(_position, _currentTarget.transform.position, step);
            transform.position = _move;
        }
        
        private void Attack()
        {
            if (_attackTimer > AttackCooldown) PlayerManager.Instance.OnDamaged(Damage);
            else _attackTimer += Time.deltaTime;
        }

        private void OnDeath()
        {
            // Sets a death flag, for the enemy manager to delete this enemy:
            _isAlive = false;
        }

        public void OnDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) _currentState = State.Die;
        }

        private void TargetUpdate()
        {
            // This randomly sets the enemies target to either be the player or one of 4 "ghost" objects that are children
            //of the player, this helps to stop all the clumping together of the large number of enemies somewhat:
            var randomIndex = Random.Range(0, PlayerManager.Instance.Ghosts.Count);
            
            // If it equals four, just follow the player as normal:
            if (randomIndex == 4) _currentTarget = PlayerManager.Instance.transform;
            var ghost = PlayerManager.Instance.Ghosts[randomIndex].transform;
            _currentTarget = ghost;
        }
        
        #endregion
    }
}

