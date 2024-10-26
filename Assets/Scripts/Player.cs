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

        // Initialize selectedItem as the first item if available
        if (inventory.inventory.Count > 0)
        {
            selectedItem = inventory.inventory[0];
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

    public void SelectNextItem()
    {
        if (inventory.inventory.Count > 0)
        {
            int currentIndex = inventory.inventory.IndexOf(selectedItem);
            currentIndex = (currentIndex + 1) % inventory.inventory.Count;
            selectedItem = inventory.inventory[currentIndex];

            Debug.Log("Selected item: " + selectedItem.ItemData.itemName);
        }
        if (selectedItem != null && selectedItem.ItemData.itemName.Contains("Bom"))
        {
            // Assign the correct preview model for the selected grenade type
            GetComponent<GrenadePlacement>().SetPreviewModel(selectedItem.ItemData.previewPrefab);
            GetComponent<GrenadePlacement>().ShowPreview();
        }
        else
        {
            GetComponent<GrenadePlacement>().HidePreview();
        }
    }

    public void UseSelectedItem()
    {
        if (selectedItem != null && selectedItem.stackSize > 0)
        {
            Debug.Log("Using grenade: " + selectedItem.ItemData.itemName);

            // Get the placement position from the GrenadePlacement script
            Vector3 placementPosition = GetComponent<GrenadePlacement>().GetPlacementPosition();

            // Instantiate the grenade prefab at the placement position
            GameObject grenadeObject = Instantiate(selectedItem.ItemData.grenadePrefab, placementPosition, Quaternion.identity);

            // Initialize grenade properties based on ItemData
            Grenade grenadeScript = grenadeObject.GetComponent<Grenade>();
            if (grenadeScript != null)
            {
                grenadeScript.InitializeGrenade(selectedItem.ItemData.radius, selectedItem.ItemData.force, selectedItem.ItemData.explosionEffect);
            }

            // Remove one from the selected item's stack
            inventory.Remove(selectedItem.ItemData);

            // Check if the selected item is now empty
            if (selectedItem.stackSize == 0)
            {
                Debug.Log("No grenades left in inventory.");
                selectedItem = null; // Clear the selected item

                // Hide the grenade preview immediately
                GetComponent<GrenadePlacement>().HidePreview();
            }
        }
        else
        {
            Debug.Log("No grenade selected or out of stock.");

            // Hide the grenade preview if no item is selected
            GetComponent<GrenadePlacement>().HidePreview();
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
