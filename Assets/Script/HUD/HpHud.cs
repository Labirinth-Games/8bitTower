using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpHud : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI display;

    private void FixedUpdate()
    {
        display.text = "HP: " + player.GetHp().ToString();
    }

}
