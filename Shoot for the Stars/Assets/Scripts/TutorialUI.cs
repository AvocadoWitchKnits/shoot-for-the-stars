using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialHintUI : MonoBehaviour
{
    public static TutorialHintUI Instance;

    [Header("UI References")]
    public GameObject hintPanel;
    public TMP_Text hintText;

    [Header("Settings")]
    public float displayDuration = 4f;

    [System.Serializable]
    public class HintEntry
    {
        public string message;
        public float delayBeforeShowing = 3f; // gap after the previous hint hides
    }

    [Header("Tutorial Sequence")]
    public List<HintEntry> hintSequence = new List<HintEntry>();

    private Coroutine hideRoutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        hintPanel.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(PlayHintSequence());
    }

    private IEnumerator PlayHintSequence()
    {
        foreach (HintEntry hint in hintSequence)
        {
            yield return new WaitForSeconds(hint.delayBeforeShowing);
            yield return ShowTimedAndWait(hint.message);
        }
    }

    private IEnumerator ShowTimedAndWait(string message)
    {
        hintText.text = message;
        hintPanel.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

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