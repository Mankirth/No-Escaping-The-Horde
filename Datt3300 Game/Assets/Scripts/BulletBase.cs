using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float damage;
    public bool destroyOnHit;
    [SerializeField] private float deathTimer;
    [SerializeField] private bool useDeathTimer;

    void FixedUpdate(){
        if(useDeathTimer){
            if(deathTimer > 0)
                deathTimer -= Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}
