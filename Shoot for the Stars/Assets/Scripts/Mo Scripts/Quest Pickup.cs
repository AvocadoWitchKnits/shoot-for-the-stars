using UnityEngine;
using System.Collections;
public class QuestPickupItem : MonoBehaviour
{
    public string itemName; // "Cherries", "Water", "Lavender Flower"

    private bool isBeingCollected = false;
    public void SuckIn(Transform target, float speed)
    {
        if (isBeingCollected) return;
        isBeingCollected = true;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        
        StartCoroutine(SuckInRoutine(target, speed));
    }

    private IEnumerator SuckInRoutine(Transform target, float speed)
    {
        while (target != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target.position) <= 0.01f)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
