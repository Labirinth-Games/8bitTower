using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHidden : MonoBehaviour
{
    public void Hidden(string id, bool leverStats)
    {
        gameObject.SetActive(leverStats);
    }
}
