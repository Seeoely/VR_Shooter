using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    private float currentHealth;
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
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10); // Adjust the amount of damage as needed
        }
    }

    private void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        healthBar.value = fillAmount;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
