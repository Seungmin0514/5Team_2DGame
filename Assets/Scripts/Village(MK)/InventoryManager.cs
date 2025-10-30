using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Dictionary<string, int> counts = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemId, int amount = 1)
    {
        if (counts.ContainsKey(itemId)) counts[itemId] += amount;
        else counts[itemId] = amount;

        Debug.Log($"[Inventory] {itemId} x{amount} (now {counts[itemId]}");
    }

    public int GetCount(string itemId)
    {
        return counts.TryGetValue(itemId, out var v) ? v : 0;
    }

    public IEnumerable<KeyValuePair<string, int>> AllItems()
    {
        foreach(var kv in counts) yield return kv;
    }

}
