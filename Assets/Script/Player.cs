using DG.Tweening;
using Enemy;
using Helpers;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine.InputSystem;
using UnityEngine;
using Utils;

[RequireComponent(typeof(SpriteUtils))]
public class Player : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Helpers.AttackHoldHelpers attackHold;
    public GameObject dashPrefab;
    public Accessories.Bag bag;

    [Header("Attacks")]
    public GameObject fireballPrefab;
    public GameObject bigBlasterPrefab;

    [Header("Stats")]
    [SerializeField] private StatsScriptableObject stats;
    

    [SerializeField] private float maxHp = 26;
    [SerializeField] private float hp = 26;
    [SerializeField] private float speed = 2;
    [SerializeField] private float fastSpeed = 4;
    [SerializeField] private float cooldownDash = 1f;

    private GlobalControls _globalControls;
    private UI.HitUI _hitUI;
    private bool _isDeath = false;

    private bool _hasAttack = false;
    private float _playerFaceSide = 1;
    private float _timeDashCooldown;

    #region Actions
    public void Move()
    {
        Vector2 movement = _globalControls.Player.Movement.ReadValue<Vector2>();

        if (movement == null) return;

        float currentSpeed = speed;

        if (movement.x != 0 || movement .y != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        //if (Input.GetKeyDown(KeyCode.LeftShift))
        //    currentSpeed = fastSpeed;

        if (movement.x != 0)
        {
            _playerFaceSide = movement.x;

            SpriteUtils.Flip(movement.x, GetComponentInChildren<Renderer>().transform);
            transform.position += Vector3.right * movement.x * currentSpeed * Time.deltaTime;
        }

        if (movement.y != 0)
        {
            transform.position += Vector3.up * movement.y * currentSpeed * Time.deltaTime;
        }
    }

    public void Dash()
    {
        if(_globalControls.Player.Dash.triggered && Time.time > _timeDashCooldown)
        {
            _timeDashCooldown = Time.time + cooldownDash;

            var instante = Instantiate(dashPrefab);
            instante.transform.position = new Vector2(transform.position.x, transform.position.y - .3f);
            SpriteUtils.Flip(_playerFaceSide, instante.transform);

            transform.position += Vector3.right * _playerFaceSide * speed * (.3f * stats.dexterity);
            Destroy(instante, .6f);
        }
    }

    public void Attack()
    {
       attackHold?.Hold(_globalControls);
    }

    public void Hit(float damage)
    {
        if(_isDeath) return;

        if (hp < 0) Die();
        
        if(damage >= stats.protection)
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
        // attack hit box
        if (collision.transform.CompareTag("Enemy") && _hasAttack)
        {
            _hasAttack = false;

            EnemyBase enemy = collision.GetComponent<EnemyBase>();
            enemy?.Hit(DiceHelper.Roll(stats.damagerDice) + stats.strength);
        }
    }
    #endregion

    private void Subscribers()
    {
        // config attack hold
        if (attackHold != null)
        {
            attackHold.OnAttackFire.AddListener(() => { _hasAttack = true; });
            attackHold.OnShortPress.AddListener(() => { animator.SetTrigger("Attack"); });
            attackHold.OnMediumPress.AddListener(() =>
            {
                animator.SetTrigger("Attack Medium");

                var instante = Instantiate(fireballPrefab);

                instante.GetComponent<Fireball>().SetDirection(_playerFaceSide);
                SpriteUtils.Flip(_playerFaceSide, instante.transform);
                instante.transform.position = new Vector2(transform.position.x + (.5f * _playerFaceSide), transform.position.y);
            }); 
            
            attackHold.OnLongPress.AddListener(() =>
            {
                animator.SetTrigger("Attack Medium");

                var instante = Instantiate(bigBlasterPrefab);

                instante.GetComponent<BigBlaster>().SetDirection(_playerFaceSide);
                instante.transform.position = new Vector2(transform.position.x + (.5f * _playerFaceSide), transform.position.y);
            });
        }
    }

    #region Unity Events
    private void Update()
    {
        if (_isDeath) return;

        Move();
        Attack();
        Dash();
    }

    private void Start()
    {
        if (_hitUI == null)
            _hitUI = GetComponent<HitUI>();

        if (bag == null)
            bag = GetComponent<Accessories.Bag>();

        Subscribers();
    }

    private void Awake()
    {
        _globalControls = new GlobalControls();        
    }

    private void OnEnable()
    {
        _globalControls.Enable();
    }

    private void OnDisable()
    {
        _globalControls.Disable();
    }
    #endregion
}
