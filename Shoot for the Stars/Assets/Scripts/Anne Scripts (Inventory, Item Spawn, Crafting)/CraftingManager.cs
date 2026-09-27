using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance { get; private set; }
    private List<ItemTypeAndCount> items = new List<ItemTypeAndCount>();
    public bool CanCraftRecipe(ItemRecipeSO recipeSO)
    {
        items = InventoryManager.Instance.GetAllItems();

        int foundItems = 0;
        foreach (ItemTypeAndCount neededItemAndCount in recipeSO.input)
        {
            foreach (ItemTypeAndCount foundItemAndCount in items)
            {
                if (foundItemAndCount.item == neededItemAndCount.item && foundItemAndCount.count >= neededItemAndCount.count)
                {
                    foundItems++;
                    break;
                }

            }
        }
        return foundItems == recipeSO.input.Length;
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
