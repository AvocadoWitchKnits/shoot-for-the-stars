using UnityEngine;

public class NPCGreeting : MonoBehaviour
{
  


    public AudioSource helloAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helloAudio.Play();
        }
    }
}

