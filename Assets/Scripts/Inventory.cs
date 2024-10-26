using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }

    public void UseItem(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item) && item.stackSize > 0)
        {
            // Use the item (e.g., instantiate its explosion effect)
            Instantiate(itemData.explosionEffect, transform.position + transform.forward * 2, Quaternion.identity);

            // Remove from stack after use
            Remove(itemData);
        }
        else
        {
            Debug.Log("Item not available in inventory or out of stock.");
        }
    }
}

