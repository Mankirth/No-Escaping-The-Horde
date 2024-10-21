using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private float fireTimer, bulletSpeed, dmgMultiplier;
    private float fireTimerReset;
    [SerializeField] private GameObject BoomerangPrefab;
    private PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        fireTimerReset = fireTimer;
        fireTimer = 1;
        controller = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fireTimer -= Time.deltaTime * controller.fireRateMultiplier;

        if(fireTimer <= 0){
            GameObject spawned = Instantiate(BoomerangPrefab, transform.position, Quaternion.identity);
            spawned.GetComponent<Rigidbody2D>().AddForce(transform.parent.GetComponent<Rigidbody2D>().velocity + (controller.direction * bulletSpeed * controller.bulletSpeedMultiplier), ForceMode2D.Impulse);
            spawned.GetComponent<BulletBase>().damage = transform.parent.GetComponent<PlayerController>().baseDamage * dmgMultiplier;
            fireTimer = fireTimerReset;
        }
    }
}
