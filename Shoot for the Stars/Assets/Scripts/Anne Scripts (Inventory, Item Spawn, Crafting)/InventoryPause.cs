using UnityEngine;

public class InventoryPause : MonoBehaviour
{
    public GameObject MainInventoryGroup;
    void Update()
    {
        
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }
}
