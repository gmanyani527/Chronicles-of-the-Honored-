using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 3; // Initial health for the enemy
    [SerializeField]
    private AudioClip deathSound; // Sound to play when enemy is destroyed
    private AudioSource audioSource; // Audio source component

    void Start()
    {
        // Ensure there's an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If missing, add a new AudioSource
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Decrease health by the given damage value
        Debug.Log("Enemy health is now: " + health); // Debugging to track health

        if (health <= 0) // If health is zero or below
        {
            Die(); // Call Die() when health reaches zero
        }
    }

    private void Die()
    {
        if (audioSource != null && deathSound != null) // Check if the AudioSource and sound are set
        {
            audioSource.PlayOneShot(deathSound); // Play the death sound
        }

        Destroy(gameObject, 1f); // Delay destruction to allow sound to play
    }
}