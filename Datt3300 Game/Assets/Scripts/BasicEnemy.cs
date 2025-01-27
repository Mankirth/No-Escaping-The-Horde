using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Vector2 toPlayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(toPlayer.magnitude < 25){
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if(rb.velocity.x < 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else if(rb.velocity.x > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        toPlayer = (Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position);
        if(rb.velocity.magnitude < moveSpeed)
            rb.AddForce(toPlayer.normalized * moveSpeed, ForceMode2D.Force);
    }
}
