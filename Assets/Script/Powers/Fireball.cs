using Enemy;
using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{    
    [SerializeField] private float speed = 1f;
    [SerializeField] private DiceType dice = DiceType.D12;

    private float _dir = 1;

    public void SetDirection(float dir)
    {
        this._dir = dir;
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
}
