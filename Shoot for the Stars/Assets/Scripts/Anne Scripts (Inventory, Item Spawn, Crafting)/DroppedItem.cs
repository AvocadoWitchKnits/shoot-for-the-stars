using System.Collections;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    bool autoStart;


    [Header("State")]
    public Item item;
    public bool pickedUp = false;

    [Header("Held Item Settings")]
    public Transform ShootPoint;

    void Start()
    {
        if (autoStart && item != null)
        {
            Initialize(item);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (pickedUp)
            return;

        if (item == null)
            return;

        bool added = InventoryManager.Instance.AddItem(item);

        if (added)
        {
            pickedUp = true;
            Destroy(gameObject);
        }
    }

    public void Initialize(Item item)
    {
        this.item = item;
        var droppedItem = Instantiate(item.prefab, ShootPoint);
        droppedItem.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }


}