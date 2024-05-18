using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    private float currentHealth;
    public Transform platform; // Reference to the platform's transform
    public float deathThreshold = -5f; // Y-coordinate threshold for death

    // Initialize the enemy's health and update the health bar
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Check for death conditions every frame
    private void Update()
    {
        // Check if the enemy falls below the death threshold
        if (transform.position.y < platform.position.y + deathThreshold)
        {
            // Destroy the enemy game object
            Destroy(gameObject);
        }
    }

    // Deal damage to the enemy by a specified amount
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        // Check if the enemy has died
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle collisions with other objects
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(Bullet.damageAmount); // Adjust the amount of damage as needed
        }
    }

    // Update the health bar to reflect the enemy's current health
    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.value = fillAmount;
    }

    // Destroy the enemy game object when it dies
    private void Die()
    {
        Destroy(gameObject);
    }
}
