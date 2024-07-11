using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    // int for health increase

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add health to the player
           // other.GetComponent<PlayerHealth>().IncreaseHealth(healthAmount);

            // Debug log statement
            Debug.Log("Health Power-Up collected!  ");

            // Deactivate the power-up object
            gameObject.SetActive(false);
        }
    }
}
