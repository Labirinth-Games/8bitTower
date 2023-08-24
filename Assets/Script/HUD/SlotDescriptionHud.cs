using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotDescriptionHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI damageDescription;

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetDamage(string damage)
    {
        this.damageDescription.text = damage;
    }
}
