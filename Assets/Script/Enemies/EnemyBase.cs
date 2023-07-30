using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyBase : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected Animator animator;

        [Header("Stats")]
        [SerializeField] protected StatsScriptableObject stats;

        [SerializeField] protected float maxHp;
        [SerializeField] protected float hp;
        [SerializeField] protected float velocity; // movement of the velocity
        [SerializeField] protected bool isAggressive = true;
        [SerializeField] protected bool canMove = true;

        protected CircleCollider2D _perceptionZone;

        public virtual void Hit(float damage) { }
        public virtual void Die() 
        {
            Destroy(gameObject);
        }

        protected virtual void Start()
        {
            _perceptionZone.radius = stats.perception;
        }

        protected virtual void OnValidate()
        {
            if(_perceptionZone == null)
                _perceptionZone = GetComponent<CircleCollider2D>();
        }
    }
}
