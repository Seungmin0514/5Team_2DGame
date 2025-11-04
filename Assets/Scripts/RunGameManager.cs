using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static RunGameManager Instance;
    public float gold;
    public int bestGold;
    public GamePlayer player;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        bestGold = PlayerPrefs.GetInt("BestGold", 0);
    }

    // Update is called once per frame
    void Update()
    {
        gold += Time.deltaTime * player.CurrentSpeed * 0.1f;
    }
    
    public void EndGame()
    {
        if (gold > bestGold)
        {
            bestGold = (int)gold;
            PlayerPrefs.SetInt("BestGold", bestGold);
            PlayerPrefs.Save();
        }
        GameDataManager.Instance.AddCoins((int)gold);
    }
}
