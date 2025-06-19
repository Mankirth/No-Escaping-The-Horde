using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    public List<GameObject> upgradeCards;
    public List<GameObject> cardPool;
    [SerializeField] private TMP_Text title;
    [SerializeField] private GameObject escapeText;
    private bool fading, escapeActive;
    private float opacity;
    
    public void Activate(){
        cardPool = new List<GameObject>(upgradeCards);
        StartCoroutine("Title");
        escapeActive = escapeText.activeSelf;
        escapeText.SetActive(false);
        for(int i = 0; i < 3; i++){
            if(cardPool.Count > 0){
                int chosen = Random.Range(0, cardPool.Count);
                cardPool[chosen].GetComponent<RectTransform>().position = transform.position + new Vector3(-9+ (i * 9), -1.5f, 0);
                cardPool.RemoveAt(chosen);
            }
        }
    }

    private IEnumerator Title(){
        fading = true;
        opacity = 0;
        title.color = new Color32(255, 255, 255, (byte)opacity);
        gameObject.GetComponent<RectTransform>().localPosition = gameObject.GetComponent<RectTransform>().localPosition + new Vector3(0, -1000, 0);
        StartCoroutine("Stupid");
        yield return new WaitForSecondsRealtime(0.5f);
        fading = false;
    }

    private IEnumerator Stupid(){
        float dumb = 0;
        while(dumb < 0.5f){
            yield return new WaitForSecondsRealtime(0.02f);
            if(fading && opacity < 255){
                opacity += 21.25f;
                title.color = new Color32(255, 255, 255, (byte)opacity);
            }
            if (fading && gameObject.GetComponent<RectTransform>().localPosition.y < 0f)
            {
                gameObject.GetComponent<RectTransform>().localPosition = gameObject.GetComponent<RectTransform>().localPosition + new Vector3(0, 100f, 0);
            }
            dumb += 0.02f;
        }
    }

    public void Reset(){
        if(escapeActive)
            escapeText.SetActive(true);
        foreach(GameObject card in upgradeCards){
            card.GetComponent<RectTransform>().position = transform.position + new Vector3(1000, 0, 0);
        }
    }

    public void removeCard(GameObject card){
        upgradeCards.Remove(card);
    }
}
