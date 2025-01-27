using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            Destroy(gameObject, 0.5f);
        }
    }
}
