using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDef", menuName = "Game/Item")]

public class ItemDef : ScriptableObject
{
    public string itemId;
    public string displayName;
    public Sprite icon;
    public int basePrice;
    [TextArea] public string description;
    public bool stackable = true;
}
