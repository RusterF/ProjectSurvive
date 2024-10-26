using UnityEngine;
using UnityEngine.Events;

public class Interactor : MonoBehaviour
{
    private Outline outline;          // Outline component (optional)
    public string message;            // Message to show on interaction
    public UnityEvent onInteraction;
    public ItemData itemData;         // Item data reference

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        outline = GetComponent<Outline>();
        DisableOutline();

    }

    public void Interact()
    {
        onInteraction.Invoke();

        if (player.CanAfford(itemData.price))
        {
            player.BuyItem(itemData);
        }
        else
        {
            Debug.Log("Not enough money to buy " + itemData.itemName);
        }
    }

    public void EnableOutline()
    {
        if (outline) outline.enabled = true;
    }

    public void DisableOutline()
    {
        if (outline) outline.enabled = false;
    }
}
