using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaneController : MonoBehaviour
{
    // Static variable to represent the global state of whether the player is on the plane
    public static bool IsPlayerOnPlane = false;

    void Update()
    {
        // Check every frame whether the player is on the plane
        IsPlayerOnPlane = CheckIfPlayerOnPlane();
    }

    private bool CheckIfPlayerOnPlane()
    {
        // Create a ray from the player's position towards the plane
        Ray ray = new Ray(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position - GameObject.FindGameObjectWithTag("Player").transform.position);

        // Check if the ray intersects with the plane
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Plane"))
            {
                return true;
            }
        }

        return false;
    }
}
