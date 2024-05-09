using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;



public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public int maxAmmo = 10;
    private int ammoCount;
    public float reloadTime = 2f;
    public Text ammoText;
    private bool isReloading = false;
    public float fireRate = 0.1f;
    private bool isShooting = false;

    private void Start()
    {
        ammoCount = maxAmmo;
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
            Shoot();
            InvokeRepeating("Shoot", fireRate, fireRate);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            CancelInvoke("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.R) || ammoCount == 0)
        {
            Reload();
        }

        if (isReloading)
        {
            return;
        }

        
    }

    void Shoot()
    {
        if (ammoCount > 0 && !isReloading)
        {
            ammoCount--;
            UpdateAmmoUI();
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            // Get the direction the player is looking
            Vector3 shootDirection = Camera.main.transform.forward;

            // Set the velocity of the bullet in the shoot direction
            bulletRigidbody.velocity = shootDirection * bulletSpeed;
        }
    }

    private void Reload()
    {
        if (isReloading || ammoCount == maxAmmo)
            return;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (Input.GetButton("Fire1") && ammoCount > 0)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }

        isShooting = false;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        // Show reload UI indication (e.g., a progress bar)
        yield return new WaitForSeconds(reloadTime);
        ammoCount = maxAmmo;
        UpdateAmmoUI();
        isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = ammoCount.ToString();
    }
}
