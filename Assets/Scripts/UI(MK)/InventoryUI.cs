using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public ItemDB itemDB;
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

        foreach(var kv in InventoryManager.Instance.AllItems())
        {
            var id = kv.Key;
            var cnt = kv.Value;
            var def = itemDB.Get(id);

            GameObject slot = Instantiate(slotPrefab, gridParent);
            var icon = slot.transform.Find("Icon")?.GetComponent<Image>();
            var nameText = slot.transform.Find("NameText")?.GetComponent<Text>();
            var countText = slot.transform.Find("CountText")?.GetComponent<Text>();

            if (icon && def && def.icon) icon.sprite = def.icon;
            if (nameText) nameText.text = def != null ? def.displayName : id;
            if (countText) countText.text = $"x{cnt}";
        }
    }
}
