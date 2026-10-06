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

    private readonly HashSet<string> seen = new HashSet<string>();
    private readonly List<(string id, string message)> active = new List<(string, string)>();
    private Coroutine hideRoutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        hintPanel.SetActive(false);
    }

    public bool HasSeen(string id) => seen.Contains(id);


    public void Show(string id, string message)
    {
        active.RemoveAll(a => a.id == id);
        active.Add((id, message));
        Display(message);
    }


    public void Hide(string id)
    {
        if (!active.Exists(a => a.id == id)) return;

        active.RemoveAll(a => a.id == id);
        seen.Add(id);


        if (active.Count > 0) Display(active[active.Count - 1].message);
        else hintPanel.SetActive(false);
    }


    public void ShowTimed(string message)
    {
        Display(message);

        if (hideRoutine != null) StopCoroutine(hideRoutine);
        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private void Display(string message)
    {
        if (hideRoutine != null) { StopCoroutine(hideRoutine); hideRoutine = null; }
        hintText.text = message;
        hintPanel.SetActive(true);
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        hintPanel.SetActive(false);
        hideRoutine = null;
    }
}