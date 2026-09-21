using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public GameObject prefab;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public bool stackable = true;


    public enum ItemType
    {
        CraftingItem
    }
    
    public enum ActionType
    {
        Collecting,
        Crafting
    }
}
