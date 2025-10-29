using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<string> ownedItems = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemID)
    {
        ownedItems.Add(itemID);
        Debug.Log("인벤토리 추가: " + itemID);
    }
}
