using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite turnOn;
    [SerializeField] private Sprite turnOff;

    [Header("Settings")]
    public string id = "Lever 001";

    [Header("Callbacks")]
    public UnityEvent<string, bool> OnTriggerLever;

    [SerializeField] private bool _stateLever = false;
    private SpriteRenderer _renderer;


    private void LeverUpdate()
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
            LeverUpdate();

            OnTriggerLever?.Invoke(id, _stateLever);
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

        LeverUpdate();
    }

    private void OnValidate()
    {
        if (_renderer == null)
            _renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    #endregion
}
