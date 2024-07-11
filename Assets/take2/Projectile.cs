using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void start()
    {
        Destroy(gameObject, 3f);
        //Destoy predab after3 secs
    }
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the tag "enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // If true, print a message or handle the collision
            Debug.Log("Projectile hit an enemy!");
            Destroy(gameObject);


            // Add your collision handling code here, for example:
            // Destroy the enemy, apply damage, etc.
            // Destroy(collision.gameObject); // Example: destroy the enemy
            // Destroy(gameObject); // Example: destroy the projectile
        }
    }
}
