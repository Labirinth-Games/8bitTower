using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rigidbody2d;
    public Animator animator;

    [SerializeField] private float speed = 0.2f;

    private float _lastPositionFace = 1;

    public void Move() {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");

        if(xMove != 0 || yMove != 0)
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

        if (xMove != 0)
        {
            Debug.Log(xMove);
            Flip(xMove);
            transform.position += Vector3.right * xMove * speed * Time.deltaTime;
        } 
        
        if (yMove != 0)
        {
            transform.position += Vector3.up * yMove * speed * Time.deltaTime;
        }
    }

    private void Flip(float position)
    {
        if (position == _lastPositionFace)
            return;

        var scale = GetComponentInChildren<Renderer>().transform.localScale;
        scale.x *= Mathf.Sign(position);

        transform.localScale = scale;

        _lastPositionFace = position;
    }

    private void Update() {
        Move();
    }

    private void Start()
    {
        if(rigidbody2d == null)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
    }
}
