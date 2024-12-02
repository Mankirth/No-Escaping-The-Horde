using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText, timerMultiplier, leveltxt;
    [SerializeField] private GameObject UpgradeScreen;
    [SerializeField] private CircleCollider2D pickup;
    [SerializeField] private AudioSource steps;
    [SerializeField] private AudioClip pickupSound, hurtSound;
    private Animator animator;
    public bool explosive, pierce;
    public float killTimerMultiplier, moveSpeed, baseDamage, fireRateMultiplier, bulletSpeedMultiplier, enemiesKilled;
    private float killTimer = 360, minutes, seconds, iframes, armour = 1, subTimer = 10, redVal = 255, multiplierScale = 1;
    private bool moveL, moveR, moveU, moveD, incrementHealth, incrementDmg, incs, subTime, dying;
    public int xp, xpCap, level = 1;
    private Rigidbody2D rb;
    public Vector2 direction = Vector2.left;
    [SerializeField] private Scrollbar scrollbar;
    private Color origColor;
    private Vector3 multiplierOriginalScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        timerMultiplier.text = Math.Round(killTimerMultiplier * 100, 0) + "%";
        //AudioMaster.instance.PlaySongs();
        AudioMaster.instance.ResetLayers();
        AudioMaster.instance.AddLayer();
        animator = gameObject.GetComponent<Animator>();
        origColor = gameObject.GetComponent<SpriteRenderer>().color;
        multiplierOriginalScale = timerMultiplier.transform.localScale;
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
        if(redVal < 255){
            redVal += Time.deltaTime * 255;
            timerMultiplier.color = new Color32(255, (byte)redVal, (byte)redVal, 255);
        }
        if(multiplierScale > 1){
            multiplierScale -= Time.deltaTime * 0.5f;
            timerMultiplier.transform.localScale = multiplierOriginalScale * multiplierScale;
        }
        if(rb.velocity.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else if(rb.velocity.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        animator.SetFloat("vel", rb.velocity.magnitude);
        scrollbar.value = (float)xp / xpCap;
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
        
        if(moveL && !moveR && !dying){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if(moveR && !dying){
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if(moveU && !moveD && !dying){
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        }
        else if(moveD && !dying){
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        }
        else{
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if(rb.velocity.magnitude > 0 && Time.timeScale != 1 && !dying){
            if(!steps.isPlaying)
                steps.Play();
            direction = rb.velocity.normalized;
        }
        else
            steps.Stop();

        if(!incs && enemiesKilled % 25 == 0){
            if(incrementDmg)
                baseDamage += 0.25f;
            if(incrementHealth)
                killTimer += 1;
            incs = true;
        }
        else if(incs == true && enemiesKilled % 25 != 0)
            incs = false;
        
        if(subTime)
            subTimer -= Time.deltaTime;

        if(subTimer <= 0 && killTimerMultiplier >= 0.95f){
            killTimerMultiplier -= 0.05f;
            timerMultiplier.text = Math.Round(killTimerMultiplier * 100, 0) + "%";
            subTimer = 10;
        }
    }

    private void Kill(){
        animator.SetBool("die", true);
        dying = true;
        StartCoroutine("Die");
    }
    public void LevelUp(){
        if(UpgradeScreen.GetComponent<UpgradeScreen>().upgradeCards.Count > 0){
            scrollbar.value = 1;
            level += 1;
            if(level <= 5)
                xpCap = 10 * level;
            else if(level <= 10)
                xpCap = 25 * level;
            else
                xpCap = 40 * level;
            leveltxt.text = "Level: " + level;
            Time.timeScale = 0f;
            UpgradeScreen.SetActive(true);
            UpgradeScreen.GetComponent<UpgradeScreen>().Activate();
        }
    }

    public void TestUpgrade(){
        Time.timeScale = 1;
        UpgradeScreen.GetComponent<UpgradeScreen>().Reset();
        UpgradeScreen.SetActive(false);
    }

    void OnCollisionStay2D(Collision2D other){
        if((other.gameObject.tag == "Enemy Attack" || other.gameObject.tag == "Enemy") && iframes <= 0){
            if(other.gameObject.GetComponent<EnemyBase>() != null){
                killTimerMultiplier += other.gameObject.GetComponent<EnemyBase>().baseDamage * armour;
                multiplierScale += other.gameObject.GetComponent<EnemyBase>().baseDamage * armour * 0.75f;
            }
            else if(other.gameObject.GetComponent<BulletBase>() != null){
                killTimerMultiplier += other.gameObject.GetComponent<BulletBase>().damage * armour;
                multiplierScale += other.gameObject.GetComponent<BulletBase>().damage * armour * 0.75f;
            }
            timerMultiplier.text = Math.Round(killTimerMultiplier * 100, 0) + "%";
            iframes = 0.5f;
            redVal = 0;
            StartCoroutine("Damage");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy Attack" && iframes <= 0){
            killTimerMultiplier += other.gameObject.GetComponent<BulletBase>().damage * armour;
            timerMultiplier.text = Math.Round(killTimerMultiplier * 100, 0) + "%";
            multiplierScale += other.gameObject.GetComponent<BulletBase>().damage * armour * 0.75f;
            Destroy(other.gameObject);
            iframes = 0.5f;
            redVal = 0;
            StartCoroutine("Damage");
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "xp"){
            other.attachedRigidbody.AddForce((transform.position - other.transform.position).normalized * 250, ForceMode2D.Force);
            if(Vector3.Distance(transform.position, other.transform.position) <= 0.5){
                AudioMaster.instance.PlaySFXClip(pickupSound, transform, 0.025f);
                if(other.gameObject.name != "Boss Xp(Clone)")
                    xp += 1;
                else
                    xp += 100;
                if(xp / xpCap >= 1 && !dying){
                    xp = xp % xpCap;
                    LevelUp();
                }
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator Die(){
        StartCoroutine("Damage");
        timerMultiplier.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<GameMenu>().Death();
    }

    private IEnumerator Damage(){
        AudioMaster.instance.PlaySFXClip(hurtSound, transform, 0.25f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Camera.main.transform.localPosition = new Vector3(0,0,-10) + UnityEngine.Random.insideUnitSphere * 0.5f;
        yield return new WaitForSeconds(0.025f);
        Camera.main.transform.localPosition = new Vector3(0,0,-10) + UnityEngine.Random.insideUnitSphere * 0.5f;
        yield return new WaitForSeconds(0.025f);
        gameObject.GetComponent<SpriteRenderer>().color = origColor;
        Camera.main.transform.localPosition = new Vector3(0,0,-10) + UnityEngine.Random.insideUnitSphere * 0.5f;
        yield return new WaitForSeconds(0.025f);
        Camera.main.transform.localPosition = new Vector3(0,0,-10);
    }

    public void SetHeal(float time){
        killTimer += time;
    }

    public void MultiplyHeal(float percent){
        killTimer = killTimer * percent;
    }

    public void HealthMultiplier(float percent){
        killTimerMultiplier += percent;
        timerMultiplier.text = Math.Round(killTimerMultiplier * 100, 0) + "%";
    }

    public void MoveSpeed(float speed){
        moveSpeed += speed;
    }

    public void MoveSpeedMultiplier(float speed){
        moveSpeed = moveSpeed * speed;
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

    public void IncreasePickup(float amount){
        pickup.radius = pickup.radius * amount;
    }

    public void ExplodingRounds(){
        explosive = true;
    }

    public void IncHealth(){
        incrementHealth = true;
    }

    public void IncDmg(){
        incrementDmg = true;
    }

    public void Armour(){
        armour = 0.5f;
    }

    public void Pierce(){
        pierce = true;
    }

    public void Vaccine(){
        subTime = true;
    }

}