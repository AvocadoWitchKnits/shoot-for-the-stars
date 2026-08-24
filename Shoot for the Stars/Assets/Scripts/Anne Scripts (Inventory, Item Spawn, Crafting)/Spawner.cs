using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cubePrefab;

    private void Start()
    {
        Instantiate(cubePrefab, transform.position, Quaternion.identity);
    }
}




// This method is called by the Input System when look input changes.
