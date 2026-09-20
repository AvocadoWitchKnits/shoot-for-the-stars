using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject

{
    public Item item;
    public Image image;
    private void Start()
    {
        InitialiseItem(item);
    }
    public void InitialiseItem(Item newItem)
    {
        image.sprite = newItem.image;
    }
    public string id;
    public string description;
    public Sprite icon;
    public GameObject prefab;
    public bool stackable = true;


}
