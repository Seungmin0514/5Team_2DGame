using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public Transform gridParent;
    public GameObject slotPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshInventory()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        foreach(var itemId in InventoryManager.Instance.ownedItems)
        { 
            GameObject slot = Instantiate(slotPrefab, gridParent);
            Text t = slot.GetComponent<Text>();
            if (t != null)
                t.text = itemId;
        }
    }
}
