using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Helpers
{
    public class Unlock : MonoBehaviour
    {
        public UnityEvent<Player> OnStayPlace;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.CompareTag("Player"))
                OnStayPlace?.Invoke(collision.transform.GetComponent<Player>());
        }

    }
}
