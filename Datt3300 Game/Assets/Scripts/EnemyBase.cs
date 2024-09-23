using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health, baseDamage;

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player Attack"){
            //take damage
        }
    }
}
