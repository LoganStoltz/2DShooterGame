using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Gun Vars
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;

    private float fireTimer;

    public int currentClip, maxClipSize = 10, currentAmmo, maxAmmoSize = 100;

    private void Update()
    {
        if(Input.GetMouseButton(0) && fireTimer <= 0f) {
            Shoot();
            fireTimer = fireRate;
        }
        else {
            fireTimer -= Time.deltaTime;
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            Debug.Log("Reloading");
        }
    }

    public void Shoot()
    {
        if(currentClip > 0)
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
            currentClip--;
        }
    }

    public void Reload()
    {
        int reloadAmount = maxClipSize - currentClip; //how many bullets to refill clip
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo; // if we do have enough ammo to refill then we return reloadAmount otherwise we return the currectAmmo.
        currentAmmo += reloadAmount;
        currentAmmo -= reloadAmount;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if(currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
    }
}
