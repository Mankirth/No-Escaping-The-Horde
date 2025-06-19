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
    private Color origColor;
    private bool firstCall = true;

    void Start(){
        hp.Changed += OnHpChanged;
        hp.Value = health;
        origColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void OnHpChanged(object target, Observable<int>.ChangedEventArgs args){
        if(!firstCall){
            if (hp.Value <= 0 && !exploding)
                Destroy(gameObject);
            else if (hp.Value <= 0){
                transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                Destroy(gameObject, 0.25f);
            }
            if (gameObject.GetComponent<SpriteRenderer>().color != Color.red)
                    origColor = gameObject.GetComponent<SpriteRenderer>().color;
            StopCoroutine("DmgColor");
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine("DmgColor");
        }
        firstCall = false;
    }

    private IEnumerator DmgColor(){
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = origColor;
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
                Instantiate(xp, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90));
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enemiesKilled += 1;
            }
            if(exploding)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
