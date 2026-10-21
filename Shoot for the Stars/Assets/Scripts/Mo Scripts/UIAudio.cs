using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public static UIAudio Instance;
    public AudioSource source;
    public AudioClip clickClip;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClick()
    {
        if (clickClip != null)
            source.PlayOneShot(clickClip);
    }
}

