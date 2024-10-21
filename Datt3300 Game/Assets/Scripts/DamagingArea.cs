using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagingArea : MonoBehaviour
{
    [SerializeField] private float dmgMultiplier;
    private float dmgTimer;
    
    void Start(){
        dmgTimer = 0.5f * GameObject.Find("Player").GetComponent<PlayerController>().fireRateMultiplier;
    }
    
    void FixedUpdate(){
        dmgTimer -= Time.deltaTime;
        if(dmgTimer <= 0){
            Pulse();
            dmgTimer = 0.5f * GameObject.Find("Player").GetComponent<PlayerController>().fireRateMultiplier;
        }
    }
    void Pulse(){
        foreach(Collider2D other in Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2)){
            if(other.tag == "Enemy"){
                other.GetComponent<EnemyBase>().hp.Value -= (int)(GameObject.Find("Player").GetComponent<PlayerController>().baseDamage * dmgMultiplier);
                other.attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}
