using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float damage;
    public bool destroyOnHit;
    [SerializeField] private float deathTimer;
    public bool useDeathTimer;
    [SerializeField] private CircleCollider2D bounce;
    
    void Start(){
        if(bounce != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().pierce){
            bounce.enabled = true;
        }
        if(useDeathTimer)
            Destroy(gameObject, deathTimer);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == 9 && gameObject.tag == "Enemy Attack" && destroyOnHit){
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == 9 && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().pierce && destroyOnHit){
            Destroy(gameObject);
        }
    }
}
