using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText, timerMultiplier;
    [SerializeField] private GameObject tempWalls;
    public float killTimerMultiplier, moveSpeed, baseDamage;
    private float killTimer = 50, minutes, seconds, iframes;
    private bool moveL, moveR, moveU, moveD;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            moveU = true;
        
        if(Input.GetKeyDown(KeyCode.S))
            moveD = true;

        if(Input.GetKeyDown(KeyCode.A))
            moveL = true;
        
        if(Input.GetKeyDown(KeyCode.D))
            moveR = true;
        
        if(Input.GetKeyUp(KeyCode.W))
            moveU = false;
        
        if(Input.GetKeyUp(KeyCode.S))
            moveD = false;

        if(Input.GetKeyUp(KeyCode.A))
            moveL = false;
        
        if(Input.GetKeyUp(KeyCode.D))
            moveR = false;
    }

    void FixedUpdate(){
        if(iframes > 0 )
            iframes -= Time.deltaTime;
        if(killTimer > 0){
            killTimer -= Time.deltaTime * killTimerMultiplier;
            minutes = (float)Math.Floor(killTimer / 60);
            seconds = killTimer % 60;
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else{
            timerText.text = "00:00";
            Kill();
        }

        tempWalls.transform.position = new Vector3(transform.position.x, 0, 0);
        
        if(moveL && !moveR){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if(moveR){
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if(moveU && !moveD){
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
        else if(moveD){
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        }
        else{
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void Kill(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionStay2D(Collision2D other){
        if((other.gameObject.tag == "Enemy Attack" || other.gameObject.tag == "Enemy") && iframes <= 0){
            killTimerMultiplier += other.gameObject.GetComponent<EnemyBase>().baseDamage;
            timerMultiplier.text = "+" + (killTimerMultiplier - 1) * 100 + "%";
            iframes = 0.5f;
        }
    }

}