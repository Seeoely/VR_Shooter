using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Diagnostics;


public class Gun : MonoBehaviour
{
    // Reference to the bullet prefab and spawn point
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    // Bullet properties
    public float bulletSpeed = 10f;
    public int maxAmmo = 10;
    private int ammoCount;
    public float reloadTime = 2f;

    // User interface elements
    public Text ammoText;
    private bool isReloading = false;

    // Fire rate properties
    public float fireRate;
    private bool isShooting = false;

    // Maximum distance properties
    public float maxDistance;

    // Player input components
    private PlayerInput playerInput;
    private InputAction fireAction;
    private InputAction reloadAction;

    // Initialize the gun's properties and UI elements
    void Awake()
    {
        // Find the PlayerInput component attached to the player GameObject
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            // Get the PlayerInput component from the player GameObject
            playerInput = playerObject.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                // Get the fire action from the PlayerInput component
                fireAction = playerInput.actions["Fire"];
            }
        }

        // Set the initial ammo count and update the UI
        ammoCount = maxAmmo;
        UpdateAmmoUI();

        // Get the reload action from the PlayerInput component
        reloadAction = playerInput.actions["Reload"];
    }

    // Set the initial ammo count and update the UI
    private void Start()
    {
        ammoCount = maxAmmo;
        UpdateAmmoUI();
    }

    // Update the gun's behavior every frame
    void Update()
    {
        // Find the player GameObject
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Get the fire and reload input values
        var fireInput = fireAction.ReadValue<float>();
        var reloadInput = reloadAction.ReadValue<float>();

        // Calculate the distance to the gun to check if player is holding it
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);

        // Check if the player is holding the gun, has enough ammo, is not reloading, and is not shooting
        if (fireInput > 0 && !isReloading && ammoCount > 0 && !isShooting && distance <= maxDistance)
        {
            StartCoroutine(ShootCoroutine());
        }

        // Check if the player is pressing the reload button or if the gun is out of ammo
        if (reloadInput > 0 || ammoCount == 0)
        {
            Reload();
        }
    }

    // Shoot a bullet and decrease the ammo count
    void Shoot()
    {
        // Decrease the ammo count and update the UI
        ammoCount--;
        UpdateAmmoUI();

        // Get the direction of the line barrel
        Vector3 shootDirection = bulletSpawnPoint.forward;

        // Instantiate the bullet at the start position of barrel
        Vector3 bulletPosition = bulletSpawnPoint.position;

        // Instantiate the bullet with the direction of the barrel
        Quaternion bulletRotation = bulletSpawnPoint.rotation * Quaternion.Euler(90f, 0f, 0f);
        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, bulletRotation);

        // Give the bullet velocity
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = shootDirection * bulletSpeed;
    }

    private IEnumerator ShootCoroutine()
    {
        // Shoot a bullet
        Shoot();

        // Set the isShooting flag to true
        isShooting = true;

        // Wait for the duration of the fire rate
        yield return new WaitForSeconds(1 / fireRate);

        // Set the isShooting flag to false
        isShooting = false;
    }

    // Start the reload coroutine if reloading is possible
    private void Reload()
    {
        // Check if reloading is not possible
        if (isReloading || ammoCount == maxAmmo)
        {
            return;
        }

        // Start the reload coroutine
        StartCoroutine(ReloadCoroutine());
    }

    // Reload the gun and update the UI
    private IEnumerator ReloadCoroutine()
    {
        // Set the isReloading flag to true
        isReloading = true;

        // Show reload UI indication (e.g., a progress bar)

        // Wait for the duration of the reload time
        yield return new WaitForSeconds(reloadTime);

        // Set the ammo count to the maximum ammo count
        ammoCount = maxAmmo;

        // Update the ammo UI
        UpdateAmmoUI();

        // Set the isReloading flag to false
        isReloading = false;
    }

    // Update the ammo UI text
    private void UpdateAmmoUI()
    {
        // Set the ammo text to the current ammo count
        ammoText.text = ammoCount.ToString();
    }
}
