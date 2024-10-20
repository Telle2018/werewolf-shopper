using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueToMainMenuButton : MonoBehaviour
{


    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnButtonPressed()
    {
        ChangeSceneByName("MainMenu");
    }
}