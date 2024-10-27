using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Gun Vars
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    private float fireRate = 0.5f;
    private float fireTimer;

    public int currentClip, maxClipSize = 10, currentAmmo, maxAmmoSize = 100;

    // Deployable Vars
    [SerializeField] private GameObject deployablePrefab;
    [SerializeField] private Transform deployPosition;
    public int deployableAmmo = 10;
    private bool isDeploying = false;

    private void Update()
    {
        if (isDeploying && Input.GetMouseButtonDown(0) && deployableAmmo > 0)
        {
            DeployObject();
        }
        else if (!isDeploying && Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            Debug.Log("Reloading");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
            Debug.Log("Weapon 1 selected");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
            Debug.Log("Weapon 2 selected");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(3);
            Debug.Log("Weapon 3 selected");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(4);
            Debug.Log("Deployable selected");
        }
    }

    public void Shoot()
    {
        if (currentClip > 0)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            currentClip--;
        }
    }

    public void Reload()
    {
        int reloadAmount = maxClipSize - currentClip;
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
    }

    public void ChangeWeapon(int weaponNum)
    {
        if (weaponNum == 1)
        {
            fireRate = 0.5f;
            isDeploying = false;
            Debug.Log(fireRate);
        }
        else if (weaponNum == 2)
        {
            fireRate = 0.2f;
            isDeploying = false;
            Debug.Log(fireRate);
        }
        else if (weaponNum == 3)
        {
            fireRate = 0f;
            isDeploying = false;
            Debug.Log(fireRate);
        }
        else if (weaponNum == 4)
        {
            isDeploying = true;
            Debug.Log("Deployable Mode");
        }
    }

    private void DeployObject()
    {
        if (deployableAmmo <= 0) return;

        // Define grid size (e.g., 1 unit per tile)
        float gridSize = 1.0f; 

        // Calculate the position in front of the player
        Vector3 forwardDirection = transform.up; // Assuming 'up' is forward in a top-down perspective
        Vector3 targetPosition = deployPosition.position + forwardDirection * gridSize;

        // Snap to nearest grid position
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(targetPosition.x / gridSize) * gridSize,
            Mathf.Round(targetPosition.y / gridSize) * gridSize,
            targetPosition.z // Keep the z as it is for a 2D game
        );

        // Instantiate the deployable object at the snapped position
        Instantiate(deployablePrefab, snappedPosition, Quaternion.identity);
        deployableAmmo--;

        if (deployableAmmo <= 0)
        {
            isDeploying = false;
            Debug.Log("No deployable ammo left!");
        }
    }
}
