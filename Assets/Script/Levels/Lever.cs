using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite turnOn;
    [SerializeField] private Sprite turnOff;

    [Header("Settings")]
    public string id = "Lever 001";
    [SerializeField] private bool _stateLever = false;

    private SpriteRenderer _renderer;
    
    public bool GetState()
    {
        return _stateLever;
    }

    private void LeverRender()
    {
        Sprite lever = _stateLever ? turnOn : turnOff;

        _renderer.sprite = lever;
    }

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))  
        {
            _stateLever = !_stateLever;
            LeverRender();
        }
    }
    #endregion


    #region Unity Events
    private void Start()
    {
        if(turnOff == null || turnOn == null) {
            Debug.LogWarning("Your need add the gameobjects to lever", this);
            return;
        }

        LeverRender();
    }

    private void OnValidate()
    {
        if (_renderer == null)
            _renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    #endregion
}
