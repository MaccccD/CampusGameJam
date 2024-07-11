using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2f; // Adjust this value to control sensitivity

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Apply rotation to the player or camera based on mouseX and mouseY
        // Example: transform.Rotate(Vector3.up, mouseX); // for horizontal rotation
        // Example: transform.Rotate(Vector3.left, mouseY); // for vertical rotation
    }
}

