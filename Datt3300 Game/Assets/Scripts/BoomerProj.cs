using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerProj : MonoBehaviour
{

    private Vector2 go;
    private bool killable;
    private float killTime = 1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 1), 150 * Time.deltaTime);
        if(killTime > 0)
            killTime -= Time.deltaTime;
        else
            killable = true;
        go = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * 25;
        gameObject.GetComponent<Rigidbody2D>().AddForce(go, ForceMode2D.Force);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && killable)
            Destroy(gameObject);
    }
}
