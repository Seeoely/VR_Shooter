using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public float rotationSpeed = 1f;
    public float distance = 5f;
    public float heightOffset = 2f;
    public float tiltAngle = 30f;

    private float mouseX;  // Mouse X movement input

    private void Update()
    {
        // Get the mouse movement input
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;

        // Calculate the rotation around the player based on mouse movement
        Quaternion rotation = Quaternion.Euler(tiltAngle, mouseX, 0f);

        // Calculate the camera's position based on the player's position, distance, and height offset
        Vector3 offset = new Vector3(0f, heightOffset, 0f);
        Vector3 desiredPosition = target.position + rotation * offset - rotation * Vector3.forward * distance;

        // Set the camera's position
        transform.position = desiredPosition;

        // Make the camera look at the player's position
        transform.LookAt(target.position);
    }
}
