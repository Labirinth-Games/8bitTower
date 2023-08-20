using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HitUI : MonoBehaviour
    {
        [SerializeField] private GameObject HitUIPrefab;

        public void Display(string value, string hexColor = "#dd3a28", float timeToDestroy = 1f)
        {
            if (HitUIPrefab != null)
            {
                var instance = Instantiate(HitUIPrefab);
                instance.transform.position = new Vector2(transform.position.x, transform.position.y + .7f);

                instance.transform.DOShakePosition(1f, .3f);
                instance.GetComponentInChildren<TextMeshPro>().text = value;
                instance.GetComponentInChildren<TextMeshPro>().color = Utils.ColorUtils.HexToColor(hexColor);

                Destroy(instance, timeToDestroy);
            }
        }
    }
}
