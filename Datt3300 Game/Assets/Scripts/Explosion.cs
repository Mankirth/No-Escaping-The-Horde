using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private GameObject explosionfx;

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
        if (tag == "Enemy Attack"){
            Camera.main.transform.localPosition = new Vector3(0, 0, -10) + UnityEngine.Random.insideUnitSphere * 0.25f;
            yield return new WaitForSeconds(0.025f);
            Camera.main.transform.localPosition = new Vector3(0, 0, -10) + UnityEngine.Random.insideUnitSphere * 0.25f;
            yield return new WaitForSeconds(0.025f);
            Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        }
        else
            yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
