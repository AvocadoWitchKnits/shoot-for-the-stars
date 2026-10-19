using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public float minSpeed = 0.2f;

    Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
        audioSource.loop = true;
    }

    void Update()
    {
        Vector3 delta = transform.position - lastPos;
        delta.y = 0f; // ignore falling/jumping
        float speed = delta.magnitude / Mathf.Max(Time.deltaTime, 0.0001f);
        lastPos = transform.position;

        bool isMoving = speed > minSpeed;

        if (isMoving && !audioSource.isPlaying)
            audioSource.Play();
        else if (!isMoving && audioSource.isPlaying)
            audioSource.Stop();
    }
}