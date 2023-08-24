using Enemy;
using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Learn
{
    public class Fireball : LearnBase
    {
        [Header("Stats")]
        [SerializeField] private float speed = 1f;
        [SerializeField] private DiceType dice = DiceType.D12;

        private float _dir = 1;

        public override void SetDirection(float dir)
        {
            _dir = dir;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag("Player"))
            {
                if (collision.transform.CompareTag("Enemy"))
                {
                    var enemy = collision.transform.GetComponent<EnemyBase>();
                    enemy.Hit(DiceHelper.Roll(dice) + (float)Manager.GameManager.Instance.playerStats?.intelligence);
                }

                Destroy(gameObject, .05f);
            }
        }

        void Update()
        {
            transform.position += (Vector3.right * _dir) * speed * Time.deltaTime;
        }

        private void Start()
        {
            level = LearnLevelEnum.Rare;
            displayName = "Fireball";
            description = "Catch the fire ball on target ";
            damageDescription = "Damage: d12 + intelligence";
        }
    }
}
