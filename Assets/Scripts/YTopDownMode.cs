using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Cinemachine;

public class YTopDownMode : MonoBehaviour
{
    // Public variable to assign the Cinemachine virtual camera from the Unity Inspector
    public CinemachineVirtualCamera virtualCamera;

    // Boolean to keep track of the camera's state
    private bool isCameraActive = true;

    void Update()
    {
        // Check if the "Y" key is pressed
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Toggle the camera's active state
            ToggleCamera(!isCameraActive);
        }
    }

    // Method to enable or disable the virtual camera
    void ToggleCamera(bool isActive)
    {
        if (virtualCamera != null)
        {
            virtualCamera.gameObject.SetActive(isActive);
            isCameraActive = isActive; // Update the camera state
        }
    }
}


