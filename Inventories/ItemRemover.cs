using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;

namespace ProjectRevolt.Inventories
{
    public class ItemRemover : MonoBehaviour
    {
        [SerializeField] private InventoryItem itemToRemove;
        [SerializeField] private int number;

        public void RemoveItem()
        {
            Inventory.GetPlayerInventory().RemoveItem(itemToRemove, number);
        }
    }
}
