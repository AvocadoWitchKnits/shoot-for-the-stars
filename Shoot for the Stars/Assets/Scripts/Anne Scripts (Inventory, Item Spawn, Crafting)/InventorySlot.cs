using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [Header("Storage")]
    [SerializeField] private bool isStorageSlot;

    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

            if (inventoryItem == null) 
            return;

            InventorySlot sourceSlot = inventoryItem.parentAfterDrag.GetComponent<InventorySlot>();

            inventoryItem.parentAfterDrag = transform;

            if (isStorageSlot)
            {
                InventoryManager.Instance.StoreItemInOpenStorage(
                    inventoryItem.item,
                    inventoryItem.count
                );
            }

            else if (sourceSlot != null && sourceSlot.isStorageSlot)
            {
                InventoryManager.Instance.ClearOpenStorage();
            }
        }
    }
}
