using System.Collections;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private float opacity, opacity2;
    [SerializeField] private TMP_Text txt, btnTxt1, btnTxt2;
    [SerializeField] private UnityEngine.UI.Image btn1, btn2;
    [SerializeField] private GameObject escapeText;
    [SerializeField] private byte r,g,b;
    
    // Start is called before the first frame update
    void Start()
    {
        escapeText.SetActive(false);
        StartCoroutine("Title");
    }

    private IEnumerator Title(){
        opacity = 0;
        opacity2 = 0;
        btnTxt1.color = new Color32(r, g, b, (byte)opacity2);
        btn1.color = new Color32(r, g, b, (byte)opacity2);
        btn2.color = new Color32(r, g, b, (byte)opacity2);
        btnTxt2.color = new Color32(r, g, b, (byte)opacity2);
        txt.color = new Color32(r, g, b, (byte)opacity);
        txt.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 90, 0);
        StartCoroutine("Stupid");
        yield return new WaitForSecondsRealtime(0.25f);
        StartCoroutine("Buttons");
        yield return new WaitForSecondsRealtime(0.5f);
    }

    private IEnumerator Stupid(){
        float dumb = 0;
        while(dumb < 0.5f){
            yield return new WaitForSecondsRealtime(0.02f);
            if(opacity < 255){
                opacity += 21.25f / 2;
                txt.color = new Color32(r, g, b, (byte)opacity);
            }
            txt.gameObject.GetComponent<RectTransform>().localPosition = txt.gameObject.GetComponent<RectTransform>().localPosition + new Vector3(0, 6f, 0);
            dumb += 0.02f;
        }
    }

    private IEnumerator Buttons(){
        float dumb = 0;
        while(dumb < 0.5f){
            yield return new WaitForSecondsRealtime(0.02f);
            if(opacity2 < 255){
                opacity2 += 21.25f / 2;
                btnTxt1.color = new Color32(r, g, b, (byte)opacity2);
                btn1.color = new Color32(r, g, b, (byte)opacity2);
                btn2.color = new Color32(r, g, b, (byte)opacity2);
                btnTxt2.color = new Color32(r, g, b, (byte)opacity2);
            }
            dumb += 0.02f;
        }
    }
}
