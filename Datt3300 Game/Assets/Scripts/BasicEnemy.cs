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
        toPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        rb.velocity = new Vector2(toPlayer.x, toPlayer.y).normalized * moveSpeed;
    }
}
