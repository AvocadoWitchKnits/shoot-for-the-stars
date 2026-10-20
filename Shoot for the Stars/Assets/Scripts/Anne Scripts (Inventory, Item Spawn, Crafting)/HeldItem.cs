using UnityEngine;

public class HeldItem : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;

    private GameObject _itemInHand;

    public void EquipItem(Item item)
    {
        Debug.Log("EquipItem received: " + item.name);

        if (item == null)
            return;

        if (item.heldPrefab == null)
        {
            Debug.LogWarning(item.name + " has no prefab assigned!");
            return;
        }

        if (_itemInHand != null)
        {
            Destroy(_itemInHand);
        }

        _itemInHand = Instantiate(item.heldPrefab, holdPoint);

        _itemInHand.transform.localPosition = Vector3.zero;
        _itemInHand.transform.localRotation = Quaternion.identity;

        Debug.Log("Spawned held prefab: " + _itemInHand.name);
    }

    public void UnequipItem()
    {
        if (_itemInHand == null)
            return;

        Destroy(_itemInHand);
        _itemInHand = null;
    }
}

