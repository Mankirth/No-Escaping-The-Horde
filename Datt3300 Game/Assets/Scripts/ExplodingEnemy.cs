using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 2){
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            Destroy(gameObject, 0.5f);
        }
    }
}
