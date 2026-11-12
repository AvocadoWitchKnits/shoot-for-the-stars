using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MainScene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Anne", LoadSceneMode.Single);
    }
}

