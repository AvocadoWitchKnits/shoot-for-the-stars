using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [System.Serializable]
    public class Quest
    {
        public string questName;
        public List<string> requiredItems = new List<string>();
        public Toggle questToggle;
        public bool isComplete = false;
    }

    public List<Quest> quests = new List<Quest>();

    private HashSet<string> collectedItems = new HashSet<string>();
    private HashSet<string> requiredItems = new HashSet<string>();
    private List<GameObject> missShepCharacters = new List<GameObject>();
    private bool missShepRevealed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Quest quest in quests)
        {
            foreach (string item in quest.requiredItems)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    requiredItems.Add(item);
            }
        }

        foreach (Transform sceneObject in FindObjectsOfType<Transform>())
        {
            if (!sceneObject.name.StartsWith("Miss Shep", System.StringComparison.Ordinal))
                continue;

            missShepCharacters.Add(sceneObject.gameObject);
            sceneObject.gameObject.SetActive(false);
        }

        if (missShepCharacters.Count == 0)
            Debug.LogWarning("No scene objects named 'Miss Shep' were found to reveal.", this);
    }

    // whenever the player picks up any item
    public void OnItemPickedUp(string itemName)
    {
        collectedItems.Add(itemName);
        CheckAllQuests();

        if (!missShepRevealed && requiredItems.Count > 0 && requiredItems.IsSubsetOf(collectedItems))
        {
            missShepRevealed = true;
            foreach (GameObject character in missShepCharacters)
                character.SetActive(true);
        }
    }

    private void CheckAllQuests()
    {
        foreach (Quest quest in quests)
        {
            if (quest.isComplete) continue;

            if (HasAllRequiredItems(quest))
            {
                CompleteQuest(quest);
            }
        }
    }

    private bool HasAllRequiredItems(Quest quest)
    {
        foreach (string required in quest.requiredItems)
        {
            if (!collectedItems.Contains(required))
                return false;
        }
        return true;
    }

    private void CompleteQuest(Quest quest)
    {
        quest.isComplete = true;

        if (quest.questToggle != null)
            quest.questToggle.isOn = true;

        Debug.Log("Quest completed: " + quest.questName);
    }
}