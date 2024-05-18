using UnityEngine;

public class Bullet : MonoBehaviour
{
    // The amount of damage this bullet deals to enemies
    public static int damageAmount = 10;

    private void Start()
    {
        // Destroy the bullet after 5 seconds to prevent clutter
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with an enemy
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Get the enemy's health component
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy
                enemyHealth.TakeDamage(damageAmount);
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
