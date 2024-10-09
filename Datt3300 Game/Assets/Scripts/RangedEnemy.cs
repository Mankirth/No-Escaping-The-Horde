using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    private Vector3 toPlayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float maxDistance, fireRate;
    [SerializeField] private GameObject bulletPrefab;
    private float fireRateReset;

    void Start(){
        fireRateReset = fireRate;
    }
    void FixedUpdate()
    {
        toPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        if(rb.velocity.magnitude < moveSpeed && toPlayer.magnitude > maxDistance)
            rb.AddForce(new Vector2(toPlayer.x, toPlayer.y).normalized * moveSpeed, ForceMode2D.Force);
        fireRate -= Time.deltaTime;
        if(fireRate <= 0){
            GameObject spawned = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            spawned.GetComponent<Rigidbody2D>().AddForce(toPlayer.normalized * 5, ForceMode2D.Impulse);
            spawned.GetComponent<BulletBase>().damage = 0.5f;
            fireRate = fireRateReset;
        }
    }
}
