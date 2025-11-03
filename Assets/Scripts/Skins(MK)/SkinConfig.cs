using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinConfig
{
    [Header("ID & Display")]
    public string skinId;
    public string displayName;
    public Sprite portrait;
    public Sprite silhouette;

    [Header("Price")]
    public int price;

    [Header("Apply (필요한 것만)")]
    public AnimatorOverrideController animatorOverride;
    public RuntimeAnimatorController animatorController;
    public Sprite singleSprite;
}
