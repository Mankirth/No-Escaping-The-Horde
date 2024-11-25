using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen, deathScreen, winScreen;
    [SerializeField] private AudioClip buttonSound;
    private bool paused;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && !deathScreen.activeSelf && !winScreen.activeSelf)
        if(paused)
            Resume();
        else if(Time.timeScale != 0)
            Pause();
    }
    
    public void MainMenu(){
        ButtonSound();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Pause(){
        ButtonSound();
        paused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void Resume(){
        ButtonSound();
        paused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void Death(){
        Time.timeScale = 0;
        deathScreen.SetActive(true);
    }

    public void Win(){
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void Restart(){
        ButtonSound();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ButtonSound(){
        AudioMaster.instance.PlaySFXClip(buttonSound, transform, 0.25f);
    }
}
