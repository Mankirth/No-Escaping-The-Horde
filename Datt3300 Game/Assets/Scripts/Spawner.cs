using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] weights;
    [SerializeField] private float leftx, rightx, topy, bottomy, maxy, miny, spawnInterval, spawnIntervalReset;
    [SerializeField] private RescueTimer rescueTimer;
    [SerializeField] private GameObject boss;
    private int rand, n = 1;
    private float moreNTimer = 20;
    private bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        spawnIntervalReset = spawnInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moreNTimer -= Time.deltaTime;
        if(moreNTimer <= 0){
            n++;
            moreNTimer = 20;
        } 
        
        if(spawnIntervalReset >= 0.05f)
            spawnIntervalReset -= Time.deltaTime / 1000;
        
        if(spawnInterval > 0)
            spawnInterval -= Time.deltaTime;
        else{
            for(int y = 0; y < n; y++){
                rand = Random.Range(0, 101);
                for(int i = 0; i < weights.Length; i++){
                    if(rand >= weights[i]){
                        Spawn(enemyPrefabs[i]);
                        break;
                    }
                }
            }
            spawnInterval = spawnIntervalReset;
        }

        if(rescueTimer.timer <= 150 && rescueTimer.timer > 140 && !triggered){
            Spawn(boss);
            Rush(enemyPrefabs[0], 20);
            triggered = true;
        }
        else if(rescueTimer.timer > 0 && rescueTimer.timer < 140 && triggered){
            triggered = false;
        }
        else if(rescueTimer.timer <= 0 && !triggered){
            Rush(enemyPrefabs[0], 30);
            Rush(enemyPrefabs[2], 10);
            Rush(enemyPrefabs[1], 5);
            triggered = true;
        }

    }

    private void Rush(GameObject prefab, int n){
        for (int i = 0; i < n; i++){
            Spawn(prefab);
        }
    }

    private void Spawn(GameObject prefab){
        float left = leftx;
        float right = rightx;
        float top = topy + transform.parent.position.y;
        float bottom = bottomy + transform.parent.position.y;
        if(top > maxy)
            top = maxy;
        if(bottom < miny)
            bottom = miny;
        Vector3 spawnPos = Vector3.zero;
        int topOrSides = Random.Range(0, 4);
        while(true){
            if(topOrSides == 0 && top <= maxy - 1){
                spawnPos = new Vector3(Random.Range(left, right), top - transform.parent.position.y, 0);
                break;
            }
            else if(topOrSides == 1 && bottom > miny + 1){
                spawnPos = new Vector3(Random.Range(left, right), bottom - transform.parent.position.y, 0);
                break;
            }
            else if(topOrSides == 2){
                spawnPos = new Vector3(left, Random.Range(bottom - transform.parent.position.y, top - transform.parent.position.y), 0);
                break;
            }
            else if(topOrSides == 3){
                spawnPos = new Vector3(right, Random.Range(bottom - transform.parent.position.y, top - transform.parent.position.y), 0);
                break;
            }
            else if(topOrSides > 3)
                topOrSides = Random.Range(0, 4);
            else
                topOrSides += 1;
        } 
        if(spawnPos != Vector3.zero && Physics2D.OverlapCircle(transform.parent.position + spawnPos, 0.1f, 512) != null)
            spawnPos = new Vector3(-spawnPos.x, spawnPos.y, 0);
        if(spawnPos != Vector3.zero && Physics2D.OverlapCircle(transform.parent.position + spawnPos, 0.1f, 512) != null)   
            spawnPos = new Vector3(-spawnPos.x, -spawnPos.y,0);
        if(spawnPos != Vector3.zero && Physics2D.OverlapCircle(transform.parent.position + spawnPos, 0.1f, 512) != null)   
            spawnPos = new Vector3(spawnPos.x, -spawnPos.y,0);
        Instantiate(prefab, transform.parent.position + spawnPos, Quaternion.identity);
    }
}
