using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName = "Game/ItemDB")]
public class ItemDB : ScriptableObject
{
    public List<ItemDef> items = new List<ItemDef>();

    public ItemDef Get(string id)
    {
        return items.Find(x => x !=null && x.itemId == id);
    }
}
