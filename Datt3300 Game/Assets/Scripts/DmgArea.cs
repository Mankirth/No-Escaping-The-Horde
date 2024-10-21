using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgArea : MonoBehaviour
{
    [SerializeField] private float spawnTimer, despawnTimer;
    [SerializeField] private GameObject dmgZone;
    private float despawnTimerReset, spawnTimerReset;
    private GameObject spawned;
    // Start is called before the first frame update
    void Start()
    {
        despawnTimerReset = despawnTimer;
        spawnTimerReset = spawnTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTimer -= Time.deltaTime * GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fireRateMultiplier;
        if(spawnTimer <= 0 && despawnTimer == despawnTimerReset){
            spawned = Instantiate(dmgZone, transform.position, Quaternion.identity);
            despawnTimer -= Time.deltaTime * GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fireRateMultiplier;
        }
        else if(spawnTimer <= 0 && despawnTimer >= 0){
            despawnTimer -= Time.deltaTime * GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().fireRateMultiplier;
        }
        else if(despawnTimer <= 0){
            despawnTimer = despawnTimerReset;
            spawnTimer = spawnTimerReset;
            Destroy(spawned);
        }
    }
}
