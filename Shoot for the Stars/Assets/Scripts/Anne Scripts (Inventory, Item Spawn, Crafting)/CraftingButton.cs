using UnityEngine;
using System.Collections.Generic;

public class CraftingButton : MonoBehaviour
{
    [SerializeField]
    ItemRecipeSO recipe;

    public void Craft()
    {
        if (recipe == null)
        {
            Debug.LogWarning("No recipe assigned!");
            return;
        }

        if (!CraftingManager.Instance.CanCraftRecipe(recipe))
        {
            Debug.Log("Not enough ingredients to craft "
                + recipe.recipeName);

            return;
        }

        InventoryManager.Instance.CraftItems(
            new List<ItemTypeAndCount>(recipe.output),
            new List<ItemTypeAndCount>(recipe.input)
        );

        Debug.Log("Crafted " + recipe.recipeName);
    }
}
