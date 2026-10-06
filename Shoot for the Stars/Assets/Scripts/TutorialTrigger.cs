using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private string hintId;
    [SerializeField, TextArea] private string message;
    [SerializeField] private bool showOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var ui = TutorialHintUI.Instance;
        if (showOnce && ui.HasSeen(hintId)) return;

        ui.Show(hintId, message);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        TutorialHintUI.Instance.Hide(hintId);
    }
}