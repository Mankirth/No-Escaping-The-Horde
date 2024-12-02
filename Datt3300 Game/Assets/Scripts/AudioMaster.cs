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
    private bool fading;
    private int i;
    // https://www.youtube.com/watch?v=DU7cgVsU2rM
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
        Debug.Log(audioClip.name);
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
        if(i < 9){
            StartCoroutine("Fade");
        }
    }

    public void ResetLayers(){
        foreach(AudioSource layer in instance.musicStems){
            layer.volume = 0;
        }
        i = 0;
        musicStems[i].volume = 1;
    }

    void FixedUpdate(){
        if(fading){
            instance.musicStems[i-1].volume -= Time.deltaTime;
            instance.musicStems[i].volume += Time.deltaTime;
        }
    }

    private IEnumerator Fade(){
        fading = true;
        yield return new WaitForSeconds(1);
        fading = false;
        instance.musicStems[i-1].volume = 0;
        musicStems[i].volume = 1;
    }
}
