using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen, deathScreen, winScreen;
    private bool paused;
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && paused && !deathScreen.activeSelf && !winScreen.activeSelf)
            Resume();
        else if(Input.GetKeyDown(KeyCode.Escape) && !deathScreen.activeSelf && !winScreen.activeSelf)
            Pause();
    }
    
    public void MainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Pause(){
        paused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void Resume(){
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
