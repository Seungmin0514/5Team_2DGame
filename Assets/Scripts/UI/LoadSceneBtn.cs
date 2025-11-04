using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneBtn : MonoBehaviour
{
    
    public void EndGame()
    {
        LoadSceneManager.Instance.EndGame();
    }
    public void RetryGame()
    {
        LoadSceneManager.Instance.EnterGame();
    }
}
