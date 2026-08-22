using UnityEngine;
[System.Serializable]
public struct DialogueLine
{
    public string SpeakerName;
    [TextArea(2, 4)]
    public string text;
}
public class NPC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public DialogueLine[] dialogueLines;
}
