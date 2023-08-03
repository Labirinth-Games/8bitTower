using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Key : ItemBase
    {
        public string getIdName()
        {
            return idName;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                player.bag.Add(this);

                gameObject.SetActive(false);
            }
        }
    }
}
