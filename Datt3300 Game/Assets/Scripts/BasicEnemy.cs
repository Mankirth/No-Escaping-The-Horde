using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Vector3 toPlayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb.velocity.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if(rb.velocity.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        toPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        if(rb.velocity.magnitude < moveSpeed)
            rb.AddForce(new Vector2(toPlayer.x, toPlayer.y).normalized * moveSpeed, ForceMode2D.Force);
    }
}
