using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Manager;

namespace UI
{
    public class LearnUI : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                GameManager.Instance.learnManager.GetALearn(); // player received a learn

                Destroy(gameObject);
            }
        }
    }
}
