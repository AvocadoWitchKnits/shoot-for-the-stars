using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecipe : MonoBehaviour
{
    public ItemRecipeSO recipeSO;

    [SerializeField] GameObject itemPrefab;

    public void UpdateRecipeUI(ItemRecipeSO newRecipeSO)
    {
        recipeSO = newRecipeSO;

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < recipeSO.input.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, transform);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = recipeSO.input[i].item.image;
            newItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = recipeSO.input[i].count.ToString();

        }
        for (int i = 0; i < recipeSO.output.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, transform);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = recipeSO.output[i].item.image;
            newItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = recipeSO.output[i].count.ToString();

        }
    }
}
