using UnityEngine;

public class QuestPickupItem : MonoBehaviour
{
    public string itemName; // "Cherries", "Water", "Lavender Flower"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.OnItemPickedUp(itemName);
            Destroy(gameObject);
        }
    }
}