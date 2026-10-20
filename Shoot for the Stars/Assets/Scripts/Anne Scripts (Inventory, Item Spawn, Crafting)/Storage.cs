using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private Item storedItem;
    public string name;
    public int size;

    public Item TakeItem()
    {
        Debug.Log(
    "TakeItem called on: " + gameObject.name +
    " | Stored Item: " + (storedItem != null ? storedItem.name : "NULL")
);

        if (storedItem == null)
            return null;

        Item itemToTake = storedItem;
        storedItem = null;

        return itemToTake;
    }
}
