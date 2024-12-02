using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pistol : MonoBehaviour
{
    public float reloadTimer, fireRate, maxAmmo, bulletSpeed, dmgMultiplier;
    private float reloadTimerReset, fireRateReset, ammo, attackRange = 10;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        fireRateReset = fireRate;
        ammo = maxAmmo;
        reloadTimerReset = reloadTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ammo > 0 && fireRate <= 0){
            Shoot();
            fireRate = fireRateReset * transform.parent.GetComponent<PlayerController>().fireRateMultiplier;
        }
        else if(ammo > 0 && fireRate > 0){
            fireRate -= Time.deltaTime;
        }
        else if(reloadTimer > 0){
            reloadTimer -= Time.deltaTime;
        }
        else if(reloadTimer <= 0){
            reloadTimer = reloadTimerReset;
            ammo = maxAmmo;
        }
    }

    private void Shoot(){
        GameObject chosenEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if(chosenEnemy != null){
            float shortestDistance = Vector3.Distance(chosenEnemy.transform.position, transform.position);
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
                if(Vector3.Distance(enemy.transform.position, transform.position) < shortestDistance){
                    chosenEnemy = enemy;
                    shortestDistance = Vector3.Distance(chosenEnemy.transform.position, transform.position);
                }
            }
            if(chosenEnemy != null && shortestDistance <= attackRange){
                GameObject spawned = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().AddForce((chosenEnemy.transform.position - transform.position).normalized * bulletSpeed * transform.parent.GetComponent<PlayerController>().bulletSpeedMultiplier, ForceMode2D.Impulse);
                spawned.GetComponent<BulletBase>().damage = transform.parent.GetComponent<PlayerController>().baseDamage * dmgMultiplier;
                AudioMaster.instance.PlaySFXClip(shootSound, transform, 0.05f);
            }
            ammo -= 1;
        }
    }

    public void PistolUp(){
        attackRange = attackRange * 1.5f;
        reloadTimerReset = reloadTimerReset / 2;
        fireRateReset = fireRateReset / 2;
    }
}
