using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount = 10;
    //public float bulletSpeed = 10f;

    private void Start()
    {
        Destroy(gameObject, 5f); // Destroy the bullet after 5 seconds to prevent clutter
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            EnemyHealth EnemyHealth = other.GetComponent<EnemyHealth>();
            if (EnemyHealth != null)
            {
                EnemyHealth.TakeDamage(damageAmount);
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
