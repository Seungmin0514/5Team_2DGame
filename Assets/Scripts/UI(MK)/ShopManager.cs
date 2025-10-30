using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("DB & 판매 목록")]
    public ItemDB itemDB;
    public List<string> saleItemIds = new List<string>();

    [Header("UI")]
    public Transform itemListParent;
    public GameObject itemSlotPrefab;
    public TMP_Text coinsText;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshShopUI()
    {
        if (coinsText)coinsText.text = "Coins: " + GameDataManager.Instance.coins;

        foreach(Transform child in itemListParent)
            Destroy(child.gameObject);

        foreach (var id in saleItemIds)
        {
            var def = itemDB.Get(id);
            if (def == null) continue;

            var go = Instantiate(itemSlotPrefab, itemListParent);
            var slot = go.GetComponent<ShopItemSlot>();
            slot.Setup(def, this);     
            slot.UpdateInteractable(); 

        }
    }

    public void PurChase(ItemDef def)
    {
        if (def == null) return;

        int price = Mathf.Max(0, def.basePrice);
        if (GameDataManager.Instance.coins < price)
        {
            Debug.Log("[Shop] Not enough coins");
            return;
        }

        GameDataManager.Instance.coins -= price;
        InventoryManager.Instance.AddItem(def.itemId, 1);
        GameEvents.OnItemBought?.Invoke(def.itemId);

        RefreshShopUI();                // 코인/버튼 상태 다시 반영
        InventoryUI.Instance?.RefreshInventory();
    }
}