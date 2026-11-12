using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : MonoBehaviour
{
    [SerializeField] private List<ItemRecipeSO> recipes;
    [SerializeField] private BoxCollider placeItemsAreaBoxCollider;
    [SerializeField] private Transform craftedItemSpawnPoint;

    public void Craft()
    {
        Debug.Log("Trying to craft...");

        Vector3 centre =
            placeItemsAreaBoxCollider.transform.TransformPoint(placeItemsAreaBoxCollider.center
);

        Vector3 halfExtents = Vector3.Scale(
            placeItemsAreaBoxCollider.size * 0.5f,
            placeItemsAreaBoxCollider.transform.lossyScale
        );

        Collider[] colliderArray = Physics.OverlapBox(
            centre,
            halfExtents,
            placeItemsAreaBoxCollider.transform.rotation
        );

        List<Item> itemsOnBench = new List<Item>();
        List<DroppedItem> droppedItemsOnBench = new List<DroppedItem>();

        foreach (Collider collider in colliderArray)
        {
            DroppedItem droppedItem =
                collider.GetComponentInParent<DroppedItem>();

            if (droppedItem != null &&
                droppedItem.item != null &&
                !droppedItemsOnBench.Contains(droppedItem))
            {
                droppedItemsOnBench.Add(droppedItem);
                itemsOnBench.Add(droppedItem.item);
            }
        }

        ItemRecipeSO matchingRecipe = null;

        foreach (ItemRecipeSO recipe in recipes)
        {
            if (CraftingManager.Instance.CanCraftRecipe(
                recipe,
                itemsOnBench))
            {
                matchingRecipe = recipe;
                break;
            }
        }
        if (matchingRecipe != null)
        {
            Debug.Log("Crafting: " + matchingRecipe.recipeName);

            ConsumeIngredients(
                matchingRecipe,
                droppedItemsOnBench
            );

            ItemTypeAndCount craftedResult =
                matchingRecipe.output[0];

            GameObject craftedObject = Instantiate(
                craftedResult.item.prefab,
                craftedItemSpawnPoint.position,
                craftedItemSpawnPoint.rotation
            );

            DroppedItem craftedDroppedItem =
                craftedObject.GetComponent<DroppedItem>();

            if (craftedDroppedItem == null)
            {
                craftedDroppedItem =
                    craftedObject.AddComponent<DroppedItem>();
            }

            craftedDroppedItem.item = craftedResult.item;
        }
        else
        {
            Debug.Log("No matching recipe!");
        }
    }

    private void ConsumeIngredients(
        ItemRecipeSO recipe,
        List<DroppedItem> droppedItemsOnBench)
        {
        foreach (ItemTypeAndCount neededItem in recipe.input)
        {
            int remaining = neededItem.count;

            for (int i = droppedItemsOnBench.Count - 1;
                 i >= 0 && remaining > 0;
                 i--)
            {
                if (droppedItemsOnBench[i].item == neededItem.item)
                {
                    Destroy(droppedItemsOnBench[i].gameObject);
                    droppedItemsOnBench.RemoveAt(i);

                    remaining--;
                }
            }
        }
    }
}