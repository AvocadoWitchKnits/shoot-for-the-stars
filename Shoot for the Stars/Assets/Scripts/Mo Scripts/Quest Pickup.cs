using UnityEngine;
using System.Collections;
public class QuestPickupItem : MonoBehaviour
{
    public string itemName;
    

    private bool isBeingCollected = false;
public void SuckIn(Transform target, float speed)
{
    if (isBeingCollected) return;
    isBeingCollected = true;

    Debug.Log("SuckIn started for: " + itemName);

    Collider col = GetComponent<Collider>();
    if (col != null) col.enabled = false;

    

    StartCoroutine(SuckInRoutine(target, speed));
}

private IEnumerator SuckInRoutine(Transform target, float speed)
{
    while (target != null && Vector3.Distance(transform.position, target.position) > 0.2f)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        yield return null;
    }

    Debug.Log("Item collected, calling QuestManager for: " + itemName);
    QuestManager.Instance.OnItemPickedUp(itemName);
    Destroy(gameObject);
}}