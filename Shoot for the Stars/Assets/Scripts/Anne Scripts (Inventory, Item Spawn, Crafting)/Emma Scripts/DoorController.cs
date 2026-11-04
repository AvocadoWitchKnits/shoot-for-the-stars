using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private string playerTag = "Player";

    private void DoorPivot(Collider other)
    {
        // Check if the object entering the trigger zone is the player
        if (other.CompareTag(playerTag))
        {
            doorAnimator.SetBool("isOpen", true);
        }
    }

    private void DoorClose(Collider other)
    {
        // Check if the object leaving the trigger zone is the player
        if (other.CompareTag(playerTag))
        {
            doorAnimator.SetBool("isOpen", false);
        }
    }
}
