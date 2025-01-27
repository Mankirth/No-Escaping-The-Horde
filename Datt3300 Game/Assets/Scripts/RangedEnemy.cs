using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    private Vector3 toPlayer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int moveSpeed;
    [SerializeField] private float maxDistance, fireRate;
    [SerializeField] private GameObject bulletPrefab;
    private float fireRateReset;

    void Start(){
        fireRateReset = fireRate;
    }
    void FixedUpdate()
    {
        toPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        if(rb.velocity.magnitude < moveSpeed && Vector2.Distance(transform.position, toPlayer) > maxDistance)
                rb.AddForce(new Vector2(toPlayer.x, toPlayer.y).normalized * moveSpeed, ForceMode2D.Force);
        if(toPlayer.magnitude <= 25){
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            if(toPlayer.x < 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else if(toPlayer.x > 0)
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            
            fireRate -= Time.deltaTime;
            if(fireRate <= 0){
                GameObject spawned = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().AddForce(toPlayer.normalized * 5, ForceMode2D.Impulse);
                fireRate = fireRateReset;
            }
        }
        else{
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
