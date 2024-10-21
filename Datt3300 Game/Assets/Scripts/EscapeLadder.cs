using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeLadder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Player"){
            Debug.Log("Winer!");
            GameObject.FindGameObjectWithTag("UpgradeScreen").GetComponent<GameMenu>().Win();
        }
    }
}
