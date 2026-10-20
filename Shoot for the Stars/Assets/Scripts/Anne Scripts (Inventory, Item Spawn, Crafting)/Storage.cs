using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private Item storedItem;
    [SerializeField]
    [Min(0)]

    private int storedCount = 1;
    public string name;
    public int size;

    public Item StoredItem
    {
        get { return storedItem; }
    }

    public int StoredCount
    {
        get { return storedCount; }
    }

    public void StoreItem(Item item, int count)
    {
        storedItem = item;
        storedCount = count;
    }

    public void ClearItem()
    {
        storedItem = null;
        storedCount = 0;
    }

    public Item TakeItem()
    {
        Debug.Log(
    "TakeItem called on: " + gameObject.name +
    " | Stored Item: " + (storedItem != null ? storedItem.name : "NULL")
);

        if (storedItem == null || storedCount <= 0)
            return null;

        Item itemToTake = storedItem;

        storedCount--;

        if(storedCount <= 0)
        {
            storedItem = null;
            storedCount = 0;
        }
    

        return itemToTake;
    }
}

