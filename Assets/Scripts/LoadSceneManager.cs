using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
            
        Instance = this;
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameCharacterManager.Instance.SetCharacter(GameDataManager.Instance.selectedCharacter);
        SceneManager.sceneLoaded -= OnGameSceneLoaded;
    }
    public void EnterGame()
    {
        SceneManager.sceneLoaded += OnGameSceneLoaded;
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void EndGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VillageScene");
    }

}
