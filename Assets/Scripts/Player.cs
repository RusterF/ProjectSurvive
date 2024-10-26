using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float money = 100f;          // Player’s current money
    public Inventory inventory;         // Reference to the inventory
    private InventoryItem selectedItem;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("No Inventory component attached to the Player!");
        }
    }

    public bool CanAfford(float price)
    {
        return money >= price;
    }

    public void BuyItem(ItemData itemData)
    {

        if (CanAfford(itemData.price))
        {
            money -= itemData.price;
            inventory.Add(itemData);
            Debug.Log($"Bought item: {itemData.itemName}");
        }
        else
        {
            Debug.Log("Not enough money to buy item.");
        }
    }

    // Method to select the next grenade in the inventory
    public void SelectNextItem()
    {
        if (inventory.inventory.Count > 0)
        {
            // Cycle through items
            int currentIndex = inventory.inventory.IndexOf(selectedItem);
            currentIndex = (currentIndex + 1) % inventory.inventory.Count;
            selectedItem = inventory.inventory[currentIndex];

            Debug.Log("Selected item: " + selectedItem.ItemData.itemName);
        }
    }

    // Method to use the selected grenade
    public void UseSelectedItem()
    {
        if (selectedItem != null)
        {
            Debug.Log("Using grenade: " + selectedItem.ItemData.itemName);

            // Assuming you have a Grenade prefab ready to instantiate
            GameObject grenadePrefab = Instantiate(selectedItem.ItemData.explosionEffect, transform.position + transform.forward, Quaternion.identity);

            // Apply any further grenade setup here (e.g., add force to throw it)
            Rigidbody rb = grenadePrefab.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * 50f);  // Example throw force
            }

            inventory.Remove(selectedItem.ItemData);  // Remove from inventory after use
        }
        else
        {
            Debug.Log("No grenade selected.");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // Press 'E' to cycle through grenades
        {
            SelectNextItem();
        }

        if (Input.GetMouseButtonDown(0))  // Left-click to throw the grenade
        {
            UseSelectedItem();
        }
    }
}
