using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Accessories
{
    public class Bag : MonoBehaviour
    {
        [Header("Items bag")]
        [SerializeField] private List<ItemBase> items = new List<ItemBase>();

        public void Add(ItemBase item)
        {
            items.Add(item);
        }

        public List<ItemBase> GetItems()
        {
            return items;
        }
    }
}
