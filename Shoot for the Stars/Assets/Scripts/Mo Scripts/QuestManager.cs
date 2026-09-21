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

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // whenever the player picks up any item
    public void OnItemPickedUp(string itemName)
    {
        collectedItems.Add(itemName);
        CheckAllQuests();
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