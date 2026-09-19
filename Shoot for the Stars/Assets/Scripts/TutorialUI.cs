using TMPro;   // add this line at the top
using UnityEngine;
using System.Collections;

public class TutorialHintUI : MonoBehaviour
{
    public static TutorialHintUI Instance;

    public GameObject hintPanel;
    public TMP_Text hintText;     
   
    public float displayDuration = 4f;

    private Coroutine hideRoutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        hintPanel.SetActive(false);
    }

    public void ShowTimed(string message)
    {
        hintText.text = message;
        hintPanel.SetActive(true);

        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        hintPanel.SetActive(false);
        hideRoutine = null;
    }
}