using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    [SerializeField] private int defaultCoins = 200;
    [SerializeField] private string defaultSkinId = "skin_basic"; // 기본 스킨 해금 장착        

    [SerializeField] private bool devCheats = false;
    [SerializeField] private int cheatAddCoins = 100; // 단축키로 추가

    const string KEY_COINS = "coins";
    const string KEY_SKINS = "owned_skins";
    const string KEY_EQUIP = "equipped_skin";

    int _coins;
    HashSet<string> _owned = new HashSet<string>();
    string _equipped;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAllOrInit();

        GameSignals.OnCoinsChanged?.Invoke(_coins);
        GameSignals.OnSkinEquipped?.Invoke(_equipped);
    }

    private void Update()
    {
        if (!devCheats) return;
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyUp(KeyCode.Plus))
        {
            AddCoins(cheatAddCoins);
        }
    }

    void OnApplicationQuit() => SaveAll();

    //coins
    public int GetCoins() => _coins;
    public bool TrySpendCoins(int price)
    {
        price = Mathf.Max(0, price);
        if (_coins < price) return false;
        SetCoins(_coins - price);
        return true;
    }
    public void AddCoins(int amount)
    {
        if (amount > 0)
            SetCoins(_coins + amount);
    }
    public void SetCoins(int value)
    {
        _coins = Mathf.Max(0, value);
        PlayerPrefs.SetInt(KEY_COINS, _coins);
        PlayerPrefs.Save();
        GameSignals.OnCoinsChanged?.Invoke(_coins);
    }

    //skins
    public bool IsOwned(string skinId) => _owned.Contains(skinId);
    public string GetEquipped() => _equipped;


    public void AddOwned(string skinId)
    {
        if (string.IsNullOrEmpty(skinId)) return;
        if (_owned.Add(skinId))
        {
            SaveOwned();
            GameSignals.OnSkinBought?.Invoke(skinId);
        }
    }
    public void Equip(string skinId)
    {
        if (!_owned.Contains(skinId)) return;
        _equipped = skinId;
        PlayerPrefs.SetString(KEY_EQUIP, _equipped);
        PlayerPrefs.Save();
        GameSignals.OnSkinEquipped?.Invoke(skinId);
    }


    // Persistence
    void LoadAllOrInit()
    {
        _coins = PlayerPrefs.HasKey(KEY_COINS) ? PlayerPrefs.GetInt(KEY_COINS, 0) : defaultCoins;


        _owned.Clear();
        var csv = PlayerPrefs.GetString(KEY_SKINS, "");
        if (!string.IsNullOrEmpty(csv))
            foreach (var p in csv.Split(',')) if (!string.IsNullOrEmpty(p)) _owned.Add(p);
        if (_owned.Count == 0) { _owned.Add(defaultSkinId); SaveOwned(); }


        _equipped = PlayerPrefs.GetString(KEY_EQUIP, "");
        if (string.IsNullOrEmpty(_equipped))
        {
            _equipped = defaultSkinId;
            PlayerPrefs.SetString(KEY_EQUIP, _equipped);
        }
        PlayerPrefs.Save();
    }


    void SaveOwned()
    {
        PlayerPrefs.SetString(KEY_SKINS, string.Join(",", _owned));
        PlayerPrefs.Save();
    }


    public void SaveAll()
    {
        PlayerPrefs.SetInt(KEY_COINS, _coins);
        SaveOwned();
        PlayerPrefs.SetString(KEY_EQUIP, _equipped);
        PlayerPrefs.Save();
    }
}
