using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("DroppedItem"))
            return;

        DroppedItem droppedItem = other.GetComponent<DroppedItem>();

        if (droppedItem == null)
            return;

        if (droppedItem.pickedUp)
            return;

        if (droppedItem.item == null)
        {
            Debug.LogWarning("DroppedItem has no Item assigned.");
            return;
        }

        bool added = InventoryManager.Instance.AddItem(droppedItem.item);

        if (added)
        {
            droppedItem.pickedUp = true;
            Destroy(other.gameObject);
        }
    }
}