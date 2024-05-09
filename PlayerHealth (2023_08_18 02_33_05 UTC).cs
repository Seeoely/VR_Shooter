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

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        // Check if the player falls below the death threshold
        if (transform.position.y < platform.position.y + deathThreshold)
        {
            // Trigger game over functionality
            GameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10); // Adjust the amount of damage as needed
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.value = fillAmount;
    }

    private void Die()
    {
         GameManager.GameOver();
    }
}
