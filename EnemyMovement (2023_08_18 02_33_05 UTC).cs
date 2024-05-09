using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    private Transform player;
    private Rigidbody rb;
    public float rotationSpeed = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * movementSpeed;
            RotateTowardsMovementDirection();
        }
    }

    private void RotateTowardsMovementDirection()
    {
        // Get the current velocity of the enemy
        Vector3 velocity = rb.velocity;

        // Check if the enemy is moving
        if (velocity != Vector3.zero)
        {
            // Calculate the rotation to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);

            // Smoothly rotate the enemy towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
