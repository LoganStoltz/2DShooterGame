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
    private bool Railgun = false;

    private RailShot railShot;

    private void Start()
    {
        railShot = GetComponent<RailShot>();
    }

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
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeWeapon(5);
            Debug.Log("Railgun selected");
        }
    }

    public void Shoot()
    {
        if (currentClip > 0)
        {
            if(Railgun)
            {
                railShot.FireRailgun();
            }
            else
            {
                Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            }
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
            Railgun = false;
            Debug.Log(fireRate);
        }
        else if (weaponNum == 2)
        {
            fireRate = 0.2f;
            isDeploying = false;
            Railgun = false;
            Debug.Log(fireRate);
        }
        else if (weaponNum == 3)
        {
            fireRate = 0f;
            isDeploying = false;
            Railgun = false;
            Debug.Log(fireRate);
        }
        else if (weaponNum == 4)
        {
            isDeploying = true;
            Railgun = false;
            Debug.Log("Deployable Mode");
        }
        else if (weaponNum == 5)
        {
            fireRate = 0.5f;
            isDeploying = false;
            Railgun = true;
            Debug.Log(fireRate);
        }
    }

    private void DeployObject()
    {
        if (deployableAmmo <= 0) return;


        float gridSize = 1.0f;
        Vector2 gridOffset = new Vector2(0.5f, 0.5f);

        Vector3 forwardDirection = transform.up; // 'up' is forward in my top-down perspective
        Vector3 targetPosition = deployPosition.position + forwardDirection * gridSize;

        // Adjust the target position to account for grid offset
        targetPosition -= (Vector3)gridOffset;

        // Snap to nearest grid position
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(targetPosition.x / gridSize) * gridSize,
            Mathf.Round(targetPosition.y / gridSize) * gridSize,
            targetPosition.z // dont worry about z as it is for a 2D game
        );

        // Apply the grid offset back to the snapped position
        snappedPosition += (Vector3)gridOffset;

        // Instantiate the deployable object at the final snapped position
        Instantiate(deployablePrefab, snappedPosition, Quaternion.identity);
        deployableAmmo--;

        if (deployableAmmo <= 0)
        {
            isDeploying = false;
            Debug.Log("No deployable ammo left!");
        }
    }
}
