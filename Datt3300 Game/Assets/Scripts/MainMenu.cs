using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonSound;
    [SerializeField] private AudioMixer mix;
    [SerializeField] private Slider master, music, sfx;
    [SerializeField] private bool skipIntro;
    [SerializeField] private GameObject fadeImg;
    private bool fading;
    private float opacity = 255;

    //https://johnleonardfrench.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
    
    void Start()
    {
        music.value = PlayerPrefs.GetFloat("MusicVolume", 0.96f);
        master.value = PlayerPrefs.GetFloat("MasterVolume", 0.96f);
        sfx.value = PlayerPrefs.GetFloat("SfxVolume", 0.96f);
        if(skipIntro)
            StartCoroutine("Fade");
    }
    
    private IEnumerator Fade(){
        fading = true;
        yield return new WaitForSeconds(0.25f);
        fadeImg.SetActive(false);
    }

    void FixedUpdate(){
        if(fading && opacity > 0){
            opacity -= 21.25f;
            fadeImg.GetComponent<Image>().color = new Color32(19, 19, 19,(byte)opacity);
        }
    }

    public void Quit(){
        ButtonSound();
        Application.Quit();
    }

    public void Play(){
        ButtonSound();
        SceneManager.LoadScene("Gameplay");
    }

    public void ButtonSound(){
        AudioMaster.instance.PlaySFXClip(buttonSound, transform, 0.25f);
    }

    public void ButtonText(GameObject text){
        float height = text.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.05f;
        text.transform.localPosition = text.transform.localPosition + new Vector3(height * 2f, -1 * height, 0);
    }

    public void ButtonUp(GameObject text){
        float height = text.transform.parent.GetComponent<RectTransform>().sizeDelta.y * 0.05f;
        text.transform.localPosition = text.transform.localPosition + new Vector3(-2f * height, height, 0);
    }

    public void SetVolume()
    {
        mix.SetFloat("MasterVol", Mathf.Log10(master.value) * 20);
        master.transform.parent.GetComponent<TMP_Text>().text = "Master Volume: " + Math.Round(master.value * 100 / 1.2, 0) + "%";
        PlayerPrefs.SetFloat("MasterVolume", master.value);
    }

    public void SetMusicVolume(){
        mix.SetFloat("MusicVol", Mathf.Log10(music.value) * 20);
        music.transform.parent.GetComponent<TMP_Text>().text = "Music Volume: " + Math.Round(music.value * 100 / 1.2, 0) + "%";
        PlayerPrefs.SetFloat("MusicVolume", music.value);
    }

    public void SetSFXVolume(){
        mix.SetFloat("SFXVol", Mathf.Log10(sfx.value) * 20);
        sfx.transform.parent.GetComponent<TMP_Text>().text = "SFX Volume: " + Math.Round(sfx.value * 100 / 1.2, 0) + "%";
        PlayerPrefs.SetFloat("SfxVolume", sfx.value);
    }

    public void Reset(){
        PlayerPrefs.DeleteAll();
    }
}
