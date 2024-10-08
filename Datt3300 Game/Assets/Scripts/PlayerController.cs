using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText, timerMultiplier;
    [SerializeField] private GameObject tempWalls, UpgradeScreen;
    public float killTimerMultiplier, moveSpeed, baseDamage, fireRateMultiplier, bulletSpeedMultiplier, attackRange;
    private float killTimer = 120, minutes, seconds, iframes;
    private bool moveL, moveR, moveU, moveD;
    public int xp, xpCap, level = 1;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        timerMultiplier.text = (killTimerMultiplier) * 100 + "%";
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
        if(iframes > 0)
            iframes -= Time.deltaTime;
        if(killTimer > 0){
            killTimer -= Time.deltaTime * killTimerMultiplier;
            minutes = (float)Math.Floor(killTimer / 60);
            seconds = (float)Math.Floor(killTimer) % 60;
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

    public void LevelUp(){
        level += 1;
        xpCap = (int)Math.Pow(xpCap, 1.5f);
        xp = 0;
        Time.timeScale = 0f;
        UpgradeScreen.SetActive(true);
        UpgradeScreen.GetComponent<UpgradeScreen>().Activate();
    }

    public void TestUpgrade(){
        Time.timeScale = 1;
        UpgradeScreen.GetComponent<UpgradeScreen>().Reset();
        UpgradeScreen.SetActive(false);
    }

    void OnCollisionStay2D(Collision2D other){
        if((other.gameObject.tag == "Enemy Attack" || other.gameObject.tag == "Enemy") && iframes <= 0){
            killTimerMultiplier += other.gameObject.GetComponent<EnemyBase>().baseDamage;
            timerMultiplier.text = (killTimerMultiplier) * 100 + "%";
            iframes = 0.5f;
        }
    }

    public void SetHeal(float time){
        killTimer += time;
    }

    public void MultiplyHeal(float percent){
        killTimer = killTimer * percent;
    }

    public void HealthMultiplier(float percent){
        killTimerMultiplier += percent;
        timerMultiplier.text = (killTimerMultiplier) * 100 + "%";
    }

    public void MoveSpeed(float speed){
        moveSpeed += speed;
    }

    public void MoveSpeedMultiplier(float speed){
        moveSpeed = moveSpeed * speed;
    }

    public void AttackRange(float range){
        attackRange += range;
    }

    public void AttackRangeMultiplier(float range){
        attackRange = attackRange * range;
    }

    public void FireRate(float speed){
        fireRateMultiplier += speed;
    }

    public void FireRateMultiplier(float speed){
        fireRateMultiplier = fireRateMultiplier * speed;
    }

    public void BulletSpeed(float speed){
        bulletSpeedMultiplier += speed;
    }

    public void BulletSpeedMultiplier(float speed){
        bulletSpeedMultiplier = bulletSpeedMultiplier * speed;
    }

    public void BaseDamage(float dmg){
        baseDamage += dmg;
    }

    public void BaseDamageMultiplier(float dmg){
        baseDamage = baseDamage * dmg;
    }

    public void AddWeapon(GameObject weaponPrefab){
        Instantiate(weaponPrefab, transform.position, transform.rotation, transform);
    }

}