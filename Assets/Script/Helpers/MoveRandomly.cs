using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace Helpers
{
    public class MoveRandomly : MonoBehaviour
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float characterVelocity;

        [Header("Callbacks")]
        public UnityEvent OnCollision;
        public UnityEvent<float, float> OnMove;
        public UnityEvent OnStop;
        public UnityEvent OnContinue;
        public UnityEvent OnStart;
        

        private float cooldownTimestamp;
        private Vector2 movementDirection;
        private Vector2 movementPerSecond;
        private bool _stop = true;

        public void calcuateNewMovementVector()
        {
            // create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
            var dirX = Random.Range(-1.0f, 1.0f);
            var dirY = Random.Range(-1.0f, 1.0f);

            movementDirection = new Vector2(dirX, dirY).normalized;
            movementPerSecond = movementDirection * characterVelocity;

            OnMove?.Invoke(dirX, dirY);
        }

        public void Init()
        {
            _stop = false;
            OnStart?.Invoke();
        }

        public void Stop()
        {
            _stop = true;
            OnStop?.Invoke();
        }

        public void Continue()
        {
            _stop = false;
            OnContinue?.Invoke();
        }

        public void Move()
        {
            // if the changeTime was reached, calculate a new movement vector
            if (Time.time > cooldownTimestamp)
            {
                cooldownTimestamp = Time.time + cooldown;
                calcuateNewMovementVector();
            }

            // move enemy: 
            float directionX = transform.position.x + (movementPerSecond.x * Time.deltaTime);
            float directionY = transform.position.y + (movementPerSecond.y * Time.deltaTime);

            transform.position = new Vector2(directionX, directionY);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            cooldownTimestamp = Time.time + cooldown;
            calcuateNewMovementVector();
        }

        private void Update()
        {
            if (_stop) return;

            Move();
        }
    }
}
