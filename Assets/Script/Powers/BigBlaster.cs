using DG.Tweening;
using Enemy;
using Helpers;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBlaster : MonoBehaviour
{
    [Header("Stats")]
    public DiceType dice = DiceType.D20;
    public float timeTolive = 1f;
   
    private bool _increaseStop = false;
    private float _dir = 1;

    private void DestroyBlaster() 
    {
        transform.DOScaleY(0, .2f);
        Destroy(gameObject, .3f);
    }

    public void SetDirection(float dir)
    {
        this._dir = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.transform.CompareTag("Player"))
        {
            _increaseStop = true;
            DestroyBlaster();

           if(collision.transform.CompareTag("Enemy"))
            {
                var enemy = collision.transform.GetComponent<EnemyBase>();
                enemy.Hit(DiceHelper.Roll(dice) + (float)GameManager.Instance.playerStats?.intelligence);
            }
        }
    }

    void Update()
    {
        if (_increaseStop) return;

        Vector2 move = new Vector2(transform.position.x + .01f * _dir, transform.position.y);
        transform.position = move;
        GetComponent<SpriteRenderer>().size += new Vector2(.02f * _dir, 0);

        var boxScale = GetComponent<SpriteRenderer>().size;
        GetComponent<BoxCollider2D>().size = new Vector2(Mathf.Abs(boxScale.x), boxScale.y);
    }
}
