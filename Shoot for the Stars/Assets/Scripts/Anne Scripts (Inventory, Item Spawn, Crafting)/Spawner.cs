using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private Item itemToSpawn; 


    public void SpawnItem()
    {
        GameObject spawnedObject = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity);
            
        DroppedItem droppedItem = spawnedObject.GetComponent<DroppedItem>();

        if (droppedItem != null)
        {
            droppedItem.item = itemToSpawn;
        }
    }
}





// This method is called by the Input System when look input changes.
