using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster instance;
    [SerializeField] private AudioSource sfxObj;
    [SerializeField] public AudioSource[] musicStems;
    // https://www.youtube.com/watch?v=DU7cgVsU2rM
    private void Awake(){
        if(instance == null){
            instance = this;
        }
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
        foreach(AudioSource layer in instance.musicStems){
            if(layer.volume != 1){
                layer.volume = 1;
                break;
            }
        }
    }

    public void ResetLayers(){
        foreach(AudioSource layer in instance.musicStems){
            layer.volume = 0;
        }
        musicStems[0].volume = 1;
    }
}
