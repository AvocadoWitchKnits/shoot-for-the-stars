using System.Collections;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private string hintId;
    [SerializeField, TextArea] private string message;
    [SerializeField] private bool showOnce = true;

    private Coroutine hideRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var ui = TutorialHintUI.Instance;
        if (showOnce && ui.HasSeen(hintId)) return;

        ui.Show(hintId, message);
        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterDelay(ui));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
            hideRoutine = null;
        }
        TutorialHintUI.Instance.Hide(hintId);
    }

    private IEnumerator HideAfterDelay(TutorialHintUI ui)
    {
        yield return new WaitForSeconds(ui.displayDuration);
        ui.Hide(hintId);
        hideRoutine = null;
    }

    private void OnDestroy()
    {
        if (TutorialHintUI.Instance != null)
            TutorialHintUI.Instance.Hide(hintId);
    }
}