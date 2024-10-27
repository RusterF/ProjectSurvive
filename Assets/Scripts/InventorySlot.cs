using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image bombIcon;  // UI Image component for the bomb icon
    public TextMeshProUGUI bombName;   // UI Text component for the bomb name
    public TextMeshProUGUI bombCount;  // UI Text component for the bomb count
     
    public void ClearSlot()
    {
        bombIcon.enabled = false;
        bombName.enabled = false;
        bombCount.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if(item == null)
        {
            ClearSlot();
            return;
        }
        bombIcon.enabled = true;
        bombName.enabled = true;
        bombCount.enabled = true;

        bombIcon.sprite = item.ItemData.icon;
        bombName.text = item.ItemData.itemName;
        bombCount.text = item.stackSize.ToString();
    }
}
