using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float mouseX;
    private bool isGrounded;
    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 horizontalVelocityBeforeCollision;
    private bool isBeingHitByEnemy;
    private bool wasHitByEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;

        // Rotate the player with mouse movement
        Quaternion rotation = Quaternion.Euler(0f, mouseX, 0f);
        rb.MoveRotation(rotation);

        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Handle jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Move the player forward based on camera direction
        Vector3 movement = cameraTransform.forward * Input.GetAxis("Vertical") +
                           cameraTransform.right * Input.GetAxis("Horizontal");
        movement.y = 0f; // Disable vertical movement
        movement.Normalize();

        // Store the player's horizontal velocity before applying the movement
        Vector3 horizontalVelocity = rb.velocity - new Vector3(0f, rb.velocity.y, 0f);

        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        // Restore the player's horizontal velocity
        rb.velocity = horizontalVelocity + new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (isBeingHitByEnemy && isGrounded)
        {
            // Restore the player's horizontal velocity after the collision
            rb.velocity = new Vector3(horizontalVelocityBeforeCollision.x, rb.velocity.y, horizontalVelocityBeforeCollision.z);

            // Reset the flag since the player is no longer being hit by an enemy
            isBeingHitByEnemy = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Store the player's horizontal velocity before the collision
            horizontalVelocityBeforeCollision = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Set the player's vertical velocity to zero to avoid any residual vertical velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Set the flag to indicate that the player is being hit by an enemy
            isBeingHitByEnemy = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset the flag if the collision is not with an enemy
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            isBeingHitByEnemy = false;
        }
    }

    private bool isHitByEnemy(Collision collision)
    {
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            wasHitByEnemy = true;
            return true;
        }
        else
        {
            wasHitByEnemy = false; // Reset the flag if the collision is not with an enemy
            return false;
        }
    }


    private void Jump()
    {
        // Apply upward force for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
