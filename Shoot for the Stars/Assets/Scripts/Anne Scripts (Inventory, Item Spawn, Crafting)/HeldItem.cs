using Unity.VisualScripting;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    private Rigidbody _itemRigidbody;
    private GameObject _itemInHand;
     

    public void EquipItem(Item item)
    { 

        if (item == null)
            return;
        Debug.Log("EquipItem received: " + item.name);

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

        _itemRigidbody = _itemInHand.GetComponent<Rigidbody>();

        if (_itemRigidbody != null)
        {
            _itemRigidbody.useGravity = false;
            _itemRigidbody.isKinematic = true;
        }
        _itemRigidbody.useGravity = false;
        Debug.Log("Spawned held prefab: " + _itemInHand.name);
    }

    public void UnequipItem()
    {
        if (_itemInHand == null)
            return;

        Destroy(_itemInHand);
        _itemInHand = null;
    }
    public bool IsHoldingItem()
    {
        return _itemInHand != null;
    }

    public void Drop()
    {
        if (_itemInHand == null)
            return;

        _itemInHand.transform.SetParent(null);

        if( _itemRigidbody != null )
        {
            _itemRigidbody.useGravity = true;
            _itemRigidbody.isKinematic = false;
        }
        _itemInHand = null;
        _itemRigidbody = null;
    }
    private void FixedUpdate()
    {
        if (_itemInHand != null && _itemRigidbody != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(holdPoint.position, _itemRigidbody.transform.position, Time.deltaTime * lerpSpeed);
            _itemRigidbody.MovePosition(newPosition);
        }

    }
}

