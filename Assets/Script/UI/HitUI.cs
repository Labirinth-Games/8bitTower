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

        public void Display(string value, float timeToDestroy = 1f)
        {
            if (HitUIPrefab != null)
            {
                var instance = Instantiate(HitUIPrefab);
                instance.transform.position = new Vector2(transform.position.x, transform.position.y + .5f);

                instance.transform.DOShakePosition(1f, .2f);
                instance.GetComponentInChildren<TextMeshPro>().text = value;

                Destroy(instance, timeToDestroy);
            }
        }
    }
}
