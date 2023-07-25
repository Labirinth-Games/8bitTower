using Enemy;
using Helpers;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utils;

[RequireComponent(typeof(SpriteUtils))]
public class Player : MonoBehaviour
{
    [Header("References")]
    public Animator animator;

    [Header("Stats")]
    [SerializeField] private float intelligence;
    [SerializeField] private float strength;
    [SerializeField] private float dexterity;
    [SerializeField] private float constitution;
    [SerializeField] private float charisma;
    [SerializeField] private float perception = .5f; // size used to detection if player its close.
    [SerializeField] private float protection = 5;
    [SerializeField] private DiceType diceAttack = DiceType.D6;

    [SerializeField] private float maxHp = 26;
    [SerializeField] private float hp = 26;
    [SerializeField] private float speed = 0.2f;

    private Utils.SpriteUtils _spriteUtils;
    private Helpers.DiceHelper _dice;
    private UI.HitUI _hitUI;
    private bool _isDeath = false;

    private float _fireRate = .5f;
    private float _fireRateCooldown;

    private bool _hasAttack = false;

    #region Actions
    public void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        if (xMove != 0 || yMove != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        if (xMove != 0)
        {
            _spriteUtils.Flip(xMove, GetComponentInChildren<Renderer>().transform.localScale);
            transform.position += Vector3.right * xMove * speed * Time.deltaTime;
        }

        if (yMove != 0)
        {
            transform.position += Vector3.up * yMove * speed * Time.deltaTime;
        }
    }

    public void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _fireRateCooldown)
        {
            _fireRateCooldown = Time.time + _fireRate;

            animator.SetTrigger("Attack");

            _hasAttack = true;
        }
    }

    public void Hit(float damage)
    {
        if(_isDeath) return;

        if (hp < 0) Die();
        
        if(damage >= protection)
        {
            hp -= damage;
            _hitUI.Display(damage.ToString());
        }
        else
        {
            _hitUI.Display("Miss");
        }
    }

    private void Die() 
    {
        animator.SetTrigger("Die");
        _isDeath = true;
    }
    #endregion

    #region Gets/Sets
    public bool IsDeath()
    {
        return _isDeath;
    }
    #endregion

    #region Colliders
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") && _hasAttack)
        {
            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            enemy?.Hit(_dice.Roll(diceAttack) + strength);

            _hasAttack = false;
        }
    }
    #endregion

    #region Unity Events
    private void Update()
    {
        if (_isDeath) return;

        Move();
        Attack();
    }

    private void Start()
    {
        if (_spriteUtils == null)
            _spriteUtils = GetComponent<SpriteUtils>();

        if (_dice == null)
            _dice = GetComponent<DiceHelper>(); 

        if (_hitUI == null)
            _hitUI = GetComponent<HitUI>();
    }
    #endregion
}
