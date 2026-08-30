using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
 public class Sceneloader : MonoBehaviour
{
  public void LoadScene(string sceneName)
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
  }
}

}
