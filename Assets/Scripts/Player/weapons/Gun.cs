using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{

    public GameObject bullet;

    public float shootForce, upwardForce;
    //Gun Specs
    public int magSize, bulletsPerTap;
    public float fireRate,BetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    //Game Refrecnces
    public Canvas canvas;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit RayHit;
    public Enemy enemy; // Drag and drop the enemy GameObject with the Enemy script attached in the Inspector.
    public bool allowInvoke = true;


    public LayerMask WhatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHole;
    public TextMeshProUGUI text;
    public GameObject reloadText;

    private void Start()
    {
        bulletsLeft = magSize;
        readyToShoot = true;
    }

    private void Update()
    {
        myInput();
        //text
        text.SetText(bulletsLeft/bulletsPerTap + " / " + magSize/bulletsPerTap);
    }

    private void myInput()
    {
        if(allowButtonHold)shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;


        /*if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out RayHit, range))//, WhatIsEnemy))
        {
            Debug.Log(RayHit.collider.name);
            if (RayHit.collider.CompareTag("Enemy"))
            {
                if (enemy != null)
                {
                    // Call the TakeDamage function when the gun fires (e.g., when a shot hits the enemy).
                    enemy.TakeDamage(damage);
                }
            }
        }*/

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
           
        }
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);




        //Graphics
       // Instantiate(bulletHole, RayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position,Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
      
        if(bulletsShot>0 && bulletsLeft>0)
        Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    

    private void Reload()
    {
        reloadText.SetActive(true);
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }
    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
        reloadText.SetActive(false);
    }
}