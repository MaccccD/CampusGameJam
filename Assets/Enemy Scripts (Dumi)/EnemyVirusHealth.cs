using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyVirusHealth : MonoBehaviour
{
    public int EnemyTypeMaxHealth = 5; // in here, the variable for the health will change depending on the number of virus shoots aimed at a particular enemy type
    public GameObject gibEffect; // Explosion particle effect, this is just for  vibes if we  wanna dd it
    public AudioClip gibSFX; // souynd effects?? no ?? 
    private GameObject _player;
    private int _currentHealth;
    public bool IsAlive = true; // Assume enemies start alive as the player will have to pick up the virus, shoot the enemy, the enemy takes damage, their health goes down and then they die.
    public static int enemiesInfected = 0;//to keeop tck across all instances, can also change it if we want to know how many have been infected.
    public Text InfectedText;
    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentHealth = EnemyTypeMaxHealth;
        UpdateDeadText();
    }

    public void DeductHealth(int healthToDeduct)
    {
        _currentHealth -= healthToDeduct; // so each time the player shoots the enemy, the enemy health gopes down by a specifi number

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        IsAlive = false;
        if (gibEffect != null)
            Instantiate(gibEffect, transform.position, Quaternion.identity); // Instantiate explosion particle effect for that annihilation effect or a zombie dead sound or sumn
        if (gibSFX != null)
            AudioSource.PlayClipAtPoint(gibSFX, transform.position, 10f); // if we gonnn have sounds then this can stya , otherwise , yall can remove it

        Destroy(gameObject);

        // Update the count of dead enemies
        enemiesInfected++;
        UpdateDeadText();

        Debug.Log("Enemy has been defeated by the player that carries the Virus , well Virus Player ");
    }

    void UpdateDeadText() // okay here Bahle can edit the code so it works with her UI if it is needed 
    {
        InfectedText.text = "Enemies infected by the virus" + enemiesInfected.ToString();
    }
}
