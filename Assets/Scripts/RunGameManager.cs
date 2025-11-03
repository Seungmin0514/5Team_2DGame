using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGameManager : MonoBehaviour
{
    public static RunGameManager Instance;
    public float gold;
    GamePlayer player;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        gold += Time.deltaTime * player.CurrentSpeed * 0.1f;
    }
    
    public void EndGame()
    {
        GameDataManager.Instance.AddCoins((int)gold);
        //ui ³ª¿À°Ô
        
        
    }
}
