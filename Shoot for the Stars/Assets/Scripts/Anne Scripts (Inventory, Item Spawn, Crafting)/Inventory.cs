using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{


    [Header("References")]
    [SerializeField]
    InventoryUI ui;

    [Header("Prefabs")]

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Item> inventory = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedItem"))
        {
            var droppedItem = other.GetComponent<DroppedItem>();

            if (droppedItem.pickedUp)
            {
                return;
            }

            droppedItem.pickedUp = true;
            AddItem(droppedItem.item);

            Destroy(other.gameObject);
        }
    }


    void AddItem(Item item)
    {
        var InventoryId = Guid.NewGuid().ToString();
        inventory.Add(InventoryId, item);
        ui.AddUIItem(InventoryId, item);
    }
}