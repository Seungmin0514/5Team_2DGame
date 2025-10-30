using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public int coins = 200; // ÀÓ½Ã·Î µ· ÁÜ

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ¾À ³Ñ¾î°¡µµ À¯Áö
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
