using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : MonoBehaviour
{
    [SerializeField] private List<ItemTypeAndCount> ItemTypeAndCount;
    [SerializeField] private BoxCollider placeItemsAreaBoxCollider; 

    
    public void Craft()
    {
        Debug.Log("Craft");
        //Physics.OverlapBox()
    }

}
