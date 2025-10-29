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
}
