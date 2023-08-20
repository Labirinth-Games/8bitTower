using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class Stair : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Player player))
            {
                GameManager.Instance.NextLevel(player);
            }
        }
    }
}
