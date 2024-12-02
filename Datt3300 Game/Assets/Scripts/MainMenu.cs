using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip buttonSound;
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
}
