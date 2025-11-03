using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinCatalog : MonoBehaviour
{
    public static SkinCatalog Instance;

    public List<SkinConfig> Skins = new List<SkinConfig>(); //3°³ µî·Ï

    void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public SkinConfig Get(string id) => Skins.Find(s => s != null && s.skinId == id);
}
