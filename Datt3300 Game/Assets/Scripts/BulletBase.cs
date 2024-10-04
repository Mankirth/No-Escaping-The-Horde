using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float damage;
    [SerializeField] private float deathTimer;

    void FixedUpdate(){
        if(deathTimer > 0)
            deathTimer -= Time.deltaTime;
        else
            Destroy(gameObject);
    }
}
