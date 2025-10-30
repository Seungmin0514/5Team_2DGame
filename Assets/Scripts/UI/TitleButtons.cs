using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour
{
    public Button playBtn;
    public Button settingBtn;
    public Button quitBtn;
    public GameObject settingPanel;

    private void Awake()
    {
        playBtn.onClick.AddListener(OnClickPlay);
        settingBtn.onClick.AddListener(OnClickSetting);
        quitBtn.onClick.AddListener(OnClickQuit);
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("VillageScene");
    }
    public void OnClickSetting()
    {
        settingPanel.SetActive(true);
    }
    public void OnClickQuit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}