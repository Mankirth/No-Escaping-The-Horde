using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private GameObject explosionfx;
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.tag == "Enemy"){
    //         other.attachedRigidbody.AddForce((other.transform.position - transform.position).normalized * 10, ForceMode2D.Impulse);
    //         other.GetComponent<EnemyBase>().hp.Value -= (int)GameObject.Find("Player").GetComponent<PlayerController>().baseDamage;
    //     }

    // }
    void Start(){
        foreach(Collider2D other in Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2)){
            if(other.tag == "Enemy"){
                other.attachedRigidbody.AddForce((other.transform.position - transform.position).normalized * 10, ForceMode2D.Impulse);
                other.GetComponent<EnemyBase>().hp.Value -= (int)GameObject.Find("Player").GetComponent<PlayerController>().baseDamage;
            }
        }
        StartCoroutine(Explode());
    }
    // Update is called once per frame
    IEnumerator Explode()
    {
        GameObject spawned = Instantiate(explosionfx, transform.position, Quaternion.identity);
        Destroy(spawned, 0.25f);
        yield return new WaitForSeconds(0.1f);
        
        Destroy(gameObject);
    }
}
