using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WallGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> segments;
    [SerializeField] private float wallSize;
    public bool openLeft, openRight;
    
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Wall Triggered " + other.tag);
        if(other.gameObject.tag == "Player"){
            if((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).x < 0 && openRight){
                Debug.Log("Spawning to the right");
                GameObject spawned = Instantiate(segments[UnityEngine.Random.Range(0, segments.Count)], transform.position + (Vector3.right * wallSize), quaternion.identity);
                spawned.GetComponent<WallGeneration>().openLeft = false;
                openRight = false;
            }
            else if((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).x > 0 && openLeft){
                Debug.Log("Spawning to the left");
                GameObject spawned = Instantiate(segments[UnityEngine.Random.Range(0, segments.Count)], transform.position + (Vector3.left * wallSize), quaternion.identity);
                spawned.GetComponent<WallGeneration>().openRight = false;
                openLeft = false;
            }
        }
    }
}
