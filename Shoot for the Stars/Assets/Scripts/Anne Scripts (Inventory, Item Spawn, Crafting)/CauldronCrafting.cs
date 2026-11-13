using UnityEngine;

public class CauldronCrafting : MonoBehaviour
{
    [SerializeField] private CraftingBench craftingBench;

    public void Interact()
    {
        if (craftingBench != null)
        {
            craftingBench.Craft();
        }
    }
}
