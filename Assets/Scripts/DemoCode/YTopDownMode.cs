using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class YTopDownMode : MonoBehaviour
{
    // Public variables to assign the Cinemachine virtual cameras from the Unity Inspector
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    public CinemachineVirtualCamera camera3;

    // Integer to keep track of the current camera state
    private int cameraState = 0; // 0: All on, 1: Camera 1 off, 2: Cameras 1 & 2 off

    void Update()
    {
        // Check if the "Y" key is pressed
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Cycle through the camera states
            ToggleCameras();
        }
    }

    // Method to toggle the cameras based on the current state
    void ToggleCameras()
    {
        // Increment the camera state (cycle through 0, 1, 2)
        cameraState = (cameraState + 1) % 3;

        switch (cameraState)
        {
            case 0:
                // All cameras active (default state)
                camera1.gameObject.SetActive(true);
                camera2.gameObject.SetActive(true);
                camera3.gameObject.SetActive(true);
                break;
            case 1:
                // Camera 1 off, Camera 2 and 3 on
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(true);
                camera3.gameObject.SetActive(true);
                break;
            case 2:
                // Cameras 1 and 2 off, only Camera 3 on
                camera1.gameObject.SetActive(false);
                camera2.gameObject.SetActive(false);
                camera3.gameObject.SetActive(true);
                break;
        }
    }
}
