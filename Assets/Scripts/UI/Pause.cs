using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        if (isPaused) PauseOff();
        else PauseOn();
    }
    public void PauseOn()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        
        isPaused = true;
    }
    public void PauseOff()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    public void onClickExitonGame()
    {
        SceneManager.LoadScene("VillageScene");
    }
    public void onClickExitonVillage()
    {
        Application.Quit();
    }
}
