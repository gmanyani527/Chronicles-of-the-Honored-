using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // For displaying UI elements

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100; // Maximum health
    private int currentHealth; // Current health

    [SerializeField] TextMeshProUGUI healthText;



    [SerializeField] GameObject gameOver;

    [SerializeField]
    private int damagePerCollision = 25; // Damage taken on collision with an enemy

    private bool isAlive = true; // Track if the player is alive

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        UpdateHealthUI(); // Update health display
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return; // If the player is already dead, ignore damage

        currentHealth -= damage; // Reduce health
        UpdateHealthUI(); // Update the health text

        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    // Handle collisions with enemies
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damagePerCollision); // Call TakeDamage when colliding with an enemy
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth); // Heal but don't exceed max health
        UpdateHealthUI(); // Update the health display
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}"; // Update the health display
        }
    }

    // Handle player death
    private void Die()
    {
        isAlive = false; // Player is no longer alive
        Debug.Log("Player has died.");

        // Display the "Game Over!" text
        if (gameOver != null)
        {
            gameOver.SetActive(true); // Show "Game Over!" message

            // Freeze the game by setting Time.timeScale to zero
            Time.timeScale = 0f; // Pause all physics, animation, and movement
        }

        // Additional game-over logic can be added here
    }
}