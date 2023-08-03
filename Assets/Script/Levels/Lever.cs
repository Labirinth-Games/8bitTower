using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : BlackBoxInput
{
    [Header("References")]
    [SerializeField] private Sprite turnOn;
    [SerializeField] private Sprite turnOff;

    [Header("Settings")]
    public string id = "Lever 001";

    private SpriteRenderer _renderer;

    
    private void LeverRender()
    {
        Sprite lever = base.state ? turnOn : turnOff;

        _renderer.sprite = lever;
    }

    #region Collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))  
        {
            base.state = !base.state;
            LeverRender();

            OnChangeInput?.Invoke(base.state, gameObject.GetComponent<BlackBoxInput>());
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
