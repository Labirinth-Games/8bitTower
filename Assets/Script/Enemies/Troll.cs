using Helpers;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utils;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Enemy
{
    [RequireComponent(typeof(SpriteUtils))]
    public class Troll : EnemyBase
    {
        [Header("References Local")]
        [SerializeField] private Helpers.MoveRandomly _movement;
        
        private Utils.SpriteUtils _spriteUtils;
        private Helpers.DiceHelper _dice;
        private UI.HitUI _hitUI;

        private float _cooldown = 1f;
        private Coroutine _attackCoroutine;

        #region Actions
        IEnumerator Attack(Player player)
        {
           while(true)
           {
                animator.SetTrigger("Attack");
                player.Hit(_dice.Roll(diceAttack) + strength);

                yield return new WaitForSeconds(_cooldown / dexterity);
           }
        }

        public override void Hit(float damage)
        {
            if (hp < 0) Die();

            if (damage >= protection)
            {
                hp -= damage;
                _hitUI.Display(damage.ToString());
            }
            else
            {
                _hitUI.Display("Miss");
            }
        }
      
        #endregion

        #region Collisions
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player") && isAggressive)
            {
                Player player = collision.GetComponent<Player>();

                if (player.IsDeath()) return;

                _movement.Stop();
                animator.SetBool("Walk", false);

                _spriteUtils.Flip(collision.transform.position.x - transform.position.x, GetComponentInChildren<Renderer>().transform.localScale);

                _attackCoroutine = StartCoroutine(Attack(player));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                _movement.Continue();
                animator.SetBool("Walk", true);

                if (_attackCoroutine != null)
                {
                    StopCoroutine(_attackCoroutine);
                }
            }

        }
        #endregion

        private void Start()
        {
            base.perception = 1f;
            base.strength = 2f;
            base.hp = 5;
            base.maxHp = 5;
            base.velocity = 1;
            base.dexterity = 1f;
            base.protection = 4f;
            base.diceAttack = DiceType.D6;

            _movement.OnMove.AddListener((dirX, dirY) =>
            {
                _spriteUtils.Flip(dirX, GetComponentInChildren<Renderer>().transform.localScale);
                animator.SetBool("Walk", true);
            });
        }

        private void OnValidate()
        {
            if (_spriteUtils == null)
                _spriteUtils = GetComponent<SpriteUtils>(); 
            
            if (_dice == null)
                _dice = GetComponent<DiceHelper>(); 
            
            if (_hitUI== null)
                _hitUI = GetComponent<HitUI>();
        }
    }
}