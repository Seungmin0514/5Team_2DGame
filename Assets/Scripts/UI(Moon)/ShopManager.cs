using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ShopItemData
{
    public string itemId;     //potion
    public string itemName;   //HP potion
    public int price;         //20
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public List<ShopItemData> shopItems;

    public Transform itemListParant;
    public GameObject itemSlotPrefab;
    public Text coinsText;

    private void Awake()
    {
        instance = this;
    }

    public void RefreshShopUI()
    {
        coinsText.text = "Coins: " + GameDataManager.Instance.coins;

        foreach(Transform child in itemListParant)
            Destroy(child.gameObject);

        foreach (var data in shopItems)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, itemListParant);
            ShopItemSlot slot = slotObj.GetComponent<ShopItemSlot>();
            slot.Setup(data, this);
        }
    }

    public void PurChase(ShopItemData data)
    {
        if (GameDataManager.Instance.coins >= data.price)
        {
            GameDataManager.Instance.coins = data.price;
            InventoryManager.Instance.AddItem(data.itemId);
            RefreshShopUI();
        }
        else
        {
            Debug.Log("코인 부족");
        }
    }
}