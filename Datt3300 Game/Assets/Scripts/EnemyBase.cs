using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public float baseDamage;
    public Observable<int> hp = new Observable<int>();
    [SerializeField] private GameObject xp, explosion, explosionPrefab;
    [SerializeField] private bool exploding;
    [SerializeField] private AudioClip deathSound;
    private float dmgTimer;
    private Color origColor;
    private bool firstCall = true;

    void Start(){
        hp.Changed += OnHpChanged;
        hp.Value = health;
        origColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void FixedUpdate(){
        if(dmgTimer > 0)
            dmgTimer -= Time.deltaTime;
        else
            gameObject.GetComponent<SpriteRenderer>().color = origColor;
    }

    void OnHpChanged(object target, Observable<int>.ChangedEventArgs args){
        if(!firstCall){
            if(hp.Value <= 0)
                Destroy(gameObject);
            if(gameObject.GetComponent<SpriteRenderer>().color != Color.red)
                origColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            dmgTimer = 0.1f;
        }
        firstCall = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player Attack"){
            if(other.gameObject.GetComponent<BulletBase>() != null){
                hp.Value -= (int)other.gameObject.GetComponent<BulletBase>().damage;
                if(GameObject.Find("Player").GetComponent<PlayerController>().explosive && other.gameObject.GetComponent<BulletBase>().destroyOnHit){
                    Instantiate(explosion, other.transform.position, Quaternion.identity);
                    Destroy(other.gameObject);
                }
                else if(other.gameObject.GetComponent<BulletBase>().destroyOnHit)
                    Destroy(other.gameObject);
            }
            if(other.gameObject.GetComponent<Rigidbody2D>() != null){
                gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.GetComponent<Rigidbody2D>().velocity / 5, ForceMode2D.Impulse);
            }
            else{
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
    
    void OnDestroy(){
        if(gameObject.scene.isLoaded){
            AudioMaster.instance.PlaySFXClip(deathSound, transform, 1f);
            hp.Changed -= OnHpChanged;
            if(GameObject.FindGameObjectWithTag("Player") != null){
                Instantiate(xp, transform.position, Quaternion.identity);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enemiesKilled += 1;
            }
            if(exploding)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
