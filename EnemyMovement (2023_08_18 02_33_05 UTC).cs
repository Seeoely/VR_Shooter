using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed;
    private Transform player;
    private Rigidbody rb;
    public float rotationSpeed = 5f;
    public float distanceToPlayer = 10f;

    // Initialize the player reference and rigidbody component
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update the enemy's movement and rotation every frame
    private void FixedUpdate()
    {
        // Check if the enemy is a zombie
        if (gameObject.CompareTag("Zombie"))
        {
            // Check if the player is not null
            if (player != null)
            {
                // Calculate the direction to the player
                Vector3 direction = player.position - transform.position;
                direction.Normalize();

                // Set the enemy's velocity to move towards the player
                rb.velocity = direction * movementSpeed;

                // Rotate the enemy to face the direction of movement
                RotateTowardsMovementDirection();
            }
        }

        // Check if the enemy is a skeleton
        if (gameObject.CompareTag("Skeleton"))
        {
            // Calculate the desired position for the skeleton
            Vector3 desiredPosition = player.position - transform.forward * distanceToPlayer;
            desiredPosition.y = 0;

            // Move the skeleton towards the player if they are within the distance threshold
            if (Vector3.Distance(transform.position, player.position) <= distanceToPlayer)
            {
                transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
            }

            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - transform.position;
            rb.velocity = directionToPlayer.normalized * movementSpeed;
            directionToPlayer.y = 0f; // Keep the rotation in the horizontal plane

            // Rotate the skeleton to face the player
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }

        /*if (gameObject.CompareTag("Slime"))
        {
            // Move the slime towards the player
            Vector3 desiredPosition = player.position - transform.forward * distanceToPlayer;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);

            // Rotate the slime to face the direction of movement
            RotateTowardsMovementDirection();
        }*/
    }

    // Rotate the enemy towards the direction of movement
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
