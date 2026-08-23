using UnityEngine;
using UnityEngine.InputSystem;
public class Keyguide : MonoBehaviour
{
    [Header("Key Guide")]

    public GameObject KeyGuidePanel;

    public void ToggleKeyGuide()
    {

        bool isNowActive = !KeyGuidePanel.activeSelf;
        KeyGuidePanel.SetActive(isNowActive);
        Time.timeScale = isNowActive ? 0f : 1f;
    }
    public void onKeyGuide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Implement your key guide logic here
            ToggleKeyGuide();
        }
    }
}

