using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Unity.Mathematics;

public class RescueTimer : MonoBehaviour
{
    public float timer;
    [SerializeField] private GameObject player, rescueLadder;
    [SerializeField] private TMP_Text escape;
    private float minutes, seconds;
    private bool triggered = false;
    private Vector3 spawnPos;

    void Start(){
        InvokeRepeating("Layer", 25, 25);
    }

    void Layer(){
        AudioMaster.instance.AddLayer();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer > 0){
            timer -= Time.deltaTime;
            minutes = (float)Math.Floor(timer / 60);
            seconds = (float)Math.Floor(timer) % 60;
            gameObject.GetComponent<TMP_Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if(!triggered){
            spawnPos = player.transform.position + Vector3.right * ((UnityEngine.Random.Range(0, 2) * 2) - 1) * 50;
            if(spawnPos != Vector3.zero && Physics2D.OverlapCircle(spawnPos, 0.1f, 512) != null){
                spawnPos = new Vector3(-spawnPos.x, spawnPos.y, 0);
                if(spawnPos != Vector3.zero && Physics2D.OverlapCircle(spawnPos, 0.1f, 512) != null){   
                    spawnPos = new Vector3(-spawnPos.x, -spawnPos.y,0);
                    if(spawnPos != Vector3.zero && Physics2D.OverlapCircle(spawnPos, 0.1f, 512) != null)   
                        spawnPos = new Vector3(spawnPos.x, -spawnPos.y,0);
                }
            }
            rescueLadder = Instantiate(rescueLadder, spawnPos, quaternion.identity);
            escape.gameObject.SetActive(true);
            timer = 0;
            triggered = true;
        }
        if(triggered){
            if((rescueLadder.transform.position - player.transform.position).x < 0)
                escape.text = "<-- ESCAPE";
            else
                escape.text = "ESCAPE -->";
        }
    }
}
