using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnBackspace : MonoBehaviour
{
    void Update()
    {
        // Check if the Backspace key is pressed
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Get the current active scene
            Scene currentScene = SceneManager.GetActiveScene();
            // Reload the current scene by its name
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
