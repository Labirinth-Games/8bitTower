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
        [SerializeField] protected float intelligence;
        [SerializeField] protected float strength;
        [SerializeField] protected float dexterity;
        [SerializeField] protected float constitution;
        [SerializeField] protected float protection;
        [SerializeField] protected float perception = .5f; // size used to detection if player its close.
        [SerializeField] protected DiceType diceAttack;

        [SerializeField] protected float maxHp;
        [SerializeField] protected float hp;
        [SerializeField] protected float velocity; // movement of the velocity
        [SerializeField] protected bool isAggressive = true;

        protected CircleCollider2D _perceptionZone;

        public virtual void Hit(float damage) { }
        public virtual void Die() 
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _perceptionZone.radius = perception;
        }

        private void OnValidate()
        {
            if(_perceptionZone == null)
                _perceptionZone = GetComponent<CircleCollider2D>();
        }
    }
}
