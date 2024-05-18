using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthBar;
    public GameManager GameManager;
    public Transform platform; // Reference to the platform's transform
    public float deathThreshold = -5f; // Y-coordinate threshold for death
    public int ZombieDamage = 10;
    public int SkeletonDamage = 5;
    public int SkeletonArrowDamage = 15;
    public int HealthPackHeal = 50;
    public GameObject HealthPack;

    // Initialize the player's health and update the health bar
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Check for game over conditions every frame
    private void Update()
    {
        // Check if the player falls below the death threshold
        if (transform.position.y < platform.position.y + deathThreshold)
        {
            // Trigger game over functionality
            GameManager.GameOver();
        }
    }

    // Handle collisions with other objects
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a zombie
        if (collision.gameObject.CompareTag("Zombie"))
        {
            TakeDamage(ZombieDamage); // Adjust the amount of damage as needed
        }

        // Check if the collision is with a skeleton
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            TakeDamage(SkeletonDamage); // Adjust the amount of damage as needed
        }

        // Check if the collision is with a skeleton arrow
        if (collision.gameObject.CompareTag("SkeletonArrow"))
        {
            TakeDamage(SkeletonArrowDamage); // Adjust the amount of damage as needed
        }

        // Check if the collision is with a health pack
        if (collision.gameObject.CompareTag("HealthPack"))
        {
            // Heal the player if their health is below maximum
            if (currentHealth < 100)
            {
                Heal(HealthPackHeal);
                Destroy(collision.gameObject);
            }

            // Prevent overhealing
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
                Destroy(collision.gameObject);
            }
        }
    }

    // Heal the player by a specified amount
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        UpdateHealthBar();
    }

    // Deal damage to the player by a specified amount
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        UpdateHealthBar();

        // Check if the player has died
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Update the health bar to reflect the player's current health
    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.value = fillAmount;
    }

    // Trigger game over functionality when the player dies
    private void Die()
    {
        GameManager.GameOver();
    }
}
