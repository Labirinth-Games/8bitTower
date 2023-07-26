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
    [SerializeField] private float speed = 2;
    [SerializeField] private float fastSpeed = 4;

    private Utils.SpriteUtils _spriteUtils;
    private Helpers.DiceHelper _dice;
    private UI.HitUI _hitUI;
    private bool _isDeath = false;

    private float _fireRate = .2f;
    private float _fireRateCooldown;

    private bool _hasAttack = false;

    // to attack configs
    private float _startTimer;
    private float _holdMedium = .5f;
    private float _holdLong = 1f;

    #region Actions
    public void Move()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        float currentSpeed = speed;

        if (xMove != 0 || yMove != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            currentSpeed = fastSpeed;

        if (xMove != 0)
        {
            _spriteUtils.Flip(xMove, GetComponentInChildren<Renderer>().transform.localScale);
            transform.position += Vector3.right * xMove * currentSpeed * Time.deltaTime;
        }

        if (yMove != 0)
        {
            transform.position += Vector3.up * yMove * currentSpeed * Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startTimer = Time.time;
            Debug.Log(("Loadding"));
        }

        if (Input.GetKeyUp(KeyCode.Space) && Time.time > _fireRateCooldown)
        {
            _fireRateCooldown = Time.time + _fireRate;
            _hasAttack = true;

            if (_startTimer + _holdLong < Time.time)
            {
                Debug.Log(("hold long"));


            }
            else if (_startTimer + _holdMedium < Time.time)
            {
                Debug.Log(("hold medium"));

            }
            else
            {
                Debug.Log(("hold short"));
                animator.SetTrigger("Attack");
            }
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
    public bool IsDeath() { return _isDeath; }
    public float GetHp() { return hp; }
    #endregion

    #region Colliders
    private void OnTriggerEnter2D(Collider2D collision)
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

public class PressAttack
{
    private float _startTimer;
    private float _holdShort = .5f;
    private float _holdMedium = 1f;
    private float _holdLong = 2f;

    public void Hold()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startTimer = Time.time;
            Debug.Log(("Loadding"));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(_startTimer + _holdLong > Time.time)
            {
                Debug.Log(("hold long"));

            }
            else if (_startTimer + _holdMedium > Time.time)
            {
                Debug.Log(("hold medium"));

            }
            else if (_startTimer + _holdShort > Time.time)
            {
                Debug.Log(("hold short"));

            }
        }
    }
}