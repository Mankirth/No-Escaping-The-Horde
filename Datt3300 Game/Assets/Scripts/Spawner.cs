using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float[] weights;
    [SerializeField] private float leftx, rightx, topy, bottomy, maxy, miny, spawnInterval, spawnIntervalReset;
    private int rand;
    // Start is called before the first frame update
    void Start()
    {
        spawnIntervalReset = spawnInterval;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnInterval > 0)
            spawnInterval -= Time.deltaTime;
        else{
            rand = UnityEngine.Random.Range(0, 101);
            for(int i = 0; i < weights.Length; i++){
                if(rand >= weights[i]){
                    Spawn(enemyPrefabs[i]);
                    break;
                }
            }
            spawnInterval = spawnIntervalReset;
        }
    }

    private void Spawn(GameObject prefab){
        float left = leftx + transform.parent.position.x;
        float right = rightx + transform.parent.position.x;
        float top = topy + transform.parent.position.y;
        float bottom = bottomy + transform.parent.position.y;
        if(top > maxy)
            top = maxy;
        if(bottom < miny)
            bottom = miny;
        Vector3 spawnPos = new Vector3(0, 0, 0);
        int topOrSides = UnityEngine.Random.Range(0, 4);
        while(true){
            if(topOrSides == 0 && top <= maxy - 1){
                spawnPos = new Vector3(UnityEngine.Random.Range(left, right), top, 0);
                break;
            }
            else if(topOrSides == 1 && bottom > miny + 1){
                spawnPos = new Vector3(UnityEngine.Random.Range(left, right), bottom, 0);
                break;
            }
            else if(topOrSides == 2){
                spawnPos = new Vector3(left, UnityEngine.Random.Range(bottom, top), 0);
                break;
            }
            else if(topOrSides == 3){
                spawnPos = new Vector3(right, UnityEngine.Random.Range(bottom, top), 0);
                break;
            }
            else
                topOrSides += 1;
        }
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
