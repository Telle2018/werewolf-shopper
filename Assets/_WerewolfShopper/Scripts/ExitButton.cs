using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // This method will be linked to the button
    public void OnButtonPress()
    {
        // If running in the Unity Editor, stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();  // Quit the game in a build
        #endif
    }
}
