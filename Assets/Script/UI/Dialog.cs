using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshPro textMesh;

    public void SetMessage(string message)
    {
        if(textMesh != null)
        {
            textMesh.text = message;
        }
    }
}
