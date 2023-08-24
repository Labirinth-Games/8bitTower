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
using Learn;

[RequireComponent(typeof(SpriteUtils))]
public class Player : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Helpers.AttackHoldHelpers attackHold;
    public GameObject dashPrefab;
    public Accessories.Bag bag;

    [Header("Stats")]
    [SerializeField] private StatsScriptableObject stats;

    //[SerializeField] private float maxHp = 26;
    [SerializeField] private float hp = 26;
    [SerializeField] private float speed = 2;
    //[SerializeField] private float fastSpeed = 4;
    [SerializeField] private float cooldownDash = 1f;

    private UI.HitUI _hitUI;
    private bool _isDeath = false;

    [SerializeField] private bool _hasAttack = false;
    private float _playerFaceSide = 1;
    private float _timeDashCooldown;

    #region Actions
    public void Move()
    {
        Vector2 movement = GameManager.Instance.globalControls.Player.Movement.ReadValue<Vector2>();

        if (movement == null) return;

        animator.SetBool("Walk", (movement.x != 0 || movement.y != 0));

        if (movement.x != 0)
        {
            _playerFaceSide = movement.x;

            SpriteUtils.Flip(movement.x, GetComponentInChildren<Renderer>().transform);
            transform.position += Vector3.right * movement.x * speed * Time.deltaTime;
        }
        else if (movement.y != 0)
        {
            transform.position += Vector3.up * movement.y * speed * Time.deltaTime;
        }
    }

    public void Dash()
    {
        if (GameManager.Instance.globalControls.Player.Dash.triggered && Time.time > _timeDashCooldown)
        {
            _timeDashCooldown = Time.time + cooldownDash;

            // sprite smoke when doing the dash
            var instante = Instantiate(dashPrefab);
            instante.transform.position = new Vector2(transform.position.x, transform.position.y - .3f);
            SpriteUtils.Flip(_playerFaceSide, instante.transform);

            // move player when do dash
            transform.position += Vector3.right * _playerFaceSide * speed * (.3f * stats.dexterity);
            Destroy(instante, .6f);
        }
    }

    public void Attack()
    {
        attackHold?.Hold(GameManager.Instance.globalControls);
    }

    public void Hit(float damage)
    {
        if (_isDeath) return;

        if (damage >= stats.protection)
        {
            hp -= damage;
            _hitUI.Display(damage.ToString());

            if (hp <= 0) Die();
        }
        else
        {
            // TODO - no futudo aqui alem de dar miss pode colocar uma anima��o de escudo
            _hitUI.Display("Miss");
        }
    }

    private void Die()
    {
        _isDeath = true;

        animator.SetTrigger("Die");
        GameManager.Instance.GameOver();
    }

    private void HitBoxRemove()
    {
        _hasAttack = false;
    }
    #endregion

    #region Gets/Sets
    public bool IsDeath() { return _isDeath; }
    public float GetHp() { return hp; }
    #endregion

    #region Colliders
    private void OnTriggerStay2D(Collider2D collision)
    {
        // attack hit box
        if (collision.TryGetComponent<EnemyBase>(out EnemyBase enemy) && _hasAttack)
        {
            _hasAttack = false;
            enemy.Hit(DiceHelper.Roll(stats.damagerDice) + stats.strength);
        }
    }
    #endregion

    private void Subscribers()
    {
        // config attack hold
        if (attackHold != null)
        {
            attackHold.OnAttackFire.AddListener(() =>
            {
                _hasAttack = true;
                Invoke(nameof(HitBoxRemove), .2f);
            });
            attackHold.OnShortPress.AddListener(() =>
            {
                animator.SetTrigger("Attack");
            });
            attackHold.OnMediumPress.AddListener(() =>
            {
                if(GameManager.Instance.learnManager.attackSecond != null)
                {
                    animator.SetTrigger("Attack Medium");

                    var instante = Instantiate(GameManager.Instance.learnManager.attackSecond);

                    instante.GetComponent<LearnBase>().SetDirection(_playerFaceSide);
                    SpriteUtils.Flip(_playerFaceSide, instante.transform);
                    instante.transform.position = new Vector2(transform.position.x + (.5f * _playerFaceSide), transform.position.y);
                }

            });

            attackHold.OnLongPress.AddListener(() =>
            {
                if(GameManager.Instance.learnManager.attackThirth != null)
                {
                    animator.SetTrigger("Attack Medium");

                    var instante = Instantiate(GameManager.Instance.learnManager.attackThirth);

                    instante.GetComponent<LearnBase>().SetDirection(_playerFaceSide);
                    instante.transform.position = new Vector2(transform.position.x + (.5f * _playerFaceSide), transform.position.y);
                }
            });
        }
    }

    #region Unity Events
    private void Update()
    {
        if (_isDeath || GameManager.Instance.isPaused) return;

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
    #endregion
}
