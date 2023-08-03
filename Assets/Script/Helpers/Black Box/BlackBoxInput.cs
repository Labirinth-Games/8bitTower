using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlackBoxInput : MonoBehaviour
{
    [SerializeField] protected bool state = false;

    [Header("Callbacks")]
    public UnityEvent<bool, BlackBoxInput> OnChangeInput;

    public bool GetState() 
    {
        return state;
    }
}
