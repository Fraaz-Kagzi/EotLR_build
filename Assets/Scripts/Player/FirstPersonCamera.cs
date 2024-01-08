
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float sensitivity = 2.0f; // Mouse sensitivity
    public float smoothing = 2.0f; // Mouse smoothing

    private Vector2 mouseLook;
    private Vector2 smoothV;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock and hide the cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        
        // Get mouse input
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Apply sensitivity and smoothing
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1f / smoothing);
        mouseLook += smoothV;

        // Clamp the vertical rotation to prevent flipping
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
        
        // Rotate the camera and the player
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.localRotation = Quaternion.AngleAxis(mouseLook.x, player.up);
    }
}


