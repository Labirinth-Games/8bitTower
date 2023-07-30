using Helpers;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utils;
using DG.Tweening;
using Unity.VisualScripting;

namespace Enemy
{
    [RequireComponent(typeof(SpriteUtils))]
    public class Troll : EnemyBase
    {
        [Header("References Local")]
        [SerializeField] private Helpers.MoveRandomly _movement;
        
        private Utils.SpriteUtils _spriteUtils;
        private UI.HitUI _hitUI;

        private float _cooldown = 1f;
        private float _attackcooldown;

        #region Actions
        public void Attack(Player player)
        {
            if (Time.time < _attackcooldown) return;
            
            _attackcooldown = Time.time + _cooldown / stats.dexterity;

            animator.SetTrigger("Attack");
            player.Hit(DiceHelper.Roll(stats.damagerDice) + stats.strength);
        }

        public override void Hit(float damage)
        {
            if (hp < 0) Die();

            if (damage >= stats.protection)
            {
                hp -= damage;
                _hitUI.Display(damage.ToString());
            }
            else
            {
                _hitUI.Display("Miss");
            }
        }
      
        private void PassiveMode()
        {
            animator.SetBool("Walk", true);
            _movement.Continue();
        }

        private void AggroMode(Player player)
        {
            _movement.Stop();
            animator.SetBool("Walk", false);

            SpriteUtils.Flip(player.transform.position.x - transform.position.x, GetComponentInChildren<Renderer>().transform);

            Attack(player);
        }
        #endregion

        #region Collisions
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player") && isAggressive)
            {
                Player player = collision.GetComponent<Player>();

                if (player.IsDeath())
                {
                    PassiveMode();
                    isAggressive = false;
                    return;
                }

                transform.DOMove(player.transform.position, 2f);
                AggroMode(player);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                PassiveMode();
            }

        }
        #endregion

        private void Subscribers()
        {
            _movement.OnMove.AddListener((dirX, dirY) =>
            {
                SpriteUtils.Flip(dirX, GetComponentInChildren<Renderer>().transform);
                animator.SetBool("Walk", true);
            });
        }

        protected override void Start()
        {
            base.stats.perception = .4f;
            base.stats.strength = 2f;
            base.stats.dexterity = 1f;
            base.stats.protection = 4f;
            base.stats.damagerDice = DiceType.D6;
            
            base.hp = 5;
            base.maxHp = 5;
            base.velocity = 1;

            if (canMove) _movement.Start();

            Subscribers();       

            base.Start();
        }

        protected override void OnValidate()
        {
            if (_spriteUtils == null)
                _spriteUtils = GetComponent<SpriteUtils>(); 
            
            if (_hitUI== null)
                _hitUI = GetComponent<HitUI>();

            base.OnValidate();
        }
    }
}