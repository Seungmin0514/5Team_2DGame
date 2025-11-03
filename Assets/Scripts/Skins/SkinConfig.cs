using UnityEngine;

public enum CharacterType { A, B, C } // AnimationClipChanger 캐릭터

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

    [Header("Animation Type")]
    public CharacterType type;   // A/B/C → (one/two/three)에 매핑
}