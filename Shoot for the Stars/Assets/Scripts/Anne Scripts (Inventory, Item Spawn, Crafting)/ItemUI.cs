using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    Image image;

    public void Initialize(Item item)
    {
        image.sprite = item.icon;
        transform.localScale = Vector3.one;
    }


}