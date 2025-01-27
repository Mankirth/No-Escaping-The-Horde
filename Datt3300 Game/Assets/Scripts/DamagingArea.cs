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
        InvokeRepeating("Pulse", 0, dmgTimer);
    }
    
    void Pulse(){
        foreach(Collider2D other in Physics2D.OverlapCircleAll(transform.position, 3f)){
            if(other.tag == "Enemy"){
                other.GetComponent<EnemyBase>().hp.Value -= (int)(GameObject.Find("Player").GetComponent<PlayerController>().baseDamage * dmgMultiplier);
                other.attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}
