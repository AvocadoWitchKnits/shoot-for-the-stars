using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance { get; private set; }
    private List<ItemTypeAndCount> items = new List<ItemTypeAndCount>();
    public bool CanCraftRecipe(ItemRecipeSO recipeSO, List<Item> availableItems)
    {
        foreach (ItemTypeAndCount neededItem in recipeSO.input)
        {
            int foundCount = 0;

            foreach (Item foundItem in availableItems)
            {
                if (foundItem == neededItem.item)
                {
                    foundCount++;
                }
            }

            if (foundCount < neededItem.count)
            {
                return false;
            }
        }
        return true;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
