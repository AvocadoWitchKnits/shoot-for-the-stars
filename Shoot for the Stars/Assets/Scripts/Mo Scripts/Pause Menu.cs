using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
     public GameObject KeyGuidePanel;
    private bool isPaused = false;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Hooked to the Pause action in Player Input -> Events
    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause pressed");
        if (!context.performed) return;

        if (isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseMenuScreen()
    {
        pauseMenuUI.SetActive(true);
        
    }
     public void ToggleKeyGuide()
    {

        bool isNowActive = !KeyGuidePanel.activeSelf;
        KeyGuidePanel.SetActive(isNowActive);
        Time.timeScale = isNowActive ? 0f : 1f;
    }
    
}