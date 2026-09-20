using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public GameObject audioImage;
private void Start()
    {
        audioImage.SetActive(false);

    }
    public void ShowAudioNotice()
    {
    
{
    Debug.Log("ShowAudioNotice called, image was: " + audioImage.activeSelf);
    audioImage.SetActive(true);
}
    }
}
