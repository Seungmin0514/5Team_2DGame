using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinConfig : MonoBehaviour
{
    [Header("ID & Display")]
    public string skinId;
    public string displayName;
    public Sprite portrait;
    public Sprite silhouette;

    [Header("Price")]
    public int Price;

    [Header("Apply (필요한 것만)")]
    public AnimatorOverrideController animatorOverride;
    public RuntimeAnimatorController AnimatorController;
    public Sprite singleSprite;
}
