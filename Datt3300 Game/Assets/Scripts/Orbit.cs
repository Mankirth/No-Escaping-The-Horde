using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private GameObject parent;
    [SerializeField] private bool turret;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate, bulletSpeed, dmgMultiplier;
    private float fireRateReset;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        fireRateReset = fireRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), parent.GetComponent<PlayerController>().bulletSpeedMultiplier * Time.deltaTime * bulletSpeed);
        if(turret){
            fireRate -= Time.deltaTime;
            if(fireRate <= 0){
                GameObject spawned = Instantiate(bulletPrefab, transform.position, transform.rotation);
                spawned.GetComponent<Rigidbody2D>().AddForce(transform.right * (bulletSpeed/4) * transform.parent.GetComponent<PlayerController>().bulletSpeedMultiplier, ForceMode2D.Impulse);
                spawned.GetComponent<BulletBase>().damage = transform.parent.GetComponent<PlayerController>().baseDamage * dmgMultiplier;
                fireRate = fireRateReset;
            }
        }
    }
}
