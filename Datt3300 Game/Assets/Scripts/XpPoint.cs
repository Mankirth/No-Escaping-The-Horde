using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpPoint : MonoBehaviour
{
    private bool triggered;
    private float speed = 50;

    void FixedUpdate(){
        if(triggered){
            speed += Time.deltaTime * 20;
            gameObject.GetComponent<Rigidbody2D>().AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * speed, ForceMode2D.Force);
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            triggered = true;
            if(Vector3.Distance(transform.position, other.transform.position) <= 0.5){
                other.gameObject.GetComponent<PlayerController>().xp += 1;
                if(other.gameObject.GetComponent<PlayerController>().xp >= other.gameObject.GetComponent<PlayerController>().xpCap)
                    other.gameObject.GetComponent<PlayerController>().LevelUp();
                Destroy(gameObject);
            }
        }
    }
}
