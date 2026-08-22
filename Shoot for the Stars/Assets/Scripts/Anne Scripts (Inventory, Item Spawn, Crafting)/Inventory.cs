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
    [SerializeField]
    GameObject droppedItemPrefab;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Item> inventory = new();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        Debug.Log("Tag is: " + other.tag);

        if (other.CompareTag("DroppedItem"))
        {
            Debug.Log("DroppedItem tag recognised!");

            var droppedItem = other.GetComponent<DroppedItem>();

            Debug.Log("DroppedItem component: " + droppedItem);

            if (droppedItem.pickedUp)
            {
                Debug.Log("Item was already marked as picked up.");
                return;
            }

            Debug.Log("About to add item.");

            droppedItem.pickedUp = true;
            AddItem(droppedItem.item);

            Debug.Log("Item added successfully.");

            Destroy(other.gameObject);
        }
    }
   // public void OnTriggerEnter(Collider other)
   // {
    //    if (other.CompareTag("DroppedItem"))
   //     {
   //         var droppedItem = other.GetComponent<DroppedItem>();
    //        if (droppedItem.pickedUp)
  //          {
   //             return;
   //         }
   //         droppedItem.pickedUp = true;
  //          AddItem(droppedItem.item);
  //          Destroy(other.gameObject);
  //      }
 //   }

    void AddItem(Item item)
    {
        var InventoryId = Guid.NewGuid().ToString();
        inventory.Add(InventoryId, item);
        ui.AddUIItem(InventoryId, item);
    }

    public void DropItem(string inventoryId)
    {
        var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
        var item = inventory.GetValueOrDefault(inventoryId);
        droppedItem.Initialize(item);
        inventory.Remove(inventoryId);
        ui.RemoveUIItem(inventoryId);
    }
}