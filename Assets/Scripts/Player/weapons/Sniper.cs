using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    public Camera fpsCamera; // Assign the first-person camera
    public int damage = 100; // Damage dealt by the sniper
    public float range = 1000f; // Maximum range of the sniper

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Fire1 is usually the left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        // Sending out a ray from the center of the camera's viewport
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name); // Prints the name of the object hit

            // Check if the hit object has an Enemy component
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Check if the hit object has an Ork component
            Ork ork = hit.transform.GetComponent<Ork>();
            if (ork != null)
            {
                ork.TakeDamage(damage);
            }

            // You can add more conditions for different enemy types if needed
        }
    }
}
