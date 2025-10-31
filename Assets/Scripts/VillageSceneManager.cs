using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VillageSceneManager : MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGmae()
    {
        GameDataManager.Instance.coins += 5;
        Debug.Log($"5ÄÚÀÎ È¹µæ, ÇöÀç ÄÚÀÎ : {GameDataManager.Instance.coins}");
        SceneManager.LoadScene("VillageScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
