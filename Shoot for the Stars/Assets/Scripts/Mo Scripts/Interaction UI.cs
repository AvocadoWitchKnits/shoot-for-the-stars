using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionUI : MonoBehaviour
{
    public GameObject interactionPromptUI;
    private Text promptText;
    private float displayDuration = 3f;
    private void ShowInteractionPrompt(string message)
    {
        if (interactionPromptUI == null)
            return;

        InteractionPromptUI prompt = interactionPromptUI.GetComponent<InteractionPromptUI>();
        if (prompt != null)
            prompt.ShowPrompt(message);
    }

    public class InteractionPromptUI : MonoBehaviour
    {
        public Text promptText;

        public void ShowPrompt(string text)
        {
            if (promptText == null)
                return;

            promptText.text = text;
            promptText.gameObject.SetActive(true);
            StartCoroutine(HidePromptAfterDelay());
        }

        private IEnumerator HidePromptAfterDelay()
        {
            yield return new WaitForSeconds(3f);

            if (promptText != null)
                promptText.gameObject.SetActive(false);
        }
    }
}
