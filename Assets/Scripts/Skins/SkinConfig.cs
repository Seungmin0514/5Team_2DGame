using UnityEngine;

[System.Serializable]
public class SkinConfig
{
    [Header("Identity")]
    public string skinId;        // "A", "B", "C"
    public string displayName;

    [Header("Price & Portrait")]
    public int price;
    public Sprite portrait;
    public Sprite silhouette;

    [Header("Type")]
    public string type;   //two one three
}