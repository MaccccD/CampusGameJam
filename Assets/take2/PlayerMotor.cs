using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // Movement
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3;

    // Shooting 
    public GameObject projectilePrefab; // Assign the projectile prefab in the Inspector
    public Transform shootPoint; // Assign the shooting point (position) in the Inspector
    public float projectileSpeed = 20f; // Speed of the projectile

    // Health Stats
    public int playerHealth;
    public int maxHealth;

    // Kill Stats
    public int killCount;

    public int viralLoad = 5; // basically the bullets a player has


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        viralLoad = 5;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Shoot();
        }
    }

    //Power Ups Logic
   

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            // Add health to the player
            playerHealth += 20; // Adjust health increase as needed

            // Debug log statement
            Debug.Log("Health Power-Up collected! Player gains Health (playerMtor Script)");

            // Deactivate the power-up object
            other.gameObject.SetActive(false); // Deactivate the health power-up object
        }
        else if (other.CompareTag("Damage"))
        {
            // Add health to the player
            // Adjust damage increase as needed

            // Debug log statement
            Debug.Log("Damage ++");

            // Deactivate the power-up object
            other.gameObject.SetActive(false); // Deactivate the health power-up object
        }
        else if (other.CompareTag("Ammo"))
        {
            
            // Debug log statement
            Debug.Log("Ammo added ++");
            viralLoad = viralLoad + 4;

            // Deactivate the power-up object
            other.gameObject.SetActive(false); // Deactivate the health power-up object
        }
    }


    void Shoot()
    {
        if(viralLoad != 0)
        {
            // Instantiate the projectile at the shooting point
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            // Get the Rigidbody component of the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Add force to the projectile to make it move
            if (rb != null)
            {
                rb.velocity = shootPoint.forward * projectileSpeed;
            }
            viralLoad -= 1;
        }
        else
        {
            Debug.Log("Viral Load is low");
        }
        
    }

    //input man script appl to cc
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }



}
