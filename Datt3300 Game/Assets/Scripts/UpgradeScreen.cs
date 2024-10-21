using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    public List<GameObject> upgradeCards;
    public List<GameObject> cardPool;
    
    public void Activate(){
        cardPool = new List<GameObject>(upgradeCards);
        for(int i = 0; i < 3; i++){
            if(cardPool.Count > 0){
                int chosen = UnityEngine.Random.Range(0, cardPool.Count);
                cardPool[chosen].GetComponent<RectTransform>().position = transform.position + new Vector3(-10 + (i * 10), 0, 0);
                cardPool.RemoveAt(chosen);
            }
        }
    }

    public void Reset(){
        foreach(GameObject card in upgradeCards){
            card.GetComponent<RectTransform>().position = transform.position + new Vector3(1000, 0, 0);
        }
    }

    public void removeCard(GameObject card){
        upgradeCards.Remove(card);
    }
}
