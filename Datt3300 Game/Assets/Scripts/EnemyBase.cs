using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health, baseDamage;
    [SerializeField] private GameObject xp;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player Attack"){
            health -= (int)other.gameObject.GetComponent<BulletBase>().damage;
            if(other.gameObject.GetComponent<BulletBase>().destroyOnHit)
                Destroy(other.gameObject);
            if(health <= 0){
                Instantiate(xp, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if(other.gameObject.GetComponent<Rigidbody2D>() != null){
                gameObject.GetComponent<Rigidbody2D>().AddForce(other.gameObject.GetComponent<Rigidbody2D>().velocity / 5, ForceMode2D.Impulse);
            }
            else{
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
        }
    }
}
