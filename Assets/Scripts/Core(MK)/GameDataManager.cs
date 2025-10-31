using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance; 

    [SerializeField] int defaultCoins = 200;  
    const string KEY_COINS = "coins";          

    int _coins;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadCoinsOrInit();
        GameSignals.OnCoinsChanged?.Invoke(_coins);
    }

    void OnApplicationQuit() => SaveCoins();
    public int GetCoins() => _coins;
    public void SetCoins(int value)
    {
        _coins = Mathf.Max(0, value);
        SaveCoins();
        GameSignals.OnCoinsChanged?.Invoke(_coins);
    }
    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        SetCoins(_coins + amount);
    }
    public bool TrySpendCoins(int price)
    {
        price = Mathf.Max(0, price);
        if (_coins < price) return false;
        SetCoins(_coins - price);
        return true;
    }

    void LoadCoinsOrInit()
    {
        if (PlayerPrefs.HasKey(KEY_COINS))
            _coins = PlayerPrefs.GetInt(KEY_COINS, 0);
        else
        {
            _coins = Mathf.Max(0, defaultCoins);
            SaveCoins();
        }
    }

    void SaveCoins()
    {
        PlayerPrefs.SetInt(KEY_COINS, _coins);
        PlayerPrefs.Save();
    }
}
