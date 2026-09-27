using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    //test
    //[SerializeField] private Item testItem;

    public static InventoryManager Instance { get; private set; }
    public List<ItemTypeAndCount> items = new List<ItemTypeAndCount>();
    public int maxStackedItems = 9;
    public InventorySlot[] inventorySlots;

    public GameObject inventoryItemPrefab;
    int selectedSlot = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeSelectedSlot(0);

        //test
        //AddItem(testItem);
        //AddItem(testItem);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) ChangeSelectedSlot(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) ChangeSelectedSlot(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) ChangeSelectedSlot(2);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) ChangeSelectedSlot(3);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) ChangeSelectedSlot(4);
        if (Keyboard.current.digit6Key.wasPressedThisFrame) ChangeSelectedSlot(5);
        if (Keyboard.current.digit7Key.wasPressedThisFrame) ChangeSelectedSlot(6);
        if (Keyboard.current.digit8Key.wasPressedThisFrame) ChangeSelectedSlot(7);
        if (Keyboard.current.digit9Key.wasPressedThisFrame) ChangeSelectedSlot(8);
    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems)
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }


        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
                
            }
            return item;
        }
        return null;
    }
    public List<ItemTypeAndCount> GetAllItems()
    {
        items.Clear();
        
        foreach(InventorySlot slot in inventorySlots)
        {
            InventoryItem itemScript = slot.GetComponentInChildren<InventoryItem>();

            if (itemScript != null)
            {
                int i = 0;
                bool wasItemAdded = false;
                foreach (ItemTypeAndCount itemAndCount in items)
                {
                    if (itemAndCount.item == itemScript.item)
                    {
                        items[i].count += itemScript.count;
                        wasItemAdded = true;
                    }
                    i++;
                }
                if (!wasItemAdded)
                {
                    items.Add(new ItemTypeAndCount(itemScript.item, itemScript.count));
                }
            }          
        }
        return items;
    }
    

    public void CraftItems(List<ItemTypeAndCount> itemsToCraft, List<ItemTypeAndCount> itemsToDestroy)
    {
        foreach (ItemTypeAndCount itemToDestroy in itemsToDestroy) 
        {
            int remainingToRemove = itemToDestroy.count;

            foreach(InventorySlot slot in inventorySlots)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot == null)
                    continue;

                if (itemInSlot.item != itemToDestroy.item)
                    continue;
                int amountToRemove = Mathf.Min(remainingToRemove, itemInSlot.count);

                itemInSlot.count -= amountToRemove;
                remainingToRemove -= amountToRemove;

                if(itemInSlot.count <= 0)
                {
                    itemInSlot.gameObject.SetActive(false);
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

                if (remainingToRemove <= 0)
                    break;
            }
        }

        foreach (ItemTypeAndCount itemToCraft in itemsToCraft)
        {
            for (int i = 0; i < itemToCraft.count; i++)
            {
                AddItem(itemToCraft.item);
            }
        }
    }
}

