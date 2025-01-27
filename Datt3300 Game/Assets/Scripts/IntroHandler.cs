using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{
    public GameObject nextStage;
    public bool lastStage, fading;
    public Image background;

    private float opacity = 255;

    void OnEnable(){
        Debug.Log("Enabled " + gameObject.name);
        StartCoroutine("Stage1");
    }

    private IEnumerator Stage1(){
        yield return new WaitForSeconds(0.25f);
        if(!lastStage)
            nextStage.SetActive(true);
        else{
            fading = true;
            yield return new WaitForSeconds(1);
            transform.parent.gameObject.SetActive(false);
        }
    }

    void FixedUpdate(){
        if(fading && opacity > 0){
            opacity -= Time.deltaTime * 255;
            background.color = new Color32(100, 100, 100,(byte)opacity);
        }
    }
}
