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
    Transform uiInventoryParent;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, GameObject> inventoryUI = new();

    public void AddUIItem(string inventoryId, Item item)
    {
        var itemUI = Instantiate(uiItemPrefab, uiInventoryParent).GetComponent<ItemUI>();

        inventoryUI.Add(inventoryId, itemUI.gameObject);

        itemUI.Initialize(item);
    }
}
