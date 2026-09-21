using UnityEngine;

public class DemoScript : MonoBehaviour

{
    public InventoryManager inventoryManager;
    public Item[] ItemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(ItemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("Item added");
        } else
        {
            Debug.Log("ITEM NOT ADDED");
        }
    }

    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received");
        }
        else
        {
            Debug.Log("Not Received");
        }
    }

    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item used");
        }
    }
}
