using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryUI : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    Transform uiInventoryParent;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, GameObject> inventoryUI = new();

    public void AddUIItem(string inventoryId, Item item)
    {
        Debug.Log("AddUIItem started");

        var itemUI = Instantiate(uiItemPrefab, uiInventoryParent).GetComponent<ItemUI>();

        Debug.Log("UI prefab instantiated");
        Debug.Log("itemUI is null: " + (itemUI == null));
        Debug.Log("item is null before Initialize: " + (item == null));
        Debug.Log("inventory is null: " + (inventory == null));
        Debug.Log("uiInventoryParent is null: " + (uiInventoryParent == null));

        inventoryUI.Add(inventoryId, itemUI.gameObject);

        Debug.Log("About to call Initialize");

        itemUI.Initialize(inventoryId, item, inventory.DropItem);

        Debug.Log("Initialize completed");
    }

    // public void AddUIItem(string inventoryId, Item item)
    // {
    //  var itemUI = Instantiate(uiItemPrefab, uiInventoryParent).GetComponent<ItemUI>();

    //        inventoryUI.Add(inventoryId, itemUI.gameObject);
    //        itemUI.Initialize(inventoryId, item, inventory.DropItem);
    //   }

    public void RemoveUIItem(string inventoryId)
    {
        var itemUI = inventoryUI.GetValueOrDefault(inventoryId);
        inventoryUI.Remove(inventoryId);
        Destroy(itemUI);
    }
}
