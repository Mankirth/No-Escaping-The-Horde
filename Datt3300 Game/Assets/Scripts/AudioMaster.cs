using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster instance;
    [SerializeField] private AudioSource sfxObj;
    [SerializeField] public AudioSource[] musicStems;
    private bool fading, fadingIntro;
    private int i, givenI;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            musicStems[0].volume = 1;
            PlaySongs();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume){
        AudioSource audioSource = Instantiate(sfxObj, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        DontDestroyOnLoad(audioSource.gameObject);
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySongs(){
        foreach(AudioSource layer in instance.musicStems){
            layer.Play();
        }
    }

    public void AddLayer(){
        i++;
        if(i < 6){
            StartCoroutine("Fade");
        }
        else{
            i = 5;
        }
    }

    public void ResetLayers(){
        fading = false;
        givenI = i;
        StopCoroutine("IntroFade");
        StartCoroutine("IntroFade");
        i = 0;
    }

    void FixedUpdate(){
        if(fading){
            instance.musicStems[i-1].volume -= Time.deltaTime;
            instance.musicStems[i].volume += Time.deltaTime;
        }

        if(fadingIntro){
            if(SceneManager.GetActiveScene().name != "Gameplay")
                instance.musicStems[1].volume -= Time.deltaTime;
            instance.musicStems[2].volume -= Time.deltaTime;
            instance.musicStems[3].volume -= Time.deltaTime;
            instance.musicStems[4].volume -= Time.deltaTime;
            instance.musicStems[5].volume -= Time.deltaTime;
            if (SceneManager.GetActiveScene().name != "Gameplay")
                    instance.musicStems[0].volume += Time.deltaTime;
                else
                    instance.musicStems[1].volume += Time.deltaTime;
        }
    }

    private IEnumerator Fade(){
        givenI = i;
        fading = true;
        yield return new WaitForSeconds(1);
        fading = false;
        if(!fadingIntro){
            musicStems[givenI-1].volume = 0;
            musicStems[givenI].volume = 1;
        }
    }

    private IEnumerator IntroFade(){
        fadingIntro = true;
        Scene currentScene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(1);
        fadingIntro = false;
        foreach(AudioSource layer in instance.musicStems){
            layer.volume = 0;
        }
        if(SceneManager.GetActiveScene().name == "Gameplay" && currentScene == SceneManager.GetActiveScene())
            instance.musicStems[1].volume = 1;
        else
            instance.musicStems[0].volume = 1;
    }

}
