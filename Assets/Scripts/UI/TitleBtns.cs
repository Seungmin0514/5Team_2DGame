using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleBtns : MonoBehaviour
{
    public Button playBtn;
    public Button settingBtn;
    public Button quitBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(OnClickPlay);
        quitBtn.onClick.AddListener(OnClickQuit);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("VillageScene");
    }
    public void OnClickQuit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}